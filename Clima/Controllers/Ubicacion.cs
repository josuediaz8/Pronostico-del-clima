using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clima.Models;

namespace Clima.Controllers
{
    class Ubicacion
    {
        public IList<Planet> planetas {get; set;}
        public Coordenadas coordenadas {get; set;}
    }
}
