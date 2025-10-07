public abstract class Dinero
{
    public float dinero { get; set; }
    public string descripcion { get; set; }

    protected Dinero(float dinero, string descripcion)
    {
        this.dinero = dinero;
        this.descripcion = descripcion;
    }
}
