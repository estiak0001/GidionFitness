using BlazorAdminTemplate.Application.DTOs.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface ITrainingClassesService
    {
        public Task<MemberClassesListAvailableClassesResponseDTO> GetMemberClassesListAsync(
                   int? weekNumber = null,
                   int? fromWeekNumber = null,
                   int? toWeekNumber = null,
                   DateTime? startDate = null,
                   DateTime? endDate = null,
                   string? orgSubGuid = null,
                   string? trainingClassTypesGUID = null);
        //public Task<MemberClassInfoResponseDTO> GetMemberClassInfoAsync(string classGUID);
        public Task<MemberClassesBookClassResponseDTO> PostMemberClassBookingAsync(string classGUID);

        public Task<MemberClassesCancelClassResponseDTO> PostMemberClassCancelAsync(string classGUID);
        public Task<MemberBookedClassesResponseDTO> GetMyClassesListAsync(
                   int? weekNumber = null,
                   int? fromWeekNumber = null,
                   int? toWeekNumber = null,
                   DateTime? startDate = null,
                   DateTime? endDate = null);
        public Task<StaffTrainingClassActionResponseDTO> StaffCreateSingleClass(StaffTrainingClassAddDTO staffTrainingClassAddDTO);
        public Task<StaffTrainingClassAddMultiResponseDTO> StaffCreateMultipleClasses(StaffTrainingClassAddMultiDTO staffTrainingClassAddMultiDTO);
        public Task<StaffTrainingClassActionResponseDTO> StaffEditClass(StaffTrainingClassEditDTO staffTrainingClassEditDTO);
        public Task<StaffTrainingClassActionResponseDTO> StaffDeleteClass(string trainingClassClassesGUID);
        public Task<StaffTrainingClassGetListResponseDTO> StaffGetClassListAsync(StaffTrainingClassGetDTO classFilter);

        public Task<StaffTrainingClassCancelResponseDTO> StaffCancelClassAsync(string classGUID);

        public Task<StaffTrainerListResponseDTO> GetTrainerListAsync();

    }
}
