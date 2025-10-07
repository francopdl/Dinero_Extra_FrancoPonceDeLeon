using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Crear usuario y cuenta
        Usuario usuario = new Usuario();
        Console.Write("Ingrese su nombre: ");
        usuario.Nombre = Console.ReadLine();

        int edad;
        while (true)
        {
            Console.Write("Ingrese su edad: ");
            if (int.TryParse(Console.ReadLine(), out edad))
                break;
            Console.WriteLine("Edad inválida, ingrese un número entero.");
        }
        usuario.Edad = edad;

        // DNI
        bool dniValido = false;
        while (!dniValido)
        {
            Console.Write("Ingrese su DNI: ");
            string dni = Console.ReadLine();
            dniValido = usuario.SetDNI(dni);
            if (!dniValido)
                Console.WriteLine("DNI inválido. Intente nuevamente.");
        }

        // Crear cuenta y wishlist
        Cuenta cuenta = new Cuenta { Nombre = usuario.Nombre };
        Wishlist wishlist = new Wishlist();

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n--- Menú ---");
            Console.WriteLine("1. Introduce un nuevo gasto básico");
            Console.WriteLine("2. Introduce un nuevo gasto extra");
            Console.WriteLine("3. Introduce un nuevo ingreso");
            Console.WriteLine("4. Mostrar gastos");
            Console.WriteLine("5. Mostrar ingresos");
            Console.WriteLine("6. Mostrar saldo");
            Console.WriteLine("7. Mostrar ahorro de un período");
            Console.WriteLine("8. Mostrar gastos imprescindibles");
            Console.WriteLine("9. Mostrar posibles ahorros del mes pasado");
            Console.WriteLine("10. Mostrar lista de deseos");
            Console.WriteLine("11. Agregar producto a la wishlist");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": // Gasto básico
                    try
                    {
                        Console.Write("Descripción del gasto básico: ");
                        string descB = Console.ReadLine();
                        float montoB = PedirMonto();
                        cuenta.AddGastoBasico(new GastoBasico(montoB, descB));
                        Console.WriteLine("Gasto básico agregado.");
                    }
                    catch (GastoException ex)
                    {
                        Console.WriteLine(ex.Message); // muestra el mensaje de saldo insuficiente
                    }
                    break;



                case "2": // Gasto extra
                    try
                    {
                        Console.Write("Descripción del gasto extra: ");
                        string descE = Console.ReadLine();
                        float montoE = PedirMonto();
                        Console.Write("Es prescindible? (s/n): ");
                        bool prescindible = Console.ReadLine().Trim().ToLower() == "s";
                        cuenta.AddGastoExtra(new GastoExtra(montoE, descE, prescindible));
                        Console.WriteLine("Gasto extra agregado.");
                    }
                    catch (GastoException ex)
                    {
                        Console.WriteLine(ex.Message); // muestra el mensaje de saldo insuficiente
                    }
                    break;


                case "3":
                    // Ingreso
                    Console.Write("Descripción del ingreso: ");
                    string descI = Console.ReadLine();
                    float montoI = PedirMonto();
                    cuenta.AddIngreso(new Ingreso(montoI, descI));
                    Console.WriteLine("Ingreso agregado.");
                    break;

                case "4":
                    // Mostrar gastos
                    Console.WriteLine("Ver gastos: 1-Todos, 2-Básicos, 3-Extras");
                    string tipoGasto = Console.ReadLine();
                    switch (tipoGasto)
                    {
                        case "1":
                            foreach (var g in cuenta.Gastos)
                                Console.WriteLine(g);
                            break;
                        case "2":
                            foreach (var g in cuenta.GetGastosBasicos(false))
                                Console.WriteLine(g);
                            break;
                        case "3":
                            foreach (var g in cuenta.GetGastosExtras(false))
                                Console.WriteLine(g);
                            break;
                        default:
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                    break;

                case "5":
                    // Mostrar ingresos
                    foreach (var i in cuenta.Ingresos)
                        Console.WriteLine(i);
                    break;

                case "6":
                    // Mostrar saldo
                    Console.WriteLine($"Saldo actual: {cuenta.Saldo}");
                    break;

                case "7":
                    // Ahorro de un período
                    Console.Write("Fecha inicio (yyyy-mm-dd): ");
                    DateTime inicio = DateTime.Parse(Console.ReadLine());
                    Console.Write("Fecha fin (yyyy-mm-dd): ");
                    DateTime fin = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine($"Ahorro en el período: {cuenta.GetAhorro(inicio, fin)}");
                    break;

                case "8":
                    // Gastos imprescindibles
                    Console.Write("Fecha inicio (yyyy-mm-dd): ");
                    DateTime inicioG = DateTime.Parse(Console.ReadLine());
                    Console.Write("Fecha fin (yyyy-mm-dd): ");
                    DateTime finG = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine($"Gastos imprescindibles: {cuenta.GetGastosImprescindibles(inicioG, finG)}");
                    break;

                case "9":
                    // Posibles ahorros del mes pasado
                    DateTime primerDiaMesPasado = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                    DateTime ultimoDiaMesPasado = primerDiaMesPasado.AddMonths(1).AddDays(-1);
                    float ahorroMesPasado = cuenta.Gastos.OfType<GastoExtra>()
                        .Where(g => g.Fecha >= primerDiaMesPasado && g.Fecha <= ultimoDiaMesPasado && g.Prescindible)
                        .Sum(g => g.dinero);
                    Console.WriteLine($"Posibles ahorros del mes pasado: {ahorroMesPasado}");
                    break;

                case "10":
                    // Mostrar wishlist
                    wishlist.MostrarProductos();
                    break;

                case "11":
                    // Agregar producto a wishlist
                    Console.Write("Nombre del producto: ");
                    string nombreP = Console.ReadLine();
                    Console.Write("Precio del producto: ");
                    float precioP = PedirMonto();
                    Console.Write("Descripción: ");
                    string descP = Console.ReadLine();
                    wishlist.AgregarProducto(new Producto(nombreP, precioP, descP));
                    Console.WriteLine("Producto agregado a la wishlist.");
                    break;

                case "0":
                    salir = true;
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }

        Console.WriteLine("Programa finalizado.");
    }

    // Método auxiliar para pedir monto válido
    static float PedirMonto()
    {
        float monto;
        while (true)
        {
            Console.Write("Ingrese el monto: ");
            if (float.TryParse(Console.ReadLine(), out monto) && monto > 0)
                return monto;
            Console.WriteLine("Monto inválido. Debe ser un número positivo.");
        }
    }
}
