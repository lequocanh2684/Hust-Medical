namespace Patient_Health_Management_System.Domain.Models
{
    public class Prescription
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("prescription_id")]
        public string PrescriptionId { get; set; }

        [BsonElement("patient_id")]
        public string PatientId { get; set; }

        [BsonElement("note")]
        public string? Note { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("created_by")]
        public string CreatedBy { get; set; }

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

        [BsonElement("medicine_ids")]
        public List<string> MedicineIds { get; set; }
    }
}
