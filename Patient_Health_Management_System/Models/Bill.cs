namespace Patient_Health_Management_System.Models
{
    public class Bill
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }

    public class MedicinesPrescription
    {
        [BsonElement("medicine_id")]
        public string MedicineId { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("unit")]
        public string Unit { get; set; }
    }
}