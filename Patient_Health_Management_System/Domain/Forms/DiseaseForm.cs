namespace Patient_Health_Management_System.Domain.Forms
{
    public class DiseaseForm
    {
        [Required]
        public string DiseaseId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? GroupName { get; set; }
    }
}
