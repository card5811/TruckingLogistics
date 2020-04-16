using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckingLogistics.Data
{
    public class AssetRoster
    {
        [Key]
        public int RosterId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserProfile))]
        public int? CompanyUserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        [Required]
        [ForeignKey(nameof(TruckAsset))]
        public int TruckId { get; set; }
        public virtual TruckAsset TruckAsset { get; set; }

        [Required]
        [ForeignKey(nameof(TrailerAsset))]
        public int TrailerId { get; set; }
        public virtual TrailerAsset TrailerAsset { get; set; }
    }
}
