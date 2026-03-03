using System;

namespace Calculadora
{
    class Program
    {
        static void Main(string[] args)
        {
            string continuar = "S";

            while (continuar.ToUpper() == "S")
            {
                Console.Clear();
                Console.WriteLine("=== CALCULADORA ===");
                Console.WriteLine("1. Suma");
                Console.WriteLine("2. Resta");
                Console.WriteLine("3. Multiplicación");
                Console.WriteLine("4. División");
                Console.Write("Elige una opción (1-4): ");
                string opcion = Console.ReadLine();

                Console.Write("Ingresa el primer número: ");
                double num1 = double.Parse(Console.ReadLine());

                Console.Write("Ingresa el segundo número: ");
                double num2 = double.Parse(Console.ReadLine());

                double resultado = 0;
                string operacion = "";

                switch (opcion)
                {
                    case "1":
                        resultado = num1 + num2;
                        operacion = "Suma";
                        break;
                    case "2":
                        resultado = num1 - num2;
                        operacion = "Resta";
                        break;
                    case "3":
                        resultado = num1 * num2;
                        operacion = "Multiplicación";
                        break;
                    case "4":
                        if (num2 == 0)
                        {
                            Console.WriteLine("Error: No se puede dividir entre cero.");
                            Console.Write("¿Deseas continuar? (S/N): ");
                            continuar = Console.ReadLine();
                            continue;
                        }
                        resultado = num1 / num2;
                        operacion = "División";
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.Write("¿Deseas continuar? (S/N): ");
                        continuar = Console.ReadLine();
                        continue;
                }

                Console.WriteLine($"\n=== RESULTADO ===");
                Console.WriteLine($"{operacion}: {num1} y {num2} = {resultado}");
                Console.Write("\n¿Deseas continuar? (S/N): ");
                continuar = Console.ReadLine();
            }

            Console.WriteLine("\nGracias por usar la calculadora. ¡Hasta luego!");
            Console.ReadKey();
        }
    }
}