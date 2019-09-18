using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clima.Models;
using Clima.Controllers;

namespace Clima
{
    class RegistroClima
    {
        private int years {get; set;}
        
        CalculosMatematicos calculos = new CalculosMatematicos();
        Datos datos = new Datos();

       

        public RegistroClima()//por defecto son 10 años
        {
            this.years = 10;
        }
      
        public RegistroClima(int years)// si se quiere cambiar el año
        {
            this.years = years;
        }

        public void EjecutandoProceso()
        {
            List<Planet> p = new List<Planet>();
            List<Coordenadas> coordenadas = new List<Coordenadas>();
            Forecast Pronostico = new Forecast();

            bool planetasAlineados = false;
            bool alineadosConSol = false;
            string tipoDeClima = string.Empty;
            int totalDias = 0;
            double perimetro = 0;

            Console.WriteLine("Eliminando registros anteriores...");
            datos.EliminarTodo(); //Elimino todo los registros y empiezo desde 0;

            Console.WriteLine("Generando Nuevos Datos...");
            totalDias = CalcularTotalDias();//calculo dias dependiendo de cantidad de años
            p = datos.ObtenerPlanetas(); //Busco los planetas desde la db


            for (int dia = 1; dia <= totalDias; dia++) //Recorro todos los dias
            {
                Coordenadas coord_planeta1 = ObtenerCoodenadas(dia, p[0]);
                Coordenadas coord_planeta2 = ObtenerCoodenadas(dia, p[1]);
                Coordenadas coord_planeta3 = ObtenerCoodenadas(dia, p[2]);
                Coordenadas coord_sol = new Coordenadas();
                coord_sol.X = 0;
                coord_sol.Y = 0;
                
                //Verifico si estan alineados entre si los planetas
                planetasAlineados = calculos.EstanAlineados(coord_planeta1, coord_planeta2, coord_planeta3, dia);

                if (planetasAlineados)
                {
                    //Ahora veo si tambien estan alineados con el sol
                    alineadosConSol = calculos.EstanAlineados(coord_planeta2, coord_planeta3, coord_sol, dia);

                    if (alineadosConSol)
                        tipoDeClima = "Sequia";
                    else
                        tipoDeClima = "Optimo";
                }
                else
                {
                    bool solDentrodelTriangulo;
                    solDentrodelTriangulo = calculos.SolDentroDelTriangulo(coord_planeta1, coord_planeta2, coord_planeta3, coord_sol);

                    if (solDentrodelTriangulo)
                    {
                        tipoDeClima = "Lluvia";
                        perimetro = calculos.HallarPerimetro(coord_planeta1, coord_planeta2, coord_planeta3);
                    }
                    else
                    {
                        tipoDeClima = "Indefinido";
                    }

                }

                Pronostico.dia = dia;
                Pronostico.clima = tipoDeClima;
                Pronostico.coordenada1 = coord_planeta1;
                Pronostico.coordenada2 = coord_planeta2;
                Pronostico.coordenada3 = coord_planeta3;
                Pronostico.perimetro = perimetro;

                datos.GuardarClima(Pronostico);
            }

            

        }

        public Coordenadas ObtenerCoodenadas(int day, Planet p)
        {
            Coordenadas coordenadas = new Coordenadas();

            coordenadas.X = calculos.CalcularCoordenadaX(day, p);
            coordenadas.Y = calculos.CalcularCoordenadaY(day, p);

            return coordenadas;
            
        }

        public int CalcularTotalDias()
        { 
            int cantidadDias;
            int velocidadMin = datos.VelocidadMin();

            //Determina cuanto durara el proceso segun el planeta mas lento(velocidad minima)
            cantidadDias = years * (360 / velocidadMin); 

            return cantidadDias;
        }

        public string DiaMasLluvioso()
        {
            Forecast maslluvioso = new Forecast();
            string msj = string.Empty;

            maslluvioso = datos.ObtenerDiaMasLluvioso();

            msj += "Dia mas lluvioso es: " + maslluvioso.dia;
            msj += "\nEl Perimetro es: " + maslluvioso.perimetro;
            msj += "\nCoordenada A : " + maslluvioso.coordenada1.X + " , " + maslluvioso.coordenada1.Y;
            msj += "\nCoordenada B : " + maslluvioso.coordenada2.X + " , " + maslluvioso.coordenada2.Y;
            msj += "\nCoordenada C : " + maslluvioso.coordenada3.X + " , " + maslluvioso.coordenada3.Y;

            return msj;
        }

        public void CalcularPeriodos()
        {
            List<Forecast> registros = new List<Forecast>();
            string clima1 = string.Empty;
            string clima2 = string.Empty;
            int contSequia = 0;
            int contOptimo = 0;
            int contLluvia = 0;


            registros = datos.ObtenerTodoLosRegistros();


            for (int i = 0; i < registros.Count; i++)
            {                
                clima1 = registros[i].clima;

                if (i < registros.Count -1 )
                {
                    clima2 = registros[i + 1].clima;

                    if (clima1 != clima2)
                    {
                        if (clima1 == "Sequia")
                            contSequia++;
                        else if (clima1 == "Optimo")
                            contOptimo++;
                        else if (clima1 == "Lluvia")
                            contLluvia++;
                    }
                        
                }
                else
                {
                    if (clima1 != registros[i-1].clima)
	                {
                        if (clima1 == "Sequia")
                            contSequia++;
                        else if (clima1 == "Optimo")
                            contOptimo++;
                        else if (clima1 == "Lluvia")
                            contLluvia++;
		 
	                } 
                }

            }

            GuardarPeriodos(contSequia, contOptimo, contLluvia);
           
        }

        public void GuardarPeriodos(int contSequia, int contOptimo, int contLluvia)//guardo periodos en db
        {
            List<Period> periodos = new List<Period>(); //agrego todos los periodos para guardar en db
            periodos.Add(new Period { nombre = "Sequia", cantidadTotal = contSequia });
            periodos.Add(new Period { nombre = "Optimo", cantidadTotal = contOptimo });
            periodos.Add(new Period { nombre = "Lluvia", cantidadTotal = contLluvia });

            datos.GuardarPeriodos(periodos); //guardo en db

        }

        public string ObtenerUnPeriodo(string clima)
        {
            Period resultado = new Period();
            string mensaje = string.Empty;


            resultado = datos.ObtenerPeriodo(clima);

            if (resultado != null)
            {
                mensaje = "El clima " + clima + " tiene " + resultado.cantidadTotal + " periodos.\n";

                if (clima == "Lluvia")
                    mensaje += DiaMasLluvioso();
            }

            else
                mensaje = "El clima " + clima + " no tiene periodos";

            return mensaje;
        }

        public string ObtenerPeriodos()
        {
            List<string> climas = new List<string>();
            climas.Add("Sequia");
            climas.Add("Optimo");
            climas.Add("Lluvia");
            string mensaje = string.Empty;


            for (int i = 0; i < climas.Count; i++)
            {
                mensaje += ObtenerUnPeriodo(climas[i]);
                
            }
           

            return mensaje;
        }
    }
}
