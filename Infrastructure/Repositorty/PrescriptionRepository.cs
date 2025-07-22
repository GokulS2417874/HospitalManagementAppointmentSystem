using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Enum;

namespace Infrastructure.Repositorty
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly AppDbContext _context;

        public PrescriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message)> AddPrescriptionAsync(PrescriptionDto dto)
        {

            var prescription = new Prescription
            {
                AppointmentId = dto.AppointmentId,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Instructions = dto.Instructions,
                Medicines = dto.Medicines.Select(m => new PrescriptionMedicine
                {
                    MedicineType = m.MedicineType,
                    Dosages = m.Dosages,
                     ScheduleTime =m.ScheduleTime
                }).ToList(),
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return (true, "Prescription added successfully.");
        }

        //public async Task<(bool Success, string Message)> UpdatePrescriptionAsync(int id, PrescriptionDto dto)
        //{
        //    var prescription = await _context.Prescriptions.FindAsync(id);
        //    if (prescription == null)
        //        return (false, "Prescription not found.");

        //    prescription.Medication = dto.Medication;
        //    prescription.Dosage = dto.Dosage;
        //    prescription.Instructions = dto.Instructions;
        //    prescription.ScheduleTime = dto.ScheduleTime;

        //    await _context.SaveChangesAsync();
        //    return (true, "Prescription updated successfully.");
        //}

        public async Task<(bool Success, string Message)> DeletePrescriptionAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return (false, "Prescription not found.");

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return (true, "Prescription deleted successfully.");
        }

        public async Task<List<Prescription>> GetPrescriptionsByPatientAsync(int patientId)
        {
            return await _context.Prescriptions
            .Where(p => p.PatientId == patientId)
            .Include(p => p.Patient)
            .ToListAsync();
        }

        public async Task<PrescriptionDto?> GetPrescriptionByIdAsync(int id)
        {
            var pres = await _context.Prescriptions.Include(p => p.Medicines).FirstOrDefaultAsync(p => p.PrescriptionId == id);
            if (pres == null)
                return null;

            //var meds = System.Enum.GetValues(typeof(MedicineType))
            //    .Cast<MedicineType>()
            //    .Where(flag => flag != MedicineType.none && pres.Medication.HasFlag(flag))
            //    .Select(m => m)
            //    .ToList();

            return new PrescriptionDto
            {
                AppointmentId = pres.AppointmentId,
                DoctorId = pres.DoctorId,
                PatientId = pres.PatientId,
                Instructions = pres.Instructions,
                Medicines = pres.Medicines.Select(m => new PrescriptionMedicineDto
                {
                    MedicineType = m.MedicineType,
                    Dosages = m.Dosages, 
                    ScheduleTime = m.ScheduleTime
                }).ToList(),

            };
        }
    }
}
