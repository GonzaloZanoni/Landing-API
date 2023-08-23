using LandingApi.AD.Models;
using LandingAPI.AD.Models.Header;
using LandingAPI.AD.Models.PortadaImagen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.PortadaImagen
{
    public class PortadaImagenServices : IPortadaImagenServices
    {
        private readonly string _connection;

        public PortadaImagenServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_PortadaImagen>> GetPortadaImagen()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("PortadaImagen_Obtener", conn))
                {
                    try
                    {
                        var portadaImagen = new List<M_PortadaImagen>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var PortadaImagen = new M_PortadaImagen
                                {
                                    IdPortada = reader.GetInt32(reader.GetOrdinal("IdPortada")),
                                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    ColorFuente = reader.GetString(reader.GetOrdinal("ColorFuente"))

                                };

                                portadaImagen.Add(PortadaImagen);
                            }
                        }

                        return portadaImagen;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener la portada " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_PortadaImagen> GetPortadaById(int IdPortada)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("PortadaImagen_ObtenerPorId", conn))
                {
                    try
                    {
                        M_PortadaImagen? m_PortadaImagen = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdPortada", SqlDbType.Int).Value = IdPortada;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_PortadaImagen = new M_PortadaImagen
                                {
                                    IdPortada = reader.GetInt32(reader.GetOrdinal("IdPortada")),
                                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    ColorFuente = reader.GetString(reader.GetOrdinal("ColorFuente"))
                                    
                                };
                            }
                        }

                        return m_PortadaImagen;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar portada por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AgregarPortadaImagen(M_PortadaImagen portadaImagen)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("PortadaImagen_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdPortada", SqlDbType.Int).Value = portadaImagen.IdPortada;
                        cmd.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = portadaImagen.Titulo;
                        cmd.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = portadaImagen.RutaImagen;
                        cmd.Parameters.Add("@ColorFuente", SqlDbType.VarChar, 50).Value = portadaImagen.ColorFuente;
                        

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
                        throw new Exception("Se produjo un error al agregar portada imagen " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> ModificarPortadaImagen(M_PortadaImagen portadaImagen)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("PortadaImagen_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdPortada", SqlDbType.Int).Value = portadaImagen.IdPortada;
                        cmd.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = portadaImagen.Titulo;
                        cmd.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = portadaImagen.RutaImagen;
                        cmd.Parameters.Add("@ColorFuente", SqlDbType.VarChar, 50).Value = portadaImagen.ColorFuente;
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
                        throw new Exception("Se produjo un error al modificar la portada " + ex.Message);
                    }
                }
            }
        }
    }
}
