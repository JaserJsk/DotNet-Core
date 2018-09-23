using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }

            // Init Seed Data
            var cities = new List<City>()
            {
                new City()
                {
                    Name = "Stockholm",
                    Description = "Largest city & Capital of Sweden",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest() {
                            Name = "Stockholm Old Town",
                            Description = "The Old Town, unsurprisingly, is the oldest part of Stockholm." },
                        new PointOfInterest() {
                            Name = "Vasa Museum",
                            Description = "Today Vasa is the world's only preserved 17th century ship." },
                        new PointOfInterest() {
                            Name = "Stockholm City Hall",
                            Description = "The Stockholm City Hall is one of the capital's most visited tourist attractions." }
                    }
                },
                new City()
                {
                    Name = "Göteborg",
                    Description = "Second largest city in Sweden",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest() {
                            Name = "Southern Goteborg Archipelago",
                            Description = "The archipelago of Gothenburg stretches along the coast like a string of pearls." },
                        new PointOfInterest() {
                            Name = "Aeroseum",
                            Description = "Aeroseum is an experience center with the ambition to describe the development of the aircraft, both civilian and military." },
                        new PointOfInterest() {
                            Name = "Botanical Garden",
                            Description = "The Garden is situated in a formerly completely rural area, where earlier a great country estate was located." }
                    }
                },
                new City()
                {
                    Name = "Malmö",
                    Description = "Third largest city in Sweden",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest() {
                            Name = "Oresund Bridge",
                            Description = "The bridge is a combined railway and motorway across the Øresund strait between Sweden and Denmark." },
                        new PointOfInterest() {
                            Name = "Turning Torso",
                            Description = "It is regarded as the first twisted skyscraper in the world." },
                        new PointOfInterest() {
                            Name = "City Library",
                            Description = "A municipal public library in Malmö, Sweden, which opened December 12, 1905." }
                    }
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
