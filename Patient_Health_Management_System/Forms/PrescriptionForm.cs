namespace Patient_Health_Management_System.Forms
{
    public class PrescriptionForm
    {
        [Required]
        public string PrescriptionId { get; set; }
        [Required]
        public string PatientId { get; set; }
        public string? Note { get; set; }
        [Required]
        public List<string> MedicineIds { get; set; }
    }
}
