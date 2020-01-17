using Auth.Infrastructure.Data;
using Auth.Infrastructure.Enum;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auth.Infrastructure.Seed
{
    public class RoleSeed
    {
        public static void CreateRole(WebAppContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            using (var transaction = context.Database.BeginTransaction()) {
                try {
                    IList<Role> roles = new List<Role>();
                    var appId = context.Application.Where(x => x.Description.Contains("melhorias")).FirstOrDefault().Id;
                    if (context.Role.Where(i => i.Id == (int)RoleEnum.ADMIN).FirstOrDefault() == null)
                        roles.Add(new Role() {
                            Code = RoleEnum.ADMIN.ToString(),
                            Id = (int)RoleEnum.ADMIN,
                            Application_Id = appId
                        });
                    if (context.Role.Where(i => i.Id == (int)RoleEnum.CONTRIBUTOR).FirstOrDefault() == null)
                        roles.Add(new Role() {
                            Code = RoleEnum.CONTRIBUTOR.ToString(),
                            Id = (int)RoleEnum.CONTRIBUTOR,
                            Application_Id = appId
                        });
                    if (context.Role.Where(i => i.Id == (int)RoleEnum.PUBLIC).FirstOrDefault() == null)
                        roles.Add(new Role() {
                            Code = RoleEnum.PUBLIC.ToString(),
                            Id = (int)RoleEnum.PUBLIC,
                            Application_Id = appId
                        });
                    if (context.Role.Where(i => i.Id == (int)RoleEnum.READER).FirstOrDefault() == null)
                        roles.Add(new Role() {
                            Code = RoleEnum.READER.ToString(),
                            Id = (int)RoleEnum.READER,
                            Application_Id = appId
                        });
                    if (roles.Any()) {
                        context.Role.AddRange(roles);
                        context.SaveChanges();

                    }
                    transaction.Commit();
                }
                catch (Exception ex) {
                    transaction.Rollback();
                    throw;
                }
            }

        }
    }
}
