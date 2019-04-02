using AutoMapper;
using Projeto.Infra.Data.Context;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Repositories;
using Projeto.Services.Mappings;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Projeto.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //executando..
            SimpleInjectorConfig();
            AutoMapperConfig();
        }

        private void AutoMapperConfig()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<EntityToModelMapping>();
                cfg.AddProfile<ModelToEntityMapping>();
            });
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader
            ("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader
                ("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader
                ("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                HttpContext.Current.Response.AddHeader
                ("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }

        private void SimpleInjectorConfig()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle
            = new AsyncScopedLifestyle();
            // Register your types, for instance using the scoped lifestyle:
            container.Register
            <IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register
            <IEstoqueRepository, EstoqueRepository>(Lifestyle.Scoped);
            container.Register
            <IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<DataContext>(Lifestyle.Scoped);
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers
            (GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver =
            new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
