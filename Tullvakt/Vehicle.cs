using System;

namespace Tullvakt
{
    public class Vehicle
    {
        public int Weight { get; set; }
        public VehicleType Type { get; set; }
        public bool IsEcoFriendly { get; set; }

        public Vehicle(int weight, VehicleType type, bool isEcoFriendly)
        {
            if (weight < 1)
                throw new ArgumentException("A vehicle must have weight", nameof(weight));

            Weight = weight;
            Type = type;
            IsEcoFriendly = isEcoFriendly;
        }
    }
}
