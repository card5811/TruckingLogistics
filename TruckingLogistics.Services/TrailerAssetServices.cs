using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckingLogistics.Data;
using TruckingLogistics.Models.Trailer;
using TruckingLogistics.Models.Truck;

namespace TruckingLogistics.Services
{
    public class TrailerAssetServices
    {
        private readonly Guid _userId;

        public TrailerAssetServices(Guid userId)
            {
            _userId = userId;
            }

        public bool TrailerCreate(CreateTrailer model)
        {
            var entity =
                new TrailerAsset()
                {
                    UserId = _userId,
                    TrailerId = model.TrailerId,
                    TrailerNumber = model.TrailerNumber,
                    TrailerVinNumber = model.TrailerVinNumber,
                    Comment = model.Comment,
                    TrailerType = model.TrailerType,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.TrailerAssets.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TrailerList> GetTrailer()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .TrailerAssets
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                        new TrailerList
                        {
                            TrailerId = e.TrailerId,
                            TrailerNumber = e.TrailerNumber,
                            TrailerVinNumber = e.TrailerVinNumber,
                            Comment = e.Comment,
                            TrailerType = e.TrailerType,
                        }
                        );

                return query.ToArray();
            }
        }

        public TrailerDetails GetTrailerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TrailerAssets
                        .Single(e => e.TrailerId == id);
                return
                    new TrailerDetails
                    {
                        TrailerId = entity.TrailerId,
                        TrailerNumber = entity.TrailerNumber,
                        TrailerVinNumber = entity.TrailerVinNumber,
                        Comment = entity.Comment,
                        TrailerType = entity.TrailerType,
                    };
            }
        }

        public bool UpdateTrailer(EditTrailer model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrailerAssets
                    .Single(e => e.TrailerId == model.TrailerId);

                entity.TrailerNumber = model.TrailerNumber;
                entity.TrailerVinNumber = model.TrailerVinNumber;
                entity.Comment = model.Comment;
                entity.TrailerType = model.TrailerType;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTrailerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TrailerAssets
                    .Single(e => e.TrailerId == id);

                ctx.TrailerAssets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
