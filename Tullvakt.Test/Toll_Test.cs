using System;
using Xunit;

namespace Tullvakt.Test
{
    public class Toll_Test
    {
        private readonly Toll SUT = new Toll();
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

        // TODO: Check Theory
        //[Theory]
        //[InlineData()]
        [Fact]
        public void Return0ForAllEcoVehiclesOnlyRegardlessOfDateTime() // Vehicle vehicle
        {
            const decimal expectedPrice = 0m;
            var timeOfPass = DateTime.Now;
            
            Assert.Equal(expectedPrice, SUT.GetPrice(EcoCar1000kg, timeOfPass));
            Assert.Equal(expectedPrice, SUT.GetPrice(EcoCar999kg, timeOfPass));
            Assert.Equal(expectedPrice, SUT.GetPrice(EcoTruck1000kg, timeOfPass));
            Assert.Equal(expectedPrice, SUT.GetPrice(EcoTruck999kg, timeOfPass));
            Assert.Equal(expectedPrice, SUT.GetPrice(EcoMotorcycle1000kg, timeOfPass));
            Assert.Equal(expectedPrice, SUT.GetPrice(EcoMotorcycle999kg, timeOfPass));
            Assert.NotEqual(expectedPrice, SUT.GetPrice(Car1000kg, timeOfPass));
            Assert.NotEqual(expectedPrice, SUT.GetPrice(Car999kg, timeOfPass));
            Assert.NotEqual(expectedPrice, SUT.GetPrice(Truck1000kg, timeOfPass));
            Assert.NotEqual(expectedPrice, SUT.GetPrice(Truck999kg, timeOfPass));
            Assert.NotEqual(expectedPrice, SUT.GetPrice(Motorcycle1000kg, timeOfPass));
            Assert.NotEqual(expectedPrice, SUT.GetPrice(Motorcycle999kg, timeOfPass));
        }

        [Fact]
        public void ReturnCorrectPriceForRegularWeekday()
        {
            var dayFirst = new DateTime(2018, 5, 7, 6, 0, 0);           // 2018-05-07 06:00:00      (Regular monday)
            var dayLast = new DateTime(2018, 5, 7, 17, 59, 59);         // 2018-05-07 17:59:59
            var eveningFirst = new DateTime(2018, 5, 7, 18, 0, 0);      // 2018-05-07 18:00:00
            var eveningLast = new DateTime(2018, 5, 7, 5, 59, 59);      // 2018-05-07 05:59:59

            Assert.Equal(1000m, SUT.GetPrice(Car1000kg, dayFirst));
            Assert.Equal(1000m, SUT.GetPrice(Car1000kg, dayLast));
            Assert.Equal(500m, SUT.GetPrice(Car1000kg, eveningFirst));
            Assert.Equal(500m, SUT.GetPrice(Car1000kg, eveningLast));

            Assert.Equal(500m, SUT.GetPrice(Car999kg, dayFirst));
            Assert.Equal(500m, SUT.GetPrice(Car999kg, dayLast));
            Assert.Equal(250m, SUT.GetPrice(Car999kg, eveningFirst));
            Assert.Equal(250m, SUT.GetPrice(Car999kg, eveningLast));

            Assert.Equal(2000m, SUT.GetPrice(Truck1000kg, dayFirst));
            Assert.Equal(2000m, SUT.GetPrice(Truck1000kg, dayLast));
            Assert.Equal(1000m, SUT.GetPrice(Truck1000kg, eveningFirst));
            Assert.Equal(1000m, SUT.GetPrice(Truck1000kg, eveningLast));

            Assert.Equal(2000m, SUT.GetPrice(Truck999kg, dayFirst));
            Assert.Equal(2000m, SUT.GetPrice(Truck999kg, dayLast));
            Assert.Equal(1000m, SUT.GetPrice(Truck999kg, eveningFirst));
            Assert.Equal(1000m, SUT.GetPrice(Truck999kg, eveningLast));

            Assert.Equal(700m, SUT.GetPrice(Motorcycle1000kg, dayFirst));
            Assert.Equal(700m, SUT.GetPrice(Motorcycle1000kg, dayLast));
            Assert.Equal(350m, SUT.GetPrice(Motorcycle1000kg, eveningFirst));
            Assert.Equal(350m, SUT.GetPrice(Motorcycle1000kg, eveningLast));

            Assert.Equal(350m, SUT.GetPrice(Motorcycle999kg, dayFirst));
            Assert.Equal(350m, SUT.GetPrice(Motorcycle999kg, dayLast));
            Assert.Equal(175m, SUT.GetPrice(Motorcycle999kg, eveningFirst));
            Assert.Equal(175m, SUT.GetPrice(Motorcycle999kg, eveningLast));
        }

