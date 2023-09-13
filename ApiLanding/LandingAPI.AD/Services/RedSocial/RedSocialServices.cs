using LandingApi.AD.Models;
using LandingAPI.AD.Models.RedSocial;
using LandingAPI.AD.Models.ServicioImagenes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.RedSocial
{
    public class RedSocialServices : IRedSocialServices
    {
        private readonly string _connection;

        public RedSocialServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_RedSocial>> GetRedSocial()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("RedSocial_Obtener", conn))
                {
                    try
                    {
                        var redSocial = new List<M_RedSocial>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var RedSocial1 = new M_RedSocial
                                {
                                    IdRedSocial = reader.GetInt32(reader.GetOrdinal("IdRedSocial")),
                                    IdFooter = reader.GetInt32(reader.GetOrdinal("IdFooter")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Ruta = reader.GetString(reader.GetOrdinal("Ruta"))

                                };

                                redSocial.Add(RedSocial1);
                            }
                        }

                        return redSocial;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener las Redes Sociales " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_RedSocial> GetRedSocialById(int IdRedSocial)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("RedSocial_ObtenerPorId", conn))
                {
                    try
                    {
                        M_RedSocial? m_RedSocial = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdRedSocial", SqlDbType.VarChar, 50).Value = IdRedSocial;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_RedSocial = new M_RedSocial
                                {
                                    IdRedSocial = reader.GetInt32(reader.GetOrdinal("IdRedSocial")),
                                    IdFooter = reader.GetInt32(reader.GetOrdinal("IdFooter")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Ruta = reader.GetString(reader.GetOrdinal("Ruta"))

                                };
                            }
                        }

                        return m_RedSocial;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar Red social por ID " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_RedSocial> GetRedSocialByName(string Nombre)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("RedSocial_ObtenerPorNombre", conn))
                {
                    try
                    {
                        M_RedSocial? m_RedSocial = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = Nombre;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_RedSocial = new M_RedSocial
                                {
                                    IdRedSocial = reader.GetInt32(reader.GetOrdinal("IdRedSocial")),
                                    IdFooter= reader.GetInt32(reader.GetOrdinal("IdFooter")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Ruta = reader.GetString(reader.GetOrdinal("Ruta"))

                                };
                            }
                        }

                        return m_RedSocial;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar Red social por nombre " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> ModificarRedSocial(M_RedSocial redSocial)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("RedSocial_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdRedSocial", SqlDbType.Int).Value = redSocial.IdRedSocial;
                        cmd.Parameters.Add("@IdFooter", SqlDbType.Int).Value = redSocial.IdFooter;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = redSocial.Nombre;
                        cmd.Parameters.Add("@Ruta", SqlDbType.VarChar, -1).Value = redSocial.Ruta;
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
                        throw new Exception("Se produjo un error al modificar la red Social " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AgregarRedSocial(M_RedSocial redSocial)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("RedSocial_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdFooter", SqlDbType.Int).Value = redSocial.IdFooter;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = redSocial.Nombre;
                        cmd.Parameters.Add("@Ruta", SqlDbType.VarChar, -1).Value = redSocial.Ruta;


                        var id = await cmd.ExecuteScalarAsync();

                        if (id != null && id != DBNull.Value)
                        {
                            return Convert.ToInt32(id);
                        }
                        else
                        {
                            return 0;
                        };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al agregar una Red Social " + ex.Message);
                    }
                }
            }
        }

        public async Task<bool> EliminarRedSocial(int IdRedSocial)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("RedSocial_Eliminar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdRedSocial", SqlDbType.Int).Value = IdRedSocial;

                        var rowsAffected = await cmd.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al eliminar una Red Social: " + ex.Message);
                    }
                }
            }
        }
    }
}
