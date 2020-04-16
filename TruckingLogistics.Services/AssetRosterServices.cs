using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckingLogistics.Data;
using TruckingLogistics.Models.RosterAsset;

namespace TruckingLogistics.Services
{
    public class AssetRosterServices
    {
        private readonly Guid _userId;

        public AssetRosterServices(Guid userId)
        {
            _userId = userId;
        }

        public bool AddToRoster(CreateAssetList model)
        {
            var entity =
                new AssetRoster
                {
                    UserId = _userId,
                    CompanyUserId = model.CompanyUserId,
                    TruckId = model.CompanyUserId,
                    TrailerId = model.TrailerId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.AssetRosters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AssetList> GetAssetLists()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .AssetRosters
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new AssetList
                                {
                                    FirstName = e.UserProfile.FirstName,
                                    TruckNumber = e.TruckAsset.TruckNumber,
                                    TrailerNumber = e.TrailerAsset.TrailerNumber
                                }
                                );
                return query.ToArray();
            }
        }

        public AssetListDetails GetAssetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AssetRosters
                        .Single(e => e.RosterId == id && e.UserId == _userId);
                return
                    new AssetListDetails
                    {
                        FirstName = entity.UserProfile.FirstName,
                        LastName = entity.UserProfile.LastName,
                        Make = entity.TruckAsset.Make,
                        Model = entity.TruckAsset.Model,
                        TruckNumber = entity.TruckAsset.TruckNumber,
                        TrailerNumber = entity.TrailerAsset.TrailerNumber,
                        TypeOfTrailer = entity.TrailerAsset.TrailerType
                    };
            }
        }

        public bool UpdateAsset(EditAssetList model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .AssetRosters
                    .Single(e => e.RosterId == model.RosterId && e.UserId == _userId);

                entity.UserProfile.FirstName = model.FirstName;
                entity.TruckAsset.TruckNumber = model.TruckNumber;
                entity.TrailerAsset.TrailerNumber = model.TrailerNumber;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteFromRoster(int rosterId)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.AssetRosters.First(e => e.RosterId == rosterId && e.UserId == _userId);
                context.AssetRosters.Remove(entity);
                context.SaveChanges();

                return context.SaveChanges() == 1;
            }
        }
    }
}
