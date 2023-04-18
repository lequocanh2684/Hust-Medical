using System.ComponentModel.DataAnnotations.Schema;

namespace Patient_Health_Management_System.Models
{
    public class Patient
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get; set;}

        [Column("patient_id")]
        [Required]
        public string PatientId {get; set;}

        [Column("name")]
        [Required]
        public string Name {get; set;}

        [Column("age")]
        [Required]
        public int Age {get; set;}

        [Column("age_type")]
        [Required]
        public string AgeType {get; set;}

        [Column("gender")]
        [Required]
        public string Gender {get; set;}

        [Column("ethnic")]
        [Required]
        public string Ethnic {get; set;}

        [Column("medical_insurance_number")]
        public string MedicalInsuranceNumber {get; set;}

        [Column("date_of_birth")]
        [Required]
        public DateTime DateOfBirth {get; set;}

        [Column("address")]
        [Required]
        public string Address {get; set;}

        [Column("phone_number")]
        [Required]
        public string PhoneNumber {get; set;}

        [Column("email")]
        public string Email {get; set;}

        [Column("created_at")]
        public DateTime CreatedAt {get; set;}

        [Column("created_by")]
        [Required]
        public string CreatedBy {get; set;}

        [Column("updated_at")]
        public DateTime UpdatedAt {get; set;}

        [Column("updated_by")]
        public string? UpdatedBy {get; set;}

        [Column("is_deleted")]
        public bool IsDeleted {get; set;}

        [Column("deleted_at")]
        public DateTime DeletedAt {get; set;}

        [Column("deleted_by")]
        public string? DeletedBy {get; set;}

    }
}