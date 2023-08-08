namespace Hust_Medical.Domain.Models
{
    public class Billing
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("bill_id")]
        public string BillId { get; set; }

        [BsonElement("patient_id")]
        public string PatientId { get; set; }

        [BsonElement("prescription_id")]
        public string PrescriptionId { get; set; }

        [BsonElement("payment_method")]
        public string? PaymentMethod { get; set; }

        [BsonElement("is_paid")]
        public bool IsPaid { get; set; }

        [BsonElement("paid_at")]
        public DateTime PaidAt { get; set; }

        [BsonElement("paid_by")]
        public string? PaidBy { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("created_by")]
        public string? CreatedBy { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("updated_by")]
        public string? UpdatedBy { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; }

        [BsonElement("deleted_at")]
        public DateTime DeletedAt { get; set; }

        [BsonElement("deleted_by")]
        public string? DeletedBy { get; set; }
    }
}