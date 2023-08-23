using LandingApi.AD.Models;
using LandingAPI.AD.Models.PortadaImagen;
using LandingAPI.AD.Models.SeccionServicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.SeccionServicios
{
    public class SeccionServicioServices : ISeccionServicioServices
    {
        private readonly string _connection;

        public SeccionServicioServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_SeccionServicios>> GetSeccionServicios()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("SeccionServicios_Obtener", conn))
                {
                    try
                    {
                        var seccionServicio = new List<M_SeccionServicios>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var SeccionServicio = new M_SeccionServicios
                                {
                                    IdSeccionServicio = reader.GetInt32(reader.GetOrdinal("IdSeccionServicio")),
                                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                    RutaPDF = reader.GetString(reader.GetOrdinal("RutaPDF")),
                                    //ColorFuente = reader.GetString(reader.GetOrdinal("ColorFuente"))

                                };

                                seccionServicio.Add(SeccionServicio);
                            }
                        }

                        return seccionServicio;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener seccion servicio " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_SeccionServicios> GetSeccionServiciosById(int IdSeccionServicio)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("SeccionServicio_ObtenerPorId", conn))
                {
                    try
                    {
                        M_SeccionServicios? m_SeccionServicios = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdSeccionServicio", SqlDbType.Int).Value = IdSeccionServicio;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_SeccionServicios = new M_SeccionServicios
                                {
                                    IdSeccionServicio = reader.GetInt32(reader.GetOrdinal("IdSeccionServicio")),
                                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                    RutaPDF = reader.GetString(reader.GetOrdinal("RutaPDF"))

                                };
                            }
                        }

                        return m_SeccionServicios;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar seccion servicio por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> ModificarSeccionServicio(M_SeccionServicios seccionServicios)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("SeccionServicio_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdSeccionServicio", SqlDbType.Int).Value = seccionServicios.IdSeccionServicio;
                        cmd.Parameters.Add("@Titulo", SqlDbType.VarChar, 50).Value = seccionServicios.Titulo;
                        cmd.Parameters.Add("@RutaPDF", SqlDbType.VarChar, 100).Value = seccionServicios.RutaPDF;
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
                        throw new Exception("Se produjo un error al modificar seccion servicio " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AgregarSeccionServicio(M_SeccionServicios seccionServicios)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("SeccionServicio_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdSeccionServicio", SqlDbType.Int).Value = seccionServicios.IdSeccionServicio;
                        cmd.Parameters.Add("@Titulo", SqlDbType.VarChar, 50).Value = seccionServicios.Titulo;
                        cmd.Parameters.Add("@RutaPDF", SqlDbType.VarChar, 100).Value = seccionServicios.RutaPDF;
                       


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
                        throw new Exception("Se produjo un error al agregar seccion servicio " + ex.Message);
                    }
                }
            }
        }
    }
}
