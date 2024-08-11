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

        public async Task<List<ProductoProveedor>> ObtenerTodos()
        {
            List<ProductoProveedor> _lista = new List<ProductoProveedor>();
            using (var conexion = new SqlConnection(_conexion))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_AllProductsSupplier", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new ProductoProveedor 
                        {
                            ProductoId = Convert.ToInt32(dr["ProductoId"]),
                            refProveedor = new Proveedor
                            {
                                Id = Convert.ToInt32(dr["ProveedorId"]),
                                Nombre = dr["Nombre"].ToString(),
                            },
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

        public Task<List<ProductoProveedor>> Buscar(string clave, string tipo)
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
