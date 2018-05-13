﻿using System;

namespace Tullvakt
{
    public class Toll
    {
        private const decimal PriceForEcoVehicle = 0m;
        private const decimal PriceBelow1000Kg = 500m;
        private const decimal PriceFrom1000Kg = 1000m;
        private const decimal PriceForTrucks = 2000m;
        private const decimal FactorForEvenings = 0.5m;
        private const decimal FactorForMotorcycle = 0.7m;
        private const decimal FactorForHolidays = 2m;
        private const int EveningStartHour = 18;
        private const int EveningEndHour = 6;

        public decimal GetPrice(Vehicle vehicle, DateTime dateTime)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            if (vehicle.IsEcoFriendly)
                return PriceForEcoVehicle;

            var price = GetBasePrice(vehicle);

            return AdjustForDateTime(price, dateTime);
        }

        private decimal GetBasePrice(Vehicle vehicle)
        {
            if (vehicle.Type == VehicleType.Truck)
                return PriceForTrucks;

            var price = vehicle.Weight < 1000 ? PriceBelow1000Kg : PriceFrom1000Kg;

            if (vehicle.Type == VehicleType.Motorcycle)
                price = AdjustForMotorcycle(price);

            return price;
        }

        private decimal AdjustForMotorcycle(decimal price) => FactorForMotorcycle * price;

        private decimal AdjustForDateTime(decimal price, DateTime dateTime)
        {
            if (DateIsWeekend(dateTime) || DateIsSwedishHoliday(dateTime))
                price = AdjustForWeekendsAndHoliday(price);

            else if (TimeIsEvening(dateTime))
                price = AdjustForEvening(price);

            return price;
        }

        private decimal AdjustForEvening(decimal price) => FactorForEvenings * price;

        private decimal AdjustForWeekendsAndHoliday(decimal price) => FactorForHolidays * price;

        private bool TimeIsEvening(DateTime dateTime)
        {
            return dateTime.Hour >= EveningStartHour || dateTime.Hour < EveningEndHour;
        }

        // Tillagd i efterhand
        private bool DateIsWeekend(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        private bool DateIsSwedishHoliday(DateTime dateTime)
        {
            return new PublicHoliday.SwedenPublicHoliday().IsPublicHoliday(dateTime);
        }
    }
}