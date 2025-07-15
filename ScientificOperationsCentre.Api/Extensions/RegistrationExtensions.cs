using ScientificOperationsCentre.Api.DAL.Interfaces;
using ScientificOperationsCentre.Api.DAL;
using ScientificOperationsCentre.Api.BusinessLogic.Interfaces;
using ScientificOperationsCentre.Api.BusinessLogic;
using ScientificOperationsCentre.Api.Mappers.Interfaces;
using ScientificOperationsCentre.Api.Mappers;


namespace ScientificOperationsCentre.Api.Extensions
{
    public static class RegistrationExtensions
    {
        public static void AddScientificOperationsCentreScopes(this IServiceCollection services)
        {
            services.AddScoped<IScientificOperationsCentreContext, ScientificOperationsCentreContext>();
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
