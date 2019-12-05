using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Utilities.IoC
{
    //Bu tool ne işe yarıcak 
    //öncelikle klasör Ioc içinde inversion of control değişimin kontrolu dependency ınjection da 
    //şimdi uygulamamızın service collection nesnesıne,sistmin servislerini
    //bu tool vasıtasıyla yonetıcez __S2v12
    //IServiceCollection Configure service de ki .netcore un kendı servıs collectionu 
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
