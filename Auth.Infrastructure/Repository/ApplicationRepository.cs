using Auth.Infrastructure.Data;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Repository
{
    class ApplicationRepository: RepositoryBase<Application>, IApplicationRepository
    {
        public ApplicationRepository(WebAppContext dbContext) : base(dbContext)
        {
        }
    }
    public interface IApplicationRepository : IRepository<Application>
    {
        
    }
}
