namespace Patient_Health_Management_System.Forms
{
    public class PrescriptionForm
    {
        public string PatientId { get; set; }
        public string Note { get; set; }
        public List<MedicinesPrescription> MedicinesPrescription { get; set; }
    }
}
