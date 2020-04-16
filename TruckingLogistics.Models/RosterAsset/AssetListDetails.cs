using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckingLogistics.Data;

namespace TruckingLogistics.Models.RosterAsset
{
    public class AssetListDetails
    {
        public int RosterId { get; set; }

        public int CompanyUserId { get; set; }

        public int TruckId { get; set; }

        public int TrailerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Make { get; set; }

        public string TruckModel { get; set; }

        public string TruckNumber { get; set; }

        public string TrailerNumber { get; set; }

        public TrailerType TypeOfTrailer { get; set; }
    }
}
