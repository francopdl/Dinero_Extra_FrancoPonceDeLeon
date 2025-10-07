using System;

public class Usuario
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    private string _dni; // Cambiar el nombre del campo privado

    public string DNI
    {
        get => _dni;
        private set => _dni = value;
    }

    public bool SetDNI(string dni) // Cambiar el nombre del método para evitar conflicto
    {
        if (es_dni_valido(dni))
        {
            DNI = dni.Trim().ToUpper();
            return true;
        }

        return false;
    }

    public bool es_dni_valido(string DNI)
    {
        if (DNI is not string)
        {
            return false;
        }

        string s = DNI.Trim().ToUpper();

        string s_sin;
        if (s.Contains("-"))
        {
            if (s.Split('-').Length - 1 != 1)
            {
                return false;
            }
            s_sin = s.Replace("-", "");
        }
        else
        {
            s_sin = s;
        }

        if (s_sin.Length != 9)
        {
            return false;
        }

        string numeros = s_sin.Substring(0, 8);

        if (!int.TryParse(numeros, out _))
        {
            return false;
        }

        char letra = s_sin[8];
        if (!(letra >= 'A' && letra <= 'Z'))
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        return $"Usuario - Nombre: {Nombre}, Edad: {Edad}, DNI: {DNI}";
    }
}