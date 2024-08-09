namespace AlMaximoTI.Repositorios.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Lista();
        Task<List<T>> ObtenerProductos(string clave, int tipoProducto);
        Task<bool> Guardar(T modelo);
        Task<bool> Editar(T modelo);
        Task<bool> Eliminar(int id);
    }
}
