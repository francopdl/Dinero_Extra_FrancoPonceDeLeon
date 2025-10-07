using System;

public class Ingreso : Dinero
{
    public DateTime Fecha { get; private set; }

    public Ingreso(float dinero, string descripcion)
        : base(dinero, descripcion)
    {
        if (dinero <= 0)
            throw new ArgumentException("El ingreso debe ser positivo");

        this.Fecha = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Ingreso: {descripcion} - {dinero} - Fecha: {Fecha.ToShortDateString()}";
    }
}
