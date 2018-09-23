using AutoMapper;
using CityInfo.API.Data;
using CityInfo.API.Interfaces;
using CityInfo.API.Models;
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
        private ICityInfoRepository _cityInfoRepository;

        #region Constructor
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        } 
        #endregion

        #region GET [ GetCities ]
        [HttpGet()]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            var results = Mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities);

            return Ok(results);
        }
        #endregion

        #region GET [ GetCity ]
        [HttpGet("{cityid}")]
        public IActionResult GetCity(int cityid, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(cityid, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var cityResult = Mapper.Map<IEnumerable<CityDto>>(city);

                return Ok(cityResult);
            }

            var cityWithoutPointsOfInterestResult = Mapper.Map<CityWithoutPointOfInterestDto>(city);

            return Ok(cityWithoutPointsOfInterestResult); 
        }
        #endregion
    }
}
