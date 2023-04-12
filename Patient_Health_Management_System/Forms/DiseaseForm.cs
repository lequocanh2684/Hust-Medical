using System.ComponentModel.DataAnnotations;

namespace Patient_Health_Management_System.Forms
{
    public class DiseaseForm
    {
        [Required]
        public string DiseaseId { get; set; }
        [Required]
        public string Name { get; set; }
        public string GroupName { get; set; }
    }
}
