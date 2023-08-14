namespace Hust_Medical.Domain.Models
{
    public class Medicine
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("medicine_id")]
        public string MedicineId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("unit")]
        public string Unit { get; set; }

        [BsonElement("how_to_use")]
        public string HowToUse { get; set; }

        [BsonElement("quantity_default")]
        public int QuantityDefault { get; set; }

        [BsonElement("group_name")]
        public string GroupName { get; set; }

        [BsonElement("import_price")]
        public int ImportPrice { get; set; }

        [BsonElement("selling_price")]
        public int SellingPrice { get; set; }

        [BsonElement("minimum_stock")]
        public int MinimumStock { get; set; }

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

        [BsonIgnore]
        public int BuyingQuantity { get; set; }
    }
}
