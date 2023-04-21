namespace Patient_Health_Management_System.Models
{
    public class MedicalExamination
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("medical_examination_id")]
        public string MedicalExaminationId { get; set; }

        [BsonElement("patient_id")]
        public string PatientId { get; set; }

        [BsonElement("disease_id")]
        public string DiseaseId { get; set; }

        [BsonElement("height")]
        public int Height {get; set;}

        [BsonElement("weight")]
        public int Weight {get; set;}

        [BsonElement("blood_pressure")]
        public string? BloodPressure {get; set;}

        [BsonElement("vascular_index")]
        public string? VascularIndex {get; set;}

        [BsonElement("body_temperature")]
        public string? BodyTemperature {get; set;}

        [BsonElement("breathing_rate")]
        public string? BreathingRate {get; set;}

        [BsonElement("note")]
        public string? Note { get; set; }

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