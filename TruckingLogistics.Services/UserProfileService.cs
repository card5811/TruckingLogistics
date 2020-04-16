using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckingLogistics.Data;
using TruckingLogistics.Models.User;

namespace TruckingLogistics.Services
{
    public class UserProfileService
    {
        private readonly Guid _userId;

        public UserProfileService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateUser(CreateUser model)
        {
            var entity =
                new UserProfile()
                {
                    UserId = _userId,
                    CompanyUserId = model.CompanyUserId,
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserProfiles.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<UserList> GetUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UserProfiles
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new UserList
                                {
                                    CompanyUserId = e.CompanyUserId,
                                    UserName = e.UserName,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    Email = e.Email
                                }
                        );

                return query.ToArray();
            }
        }

        public UserDetail GetUserByUserName(string userName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.UserName == userName && e.UserId == _userId);
                return
                    new UserDetail
                    {
                        CompanyUserId = entity.CompanyUserId,
                        UserName = entity.UserName,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                    };
            }
        }

        public UserDetail GetUserById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserProfiles
                        .Single(e => e.CompanyUserId == id);
                return
                    new UserDetail
                    {
                        CompanyUserId = entity.CompanyUserId,
                        UserName = entity.UserName,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Email = entity.Email
                    };
            }
        }

        public bool EditUser(EditUser model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .UserProfiles
                    .Single(e => e.CompanyUserId == model.CompanyUserId);

                entity.UserName = model.UserName;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .UserProfiles
                    .Single(e => e.CompanyUserId == userId);

                ctx.UserProfiles.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
