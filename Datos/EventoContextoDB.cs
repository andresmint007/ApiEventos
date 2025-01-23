using Entidades.Entidades;
using Entidades;
using Entidades.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class EventoContextoDB : IRepositoryComun<Evento>
    {
        private string _string_BD = string.Empty;
        public EventoContextoDB(IConfiguration configuration)
        {

            _string_BD = configuration!.GetConnectionString("ConexionBaseDatos")!;
        }
        public async Task<bool> ActualizarAsync(Evento entity)
        {
            using (MySqlConnection conn = new MySqlConnection(_string_BD))
            {
                bool actualizacion = false;
                try
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand("PAEditarEvento", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_upd_idevento", entity.idEvento);
                        cmd.Parameters.AddWithValue("p_upd_FechaHora", entity.fechaHora);
                        cmd.Parameters.AddWithValue("p_upd_EVT_Ubicacion", entity.ubicacion);
                        cmd.Parameters.AddWithValue("p_upd_EVT_Capacidad", entity.capacidad);

                        MySqlParameter outputIdParam = new MySqlParameter("p_upd_IdEvento_Ok", MySqlDbType.Int32)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputIdParam);

                        await cmd.ExecuteNonQueryAsync();

                       int idEvento = Convert.ToInt32(outputIdParam.Value);
                       actualizacion= idEvento > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                await conn.CloseAsync();
                return actualizacion;
            }
        }

        public async Task<int> CrearAsync(Evento entity)
        {
            using (MySqlConnection conn = new MySqlConnection(_string_BD))
            {
                int idEvento = 0;
                try
                {
                    await conn.OpenAsync(); 
                    using (MySqlCommand cmd = new MySqlCommand("PAInsertarEvento", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_IdUsuario", entity.idUsuario);
                        cmd.Parameters.AddWithValue("p_Nombre", entity.nombre);
                        cmd.Parameters.AddWithValue("p_Descripcion", entity.descripcion);
                        cmd.Parameters.AddWithValue("p_FechaHora", entity.fechaHora);
                        cmd.Parameters.AddWithValue("p_EVT_Ubicacion", entity.ubicacion);
                        cmd.Parameters.AddWithValue("p_EVT_Capacidad", entity.capacidad);

                        MySqlParameter outputIdParam = new MySqlParameter("p_IdEvento", MySqlDbType.Int32)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputIdParam);

                        await cmd.ExecuteNonQueryAsync(); 

                        idEvento = Convert.ToInt32(outputIdParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                await conn.CloseAsync();
                return idEvento;
            }
        }


        public async Task<bool> EliminarAsync(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(_string_BD))
            {
                bool actualizacion = false;
                try
                {
                    await conn.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand("PADesactivarEvento", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_del_idevento",id);

                        MySqlParameter outputIdParam = new MySqlParameter("p_del_IdEvento_Ok", MySqlDbType.Int32)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputIdParam);

                        await cmd.ExecuteNonQueryAsync();

                        int idEvento = Convert.ToInt32(outputIdParam.Value);
                        actualizacion = idEvento > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                await conn.CloseAsync();
                return actualizacion;
            }
        }

        public Task<Evento> ObtenerPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Evento> ObtenerPorIdAsync(int id)
        {
            if (id<=0)
                throw new ArgumentNullException(nameof(id), "El ID no puede ser vacio.");

            Evento evento = null;

            string query = "SELECT EVT_IdEvento,USU_IdUsuario,EVT_Nombre,EVT_Descripcion,EVT_FechaHora,EVT_Ubicacion,EVT_Capacidad" +
                           " FROM eventos WHERE " +
                           "EVT_IdEvento =@IdEvento " +
                           "AND EVT_Estado = TRUE;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_string_BD))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@IdEvento", id);

                        using (DbDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                evento = new Evento
                                {
                                    idEvento = Convert.ToInt32(reader["EVT_IdEvento"]),
                                    idUsuario = Convert.ToInt32(reader["USU_IdUsuario"]),
                                    nombre = reader["EVT_Nombre"].ToString(),
                                    descripcion = reader["EVT_Descripcion"].ToString(),
                                    fechaHora = Convert.ToDateTime(reader["EVT_FechaHora"]),
                                    ubicacion = reader["EVT_Ubicacion"].ToString(),
                                    capacidad = Convert.ToInt32(reader["EVT_Capacidad"])
                                };
                            }
                        }
                    }
                    await conn.CloseAsync();
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
            return evento;
        }

        public Task<List<Evento>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
