using System;
using System.Collections.Generic;
using Xunit;

namespace Tullvakt.Test
{
    public class Toll_GetPriceShould
    {
        private readonly Toll toll = new Toll();
        private readonly Vehicle Car1000kg = new Vehicle(1000, VehicleType.Car, false);
        private readonly Vehicle Car999kg = new Vehicle(999, VehicleType.Car, false);
        private readonly Vehicle EcoCar1000kg = new Vehicle(1000, VehicleType.Car, true);
        private readonly Vehicle EcoCar999kg = new Vehicle(999, VehicleType.Car, true);
        private readonly Vehicle Truck1000kg = new Vehicle(1000, VehicleType.Truck, false);
        private readonly Vehicle Truck999kg = new Vehicle(999, VehicleType.Truck, false);
        private readonly Vehicle EcoTruck1000kg = new Vehicle(1000, VehicleType.Truck, true);
        private readonly Vehicle EcoTruck999kg = new Vehicle(999, VehicleType.Truck, true);
        private readonly Vehicle Motorcycle1000kg = new Vehicle(1000, VehicleType.Motorcycle, false);
        private readonly Vehicle Motorcycle999kg = new Vehicle(999, VehicleType.Motorcycle, false);
        private readonly Vehicle EcoMotorcycle1000kg = new Vehicle(1000, VehicleType.Motorcycle, true);
        private readonly Vehicle EcoMotorcycle999kg = new Vehicle(999, VehicleType.Motorcycle, true);

        [Fact]
        public void Return0ForAllEcoVehiclesOnlyRegardlessOfDateTime()
        {
            const decimal price = 0m;
            var dateTime = DateTime.Now;
            
            Assert.Equal(price, toll.GetPrice(EcoCar1000kg, dateTime));
            Assert.Equal(price, toll.GetPrice(EcoCar999kg, dateTime));
            Assert.Equal(price, toll.GetPrice(EcoTruck1000kg, dateTime));
            Assert.Equal(price, toll.GetPrice(EcoTruck999kg, dateTime));
            Assert.Equal(price, toll.GetPrice(EcoMotorcycle1000kg, dateTime));
            Assert.Equal(price, toll.GetPrice(EcoMotorcycle999kg, dateTime));
            Assert.NotEqual(price, toll.GetPrice(Car1000kg, dateTime));
            Assert.NotEqual(price, toll.GetPrice(Car999kg, dateTime));
            Assert.NotEqual(price, toll.GetPrice(Truck1000kg, dateTime));
            Assert.NotEqual(price, toll.GetPrice(Truck999kg, dateTime));
            Assert.NotEqual(price, toll.GetPrice(Motorcycle1000kg, dateTime));
            Assert.NotEqual(price, toll.GetPrice(Motorcycle999kg, dateTime));
        }

        [Fact]
        public void ReturnCorrectPriceForRegularWeekday()
        {
            var dayFirst = new DateTime(2018, 5, 7, 6, 0, 0);           // 2018-05-07 06:00:00      (Regular monday)
            var dayLast = new DateTime(2018, 5, 7, 17, 59, 59);         // 2018-05-07 17:59:59
            var eveningFirst = new DateTime(2018, 5, 7, 18, 0, 0);      // 2018-05-07 18:00:00
            var eveningLast = new DateTime(2018, 5, 7, 5, 59, 59);      // 2018-05-07 05:59:59

            Assert.Equal(1000m, toll.GetPrice(Car1000kg, dayFirst));
            Assert.Equal(1000m, toll.GetPrice(Car1000kg, dayLast));
            Assert.Equal(500m, toll.GetPrice(Car1000kg, eveningFirst));
            Assert.Equal(500m, toll.GetPrice(Car1000kg, eveningLast));

            Assert.Equal(500m, toll.GetPrice(Car999kg, dayFirst));
            Assert.Equal(500m, toll.GetPrice(Car999kg, dayLast));
            Assert.Equal(250m, toll.GetPrice(Car999kg, eveningFirst));
            Assert.Equal(250m, toll.GetPrice(Car999kg, eveningLast));

            Assert.Equal(2000m, toll.GetPrice(Truck1000kg, dayFirst));
            Assert.Equal(2000m, toll.GetPrice(Truck1000kg, dayLast));
            Assert.Equal(1000m, toll.GetPrice(Truck1000kg, eveningFirst));
            Assert.Equal(1000m, toll.GetPrice(Truck1000kg, eveningLast));

            Assert.Equal(2000m, toll.GetPrice(Truck999kg, dayFirst));
            Assert.Equal(2000m, toll.GetPrice(Truck999kg, dayLast));
            Assert.Equal(1000m, toll.GetPrice(Truck999kg, eveningFirst));
            Assert.Equal(1000m, toll.GetPrice(Truck999kg, eveningLast));

            Assert.Equal(700m, toll.GetPrice(Motorcycle1000kg, dayFirst));
            Assert.Equal(700m, toll.GetPrice(Motorcycle1000kg, dayLast));
            Assert.Equal(350m, toll.GetPrice(Motorcycle1000kg, eveningFirst));
            Assert.Equal(350m, toll.GetPrice(Motorcycle1000kg, eveningLast));

            Assert.Equal(350m, toll.GetPrice(Motorcycle999kg, dayFirst));
            Assert.Equal(350m, toll.GetPrice(Motorcycle999kg, dayLast));
            Assert.Equal(175m, toll.GetPrice(Motorcycle999kg, eveningFirst));
            Assert.Equal(175m, toll.GetPrice(Motorcycle999kg, eveningLast));
        }

