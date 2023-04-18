using Patient_Health_Management_System.Data;
namespace Patient_Health_Management_System.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly PatietHealthDbContext _context;

        public PatientRepo(PatietHealthDbContext context)
        {
            _context = context;
        }
        public async Task CreatePatient(Patient patient)
        {
           try
            {
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Patient?> GetPatientById(Guid id)
        {
            try
            {
                return await _context.Patients.Where(p => p.Id.Equals(id) && p.IsDeleted == false).FirstOrDefaultAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Patient>> GetPatients()
        {
            try
            {
                return await _context.Patients.Where(p => p.IsDeleted == false).ToListAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task ModifyPatientById(Patient patient)
        {
            try
            {
                _context.Patients.Update(patient);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
