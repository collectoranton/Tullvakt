using System;
using Xunit;

namespace Tullvakt.Test
{
    public class Vehicle_Should
    {
        [Fact]
        public void ThrowArgumentExceptionOnInvalidWeight()
        {
            Assert.Throws<ArgumentException>(() => new Vehicle(0, VehicleType.Car, false));
            Assert.Throws<ArgumentException>(() => new Vehicle(int.MinValue, VehicleType.Car, false));
        }
    }
}
