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

        public async Task<List<Prescription>> GetPrescriptionsByPatientAsync(int AppointmentId)
        {
            return await _context.Prescriptions
                .Where(p => p.AppointmentId == AppointmentId)
                .Include(p => p.Patient)
                .Include(p => p.Medicines)
                .AsNoTracking()  // Add this to prevent circular reference tracking
                .ToListAsync();
        }

        public async Task<List<PrescriptionDto>> GetPrescriptionByIdAsync(int AppointmentId)
        {
            var prescriptions = await _context.Prescriptions
                .Where(p => p.AppointmentId == AppointmentId)
                .Include(p => p.Patient)
                .Include(p => p.Medicines)
                .AsNoTracking()
                .ToListAsync();

            return prescriptions.Select(p => new PrescriptionDto
            {
                AppointmentId = p.AppointmentId,
                PatientId = p.PatientId,
                DoctorId = p.DoctorId,
                Instructions = p.Instructions,
                Medicines = p.Medicines.Select(m => new PrescriptionMedicineDto
                {
                    PrescriptionId = m.PrescriptionId,
                    MedicineType = m.MedicineType,
                    Dosages = m.Dosages,
                    ScheduleTime = m.ScheduleTime
                }).ToList()
            }).ToList();
        }
    }
}
