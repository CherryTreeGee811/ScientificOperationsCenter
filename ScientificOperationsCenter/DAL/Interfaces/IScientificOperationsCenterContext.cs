using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Models;

namespace ScientificOperationsCenter.DAL.Interfaces
{
    public interface IScientificOperationsCenterContext
    {
        public DbSet<Temperatures> Temperatures { get; }


        public DbSet<RadiationMeasurements> RadiationMeasurements { get; }
    }
}
