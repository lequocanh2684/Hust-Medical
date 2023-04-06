namespace Patient_Health_Management_System.Models
{
    public class Prescription
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("doctor_id")]
        public string DoctorId { get; set; }

        [BsonElement("patient_id")]
        public string PatientId { get; set; }

        [BsonElement("note")]
        public string Note { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("created_by")]
        public string CreatedBy { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("updated_by")]
        public string UpdatedBy { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; }

        [BsonElement("deleted_at")]
        public DateTime DeletedAt { get; set; }

        [BsonElement("deleted_by")]
        public string DeletedBy { get; set; }

        [BsonElement("medicines_prescription")]
        public List<MedicinePrescription> MedicinesPrescription { get; set; }
    }

    public class MedicinePrescription
    {
        [BsonElement("medicine_id")]
        public string MedicineId { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("unit")]
        public string Unit { get; set; }
    }
}
