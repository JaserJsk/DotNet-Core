using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Data
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Stockholm",
                    Description = "Largest city & Capital of Sweden",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Stockholm Old Town",
                            Description = "The Old Town, unsurprisingly, is the oldest part of Stockholm."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Vasa Museum",
                            Description = "Today Vasa is the world's only preserved 17th century ship."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Stockholm City Hall",
                            Description = "The Stockholm City Hall is one of the capital's most visited tourist attractions."
                        }
                    }
                },

                new CityDto()
                {
                    Id = 2,
                    Name = "Göteborg",
                    Description = "Second largest city in Sweden",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Southern Goteborg Archipelago",
                            Description = "The archipelago of Gothenburg stretches along the coast like a string of pearls. "
                        },
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Aeroseum",
                            Description = "Aeroseum is an experience center with the ambition to describe the development of the aircraft, both civilian and military."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "Botanical Garden",
                            Description = "The Garden is situated in a formerly completely rural area, where earlier a great country estate was located."
                        }
                    }
                },

                new CityDto()
                {
                    Id = 3,
                    Name = "Malmö",
                    Description = "Third largest city in Sweden",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 7,
                            Name = "Oresund Bridge",
                            Description = "The bridge is a combined railway and motorway across the Øresund strait between Sweden and Denmark."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 8,
                            Name = "Turning Torso",
                            Description = "It is regarded as the first twisted skyscraper in the world."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 9,
                            Name = "City Library",
                            Description = "A municipal public library in Malmö, Sweden, which opened December 12, 1905."
                        }
                    }
                }
            };
        }
    }
}
