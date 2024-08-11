namespace AlMaximoTI.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public TipoProducto refTipoProducto { get; set; }
        public byte EsActivo { get; set; }
        public decimal Precio { get; set; }
        public List<ProductoProveedor> Proveedores { get; set; }
    }
}
