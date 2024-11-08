using System.ComponentModel.DataAnnotations;


namespace ScientificOperationsCenter.Models
{
    public class Temperatures
    {
        public int Id { get; set; }


        public DateOnly Date { get; set; }


        public TimeOnly Time { get; set; }


        public int TemperatureCelcius { get; set; }
    }
}
