using LandingApi.AD.Models;
using LandingAPI.AD.Models.Galeria;
using LandingAPI.AD.Models.Header;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Galeria
{
    public class GaleriaServices : IGaleriaServices
    {
        private readonly string _connection;

        public GaleriaServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_Galeria>> GetGaleria()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Galeria_Obtener", conn))
                {
                    try
                    {
                        var galeria = new List<M_Galeria>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Galeria = new M_Galeria
                                {
                                    IdGaleria = reader.GetInt32(reader.GetOrdinal("IdGaleria")),
                                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                    ColorFuente = reader.GetString(reader.GetOrdinal("ColorFuente")),
                                    ColorFondo = reader.GetString(reader.GetOrdinal("ColorFondo"))

                                };

                                galeria.Add(Galeria);
                            }
                        }

                        return galeria;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener el header " + ex.Message);
                    }
                }
            }
        }

        public async Task<M_Galeria> GetGaleriaById(int IdGaleria)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Galeria_ObtenerPorId", conn))
                {
                    try
                    {
                        M_Galeria? m_Galeria = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdGaleria", SqlDbType.Int).Value = IdGaleria;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Galeria = new M_Galeria
                                {
                                    IdGaleria = reader.GetInt32(reader.GetOrdinal("IdGaleria")),
                                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                    ColorFuente = reader.GetString(reader.GetOrdinal("ColorFuente")),
                                    ColorFondo = reader.GetString(reader.GetOrdinal("ColorFondo"))


                                };
                            }
                        }

                        return m_Galeria;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar header por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AgregarGaleria(M_Galeria galeria)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Galeria_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdGaleria", SqlDbType.Int).Value = galeria.IdGaleria;
                        cmd.Parameters.Add("@Titulo", SqlDbType.VarChar, 50).Value = galeria.Titulo;
                        cmd.Parameters.Add("@ColorFuente", SqlDbType.VarChar, 100).Value = galeria.ColorFuente;
                        cmd.Parameters.Add("@ColorFondo", SqlDbType.VarChar, 100).Value = galeria.ColorFondo;
                       // cmd.Parameters.Add("@ColorFondo", SqlDbType.VarChar, 50).Value = header.ColorFondo;

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
                        throw new Exception("Se produjo un error al agregar galeria " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> ModificarGaleria(M_Galeria galeria)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Galeria_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdGaleria", SqlDbType.Int).Value = galeria.IdGaleria;
                        cmd.Parameters.Add("@Titulo", SqlDbType.VarChar, 50).Value = galeria.Titulo;
                        cmd.Parameters.Add("@ColorFuente", SqlDbType.VarChar, 100).Value = galeria.ColorFuente;
                        cmd.Parameters.Add("@ColorFondo", SqlDbType.VarChar, 100).Value = galeria.ColorFondo;
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
                        throw new Exception("Se produjo un error al modificar la galeria " + ex.Message);
                    }
                }
            }
        }


    }
}
