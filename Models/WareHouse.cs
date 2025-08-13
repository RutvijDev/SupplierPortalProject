using System.ComponentModel.DataAnnotations;

namespace CollegeProject.Models
{
    public class WareHouse
    {
        [Key]
        [Required]
        [MaxLength(15)]
        public string? WareHouseID { get; set; }

        [Required]
        [MaxLength(30)]
        public string? WareHouseName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? WareHouseAddress { get; set; }
    }
}
