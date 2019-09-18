using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using APIClima.Models;

namespace APIClima.Controllers
{
    public class ForecastController : ApiController
    {
        private Entities db = new Entities();

        // GET api/Forecast
        public IHttpActionResult GetForecasts()
        {
            var result = db.Forecasts.Select(x => new { dia = x.dia, clima = x.clima }).ToList();

            return Ok(result);

        }

        // GET api/Forecast/5
        [ResponseType(typeof(Forecasts))]
        public IHttpActionResult GetForecasts(int id)
        {
            string result = string.Empty;
            var forecasts = db.Forecasts.Select(x => new { dia = x.dia, clima = x.clima}) //busco con EF por dia
                                        .ToList()
                                        .FirstOrDefault(x => x.dia == id); 
            
            if (forecasts == null)
            {
                return NotFound();
            }

            return Ok(forecasts);
        }

    }
}