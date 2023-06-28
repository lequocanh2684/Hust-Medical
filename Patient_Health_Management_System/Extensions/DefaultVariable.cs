namespace Patient_Health_Management_System.Extensions
{
    public static class DefaultVariable
    {
        public const string UpdatedAt = "1/1/0001 12:00:00 AM";
        public const string DeletedAt = "1/1/0001 12:00:00 AM";
        public const string PaidAt = "1/1/0001 12:00:00 AM";

        public static readonly List<string> specialist = new List<string>
        {
            "Răng hàm mặt", "Tai mũi họng", "Nội soi"
        };
    }
}