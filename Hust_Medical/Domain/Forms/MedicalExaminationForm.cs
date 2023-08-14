namespace Hust_Medical.Domain.Forms
{
    public class MedicalExaminationForm
    {
        [Required]
        public string PatientId { get; set; } = default!;
        [Required]
        public string DiseaseName { get; set; } = default!;
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required] public string BloodPressure { get; set; } = default!;
        [Required] public string VascularIndex { get; set; } = default!;
        [Required] public string BodyTemperature { get; set; } = default!;
        [Required] public string BreathingRate { get; set; } = default!;
        public string? Note { get; set; }
    }
}
