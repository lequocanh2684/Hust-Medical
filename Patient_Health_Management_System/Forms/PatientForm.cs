namespace Patient_Health_Management_System.Forms
{
    public class PatientForm
    {
        [Required]
        public string PatientId {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public int Age {get; set;}
        [Required]
        public string AgeType {get; set;}
        [Required]
        public string Gender {get; set;}
        [Required]
        public string Ethnic {get; set;}
        public string MedicalInsuranceNumber {get; set;}
        [Required]
        public DateTime DateOfBirth {get; set;}
        [Required]
        public string Address {get; set;}
        [Required]
        public string PhoneNumber {get; set;}
        public string Email {get; set;}
    }
}