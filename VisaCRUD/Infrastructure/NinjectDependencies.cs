using Ninject.Modules;
using System;
using System.Configuration;
using VisaCRUD.DAL.Interfaces;
using VisaCRUD.DAL.Repositories;

namespace VisaCRUD.Infrastructure
{
    public class NinjectDependencies : NinjectModule
    {
        public override void Load()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;

            Bind<IVisasRepository>().To<VisasRepository>()
                .WithConstructorArgument(nameof(connectionString), connectionString);
        }
    }
}