using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckingLogistics.Models.Truck
{
    public class DeleteTruck
    {
        public int TruckId { get; set; }

        public string TruckNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int? Mileage { get; set; }

        public string Comment { get; set; }
    }
}
