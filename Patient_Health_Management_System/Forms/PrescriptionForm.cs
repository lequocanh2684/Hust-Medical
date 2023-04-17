namespace Patient_Health_Management_System.Forms
{
    public class PrescriptionForm
    {
        [Required]
        public string PrescriptionId { get; set; }
        public string PatientId { get; set; }
        public string Note { get; set; }
        public List<MedicinesPrescription> MedicinesPrescription { get; set; }
    }
}
