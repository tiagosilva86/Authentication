using Auth.Infrastructure.Data;
using Auth.Infrastructure.Enum;
using Auth.Infrastructure.Service;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auth.Infrastructure.Seed
{
    public class UserSeed
    {
        public static void CreateUser(WebAppContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Cipher c = new Cipher();
            using (var transaction = context.Database.BeginTransaction()) {
                try {

                    User user;
                    if (context.User.Where(i => i.Login.ToLower() == "admin").FirstOrDefault() == null) {
                        user = new User() {
                            Login = "admin",
                            Email = "",
                            Name = "Default seed user",
                            Password = c.Encrypt("admin"),
                            LastAccess = DateTime.Now,
                            Active = true
                        };

                        context.User.Add(user);
                        context.SaveChanges();
                        var roleId = context.Role.Where(r =>
                            r.Id.Equals((int)RoleEnum.ADMIN)).FirstOrDefault().Id;

                        UserRole userRole = new UserRole() {
                            User_Id = user.Id,
                            Role_Id = roleId
                        };

                        context.UserRoles.Add(userRole);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
