using LandingApi.AD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using LandingAPI.AD.Models.Contactos;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using LandingApi.AD.Models.Contactos;

namespace LandingAPI.AD.Services.Contactos
{
    public class ContactoService : IContactoServices
    {
        private readonly string _connection;

        public ContactoService(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<M_Contactos> GetContactoById(int IdContacto)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Contactos_ObtenerContactoPorId", conn))
                {
                    try
                    {
                        M_Contactos? m_Contacto = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdContacto", SqlDbType.Int).Value = IdContacto;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Contacto = new M_Contactos
                                {
                                    IdContacto = reader.GetInt32(reader.GetOrdinal("IdContacto")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"))
                                };
                            }
                        }

                        return m_Contacto;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar contacto por id " + ex.Message);
                    }
                }
            }
        }

        public async Task<M_Contactos> GetContactosByName(string contactoName)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Contactos_ObtenerContactosPorNombre", conn))
                {
                    try
                    {
                        M_Contactos? m_Contactos = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = contactoName;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Contactos = new M_Contactos
                                {
                                    IdContacto = reader.GetInt32(reader.GetOrdinal("IdContacto")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"))
                                };
                            }
                        }

                        return m_Contactos;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar contactos por nombre " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> AgregarContacto(M_Contactos contactos)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Contactos_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = contactos.Nombre;
                        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value = contactos.Telefono;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = contactos.Email;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 50).Value = contactos.Mensaje;
                        var id = await cmd.ExecuteScalarAsync();

                        if (id != null && id != DBNull.Value)
                        {
                            return Convert.ToInt32(id);
                        }
                        else
                        {
                            return 0;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al agregar el contacto " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> ModificarContacto(M_Contactos contactos)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Contactos_Modificar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdContacto", SqlDbType.Int).Value = contactos.IdContacto;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = contactos.Nombre;
                        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value = contactos.Telefono;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = contactos.Email;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 50).Value = contactos.Mensaje;
                        var rowAffected = await cmd.ExecuteScalarAsync();

                        if (rowAffected != null && rowAffected != DBNull.Value)
                        {
                            return Convert.ToInt32(rowAffected);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al modificar contactos " + ex.Message);
                    }
                }
            }
        }
    }
}
