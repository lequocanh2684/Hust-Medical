namespace Hust_Medical.Domain.Models
{
    public class MedicinePrescribed
    {
        [BsonElement("medicine_id")]
        public string MedicineId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("buying_quantity")]
        public int BuyingQuantity { get; set; }

        [BsonElement("selling_price")]
        public int SellingPrice { get; set; }

        [BsonElement("how_to_use")]
        public string HowToUse { get; set; }
    }
}