using LandingApi.AD.Models;
using LandingAPI.AD.Models.Header;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Header
{
    public class HeaderServices : IHeaderServices
    {
        private readonly string _connection;

        public HeaderServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_Header>> GetHeader()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Header_ObtenerHeader", conn))
                {
                    try
                    {
                        var header = new List<M_Header>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Header = new M_Header
                                {
                                    IdHeader = reader.GetInt32(reader.GetOrdinal("IdHeader")),
                                    RutaLogo = reader.GetString(reader.GetOrdinal("RutaLogo")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    ColorFuente = reader.GetString(reader.GetOrdinal("ColorFuente")),
                                    ColorFondo = reader.GetString(reader.GetOrdinal("ColorFondo")),
                                    
                                };

                                header.Add(Header);
                            }
                        }

                        return header;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener el header " + ex.Message);
                    }
                }
            }
        }

        public async Task<M_Header> GetHeaderById(int IdHeader)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Header_ObtenerHeaderPorId", conn))
                {
                    try
                    {
                        M_Header? m_Header = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdHeader", SqlDbType.Int).Value = IdHeader;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Header = new M_Header
                                {
                                    IdHeader = reader.GetInt32(reader.GetOrdinal("IdHeader")),
                                    RutaLogo = reader.GetString(reader.GetOrdinal("RutaLogo")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    ColorFuente = reader.GetString(reader.GetOrdinal("ColorFuente")),
                                    ColorFondo = reader.GetString(reader.GetOrdinal("ColorFondo"))
                                    

                                };
                            }
                        }

                        return m_Header;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar header por id " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> AgregarHeader(M_Header header)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Header_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdHeader", SqlDbType.Int).Value = header.IdHeader;
                        cmd.Parameters.Add("@RutaLogo", SqlDbType.VarChar, -1).Value = header.RutaLogo;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = header.Nombre;
                        cmd.Parameters.Add("@ColorFuente", SqlDbType.VarChar, 50).Value = header.ColorFuente;
                        cmd.Parameters.Add("@ColorFondo", SqlDbType.VarChar, 50).Value = header.ColorFondo;
                        
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
                        throw new Exception("Se produjo un error al agregar header " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> ModificarHeader(M_Header header)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Header_Modificar", conn))
                {
                    try
                    {
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdHeader", SqlDbType.Int).Value = header.IdHeader;
                        cmd.Parameters.Add("@RutaLogo", SqlDbType.VarChar, -1).Value = header.RutaLogo;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = header.Nombre;
                        cmd.Parameters.Add("@ColorFuente", SqlDbType.VarChar, 50).Value = header.ColorFuente;
                        cmd.Parameters.Add("@ColorFondo", SqlDbType.VarChar, 50).Value = header.ColorFondo;
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
                        throw new Exception("Se produjo un error al modificar el header " + ex.Message);
                    }
                }
            }
        }
    }
}
