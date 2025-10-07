public class Gasto : Dinero
{
    public Gasto(float dinero, string descripcion) : base(dinero, descripcion)
    {
        if (dinero <= 0)
            throw new ArgumentException("El gasto debe ser positivo");
    }

    public override string ToString()
    {
        return $"Gasto: {descripcion}, Cantidad: {dinero}";
    }
}
