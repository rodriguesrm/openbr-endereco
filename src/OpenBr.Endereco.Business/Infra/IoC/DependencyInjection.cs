﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OpenBr.Endereco.Business.Infra.MongoDb;
using OpenBr.Endereco.Business.Repositories;
using OpenBr.Endereco.Business.Infra.Config;

namespace OpenBr.Endereco.Business.Infra.IoC
{

    /// <summary>
    /// Injetor de serviços de dependência
    /// </summary>
    public static class DependencyInjection
    {

        /// <summary>
        /// Registrador de serviço de injeção de dependência
        /// </summary>
        /// <param name="services">Coleção de serviços de injeção</param>
        /// <param name="configuration">Coleção de configurações</param>
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {

            // Configurações
            services.AddScoped<IConfigurationBuilder, ConfigurationBuilder>();
            services.Configure<ApplicationConfig>(configuration.Bind);

            //MongoDB
            services.AddScoped<MongoDatabaseProvider>();
            services.AddScoped(s => s.GetService<MongoDatabaseProvider>().GetDatabase());

            // DbCreator
            services.AddScoped<IDbDocumentCollectionCreator, MongoCollectionCreator>();
            services.RegisterAllTypes<IDocumentCollectionCreator>(ServiceLifetime.Scoped, typeof(MongoCollectionCreator).Assembly);

            // Repositorios
            services.AddScoped<ICepRepository, CepRepository>();
            services.AddScoped<IBuscaRepository, BuscaRepository>();

            return services;

        }


    }
}
