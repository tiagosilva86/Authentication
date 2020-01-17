using Auth.Infrastructure.Data;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Text;
// Needed to make join
using System.Linq;

namespace Auth.Infrastructure.Repository
{
    class EndPointRepository : RepositoryBase<EndPoint>, IEndPointRepository
    {
        public EndPointRepository(WebAppContext dbContext) : base(dbContext)
        {
        }

        public List<Guid> GetByUserIdAndApplicationId(Guid userId, Guid applicationId)
        {

                var dt = (from roleEndPoint in dbContext.RoleEndPoints
                          join roleUser in dbContext.UserRoles
                          on roleEndPoint.Role_Id equals roleUser.Role_Id
                          join role in dbContext.Role
                          on roleUser.Role_Id equals role.Id
                          join application in dbContext.Application
                          on role.Application_Id equals application.Id
                          join EP in dbContext.EndPoint on roleEndPoint.EndPoint_Id equals EP.Hash
                          where roleUser.User_Id == userId
                          && application.Id == applicationId
                          select new { EP.Hash }).Select(x=> x.Hash).ToList();
                if (dt.Any())
                    return dt;
                return null;       
        }
    }
    public interface IEndPointRepository : IRepository<EndPoint>
    {
        List<Guid> GetByUserIdAndApplicationId(Guid userId, Guid applicationId);
    }
}
