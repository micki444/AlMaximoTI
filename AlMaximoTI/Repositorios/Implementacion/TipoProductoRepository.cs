﻿using AlMaximoTI.Models;
using AlMaximoTI.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace AlMaximoTI.Repositorios.Implementacion
{
    public class TipoProductoRepository : IGenericRepository<TipoProducto>
    {
        private readonly string _conexion = "";

        public TipoProductoRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("conexion");
        }

        public Task<List<TipoProducto>> Buscar(string clave, string tipo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(TipoProducto modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(TipoProducto modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TipoProducto>> ObtenerTodos()
        {
            List<TipoProducto> _lista = new List<TipoProducto>();

            using (var conexion = new SqlConnection(_conexion))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_AllProductsType", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        var tipo = new TipoProducto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString()
                        };

                       

                        _lista.Add(tipo);
                    }
                }
            }

            return _lista;
        }
    }
}
