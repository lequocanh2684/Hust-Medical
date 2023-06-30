namespace Patient_Health_Management_System.Domain.Forms
{
    public class BillingForm
    {
        public string PatientId { get; set; }

        public string PrescriptionId { get; set; }

        public string? PaymentMethod { get; set; }

        public bool IsPaid { get; set; }

    }
}
