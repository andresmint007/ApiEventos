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
using System.Web.Helpers;

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

        public Task<int> CrearAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {;

            Usuario usuario = null;

            string query = "SELECT USU_IdUsuario,USU_Nombre, USU_Email, USU_Password, USU_FechaCreacion " +
                           "FROM Usuarios " +
                           "WHERE USU_IdUsuario = @Iduser";

            using (MySqlConnection conn = new MySqlConnection(_string_BD))
            {
                await conn.OpenAsync();

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Iduser", id);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                idUsuario = Convert.ToInt32(reader["USU_IdUsuario"]),
                                nombre = reader["USU_Nombre"].ToString(),
                                email = reader["USU_Email"].ToString(),
                                password = reader["USU_Password"].ToString(),
                                fechaCreacion = Convert.ToDateTime(reader["USU_FechaCreacion"])
                            };
                        }
                    }
                }
                await conn.CloseAsync();
            }

            return usuario;
        }
        public async Task<Usuario> ObtenerPorEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), "El email no puede ser nulo o vacío.");

            Usuario usuario = null;

            string query = "SELECT USU_Nombre, USU_Email, USU_Password, USU_FechaCreacion " +
                           "FROM Usuarios " +
                           "WHERE USU_Email = @Email";

            using (MySqlConnection conn = new MySqlConnection(_string_BD))
            {
                await conn.OpenAsync();

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                nombre = reader["USU_Nombre"].ToString(),
                                email = reader["USU_Email"].ToString(),
                                password = reader["USU_Password"].ToString(),
                                fechaCreacion = Convert.ToDateTime(reader["USU_FechaCreacion"])
                            };
                        }
                    }
                }
                await conn.CloseAsync();
            }
            
            return usuario;
        
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_string_BD))
                {
                    await conn.OpenAsync();

                    string query = "SELECT USU_IdUsuario, USU_Nombre, USU_Email, USU_Password, USU_FechaCreacion FROM Usuarios";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
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
                    await conn.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
            }
            return usuarios;
        }

    }
}
