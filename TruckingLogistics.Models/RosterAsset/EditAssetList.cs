using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckingLogistics.Data;

namespace TruckingLogistics.Models.RosterAsset
{
    public class EditAssetList
    {
        public int RosterId { get; set; }

        public string FirstName { get; set; }

        public string TruckNumber { get; set; }

        public string TrailerNumber { get; set; }
    }
}
