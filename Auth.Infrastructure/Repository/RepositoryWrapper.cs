using Auth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Repository
{
    public interface IRepositoryWrapper
    {
    
        IUserRepository User { get; }
        IUserRoleRepository UserRole { get; }
        ITokenRepository Token { get; }
        IEndPointRepository EndPoint { get; }
        IApplicationRepository Application { get; }
        void Commit();
    }
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private WebAppContext dbContext;
        private IApplicationRepository _application;
        private IUserRepository _user;
        private IUserRoleRepository _userRole;
        private ITokenRepository _token;
        private IEndPointRepository _endPoint;
        public IUserRepository User
        {
            get {
                if (_user == null) {
                    _user = new UserRepository(dbContext);
                }
                return _user;
            }
        }
        public IApplicationRepository Application
        {
            get {
                if(_application == null) {
                    _application = new ApplicationRepository(dbContext);
                }
                return _application;
            }
        }
        public IEndPointRepository EndPoint
        {
            get {
                if (_endPoint == null) {
                    _endPoint = new EndPointRepository(dbContext);
                }
                return _endPoint;
            }
        }
        public ITokenRepository Token
        {
            get {
                if(_token == null) {
                    _token = new TokenRepository(dbContext);
                }
                return _token;
            }
        }
        public IUserRoleRepository UserRole
        {
            get {
                if (_userRole == null) {
                    _userRole = new UserRoleRepository(dbContext);
                }
                return _userRole;
            }
        }
        public RepositoryWrapper(WebAppContext repositoryContext)
        {
            this.dbContext = repositoryContext;
        }
        public void Commit()
        {
            dbContext.SaveChanges();
        }
    }
}
