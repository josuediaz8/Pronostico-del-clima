using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using Clima.Controllers;

namespace Clima.Models
{
    class Planet
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public double velocidad { get; set; }
        public int direccion { get; set; }
        public int distancia { get; set; }

    }

    class Forecast
    {
        public int Id {get; set;}
        public int dia {get; set;}
        public string clima {get; set;}
        public Coordenadas coordenada1 {get; set;}
        public Coordenadas coordenada2 {get; set;}
        public Coordenadas coordenada3 {get; set;}
        public double perimetro { get; set; }
    
    }

    class Period
    {
        public int Id {get; set;}
        public string nombre { get; set; }
        public int cantidadTotal { get; set; }
    }

    class PlanetasDbContext : DbContext //permite gestionar la db a traves del ORM Entity framework
    {
        public DbSet<Planet> Planetas { get; set; }
        public DbSet<Forecast> Pronosticos { get; set; }
        public DbSet<Period> Periodos {get; set;}

    }
}
