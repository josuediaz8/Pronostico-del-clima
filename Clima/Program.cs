using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clima
{
    static class Program
    {

        static void Main(string[] args)
        {
            int opcion;
            string clima = string.Empty;


            try
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("\n\n*************    ¿Que desea Consultar? Ingrese el nro y presione 'Enter'   ***********\n\n");
                    Console.WriteLine("1. ¿Cuántos períodos de sequía habrá?");
                    Console.WriteLine("2. ¿Cuántos períodos de lluvia habrá y qué día será el pico máximo de lluvia?");
                    Console.WriteLine("3. ¿Cuántos períodos de condiciones óptimas de presión y temperatura habrá?");
                    Console.WriteLine("4. Cantidad total de periodos para todos los climas");

                    opcion = int.Parse(Console.ReadLine());

                } while (opcion < 1 || opcion > 4);

                if (opcion != 4)
                {
                    switch (opcion)
                    {
                        case 1:
                            ObtenerUnPeriodo("Sequia");
                            break;

                        case 2:
                            ObtenerUnPeriodo("Lluvia");
                            break;

                        case 3:
                            ObtenerUnPeriodo("Optimo");
                            break;

                        default:
                            break;
                    }

                    clima = Console.ReadLine();

                }
                else
                {
                    ObtenerPeriodos();
                }
                    
                
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
           
            
        }

        static public void EjecutarProceso()
        {
            RegistroClima controlador = new RegistroClima();

            controlador.EjecutandoProceso();
            controlador.CalcularPeriodos();

        }

        static public void ObtenerUnPeriodo(string clima)
        {
            RegistroClima controlador = new RegistroClima();
            string mensaje = string.Empty;
            Console.WriteLine("Calculando el periodo de la opcion seleccionada...\n\n");

            EjecutarProceso();

            mensaje = controlador.ObtenerUnPeriodo(clima);

            Console.WriteLine(mensaje);
        }

        static public void ObtenerPeriodos()
        {
            RegistroClima controlador = new RegistroClima();
            string mensaje = string.Empty;
            Console.WriteLine("Calculando todos los periodos...\n\n");

            EjecutarProceso();

            mensaje = controlador.ObtenerPeriodos();

            
            Console.WriteLine(mensaje);
        }

    }
}
