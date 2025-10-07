public class Producto
{
    public string Nombre { get; set; }
    public float Precio { get; set; }
    public string Descripcion { get; set; }

    public Producto(string nombre, float precio, string descripcion)
    {
        Nombre = nombre;
        Precio = precio;
        Descripcion = descripcion;
    }

    public override string ToString()
    {
        return $"{Nombre} - Precio: {Precio} - {Descripcion}";
    }
}