        // Tillagd i efterhand
        [Fact]
        public void ReturnCorrectPriceForWeekend()
        {
            var dayFirst = new DateTime(2018, 5, 13, 6, 0, 0);           // 2018-05-13 06:00:00      (Regular Sunday)
            var dayLast = new DateTime(2018, 5, 13, 17, 59, 59);         // 2018-05-13 17:59:59
            var eveningFirst = new DateTime(2018, 5, 13, 18, 0, 0);      // 2018-05-13 18:00:00
            var eveningLast = new DateTime(2018, 5, 13, 5, 59, 59);      // 2018-05-13 05:59:59

            Assert.Equal(2000m, SUT.GetPrice(Car1000kg, dayFirst));
            Assert.Equal(2000m, SUT.GetPrice(Car1000kg, dayLast));
            Assert.Equal(2000m, SUT.GetPrice(Car1000kg, eveningFirst));
            Assert.Equal(2000m, SUT.GetPrice(Car1000kg, eveningLast));

            Assert.Equal(1000m, SUT.GetPrice(Car999kg, dayFirst));
            Assert.Equal(1000m, SUT.GetPrice(Car999kg, dayLast));
            Assert.Equal(1000m, SUT.GetPrice(Car999kg, eveningFirst));
            Assert.Equal(1000m, SUT.GetPrice(Car999kg, eveningLast));

            Assert.Equal(4000m, SUT.GetPrice(Truck1000kg, dayFirst));
            Assert.Equal(4000m, SUT.GetPrice(Truck1000kg, dayLast));
            Assert.Equal(4000m, SUT.GetPrice(Truck1000kg, eveningFirst));
            Assert.Equal(4000m, SUT.GetPrice(Truck1000kg, eveningLast));

            Assert.Equal(4000m, SUT.GetPrice(Truck999kg, dayFirst));
            Assert.Equal(4000m, SUT.GetPrice(Truck999kg, dayLast));
            Assert.Equal(4000m, SUT.GetPrice(Truck999kg, eveningFirst));
            Assert.Equal(4000m, SUT.GetPrice(Truck999kg, eveningLast));

            Assert.Equal(1400m, SUT.GetPrice(Motorcycle1000kg, dayFirst));
            Assert.Equal(1400m, SUT.GetPrice(Motorcycle1000kg, dayLast));
            Assert.Equal(1400m, SUT.GetPrice(Motorcycle1000kg, eveningFirst));
            Assert.Equal(1400m, SUT.GetPrice(Motorcycle1000kg, eveningLast));

            Assert.Equal(700m, SUT.GetPrice(Motorcycle999kg, dayFirst));
            Assert.Equal(700m, SUT.GetPrice(Motorcycle999kg, dayLast));
            Assert.Equal(700m, SUT.GetPrice(Motorcycle999kg, eveningFirst));
            Assert.Equal(700m, SUT.GetPrice(Motorcycle999kg, eveningLast));
        }

        [Fact]
        public void ReturnCorrectPriceForHoliday()
        {
            var dayFirst = new DateTime(2018, 5, 10, 6, 0, 0);           // 2018-05-10 06:00:00      (Kristi himmelsfärd)
            var dayLast = new DateTime(2018, 5, 10, 17, 59, 59);         // 2018-05-10 17:59:59
            var eveningFirst = new DateTime(2018, 5, 10, 18, 0, 0);      // 2018-05-10 18:00:00
            var eveningLast = new DateTime(2018, 5, 10, 5, 59, 59);      // 2018-05-10 05:59:59

            Assert.Equal(2000m, SUT.GetPrice(Car1000kg, dayFirst));
            Assert.Equal(2000m, SUT.GetPrice(Car1000kg, dayLast));
            Assert.Equal(2000m, SUT.GetPrice(Car1000kg, eveningFirst));
            Assert.Equal(2000m, SUT.GetPrice(Car1000kg, eveningLast));

            Assert.Equal(1000m, SUT.GetPrice(Car999kg, dayFirst));
            Assert.Equal(1000m, SUT.GetPrice(Car999kg, dayLast));
            Assert.Equal(1000m, SUT.GetPrice(Car999kg, eveningFirst));
            Assert.Equal(1000m, SUT.GetPrice(Car999kg, eveningLast));

            Assert.Equal(4000m, SUT.GetPrice(Truck1000kg, dayFirst));
            Assert.Equal(4000m, SUT.GetPrice(Truck1000kg, dayLast));
            Assert.Equal(4000m, SUT.GetPrice(Truck1000kg, eveningFirst));
            Assert.Equal(4000m, SUT.GetPrice(Truck1000kg, eveningLast));

            Assert.Equal(4000m, SUT.GetPrice(Truck999kg, dayFirst));
            Assert.Equal(4000m, SUT.GetPrice(Truck999kg, dayLast));
            Assert.Equal(4000m, SUT.GetPrice(Truck999kg, eveningFirst));
            Assert.Equal(4000m, SUT.GetPrice(Truck999kg, eveningLast));

            Assert.Equal(1400m, SUT.GetPrice(Motorcycle1000kg, dayFirst));
            Assert.Equal(1400m, SUT.GetPrice(Motorcycle1000kg, dayLast));
            Assert.Equal(1400m, SUT.GetPrice(Motorcycle1000kg, eveningFirst));
            Assert.Equal(1400m, SUT.GetPrice(Motorcycle1000kg, eveningLast));

            Assert.Equal(700m, SUT.GetPrice(Motorcycle999kg, dayFirst));
            Assert.Equal(700m, SUT.GetPrice(Motorcycle999kg, dayLast));
            Assert.Equal(700m, SUT.GetPrice(Motorcycle999kg, eveningFirst));
            Assert.Equal(700m, SUT.GetPrice(Motorcycle999kg, eveningLast));
        }

        [Fact]
        public void ThrowArgumentNullExceptionOnNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => SUT.GetPrice(null, DateTime.Now));
        }
    }
}