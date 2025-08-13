using System.ComponentModel.DataAnnotations;

namespace CollegeProject.Models.ViewModels
{
    public class WareHouseVM
    {
        [MaxLength(15)]
        public string? WareHouseID { get; set; }

        [Required(ErrorMessage = "WareHouseName is Required!")]
        [MaxLength(30)]
        public string? WareHouseName { get; set; }

        [Required(ErrorMessage = "WareHouseAdress is Required!")]
        [MaxLength(100)]
        public string? WareHouseAddress { get; set; }
    }
}
