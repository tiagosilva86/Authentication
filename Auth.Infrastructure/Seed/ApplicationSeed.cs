using Auth.Infrastructure.Data;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Seed
{
    class ApplicationSeed
    {
        public static void CreateApplication(WebAppContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            using (var transaction = context.Database.BeginTransaction()) {
                try {
                    context.Application.Add(new Application() {
                        Description = "Ideias de melhorias",
                    });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
