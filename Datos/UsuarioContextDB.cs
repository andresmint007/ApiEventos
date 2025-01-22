using Entidades.Entidades;
using Entidades.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class UsuarioContextDB : IRepositoryComun<Usuario>
    {
        private string _string_BD = string.Empty;
        public UsuarioContextDB(IConfiguration configuration) {

            _string_BD = configuration!.GetConnectionString("ConexionBaseDatos")!;
        }
        public Task<bool> ActualizarAsync(Usuario entity)
        {

            throw new NotImplementedException();
        }

        public Task<Usuario> CrearAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_string_BD))
                {
                    await connection.OpenAsync();

                    string query = "SELECT USU_IdUsuario, USU_Nombre, USU_Email, USU_Password, USU_FechaCreacion FROM Usuarios";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (DbDataReader reader = await command.ExecuteReaderAsync())  
                        {
                            while (await reader.ReadAsync())  
                            {
                                Usuario usuario = new Usuario
                                {
                                    idUsuario = reader.GetInt32(reader.GetOrdinal("USU_IdUsuario")),
                                    nombre =    reader.GetString(reader.GetOrdinal("USU_Nombre")),
                                    email =     reader.GetString(reader.GetOrdinal("USU_Email")),
                                    password =  reader.GetString(reader.GetOrdinal("USU_Password")),
                                    fechaCreacion = reader.IsDBNull(reader.GetOrdinal("USU_FechaCreacion"))
                                                             ? (DateTime?)null
                                                             : reader.GetDateTime(reader.GetOrdinal("USU_FechaCreacion"))
                                };

                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
            }

            return usuarios;
        }

    }
}
