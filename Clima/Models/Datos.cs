using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clima.Models
{
    class Datos
    {
        PlanetasDbContext db { get; set; }

        public Datos()
        {
            db = new PlanetasDbContext();
        }

        public void EliminarTodo()
        {
            try
            {
                List<Forecast> list = new List<Forecast>();

                var registros = db.Pronosticos.ToList(); //obtengo todos los planetas

                for (int i = 0; i < registros.Count; i++)
                {
                    list.Add(registros[i]);
                }

                db.Pronosticos.RemoveRange(list);

                db.SaveChanges();
 
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtengo la velocidad minima de todos los planetas
        /// </summary>
        public int VelocidadMin() //velocidad del planeta mas lento.
        {
            int velocidadMinima;

            try
            {
                var velocidadMin = db.Planetas.OrderBy(x => x.velocidad).FirstOrDefault();
                velocidadMinima = Convert.ToInt32(velocidadMin.velocidad);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return velocidadMinima;
        }

        /// <summary>
        /// Retorna lista de Planetas.
        /// </summary>
        public List<Planet> ObtenerPlanetas()
        {
            List<Planet> p = new List<Planet>();

            try
            {
                var planets = db.Planetas.ToList();

                for (int i = 0; i < planets.Count; i++)
                {
                    p.Add(planets[i]);

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

            return p;
        }

        public void GuardarClima(Forecast Pronostico)
        {
            try
            {
                db.Pronosticos.Add(Pronostico);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Forecast ObtenerDiaMasLluvioso()
        {
            Forecast diaMasLluvioso = new Forecast();

            //Dia mas lluvioso segun perimetro
            diaMasLluvioso = db.Pronosticos.OrderByDescending(x => x.perimetro).FirstOrDefault();

            return diaMasLluvioso;
        }

        /// <summary>
        /// Retorna todos los registros de la tabla Forecasts
        /// </summary>
        /// <returns></returns>
        public List<Forecast> ObtenerTodoLosRegistros()
        {
            List<Forecast> registros = new List<Forecast>();

            var d = db.Pronosticos.ToList();

            for (int i = 0; i < d.Count; i++)
            {
                registros.Add(d[i]);
            }

            return registros;
        }


        public void GuardarPeriodos(List<Period> Periodos)
        {
            try
            {
                for (int i = 0; i < Periodos.Count; i++)
                {
                    db.Periodos.Add(Periodos[i]);
                    db.SaveChanges();
                    
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Period ObtenerPeriodo(string clima)//Obtengo periodos
        {
            Period resultado = new Period();

            try
            {
                resultado = db.Periodos
                              .Where(x => x.nombre == clima)
                              .FirstOrDefault();

                return resultado;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
