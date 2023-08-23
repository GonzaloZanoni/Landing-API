using LandingApi.AD.Models;
using LandingAPI.AD.Models.Contactos;
using LandingAPI.AD.Models.PortadaImagen;
using LandingAPI.AD.Models.ServicioImagenes;
using LandingAPI.AD.Models.Testimonios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.ServicioImagenes
{
    public class ServicioImagenServices : IServicioImagenServices
    {
        private readonly string _connection;

        public ServicioImagenServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_ServicioImagenes>> GetServicioImagenes()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("ServicioImagen_Obtener", conn))
                {
                    try
                    {
                        var servicioImagenes = new List<M_ServicioImagenes>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var ServicioImagenes1 = new M_ServicioImagenes
                                {
                                    IdServicioImagen = reader.GetInt32(reader.GetOrdinal("IdServicioImagen")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    IdSeccionServicio = reader.GetInt32(reader.GetOrdinal("IdSeccionServicio"))

                                };

                                servicioImagenes.Add(ServicioImagenes1);
                            }
                        }

                        return servicioImagenes;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener los servicios/imagenes " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_ServicioImagenes> GetServicioImagenById(int IdServicioImagen)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("ServicioImagen_ObtenerPorId", conn))
                {
                    try
                    {
                        M_ServicioImagenes? m_ServicioImagenes = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdServicioImagen", SqlDbType.Int).Value = IdServicioImagen;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_ServicioImagenes = new M_ServicioImagenes
                                {
                                    IdServicioImagen = reader.GetInt32(reader.GetOrdinal("IdServicioImagen")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    IdSeccionServicio = reader.GetInt32(reader.GetOrdinal("IdSeccionServicio"))

                                };
                            }
                        }

                        return m_ServicioImagenes;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar Servicio/Imagen por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_ServicioImagenes> GetServicioImagenByName(string Descripcion)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("ServicioImagen_ObtenerPorNombre", conn))
                {
                    try
                    {
                        M_ServicioImagenes? m_ServicioImagenes = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 50).Value = Descripcion;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_ServicioImagenes = new M_ServicioImagenes
                                {
                                    IdServicioImagen = reader.GetInt32(reader.GetOrdinal("IdServicioImagen")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    IdSeccionServicio = reader.GetInt32(reader.GetOrdinal("IdSeccionServicio"))
                                    
                                };
                            }
                        }

                        return m_ServicioImagenes;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar contactos por nombre " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> ModificarServicioImagenes(M_ServicioImagenes servicioImagenes)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("ServicioImagene_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdServicioImagen", SqlDbType.Int).Value = servicioImagenes.IdServicioImagen;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = servicioImagenes.Descripcion;
                        cmd.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = servicioImagenes.RutaImagen;
                        cmd.Parameters.Add("@IdSeccionServicio", SqlDbType.Int).Value = servicioImagenes.IdSeccionServicio;
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
                        throw new Exception("Se produjo un error al modificar el testimonio " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AgregarServicioImagen(M_ServicioImagenes servicioImagenes)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("ServicioImagen_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = servicioImagenes.Descripcion;
                        cmd.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = servicioImagenes.RutaImagen;
                        cmd.Parameters.Add("@IdSeccionServicio", SqlDbType.Int).Value = servicioImagenes.IdSeccionServicio;


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
                        throw new Exception("Se produjo un error al agregar una Imagen a servicios " + ex.Message);
                    }
                }
            }
        }
    }
}
