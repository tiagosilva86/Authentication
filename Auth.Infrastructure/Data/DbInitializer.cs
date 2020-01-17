using Auth.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(WebAppContext context)
        {
            var db = context.Database.EnsureCreated();
            if (context.User.Any())
                return;
            ApplicationSeed.CreateApplication(context);
            RoleSeed.CreateRole(context);
            UserSeed.CreateUser(context);
           
        }
    }
}