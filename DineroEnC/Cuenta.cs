using System;
using System.Collections.Generic;
using System.Linq;

public class Cuenta : Usuario
{
    public float Saldo { get; private set; }
    public List<Ingreso> Ingresos { get; private set; }
    public List<Gasto> Gastos { get; private set; }

    public Cuenta()
    {
        Ingresos = new List<Ingreso>();
        Gastos = new List<Gasto>();
        Saldo = 0;
    }

    public void AddIngreso(Ingreso ingreso)
    {
        Ingresos.Add(ingreso);
        Saldo += ingreso.dinero;
    }

    public void AddGastoBasico(GastoBasico gasto)
    {
        if (Saldo - gasto.dinero < 0)
            throw new GastoException();

        Gastos.Add(gasto);
        Saldo -= gasto.dinero;
    }

    public void AddGastoExtra(GastoExtra gasto)
    {
        if (Saldo - gasto.dinero < 0)
            throw new GastoException();

        Gastos.Add(gasto);
        Saldo -= gasto.dinero;
    }

    public List<GastoBasico> GetGastosBasicos(bool esteMes)
    {
        var gastos = Gastos.OfType<GastoBasico>().ToList();
        if (esteMes)
        {
            gastos = gastos.Where(g => g.Fecha.Month == DateTime.Now.Month
                                    && g.Fecha.Year == DateTime.Now.Year).ToList();
        }
        return gastos;
    }

    public List<GastoExtra> GetGastosExtras(bool esteMes)
    {
        var gastos = Gastos.OfType<GastoExtra>().ToList();
        if (esteMes)
        {
            gastos = gastos.Where(g => g.Fecha.Month == DateTime.Now.Month
                                    && g.Fecha.Year == DateTime.Now.Year).ToList();
        }
        return gastos;
    }

    public float GetAhorro(DateTime inicio, DateTime fin)
    {
        float ingresosPeriodo = Ingresos
            .Where(i => i.Fecha >= inicio && i.Fecha <= fin)
            .Sum(i => i.dinero);

        float gastosPeriodo = Gastos
            .Where(g => (g is GastoBasico gb && gb.Fecha >= inicio && gb.Fecha <= fin)
                     || (g is GastoExtra ge && ge.Fecha >= inicio && ge.Fecha <= fin))
            .Sum(g => g.dinero);

        return ingresosPeriodo - gastosPeriodo;
    }

    public float GetGastosImprescindibles(DateTime inicio, DateTime fin)
    {
        float gastosBasicos = Gastos.OfType<GastoBasico>()
            .Where(g => g.Fecha >= inicio && g.Fecha <= fin)
            .Sum(g => g.dinero);

        float gastosExtrasNoPrescindibles = Gastos.OfType<GastoExtra>()
            .Where(g => g.Fecha >= inicio && g.Fecha <= fin && !g.Prescindible)
            .Sum(g => g.dinero);

        return gastosBasicos + gastosExtrasNoPrescindibles;
    }
}
