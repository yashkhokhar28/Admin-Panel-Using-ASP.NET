using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }

        [Required]
        [DisplayName("City Name")]
        public string? CityName { get; set; }
        [Required]
        [DisplayName("City Code")]
        public string? CityCode { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
