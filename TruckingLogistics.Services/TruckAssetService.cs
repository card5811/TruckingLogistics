using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckingLogistics.Data;
using TruckingLogistics.Models.Truck;

namespace TruckingLogistics.Services
{
    public class TruckAssetService
    {
        private readonly Guid _userId;

        public TruckAssetService(Guid userId)
        {
            _userId = userId;
        }

        public bool TruckCreate(CreateTruck model)
        {
            var entity =
                new TruckAsset()
                {
                    UserId = _userId,
                    TruckId = model.TruckId,
                    TruckNumber = model.TruckNumber,
                    Make = model.Make,
                    TruckModel = model.TruckModel,
                    Mileage = model.Mileage,
                    Comment = model.Comment,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.TruckAssets.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TruckList> GetTrucks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .TruckAssets
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                        new TruckList
                        {
                            TruckId = e.TruckId,
                            TruckNumber = e.TruckNumber,
                            Make = e.Make,
                            TruckModel = e.TruckModel,
                            Mileage = e.Mileage
                        }
                        );

                return query.ToArray();
            }
        }

        public TruckDetail GetTruckById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TruckAssets
                        .Single(e => e.TruckId == id);
                return
                    new TruckDetail
                    {
                        TruckId = entity.TruckId,
                        TruckNumber = entity.TruckNumber,
                        Make = entity.Make,
                        TruckModel = entity.TruckModel,
                        Mileage = entity.Mileage,
                        Comment = entity.Comment
                    };
            }
        }

        public bool UpdateTruck(EditTruck model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TruckAssets
                    .Single(e => e.TruckId == model.TruckId);

                entity.TruckNumber = model.TruckNumber;
                entity.Make = model.Make;
                entity.TruckModel = model.TruckModel;
                entity.Mileage = model.Mileage;
                entity.Comment = model.Comment;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTruckById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .TruckAssets
                    .Single(e => e.TruckId == id);

                ctx.TruckAssets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}