using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckingLogistics.Data
{
    public enum TrailerType { FlatBed_48FT, FlatBed_53FT, Conestoga_48Ft, Conestoga_48FT, Conestoga_53FT, Single_StepDeck_48FT, Single_StepDeck_53FT, Double_StepDeck_48FT, Double_StepDeck_53FT, DryVan_48FT, DryVan_53Ft, Reefer_48FT, Reefer_53FT }
    public class TrailerAsset
    {
        [Key]
        public int TrailerId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string TrailerNumber { get; set; }

        [Required]
        public string TrailerVinNumber { get; set; }

        [Required]
        public TrailerType TrailerType { get; set; }

        public int? TrailerMileage { get; set; }

        public string Comment { get; set; }

    }
}
