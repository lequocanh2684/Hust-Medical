namespace Patient_Health_Management_System.Domain.Models
{
    public class MedicinePrescribed
    {
        [BsonElement("medicine_id")]
        public string MedicineId { get; set; }

        [BsonElement("quantity")]
        public int BuyingQuantity { get; set; }

        [BsonElement("selling_price")]
        public int SellingPrice { get; set; }
    }
}