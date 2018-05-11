using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tullvakt
{
    public partial class Vehicle
    {
        public int Weight { get; set; }
        public VehicleType Type { get; set; }
        public bool IsEcoFriendly { get; set; }

        public Vehicle(int weight, VehicleType type, bool isEcoFriendly)
        {
            Weight = weight;
            Type = type;
            IsEcoFriendly = isEcoFriendly;
        }
    }
}
