
namespace Patient_Health_Management_System.Forms
{
    public class MedicalExaminationForm
    {
        [Required]
        public string MedicalExaminationId { get; set; }

        public string PatientId { get; set; }
        
        public string DiseaseId { get; set; }
 
        public int Height {get; set;}

        public int Weight {get; set;}

        public string BloodPressure {get; set;}

        public string VascularIndex {get; set;}

        public string BodyTemperature {get; set;}

        public string BreathingRate {get; set;}

        public string Note { get; set; }
    }
}
