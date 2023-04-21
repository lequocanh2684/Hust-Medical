
namespace Patient_Health_Management_System.Forms
{
    public class MedicineForm
    {
        [Required]
        public string MedicineId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Unit { get; set; }
        public string? HowToUse { get; set; }
        public int QuantityDefault { get; set; }
        [Required]
        public string GroupName { get; set; }
        public int ImportPrice { get; set; }
        public int SellingPrice { get; set; }
        public int MinimumStock { get; set; }
    }
}