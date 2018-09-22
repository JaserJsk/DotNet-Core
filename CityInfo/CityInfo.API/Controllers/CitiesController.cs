using CityInfo.API.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        #region GetCities
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }
        #endregion

        #region GetCity
        [HttpGet("{cityid}")]
        public IActionResult GetCity(int cityid)
        {
            // Find city
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityid);
            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        } 
        #endregion
    }
}
