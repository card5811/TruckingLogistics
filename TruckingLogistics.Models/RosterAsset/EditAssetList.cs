﻿using System;
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

        public int CompanyUserId { get; set; }

        public int TruckId { get; set; }

        public int TrailerId { get; set; }
    }
}
