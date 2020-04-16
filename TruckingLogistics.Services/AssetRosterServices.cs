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
                    RosterId = model.RosterId,
                    CompanyUserId = model.CompanyUserId,
                    TruckId = model.TruckId,
                    TrailerId = model.TrailerId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.AssetRosters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AssetList> GetAssets()
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
                                    RosterId = e.RosterId,
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
                        .Single(e => e.RosterId == id);
                return
                    new AssetListDetails
                    {
                        FirstName = entity.UserProfile.FirstName,
                        LastName = entity.UserProfile.LastName,
                        Make = entity.TruckAsset.Make,
                        TruckModel = entity.TruckAsset.TruckModel,
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
                    .Single(e => e.RosterId == model.RosterId);

                entity.UserProfile.CompanyUserId = model.CompanyUserId;
                entity.TruckAsset.TruckId = model.TruckId;
                entity.TrailerAsset.TrailerId = model.TrailerId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteFromRoster(int rosterId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .AssetRosters
                    .Single(e => e.RosterId == rosterId);

                ctx.AssetRosters.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
