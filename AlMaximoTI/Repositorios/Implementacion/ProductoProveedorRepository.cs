using AlMaximoTI.Models;
using AlMaximoTI.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace AlMaximoTI.Repositorios.Implementacion
{
    public class ProductoProveedorRepository : IGenericRepository<ProductoProveedor>
    {
        private readonly string _conexion = "";

        public ProductoProveedorRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("conexion");
        }

        public async Task<List<ProductoProveedor>> Lista()
        {
            List<ProductoProveedor> _lista = new List<ProductoProveedor>();
            using (var conexion = new SqlConnection(_conexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ProductoProveedor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new ProductoProveedor 
                        {
                            Producto = dr["Producto"].ToString(),
                            Proveedor = dr["Proveedor"].ToString(),
                            ClaveProveedor = dr["Clave"].ToString(),
                            Costo = Convert.ToDecimal(dr["Costo"])
                       
                    });
                    }
                }
            }
            return _lista;
        }
        public Task<bool> Editar(ProductoProveedor modelo)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductoProveedor>> ObtenerProductos(string clave, int? tipoProducto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(ProductoProveedor modelo)
        {
            throw new NotImplementedException();
        }

        
    }
}
