using Entidades.Entidades;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Datos
{
    public class InscripcionesContextoDB
    {
        private string _string_BD = string.Empty;
        public InscripcionesContextoDB(IConfiguration configuration)
        {

            _string_BD = configuration!.GetConnectionString("ConexionBaseDatos")!;
        }
        public async Task<int> InscribirEventoUsuario( int idEvento, int idUsuario)
        {
            int idinscripcion = 0;
            using (MySqlConnection conn = new MySqlConnection(_string_BD))
            {
                try
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand("PAInscribirEvento", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_idEvento", idEvento);
                        cmd.Parameters.AddWithValue("p_idUsuario", idUsuario);

                        MySqlParameter outputIdParam = new MySqlParameter("id_inscripcion", MySqlDbType.Int32)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputIdParam);

                        await cmd.ExecuteNonQueryAsync();

                        idinscripcion = Convert.ToInt32(outputIdParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                await conn.CloseAsync();
                return idinscripcion;
            }
        }
        public async Task<int> ObetnerNumeroParticipantes(int idEvento)
        {

            int cuenta = 0;
            string query = "SELECT COUNT(INS_IdInscripcion) AS cuenta_Asistenes FROM inscripciones WHERE EVT_IdEvento = @idEvento;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_string_BD))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);
                        object result = await command.ExecuteScalarAsync();
                        if (result != null && int.TryParse(result.ToString(), out int count))
                        {
                            cuenta = count;
                        }
                    }
                    await conn.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
            }

            return cuenta;
        }
        public async Task<int> ObetnerNumeroEventosUsuario(int idEvento, int idUsuario)
        {

            int cuenta = 0;
            string query = "SELECT COUNT(INS_IdInscripcion) FROM inscripciones WHERE USU_IdUsuario =@idUsuario;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_string_BD))
                {
                    await conn.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);

                        object result = await command.ExecuteScalarAsync();
                        if (result != null && int.TryParse(result.ToString(), out int count))
                        {
                            cuenta = count;
                        }
                    }
                    await conn.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
            }

            return cuenta;
        }
        public async Task<List<EventoIsncritos>> ObetenerEventosInscritos()
        {
            List<EventoIsncritos> eventos = new List<EventoIsncritos>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_string_BD))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand("PAObtenerEventosParticipante", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (DbDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                EventoIsncritos eventoIns = new EventoIsncritos
                                {
                                    idEvento = Convert.ToInt32(reader["EVT_IdEvento"]),
                                    nombre = reader["EVT_Nombre"].ToString(),
                                    descripcion = reader["EVT_Descripcion"].ToString(),
                                    fechaHora = Convert.ToDateTime(reader["EVT_FechaHora"]),
                                    ubicacion = reader["EVT_Ubicacion"].ToString(),
                                    capacidad = Convert.ToInt32(reader["EVT_Capacidad"]),
                                    listaUsuarios = reader["ListaUsuarios"].ToString(),
                                };

                                eventos.Add(eventoIns);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
            }
            return eventos;
        }

    }
}
