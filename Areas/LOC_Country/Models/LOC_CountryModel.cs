using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }

        [Required]
        public string? CountryName { get; set; }
        [Required]
        public string? CountryCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
