using CityInfo.API.Entities;
using CityInfo.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        #region CityExists
        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        } 
        #endregion

        #region GetCities
        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }
        #endregion

        #region GetCity
        public City GetCity(int cityId, bool inludePointsOfInterest)
        {
            if (inludePointsOfInterest)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefault();
            }

            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }
        #endregion

        #region GetPointOfInterestForCities
        public IEnumerable<PointOfInterest> GetPointOfInterestForCities(int cityId)
        {
            return _context.PointsOfInterest
                .Where(p => p.CityId == cityId).ToList();
        }
        #endregion

        #region GetPointOfInterestForCity
        public PointOfInterest GetPointOfInterestForCity(int cityid, int pointOfInterestId)
        {
            return _context.PointsOfInterest
                .Where(p => p.CityId == cityid && p.Id == pointOfInterestId).FirstOrDefault();
        }
        #endregion

        #region AddPointOfInterestForCity
        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest);
        }
        #endregion

        #region DeletePointOfInterest
        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        } 
        #endregion

        #region Save
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        } 
        #endregion
    }
}
