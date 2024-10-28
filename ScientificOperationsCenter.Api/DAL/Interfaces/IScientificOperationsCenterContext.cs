using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.Models;

namespace ScientificOperationsCenter.Api.DAL.Interfaces
{
    public interface IScientificOperationsCenterContext
    {
        public DbSet<Temperatures> Temperatures { get; }


        public DbSet<RadiationMeasurements> RadiationMeasurements { get; }
    }
}
