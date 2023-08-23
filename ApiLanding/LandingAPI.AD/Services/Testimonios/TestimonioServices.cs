using LandingApi.AD.Models;
using LandingAPI.AD.Models.Header;
using LandingAPI.AD.Models.PortadaImagen;
using LandingAPI.AD.Models.Testimonios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Testimonios
{
    public class TestimonioServices : ITestimonioServices
    {
        private readonly string _connection;

        public TestimonioServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_Testimonio>> GetTestimonio()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Testimonio_Obtener", conn))
                {
                    try
                    {
                        var testimonio = new List<M_Testimonio>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Testimonio = new M_Testimonio
                                {
                                    IdTestimonio = reader.GetInt32(reader.GetOrdinal("IdTestimonio")),
                                    NombreCliente = reader.GetString(reader.GetOrdinal("NombreCliente")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    Parrafo = reader.GetString(reader.GetOrdinal("Parrafo")),
                                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))

                                };

                                testimonio.Add(Testimonio);
                            }
                        }

                        return testimonio;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener los testimonios " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_Testimonio> GetTestimonioById(int IdTestimonio)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Testimonio_ObtenerPorId", conn))
                {
                    try
                    {
                        M_Testimonio? m_Testimonio = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdTestimonio", SqlDbType.Int).Value = IdTestimonio;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Testimonio = new M_Testimonio
                                {
                                    IdTestimonio = reader.GetInt32(reader.GetOrdinal("IdTestimonio")),
                                    NombreCliente = reader.GetString(reader.GetOrdinal("NombreCliente")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    Parrafo = reader.GetString(reader.GetOrdinal("Parrafo")),
                                    Titulo =reader.GetString(reader.GetOrdinal("Titulo")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))

                                };
                            }
                        }

                        return m_Testimonio;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar testimonio por id " + ex.Message);
                    }
                }
            }
        }

        public async Task<M_Testimonio> GetTestimonioByName(string nombreCliente)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Testimonio_ObtenerPorNombre", conn))
                {
                    try
                    {
                        M_Testimonio? m_Testimonio = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar, 50).Value = nombreCliente;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Testimonio = new M_Testimonio
                                {
                                    IdTestimonio = reader.GetInt32(reader.GetOrdinal("IdTestimonio")),
                                    NombreCliente = reader.GetString(reader.GetOrdinal("NombreCliente")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("RutaImagen")),
                                    Parrafo = reader.GetString(reader.GetOrdinal("Parrafo")),
                                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };
                            }
                        }

                        return m_Testimonio;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar testimonio por nombre " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> ModificarTestimonio(M_Testimonio testimonio)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Testimonio_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdTestimonio", SqlDbType.Int).Value = testimonio.IdTestimonio;
                        cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar, 100).Value = testimonio.NombreCliente;
                        cmd.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = testimonio.RutaImagen;
                        cmd.Parameters.Add("@Parrafo", SqlDbType.VarChar, 500).Value = testimonio.Parrafo;
                        cmd.Parameters.Add("@Titulo", SqlDbType.VarChar, 50).Value = testimonio.Titulo;
                        cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = testimonio.Activo;
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

        public async Task<int> AgregarTestimonio(M_Testimonio testimonio)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Testimonio_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@IdTestimonio", SqlDbType.Int).Value = testimonio.IdTestimonio;
                        cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar, 100).Value = testimonio.NombreCliente;
                        cmd.Parameters.Add("@RutaImagen", SqlDbType.VarChar, -1).Value = testimonio.RutaImagen;
                        cmd.Parameters.Add("@Parrafo", SqlDbType.VarChar, 500).Value = testimonio.Parrafo;
                        cmd.Parameters.Add("@Titulo", SqlDbType.VarChar, 50).Value = testimonio.Titulo;
                        cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = testimonio.Activo;

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
                        throw new Exception("Se produjo un error al agregar el testimonio " + ex.Message);
                    }
                }
            }
        }
    }
}
