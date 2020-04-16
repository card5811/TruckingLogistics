using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckingLogistics.Models.RosterAsset
{
    public class DeleteAssetList
    {
        public int RosterId { get; set; }

        public string FirstName { get; set; }

        public string TruckNumber { get; set; }

        public string TrailerNumber { get; set; }
    }
}
