namespace Patient_Health_Management_System.Domain.Forms
{
    public class PrescriptionForm
    {
        [Required]
        public string PrescriptionId { get; set; }
        [Required]
        public string PatientId { get; set; }
        public string? Note { get; set; }

        [Required]
        public List<MedicinePrescribed> MedicinePrescribed { get; set; }
    }
}
