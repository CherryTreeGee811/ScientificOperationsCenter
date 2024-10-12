using ScientificOperationsCenter.ViewModels;

namespace ScientificOperationsCenter.Mappers.Interfaces
{
    public interface ITemperaturesMapper
    {
        public IEnumerable<TemperaturesTimeViewModel> GetTemperaturesForTheDay(DateOnly date);


        public IEnumerable<TemperaturesDateViewModel> GetTemperaturesForTheMonth(DateOnly date);


        public IEnumerable<TemperaturesDateViewModel> GetTemperaturesForTheYear(DateOnly date);
    }
}
