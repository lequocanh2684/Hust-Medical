namespace Patient_Health_Management_System.Domain.Forms
{
    public class PatientForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Ethnic { get; set; }
        public string? MedicalInsuranceNumber { get; set; }
        [Required]
        public string IDNumber { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}