using Auth.Infrastructure.Data;
using Auth.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repository
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(WebAppContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<string>> GetUserRoles(Guid userId)
        {
            var dt =await (from userRoles in dbContext.UserRoles
                         join
                         roles in dbContext.Role on userRoles.Role_Id equals roles.Id
                         where userRoles.User_Id.Equals(userId)
                         select new { role = roles.Code }).ToAsyncEnumerable().Select(x=> x.role).ToList();
            return dt;
        }
    }
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<List<string>> GetUserRoles(Guid userId);
    }
}
