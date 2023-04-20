namespace Patient_Health_Management_System.Models
{
    public class MedicinePrescribed
    {
        [BsonElement("medicine_id")]
        public string MedicineId { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("unit")]
        public string Unit { get; set; }

        [BsonElement("selling_price")]
        public int SellingPrice { get; set; }
    }
}