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
        public async Task<List<Producto>> ObtenerProductos(string clave = null, int? tipoProductoId = null)
        {
            List<Producto> _lista = new List<Producto>();
            using (var conexion = new SqlConnection(_conexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerProductos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(clave))
                {
                    cmd.Parameters.AddWithValue("@Clave", clave);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Clave", DBNull.Value);
                }

                if (tipoProductoId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@TipoProductoId", tipoProductoId.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TipoProductoId", DBNull.Value);
                }


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Producto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Clave = dr["Clave"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            TipoProductoId = Convert.ToInt32(dr["TipoProductoId"]),
                            EsActivo = Convert.ToByte(dr["EsActivo"]),
                            Precio = Convert.ToDecimal(dr["Precio"])
                        });
                    }
                }
            }
            return _lista;
        }

        public Task<List<Producto>> Lista()
        {
            throw new NotImplementedException();
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
                cmd.Parameters.AddWithValue("@TipoProductoId", modelo.TipoProductoId);
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
                cmd.Parameters.AddWithValue("@TipoProductoId", modelo.TipoProductoId);
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
