namespace Patient_Health_Management_System.Domain.Models
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }

        [BsonElement("role_id")]
        public string RoleId { get; set; }

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
    }
}
