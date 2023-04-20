namespace Patient_Health_Management_System.Forms
{
    public class BillingForm
    {
        public string BillId { get; set; }

        public string PatientId { get; set; }

        public string PrescriptionId { get; set; }

        public List<MedicinePrescribed> MedicinePrescribed { get; set; }

        public int TotalPrice { get; set; }
    }
}
