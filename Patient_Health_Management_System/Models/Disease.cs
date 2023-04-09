namespace Patient_Health_Management_System.Models
{
    public class Disease
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonElement("disease_id")]
        [BsonRequired]
        public string DiseaseId { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("group_name")]
        [BsonRequired]
        public string GroupName { get; set; }

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
