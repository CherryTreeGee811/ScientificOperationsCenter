using System.ComponentModel.DataAnnotations;


namespace ScientificOperationsCenter.Models
{
    public class RadiationMeasurements
    {
        public int Id { get; set; }


        public DateOnly Date { get; set; }


        public TimeOnly Time { get; set; }


        public int Milligrays { get; set; }
    }
}
