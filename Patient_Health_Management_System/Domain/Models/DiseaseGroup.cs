﻿namespace Patient_Health_Management_System.Domain.Models
{
    public class DiseaseGroup
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("disease_group_name")]
        public string Name { get; set; }

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