        [Fact]
        public void ReturnCorrectPriceForHoliday()
        {
            var dayFirst = new DateTime(2018, 5, 10, 6, 0, 0);           // 2018-05-10 06:00:00      (Kristi himmelsfärd)
            var dayLast = new DateTime(2018, 5, 10, 17, 59, 59);         // 2018-05-10 17:59:59
            var eveningFirst = new DateTime(2018, 5, 10, 18, 0, 0);      // 2018-05-10 18:00:00
            var eveningLast = new DateTime(2018, 5, 10, 5, 59, 59);      // 2018-05-10 05:59:59

            Assert.Equal(2000m, toll.GetPrice(Car1000kg, dayFirst));
            Assert.Equal(2000m, toll.GetPrice(Car1000kg, dayLast));
            Assert.Equal(2000m, toll.GetPrice(Car1000kg, eveningFirst));
            Assert.Equal(2000m, toll.GetPrice(Car1000kg, eveningLast));

            Assert.Equal(1000m, toll.GetPrice(Car999kg, dayFirst));
            Assert.Equal(1000m, toll.GetPrice(Car999kg, dayLast));
            Assert.Equal(1000m, toll.GetPrice(Car999kg, eveningFirst));
            Assert.Equal(1000m, toll.GetPrice(Car999kg, eveningLast));

            Assert.Equal(4000m, toll.GetPrice(Truck1000kg, dayFirst));
            Assert.Equal(4000m, toll.GetPrice(Truck1000kg, dayLast));
            Assert.Equal(4000m, toll.GetPrice(Truck1000kg, eveningFirst));
            Assert.Equal(4000m, toll.GetPrice(Truck1000kg, eveningLast));

            Assert.Equal(4000m, toll.GetPrice(Truck999kg, dayFirst));
            Assert.Equal(4000m, toll.GetPrice(Truck999kg, dayLast));
            Assert.Equal(4000m, toll.GetPrice(Truck999kg, eveningFirst));
            Assert.Equal(4000m, toll.GetPrice(Truck999kg, eveningLast));

            Assert.Equal(1400m, toll.GetPrice(Motorcycle1000kg, dayFirst));
            Assert.Equal(1400m, toll.GetPrice(Motorcycle1000kg, dayLast));
            Assert.Equal(1400m, toll.GetPrice(Motorcycle1000kg, eveningFirst));
            Assert.Equal(1400m, toll.GetPrice(Motorcycle1000kg, eveningLast));

            Assert.Equal(700m, toll.GetPrice(Motorcycle999kg, dayFirst));
            Assert.Equal(700m, toll.GetPrice(Motorcycle999kg, dayLast));
            Assert.Equal(700m, toll.GetPrice(Motorcycle999kg, eveningFirst));
            Assert.Equal(700m, toll.GetPrice(Motorcycle999kg, eveningLast));
        }

        [Fact]
        public void ThrowExceptionsOnInvalidInputs()
        {

        }
    }
}