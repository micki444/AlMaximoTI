namespace AlMaximoTI.Repositorios.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> ObtenerTodos();
        Task<List<T>> Buscar(string clave, string tipo);
        Task<bool> Guardar(T modelo);
        Task<bool> Editar(T modelo);
        Task<bool> Eliminar(int id);
    }
}
