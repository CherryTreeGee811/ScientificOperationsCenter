using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.DAL
{
    public class ScientificOperationsCenterContext : DbContext, IScientificOperationsCenterContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Temperatures>().HasData(
               new Temperatures { Id = 1, Date = new DateOnly(2024, 09, 02), Time = new TimeOnly(16, 00), TemperatureCelcius = 20 },
               new Temperatures { Id = 2, Date = new DateOnly(2024, 09, 05), Time = new TimeOnly(15, 00), TemperatureCelcius = 15 },
               new Temperatures { Id = 3, Date = new DateOnly(2024, 09, 07), Time = new TimeOnly(12, 00), TemperatureCelcius = -10 },
               new Temperatures { Id = 4, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(16, 00), TemperatureCelcius = -4 },
               new Temperatures { Id = 5, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(19, 00), TemperatureCelcius = 12 },
               new Temperatures { Id = 6, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(12, 00), TemperatureCelcius = 11 },
               new Temperatures { Id = 7, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 30), TemperatureCelcius = 12 },
               new Temperatures { Id = 8, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 00), TemperatureCelcius = 10 },
               new Temperatures { Id = 9, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(06, 00), TemperatureCelcius = 9 },
               new Temperatures { Id = 10, Date = new DateOnly(2024, 11, 02), Time = new TimeOnly(09, 10), TemperatureCelcius = -2 },
               new Temperatures { Id = 11, Date = new DateOnly(2024, 11, 03), Time = new TimeOnly(04, 30), TemperatureCelcius = 5 },
               new Temperatures { Id = 12, Date = new DateOnly(2024, 12, 21), Time = new TimeOnly(04, 50), TemperatureCelcius = 8 },
               new Temperatures { Id = 13, Date = new DateOnly(2024, 12, 10), Time = new TimeOnly(04, 10), TemperatureCelcius = 1 },
               new Temperatures { Id = 14, Date = new DateOnly(2024, 12, 08), Time = new TimeOnly(04, 30), TemperatureCelcius = 5 },
               new Temperatures { Id = 15, Date = new DateOnly(2025, 01, 03), Time = new TimeOnly(05, 30), TemperatureCelcius = 15 },
               new Temperatures { Id = 16, Date = new DateOnly(2025, 01, 05), Time = new TimeOnly(03, 30), TemperatureCelcius = 5 }
            );

            builder.Entity<RadiationMeasurements>().HasData(
               new RadiationMeasurements { Id = 1, Date = new DateOnly(2024, 09, 08), Time = new TimeOnly(16, 00), Milligrays = 100 },
               new RadiationMeasurements { Id = 2, Date = new DateOnly(2024, 09, 08), Time = new TimeOnly(15, 00), Milligrays = 140 },
               new RadiationMeasurements { Id = 3, Date = new DateOnly(2024, 09, 08), Time = new TimeOnly(12, 00), Milligrays = 162 },
               new RadiationMeasurements { Id = 4, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(16, 00), Milligrays = 100 },
               new RadiationMeasurements { Id = 5, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(19, 00), Milligrays = 120 },
               new RadiationMeasurements { Id = 6, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(12, 00), Milligrays = 190 },
               new RadiationMeasurements { Id = 7, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 30), Milligrays = 120 },
               new RadiationMeasurements { Id = 8, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 00), Milligrays = 110 },
               new RadiationMeasurements { Id = 9, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(06, 00), Milligrays = 160 },
               new RadiationMeasurements { Id = 10, Date = new DateOnly(2024, 11, 02), Time = new TimeOnly(09, 10), Milligrays = 100 },
               new RadiationMeasurements { Id = 11, Date = new DateOnly(2024, 11, 03), Time = new TimeOnly(02, 30), Milligrays = 200 },
               new RadiationMeasurements { Id = 12, Date = new DateOnly(2024, 12, 01), Time = new TimeOnly(04, 20), Milligrays = 120 },
               new RadiationMeasurements { Id = 13, Date = new DateOnly(2024, 12, 02), Time = new TimeOnly(04, 10), Milligrays = 132 },
               new RadiationMeasurements { Id = 14, Date = new DateOnly(2024, 12, 05), Time = new TimeOnly(07, 30), Milligrays = 126 },
               new RadiationMeasurements { Id = 15, Date = new DateOnly(2025, 01, 03), Time = new TimeOnly(04, 30), Milligrays = 200 },
               new RadiationMeasurements { Id = 16, Date = new DateOnly(2025, 01, 05), Time = new TimeOnly(04, 30), Milligrays = 200 }
           );
        }


        public DbSet<Temperatures> Temperatures { get; set; }


        public DbSet<RadiationMeasurements> RadiationMeasurements { get; set; }


        public ScientificOperationsCenterContext(DbContextOptions options) : base(options)
        { }
    }
}
