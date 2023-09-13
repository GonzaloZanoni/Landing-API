using LandingApi.AD.Models;
using LandingAPI.AD.Models.GaleriaImagenes;
using LandingAPI.AD.Models.ServicioImagenes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.GaleriaImagenes
{
    public class GaleriaImagenServices : IGaleriaImagenServices
    {
        private readonly string _connection;

        public GaleriaImagenServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_GaleriaImagen>> GetGaleriaImagenes()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("GaleriaImagen_Obtener", conn))
                {
                    try
                    {
                        var galeriaImagenes = new List<M_GaleriaImagen>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var GaleriaImagenes1 = new M_GaleriaImagen
                                {
                                    IdGaleriaImagen = reader.GetInt32(reader.GetOrdinal("IdGaleriaImagen")),
                                    IdGaleria = reader.GetInt32(reader.GetOrdinal("IdGaleria")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))

                                };

                                galeriaImagenes.Add(GaleriaImagenes1);
                            }
                        }

                        return galeriaImagenes;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener los galeria/imagenes " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_GaleriaImagen> GetGaleriaImagenById(int IdGaleriaImagen)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("GaleriaImagen_ObtenerPorId", conn))
                {
                    try
                    {
                        M_GaleriaImagen? m_GaleriaImagen = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdGaleriaImagen", SqlDbType.Int).Value = IdGaleriaImagen;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_GaleriaImagen = new M_GaleriaImagen
                                {
                                    IdGaleriaImagen = reader.GetInt32(reader.GetOrdinal("IdGaleriaImagen")),
                                    IdGaleria = reader.GetInt32(reader.GetOrdinal("IdGaleria")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))

                                };
                            }
                        }

                        return m_GaleriaImagen;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar Galeria/Imagen por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_GaleriaImagen> GetGaleriaImagenByName(string Descripcion)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("GaleriaImagen_ObtenerPorNombre", conn))
                {
                    try
                    {
                        M_GaleriaImagen? m_GaleriaImagen = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = Descripcion;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_GaleriaImagen = new M_GaleriaImagen
                                {
                                    IdGaleriaImagen = reader.GetInt32(reader.GetOrdinal("IdGaleriaImagen")),
                                    IdGaleria = reader.GetInt32(reader.GetOrdinal("IdGaleria")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),

                                };
                            }
                        }

                        return m_GaleriaImagen;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar imagen por nombre en Galeria" + ex.Message);
                    }
                }
            }
        }
        public async Task<int> ModificarGaleriaImagen(M_GaleriaImagen galeriaImagen)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("GaleriaImagen_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdGaleriaImagen", SqlDbType.Int).Value = galeriaImagen.IdGaleriaImagen;
                        cmd.Parameters.Add("@IdGaleria", SqlDbType.Int).Value = galeriaImagen.IdGaleria;
                        cmd.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = galeriaImagen.RutaImagen;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = galeriaImagen.Descripcion;
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
                        throw new Exception("Se produjo un error al modificar la Imagen " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AgregarGaleriaImagen(M_GaleriaImagen galeriaImagen)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("galeriaImagen_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdGaleria", SqlDbType.Int).Value = galeriaImagen.IdGaleria;
                        cmd.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = galeriaImagen.RutaImagen;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = galeriaImagen.Descripcion;


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
                        throw new Exception("Se produjo un error al agregar una Imagen a Galeria " + ex.Message);
                    }
                }
            }
        }

        public async Task<bool> EliminarGaleriaImagen(int IdGaleriaImagen)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("galeriaImagen_Eliminar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdGaleriaImagen", SqlDbType.Int).Value = IdGaleriaImagen;

                        var rowsAffected = await cmd.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al eliminar una imagen de la galería: " + ex.Message);
                    }
                }
            }
        }

    }
}
