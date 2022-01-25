using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.UseCases.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Grafos.TrabalhoPraticoUm.Mappings
{
    public static class ServiceMappings
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IGraphService, GraphService>();
            services.AddSingleton<IMemoryService, MemoryService>();
        }
    }
}
