using System.ComponentModel.DataAnnotations;


namespace ScientificOperationsCenter.Api.Models
{
    public class Temperatures
    {
        [Key]
        public int Id { get; set; }


        public DateOnly Date { get; set; }


        public TimeOnly Time { get; set; }


        public int TemperatureCelcius { get; set; }
    }
}
