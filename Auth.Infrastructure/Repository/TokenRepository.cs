using Auth.Infrastructure.Data;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Repository
{
    class TokenRepository : RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(WebAppContext dbContext) : base(dbContext)
        {
        }
    }
    public interface ITokenRepository : IRepository<Token>
    {

    }
}
