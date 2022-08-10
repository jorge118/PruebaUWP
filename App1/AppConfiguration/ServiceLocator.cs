using App1.AutoMapper;
using App1.Services;
using App1.Services.DataStores.Backbone;
using App1.Services.DataStores.Finanzas;
using App1.Services.Infrastructure;
using Autofac;
using AutoMapper;
using Reyma.Utils.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace App1.AppConfiguration
{
    public static class ServiceLocator
    {


        public static IContainer ConfigureServices()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HttpClientService>().AsSelf()
                      .As<IHttpClientService>().SingleInstance();

            builder.RegisterType<HttpClientBuilderStandar>().As<IHttpClientBuilder>().SingleInstance();
            builder.RegisterType<HttpResponseValidatorStandar>().As<IHttpResponseValidator>().SingleInstance();
            builder.RegisterType<PagedGetUriBuilderStandar>().As<IPagedGetUriBuilder>().SingleInstance();
            builder.RegisterType<HttpContentBuilderStandar>().As<IHttpContentBuilder>().SingleInstance();
            builder.RegisterType<HttpResponseDeserializerStandar>().As<IHttpResponseDeserializer>().SingleInstance();


            builder.RegisterType<DialogService>()
                .As<IDialogService>()
                .SingleInstance();

            builder.RegisterType<FilePickerService>().As<IFilePickerService>().SingleInstance();

            builder.RegisterType<EmpresaDataStore>();
            builder.RegisterType<TurnoDataStore>();

            builder.RegisterAssemblyTypes().AssignableTo(typeof(AutoMapperProfile));

            builder.RegisterType<HttpClientService>().AsSelf();

            builder.Register<IMapper>(c => new Mapper(c.Resolve<IConfigurationProvider>(), c.Resolve)).InstancePerDependency();



            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                    cfg.AddProfile(profile);

            })).AsSelf().SingleInstance();

            builder.Register(c => new HttpClientService()).AsSelf().SingleInstance();

            builder.Register(c => AutoMapperProfile.CreateMapper()).As<IMapper>().InstancePerLifetimeScope();


            builder.Register(c => c.Resolve<HttpClientService>()).As<IHttpClientService>().InstancePerLifetimeScope();

            return builder.Build();
        }


      

    }
}
