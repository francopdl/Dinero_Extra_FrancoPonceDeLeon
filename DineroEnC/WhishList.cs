using System;
using System.Collections.Generic;

public class Wishlist
{
    public List<Producto> Productos { get; private set; }

    public Wishlist()
    {
        Productos = new List<Producto>();
    }

    // Agregar un producto a la wishlist
    public void AgregarProducto(Producto producto)
    {
        Productos.Add(producto);
    }

    // Mostrar todos los productos en la wishlist
    public void MostrarProductos()
    {
        if (Productos.Count == 0)
        {
            Console.WriteLine("La wishlist está vacía.");
            return;
        }

        foreach (var producto in Productos)
        {
            Console.WriteLine(producto);
        }
    }
}
