using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.DAL;
using ScientificOperationsCenter.Api.BusinessLogic.Interfaces;
using ScientificOperationsCenter.Api.BusinessLogic;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using ScientificOperationsCenter.Api.Mappers;


namespace ScientificOperationsCenter.Api.Extensions
{
    public static class RegistrationExtensions
    {
        public static void AddScientificOperationsCenterScopes(this IServiceCollection services)
        {
            services.AddScoped<IScientificOperationsCenterContext, ScientificOperationsCenterContext>();
            services.AddScoped<ITemperaturesRepository, TemperaturesRepository>();
            services.AddScoped<ITemperaturesService, TemperaturesService>();
            services.AddScoped<ITemperaturesMapper, TemperaturesMapper>();
            services.AddScoped<IRadiationMeasurementsRepository, RadiationMeasurementsRepository>();
            services.AddScoped<IRadiationMeasurementsService, RadiationMeasurementsService>();
            services.AddScoped<IRadiationMeasurementsMapper, RadiationMeasurementsMapper>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddHttpClient<ILoginRepository, LoginRepository>();
        }
    }
}
