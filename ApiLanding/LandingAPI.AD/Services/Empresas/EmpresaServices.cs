using LandingApi.AD.Models;
using LandingAPI.AD.Models.Contactos;
using LandingAPI.AD.Models.Empresas;
using LandingAPI.AD.Models.Footer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Empresas
{
    public class EmpresaServices : IEmpresaServices
    {
        private readonly string _connection;

        public EmpresaServices(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_Empresas>> GetEmpresa()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Empresa_Obtener", conn))
                {
                    try
                    {
                        var empresa = new List<M_Empresas>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Empresa = new M_Empresas
                                {
                                    IdEmpresa = reader.GetInt32(reader.GetOrdinal("IdEmpresa")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Localidad = reader.GetString(reader.GetOrdinal("Localidad")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    RazonSocial = reader.GetString(reader.GetOrdinal("RazonSocial"))

                                };

                                empresa.Add(Empresa);
                            }
                        }

                        return empresa;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al obtener empresas " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_Empresas> GetEmpresaById(int IdEmpresa)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Empresa_ObtenerPorId", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdEmpresa", SqlDbType.Int).Value = IdEmpresa;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new M_Empresas
                                {
                                    IdEmpresa = reader.GetInt32(reader.GetOrdinal("IdEmpresa")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                                    Localidad = reader.IsDBNull(reader.GetOrdinal("Localidad")) ? null : reader.GetString(reader.GetOrdinal("Localidad")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                                    RazonSocial = reader.IsDBNull(reader.GetOrdinal("RazonSocial")) ? null : reader.GetString(reader.GetOrdinal("RazonSocial"))
                                };
                            }
                            else
                            {
                                // Manejar el caso en el que no se encuentra ninguna empresa con el ID proporcionado
                                return null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al buscar empresa por ID " + ex.Message);
                    }
                }
            }
        }

        public async Task<M_Empresas> GetEmpresaByName(string empresaName)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Empresa_ObtenerEmpresaPorNombre", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = empresaName;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new M_Empresas
                                {
                                    IdEmpresa = reader.GetInt32(reader.GetOrdinal("IdEmpresa")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                                    Localidad = reader.IsDBNull(reader.GetOrdinal("Localidad")) ? null : reader.GetString(reader.GetOrdinal("Localidad")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                                    RazonSocial = reader.IsDBNull(reader.GetOrdinal("RazonSocial")) ? null : reader.GetString(reader.GetOrdinal("RazonSocial"))
                                };
                            }
                            else
                            {
                                // Manejar el caso en el que no se encuentra ninguna empresa con el nombre proporcionado
                                return null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al buscar empresa por nombre " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> AgregarEmpresa(M_Empresas empresa)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Empresa_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@IdEmpresa", SqlDbType.Int).Value = empresa.IdEmpresa;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = empresa.Nombre;
                        cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = empresa.Direccion;
                        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value = empresa.Telefono;
                        cmd.Parameters.Add("@Localidad", SqlDbType.VarChar, 50).Value = empresa.Localidad;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = empresa.Email;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 500).Value = empresa.Descripcion;
                        cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 100).Value = empresa.RazonSocial;

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
                        throw new Exception("Se produjo un error al agregar empresa " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> ModificarEmpresa(M_Empresas empresa)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Empresa_Modificar", conn))
                {
                    try
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdEmpresa", SqlDbType.Int).Value = empresa.IdEmpresa;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = empresa.Nombre;
                        cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = empresa.Direccion;
                        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value = empresa.Telefono;
                        cmd.Parameters.Add("@Localidad", SqlDbType.VarChar, 50).Value = empresa.Localidad;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = empresa.Email;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 500).Value = empresa.Descripcion;
                        cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 100).Value = empresa.RazonSocial;
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
                        throw new Exception("Se produjo un error al modificar empresa " + ex.Message);
                    }
                }
            }
        }

        public async Task<bool> EliminarEmpresa(int IdEmpresa)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Empresa_Eliminar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdEmpresa", SqlDbType.Int).Value = IdEmpresa;

                        var rowsAffected = await cmd.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al eliminar una empresa de la galería: " + ex.Message);
                    }
                }
            }
        }
    }
}
