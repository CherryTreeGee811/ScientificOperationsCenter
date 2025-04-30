using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.DAL
{
    /// <summary>
    /// Represents the database context for the Scientific Operations Center,
    /// including the configuration of tables and initial records.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public class ScientificOperationsCenterContext(DbContextOptions options)
        : DbContext(options), IScientificOperationsCenterContext
    {
        /// <summary>
        /// Gets or sets the DbSet for temperature records.
        /// </summary>
        public virtual DbSet<Temperatures> Temperatures { get; set; }


        /// <summary>
        /// Gets or sets the DbSet for radiation measurement records.
        /// </summary>
        public virtual DbSet<RadiationMeasurements> RadiationMeasurements { get; set; }


        /// <summary>
        /// Configures the model and seeds initial data for the database.
        /// </summary>
        /// <param name="builder">The model builder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Temperatures>().HasData(
               new() { Id = 1, Date = new DateOnly(2024, 09, 02), Time = new TimeOnly(16, 00), TemperatureCelcius = 20 },
               new() { Id = 2, Date = new DateOnly(2024, 09, 05), Time = new TimeOnly(15, 00), TemperatureCelcius = 15 },
               new() { Id = 3, Date = new DateOnly(2024, 09, 07), Time = new TimeOnly(12, 00), TemperatureCelcius = -10 },
               new() { Id = 4, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(16, 00), TemperatureCelcius = -4 },
               new() { Id = 5, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(19, 00), TemperatureCelcius = 12 },
               new() { Id = 6, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(12, 00), TemperatureCelcius = 11 },
               new() { Id = 7, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 30), TemperatureCelcius = 12 },
               new() { Id = 8, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 00), TemperatureCelcius = 10 },
               new() { Id = 9, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(06, 00), TemperatureCelcius = 9 },
               new() { Id = 10, Date = new DateOnly(2024, 11, 02), Time = new TimeOnly(09, 10), TemperatureCelcius = -2 },
               new() { Id = 11, Date = new DateOnly(2024, 11, 03), Time = new TimeOnly(04, 30), TemperatureCelcius = 5 },
               new() { Id = 12, Date = new DateOnly(2024, 12, 21), Time = new TimeOnly(04, 50), TemperatureCelcius = 8 },
               new() { Id = 13, Date = new DateOnly(2024, 12, 10), Time = new TimeOnly(04, 10), TemperatureCelcius = 1 },
               new() { Id = 14, Date = new DateOnly(2024, 12, 08), Time = new TimeOnly(04, 30), TemperatureCelcius = 5 },
               new() { Id = 15, Date = new DateOnly(2025, 01, 03), Time = new TimeOnly(05, 30), TemperatureCelcius = 15 },
               new() { Id = 16, Date = new DateOnly(2025, 01, 05), Time = new TimeOnly(03, 30), TemperatureCelcius = 5 }
            );

            modelBuilder.Entity<RadiationMeasurements>().HasData(
               new() { Id = 1, Date = new DateOnly(2024, 09, 08), Time = new TimeOnly(16, 00), Milligrays = 100 },
               new() { Id = 2, Date = new DateOnly(2024, 09, 08), Time = new TimeOnly(15, 00), Milligrays = 140 },
               new() { Id = 3, Date = new DateOnly(2024, 09, 08), Time = new TimeOnly(12, 00), Milligrays = 162 },
               new() { Id = 4, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(16, 00), Milligrays = 100 },
               new() { Id = 5, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(19, 00), Milligrays = 120 },
               new() { Id = 6, Date = new DateOnly(2024, 10, 08), Time = new TimeOnly(12, 00), Milligrays = 190 },
               new() { Id = 7, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 30), Milligrays = 120 },
               new() { Id = 8, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(21, 00), Milligrays = 110 },
               new() { Id = 9, Date = new DateOnly(2024, 10, 09), Time = new TimeOnly(06, 00), Milligrays = 160 },
               new() { Id = 10, Date = new DateOnly(2024, 11, 02), Time = new TimeOnly(09, 10), Milligrays = 100 },
               new() { Id = 11, Date = new DateOnly(2024, 11, 03), Time = new TimeOnly(02, 30), Milligrays = 200 },
               new() { Id = 12, Date = new DateOnly(2024, 12, 01), Time = new TimeOnly(04, 20), Milligrays = 120 },
               new() { Id = 13, Date = new DateOnly(2024, 12, 02), Time = new TimeOnly(04, 10), Milligrays = 132 },
               new() { Id = 14, Date = new DateOnly(2024, 12, 05), Time = new TimeOnly(07, 30), Milligrays = 126 },
               new() { Id = 15, Date = new DateOnly(2025, 01, 03), Time = new TimeOnly(04, 30), Milligrays = 200 },
               new() { Id = 16, Date = new DateOnly(2025, 01, 05), Time = new TimeOnly(04, 30), Milligrays = 200 }
           );
        }
    }
}
