namespace Patient_Health_Management_System.Domain.Models
{
    public class Patient
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("patient_id")]
        public string PatientId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("ethnic")]
        public string Ethnic { get; set; }

        [BsonElement("medical_insurance_number")]
        public string? MedicalInsuranceNumber { get; set; }

        [BsonElement("id_number")]
        public string IDNumber { get; set; }

        [BsonElement("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("phone_number")]
        public string? PhoneNumber { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

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
    }
}