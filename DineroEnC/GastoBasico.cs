using System;

public class GastoBasico : Gasto
{
    public DateTime Fecha { get; private set; }

    public GastoBasico(float dinero, string descripcion)
        : base(dinero, descripcion)
    {
        this.Fecha = DateTime.Now;
    }

    public override string ToString()
    {
        return $"[BÁSICO] {descripcion} - {dinero} - Fecha: {Fecha.ToShortDateString()}";
    }
}
