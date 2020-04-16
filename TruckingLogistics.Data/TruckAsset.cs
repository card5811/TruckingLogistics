using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckingLogistics.Data
{
    public class TruckAsset
    {
        [Key]
        public int TruckId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string TruckNumber { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public int? Mileage { get; set; }

        public string Comment { get; set; }
    }
}
