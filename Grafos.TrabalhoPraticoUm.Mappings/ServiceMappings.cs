using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Borders.UseCases;
using Grafos.TrabalhoPraticoUm.UseCases.Services;
using Grafos.TrabalhoPraticoUm.UseCases.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Grafos.TrabalhoPraticoUm.Mappings
{
    public static class ServiceMappings
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IGraphService, GraphService>();
            services.AddSingleton<IGraphVisualizerUseCase, GraphVisualizerUseCase>();
        }
    }
}
