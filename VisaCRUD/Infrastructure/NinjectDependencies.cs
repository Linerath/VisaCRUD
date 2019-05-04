using System;
using Ninject.Modules;
using System.Configuration;
using VisaCRUD.DAL.Interfaces;
using VisaCRUD.DAL.Repositories;
using VisaCRUD.Infrastructure.Interfaces;
using VisaCRUD.Security;

namespace VisaCRUD.Infrastructure
{
    public class NinjectDependencies : NinjectModule
    {
        public override void Load()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;

            Bind<IAuthProvider>().To<DbAuthProvider>();
            Bind<IVisasRepository>().To<VisasRepository>()
                .WithConstructorArgument(nameof(connectionString), connectionString);
            Bind<IUsersRepository>().To<UsersRepository>()
                .WithConstructorArgument(nameof(connectionString), connectionString);
        }
    }
}