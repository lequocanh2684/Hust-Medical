namespace Hust_Medical.Domain.Forms
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
