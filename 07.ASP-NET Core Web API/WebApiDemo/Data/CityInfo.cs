using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Data
{
    public class CityInfo
    {
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Range(0, long.MaxValue)]
        public long Population { get; set; }

        [Range(-100, +100)]
        public decimal Temperature { get; set; }
    }
}
