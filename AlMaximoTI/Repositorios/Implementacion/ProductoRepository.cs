using AlMaximoTI.Models;
using AlMaximoTI.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace AlMaximoTI.Repositorios.Implementacion
{
    public class ProductoRepository : IGenericRepository<Producto>
    {
        private readonly string _conexion = "";

        public ProductoRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("conexion");
        }
        public async Task<List<Producto>> Buscar(string clave,String tipo)
        {
            List<Producto> _lista = new List<Producto>();

            using (var conexion = new SqlConnection(_conexion))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_FindProducts", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregar parámetros
                cmd.Parameters.AddWithValue("@Clave", (object)clave ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tipo", (object)tipo ?? DBNull.Value);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        var producto = new Producto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Clave = dr["Clave"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            refTipoProducto = new Models.TipoProducto
                            {
                                Id = Convert.ToInt32(dr["TipoId"]),
                                Nombre = dr["TipoNombre"].ToString(),
                            },
                            EsActivo = Convert.ToByte(dr["EsActivo"]),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Proveedores = new List<ProductoProveedor>()
                        };

                        // Agregar proveedores si existen
                        if (!dr.IsDBNull(dr.GetOrdinal("ProveedorId")))
                        {
                            producto.Proveedores.Add(new ProductoProveedor
                            {
                                ProductoId = producto.Id,
                                refProveedor = new Proveedor
                                {
                                    Id = Convert.ToInt32(dr["ProveedorId"]),
                                    Nombre = dr["Proveedor"].ToString(),
                                },
                                ClaveProveedor = dr["ClaveProveedor"].ToString(),
                                Costo = Convert.ToDecimal(dr["Costo"])
                            });
                        }

                        _lista.Add(producto);
                    }
                }
            }

            return _lista;
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            List<Producto> _lista = new List<Producto>();

            using (var conexion = new SqlConnection(_conexion))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_AllProducts", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        var producto = new Producto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Clave = dr["Clave"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            refTipoProducto = new Models.TipoProducto
                            {
                                Id = Convert.ToInt32(dr["TipoId"]),
                                Nombre = dr["TipoNombre"].ToString(),
                            },
                            EsActivo = Convert.ToByte(dr["EsActivo"]),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Proveedores = new List<ProductoProveedor>()
                        };

                        // Agregar proveedores si existen
                        if (!dr.IsDBNull(dr.GetOrdinal("ProveedorId")))
                        {
                            producto.Proveedores.Add(new ProductoProveedor
                            {
                                ProductoId = producto.Id,
                                refProveedor = new Proveedor
                                {
                                    Id = Convert.ToInt32(dr["ProveedorId"]),
                                    Nombre = dr["Proveedor"].ToString(),
                                },
                                ClaveProveedor = dr["ClaveProveedor"].ToString(),
                                Costo = Convert.ToDecimal(dr["Costo"])
                            });
                        }

                        _lista.Add(producto);
                    }
                }
            }

            return _lista;
        }


        public async Task<bool> Guardar(Producto modelo)
        {
            using (var conexion = new SqlConnection(_conexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertarActualizarProducto", conexion);
                cmd.Parameters.AddWithValue("@ProductoId", modelo.Id == 0 ? (object)DBNull.Value : modelo.Id);
                cmd.Parameters.AddWithValue("@Clave", modelo.Clave);
                cmd.Parameters.AddWithValue("@Nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("@TipoProductoId", modelo.refTipoProducto.Id);
                cmd.Parameters.AddWithValue("@EsActivo", modelo.EsActivo);
                cmd.Parameters.AddWithValue("@Precio", modelo.Precio);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                    return false;
            }
        }

        public async Task<bool> Editar(Producto modelo)
        {
            using (var conexion = new SqlConnection(_conexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertarActualizarProducto", conexion);
                cmd.Parameters.AddWithValue("@ProductoId", modelo.Id == 0 ? (object)DBNull.Value : modelo.Id);
                cmd.Parameters.AddWithValue("@Clave", modelo.Clave);
                cmd.Parameters.AddWithValue("@Nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("@TipoProductoId", modelo.refTipoProducto.Id);
                cmd.Parameters.AddWithValue("@EsActivo", modelo.EsActivo);
                cmd.Parameters.AddWithValue("@Precio", modelo.Precio);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                    return false;
            }
        }
        
        public async Task<bool> Eliminar(int id)
        {
            using (var conexion = new SqlConnection(_conexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarProducto", conexion);
                cmd.Parameters.AddWithValue("@ProductoId", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                return false;
            }
        }

        

       
    }
}
