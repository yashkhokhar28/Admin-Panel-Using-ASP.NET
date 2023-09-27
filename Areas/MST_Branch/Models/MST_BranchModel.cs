using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Areas.MST_Branch.Models
{
    public class MST_BranchModel
    {
        public int BranchID { get; set; }
        [Required]
        [DisplayName("Branch Name")]
        public string? BranchName { get; set; }
        [Required]
        [DisplayName("Branch Code")]
        public string? BranchCode { get; set; }
        public string? Created { get; set; }
        public string? Modified { get; set; }

    }
    public class MST_BranchFilterModel
    {
        public int? BranchID { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
    }
}
