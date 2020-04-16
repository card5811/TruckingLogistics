using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckingLogistics.Data;

namespace TruckingLogistics.Models.Trailer
{
   
    public class CreateTrailer
    {
        public int TrailerId { get; set; }
        public string TrailerNumber { get; set; }
        public string TrailerVinNumber { get; set; }
        public TrailerType TrailerType { get; set; }
        public int? TrailerMileage { get; set; }

        [MaxLength(5000, ErrorMessage = "Only 5,000 characters allowed.")]
        public string Comment { get; set; }
    }
}
