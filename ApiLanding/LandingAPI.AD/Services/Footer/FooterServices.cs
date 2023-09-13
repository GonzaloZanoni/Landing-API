using LandingApi.AD.Models;
using LandingAPI.AD.Models.Footer;
using LandingAPI.AD.Models.Galeria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Footer
{
    public class FooterServices : IFooterServices
    {
        private readonly string _connection;

        public FooterServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_Footer>> GetFooter()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Footer_Obtener", conn))
                {
                    try
                    {
                        var footer = new List<M_Footer>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Footer = new M_Footer
                                {
                                    IdFooter = reader.GetInt32(reader.GetOrdinal("IdFooter")),
                                    DescripcionDireccion = reader.GetString(reader.GetOrdinal("DescripcionDireccion")),
                                    RutaDireccion = reader.GetString(reader.GetOrdinal("RutaDireccion")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    RutaTelefono = reader.GetString(reader.GetOrdinal("RutaTelefono")),
                                    DescripcionEmail = reader.GetString(reader.GetOrdinal("DescripcionEmail")),
                                    RutaEmail = reader.GetString(reader.GetOrdinal("RutaEmail")),
                                    Localidad = reader.GetString(reader.GetOrdinal("Localidad"))

                                };

                                footer.Add(Footer);
                            }
                        }

                        return footer;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener el footer " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_Footer> GetFooterById(int IdFooter)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Footer_ObtenerPorId", conn))
                {
                    try
                    {
                        M_Footer? m_Footer = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdFooter", SqlDbType.Int).Value = IdFooter;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Footer = new M_Footer
                                {
                                    IdFooter = reader.GetInt32(reader.GetOrdinal("IdFooter")),
                                    DescripcionDireccion = reader.GetString(reader.GetOrdinal("DescripcionDireccion")),
                                    RutaDireccion = reader.GetString(reader.GetOrdinal("RutaDireccion")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    RutaTelefono = reader.GetString(reader.GetOrdinal("RutaTelefono")),
                                    DescripcionEmail = reader.GetString(reader.GetOrdinal("DescripcionEmail")),
                                    RutaEmail = reader.GetString(reader.GetOrdinal("RutaEmail")),
                                    Localidad = reader.GetString(reader.GetOrdinal("Localidad"))


                                };
                            }
                        }

                        return m_Footer;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar footer por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AgregarFooter(M_Footer footer)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Footer_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdFooter", SqlDbType.Int).Value = footer.IdFooter;
                        cmd.Parameters.Add("@DescripcionDireccion", SqlDbType.VarChar, 50).Value = footer.DescripcionDireccion;
                        cmd.Parameters.Add("@RutaDireccion", SqlDbType.VarChar, 100).Value = footer.RutaDireccion;
                        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 100).Value = footer.Telefono;
                        cmd.Parameters.Add("@RutaTelefono", SqlDbType.VarChar, 50).Value = footer.RutaTelefono;
                        cmd.Parameters.Add("@DescripcionEmail", SqlDbType.VarChar, 50).Value = footer.DescripcionEmail;
                        cmd.Parameters.Add("@RutaEmail", SqlDbType.VarChar, 50).Value = footer.RutaEmail;
                        cmd.Parameters.Add("@Localidad", SqlDbType.VarChar, 50).Value = footer.Localidad;

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
                        throw new Exception("Se produjo un error al agregar footer " + ex.Message);
                    }
                }
            }
        }
         public async Task<int>ModificarFooter(M_Footer footer)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Footer_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdFooter", SqlDbType.Int).Value = footer.IdFooter;
                        cmd.Parameters.Add("@DescripcionDireccion", SqlDbType.VarChar, 50).Value = footer.DescripcionDireccion;
                        cmd.Parameters.Add("@RutaDireccion", SqlDbType.VarChar, 100).Value = footer.RutaDireccion;
                        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 100).Value = footer.Telefono;
                        cmd.Parameters.Add("@RutaTelefono", SqlDbType.VarChar, 50).Value = footer.RutaTelefono;
                        cmd.Parameters.Add("@DescripcionEmail", SqlDbType.VarChar, 50).Value = footer.DescripcionEmail;
                        cmd.Parameters.Add("@RutaEmail", SqlDbType.VarChar, 50).Value = footer.RutaEmail;
                        cmd.Parameters.Add("@Localidad", SqlDbType.VarChar, 50).Value = footer.Localidad;
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
                        throw new Exception("Se produjo un error al modificar el footer " + ex.Message);
                    }
                }
            }
        }
    }
}
