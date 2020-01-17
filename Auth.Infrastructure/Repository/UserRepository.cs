using Auth.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Enum;

namespace Auth.Infrastructure.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(WebAppContext dbContext)
        : base(dbContext) { }

        public async Task<bool> CreateBasicUser(User user)
        {
            dbContext.User.Add(user);
            var userRole = new UserRole() {
                User_Id = user.Id,
                Role_Id = (int)RoleEnum.PUBLIC
            };
           await dbContext.UserRoles.AddAsync(userRole);
            return true;
        }

        public async Task<User> Authenticate(string login, string password)
        {
            try {
                var user = await dbContext.User.Where(x => x.Login == login).AsNoTracking().FirstOrDefaultAsync();
                    return user;
                throw new Exception("User not found");
            }
            catch (Exception) {

                throw;
            }
        }
        public async Task<User> GetAsyncWhere(Expression<Func<User, bool>> filter)
        {
            return await dbContext.User.Where(filter).FirstOrDefaultAsync();
        }
    }
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsyncWhere(Expression<Func<User, bool>> filter);
        Task<bool> CreateBasicUser(User user);
        Task<User> Authenticate(string username, string password);
    }

}
