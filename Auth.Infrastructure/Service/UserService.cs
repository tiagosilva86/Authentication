using Auth.Infrastructure.Repository;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Service
{
    public class UserService : IUserService
    {

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserService
    {
        //Methods you want to expose
        IEnumerable<User> GetAll();
 
    }
}
