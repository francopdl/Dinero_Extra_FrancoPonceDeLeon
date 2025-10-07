using System;

public class GastoExtra : Gasto
{
    public DateTime Fecha { get; private set; }
    public bool Prescindible { get; private set; }

    public GastoExtra(float dinero, string descripcion, bool prescindible)
        : base(dinero, descripcion)
    {
        this.Fecha = DateTime.Now;
        this.Prescindible = prescindible;
    }

    public override string ToString()
    {
        return $"[EXTRA] {descripcion} - {dinero} - Fecha: {Fecha.ToShortDateString()} - Prescindible: {Prescindible}";
    }
}
