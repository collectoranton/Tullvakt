using System;

namespace Tullvakt
{
    public class Vehicle
    {
        public int Weight { get; }
        public VehicleType Type { get; }
        public bool IsEcoFriendly { get; }

        public Vehicle(int weight, VehicleType type, bool isEcoFriendly = false)
        {
            if (weight < 1)
                throw new ArgumentException("A vehicle must have weight", nameof(weight));

            Weight = weight;
            Type = type;
            IsEcoFriendly = isEcoFriendly;
        }
    }
}
