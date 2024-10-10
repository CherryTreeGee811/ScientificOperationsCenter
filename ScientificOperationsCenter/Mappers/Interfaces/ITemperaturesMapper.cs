using ScientificOperationsCenter.ViewModels;

namespace ScientificOperationsCenter.Mappers.Interfaces
{
    public interface ITemperaturesMapper
    {
        public IEnumerable<TemperatureTimeViewModel> GetTemperaturesForTheDay(DateOnly date);


        public IEnumerable<TemperatureDateViewModel> GetTemperaturesForTheMonth(DateOnly date);


        public IEnumerable<TemperatureDateViewModel> GetTemperaturesForTheYear(DateOnly date);
    }
}
