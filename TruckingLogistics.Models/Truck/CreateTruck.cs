using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckingLogistics.Models.Truck
{
    public class CreateTruck
    {
        public int TruckId { get; set; }

        public string TruckNumber { get; set; }

        [Required]
        [MinLength(2, ErrorMessage ="Please enter correct make of truck")]
        [MaxLength(50, ErrorMessage ="Please enter correct make of truck")]
        public string Make { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Please enter correct model of truck")]
        [MaxLength(50, ErrorMessage = "Please enter correct model of truck")]
        public string TruckModel { get; set; }

        public int? Mileage { get; set; }

        [MaxLength(5000, ErrorMessage ="Only 5,000 characters allowed.")]
        public string Comment { get; set; }
    }
}
