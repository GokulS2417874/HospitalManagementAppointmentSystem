using Domain.Models;
using Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IPrescriptionRepository
    {
        Task<(bool Success, string Message)> AddPrescriptionAsync(PrescriptionDto dto);
       // Task<(bool Success, string Message)> UpdatePrescriptionAsync(int id, PrescriptionDto dto);
        Task<(bool Success, string Message)> DeletePrescriptionAsync(int id);
        Task<List<Prescription>> GetPrescriptionsByPatientAsync(int patientId);
        Task<PrescriptionDto?> GetPrescriptionByIdAsync(int id);
    }


}
