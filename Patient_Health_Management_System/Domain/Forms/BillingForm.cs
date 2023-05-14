namespace Patient_Health_Management_System.Domain.Forms
{
    public class BillingForm
    {
        public string BillId { get; set; }

        public string PatientId { get; set; }

        public string PrescriptionId { get; set; }

        public int TotalPrice { get; set; }
    }
}
