using BlazorAdminTemplate.Application.DTOs.Chips;
using BlazorAdminTemplate.Application.DTOs.Classes;
using BlazorAdminTemplate.Application.DTOs.ClassTypes;
using BlazorAdminTemplate.Application.DTOs.Contracts;
using BlazorAdminTemplate.Application.DTOs.Location;
using BlazorAdminTemplate.Application.DTOs.Members;
using BlazorAdminTemplate.Application.DTOs.Memberships;
using BlazorAdminTemplate.Application.DTOs.Notes;
using BlazorAdminTemplate.Application.DTOs.Payment;
using BlazorAdminTemplate.Application.DTOs.Training;
using BlazorAdminTemplate.Application.DTOs.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IStaffManagementService
    {
        public Task<MemberBookedClassesResponseDTO> GetMembersBookedClassesAsync(string memberGuid, int? weekNumber = null, int? fromWeekNumber = null, int? toWeekNumber = null, DateTime? startDate = null, DateTime? endDate = null);

        public Task<MemberAccessLogsTotalResponseDTO> GetTotalMemberAccessLogsAsync(string memberGuid);
        public Task<AccessLogsListReponseDTO> GetMemberAccessLogsAsync(string memberGuid, int page, int pageSize);
        public Task<MemberPaymentMembershipInfoResponseDTO> StaffGetMembershipInfo(string memberGuid);
        public Task<MemberPaymentListReponseDTO> StaffGetMemberPaymentsAsync(string memberGuid, int page, int pageSize);
        public Task<StaffShowSignedMemberContractResponseDTO> StaffGetSignedMemberContracts(string memberGuid, int page, int pageSize);

        public Task<StaffTrainingClassLocationListResponseDTO> StaffGetClassLocationsAsync(int page, int pageSize);
        public Task<StaffTrainingClassNewLocationResponseDTO> StaffAddClassLocationAsync(AddNewLocationDTO addNewLocationDTO);
        public Task<StaffTrainingClassUpdateResponseDTO> StaffUpdateClassLocationAsync(StaffTrainingClassLocationDTO locationDTO);
        public Task<StaffTrainingClassDeleteResponseDTO> StaffDeleteClassLocationAsync(string locationGuid);

        public Task<StaffTrainingClassTypeListResponseDTO> StaffGetClassTypesAsync(int page, int pageSize);
        public Task<StaffTrainingClassTypeAddResponseDTO> StaffAddClassTypeAsync(AddNewTypeDTO addNewTypeDTO);
        public Task<StaffTrainingClassTypeUpdateResponseDTO> StaffUpdateClassTypeAsync(StaffTrainingClassTypeDTO typeDTO);
        public Task<StaffTrainingClassTypeDeleteResponseDTO> StaffDeleteClassTypeAsync(string typeGuid);

        public Task<StaffChangeMemberPasswordResponseDto> StaffChangeMemberPasswordAsync(StaffChangeMemberPasswordDto staffChangeMemberPasswordDto);
        public Task<StaffCancelMembershipCheckResponseDTO> StaffCancelMembershipCheckPolicyAsync(StaffCancelMembershipCheckDTO staffCancelMembershipCheckDTO);
        public Task<StaffCancelMembershipResponseDTO> StaffCancelMembershipAsync(StaffCancelMembershipDTO staffCancelMembershipDTO);

        public Task<MemberPaymentCardInfoResponseDTO> StaffGetMemberPaymentCardsAsync(string memberGuid);

        public Task<StaffMemberNotesResponseDTO> StaffGetMemberNotesAsync(StaffMemberNotesRequestDTO staffMemberNotesRequestDTO);

        public Task<StaffMemberNoteActionsResponseDTO> StaffAddMemberNoteAsync(StaffAddMemberNoteDTO staffAddMemberNoteDTO);
        public Task<StaffMemberNoteActionsResponseDTO> StaffEditMemberNoteAsync(StaffEditMemberNoteDTO staffEditMemberNoteDTO);

        public Task<StaffMemberNoteActionsResponseDTO> StaffDeleteMemberNoteAsync(string memberNotesGuid);

        public Task<StaffMemberNoteResponseDTO> StaffGetMemberNoteAsync(string memberNotesGuid);

        public Task<StaffGetMemberChipResponseDTO> StaffGetMemberChipsAsync(StaffGetMemberChipDTO staffGetMemberChipDTO);
        public Task<StaffMemberChipActionResponseDTO> StaffCreateMemberChipAsync(StaffMemberChipCreateDTO staffMemberChipCreateDTO);
        public Task<StaffMemberChipActionResponseDTO> StaffEditMemberChipAsync(StaffMemberChipEditDTO staffMemberChipEditDTO);
        public Task<StaffMemberChipDeleteResponseDTO> StaffDeleteMemberChipAsync(string memberChipGuid);
        public Task<StaffGetChipTypeResponseDTO> StaffGetChipTypesAsync();
        public Task<StaffHoldMemberSubscriptionResponseDTO> StaffHoldMemberSubscriptionAsync(StaffHoldMemberSubscriptionDTO staffHoldMemberSubscriptionDTO);
        public Task<StaffMemberMembershipChangeResponseDTO> StaffChangeMemberMembershipAsync(StaffMemberMembershipChangeDTO staffMemberMembershipChangeDTO);
        public Task<StaffAccessControllerGroupResponseDTO> StaffGetAccessControllerGroupAsync(string groupGuid);
        public Task<StaffAccessControllerGroupListResponseDTO> StaffGetAccessControllerGroupListAsync(StaffAccessControllerGroupDTO staffAccessControllerGroupDTO);
        public Task<StaffAccessControllerGroupAddResponseDTO> StaffGetAccessControllerGroupAddAsync(StaffAccessControllerGroupAddDTO addDTO);
        public Task<StaffAccessControllerGroupEditResponseDTO> StaffGetAccessControllerGroupEditAsync(StaffAccessControllerGroupEditDTO editDTO);
        public Task<StaffAccessControllerGroupDeleteResponseDTO> StaffGetAccessControllerGroupDeleteAsync(string groupGuid);
        public Task<AccessControllerLevelsResponseDTO> StaffGetAccessControllerLevelsAsync();

        public Task<StaffScheduleDayConfigResponseDTO> StaffGetScheduleDayConfigsAsync(string accessGroupsGUID);
        public Task<StaffScheduleDayConfigToggleResponseDTO> StaffToggleScheduleDayConfigAsync(StaffScheduleDayConfigToggleDTO toggleDTO);
        public Task<StaffScheduleTimeSlotResponseDTO> StaffGetScheduleTimeSlotAsync(string dayConfigGuid);
        public Task<StaffScheduleTimeSlotListResponseDTO> StaffGetScheduleTimeSlotsAsync(string dayConfigGuid);
        public Task<StaffScheduleTimeSlotCreateResponseDTO> StaffCreateScheduleTimeSlotAsync(StaffScheduleTimeSlotCreateDTO createDTO);
        public Task<StaffScheduleTimeSlotUpdateResponseDTO> StaffUpdateScheduleTimeSlotAsync(StaffScheduleTimeSlotUpdateDTO updateDTO);
        public Task<StaffScheduleTimeSlotDeleteResponseDTO> StaffDeleteScheduleTimeSlotAsync(string timeSlotGuid);

        public Task<StaffScheduleExceptionListResponseDTO> StaffGetScheduleExceptionsAsync(string AccessGroupGuid);
        public Task<StaffScheduleExceptionResponseDTO> StaffGetScheduleExceptionAsync(string exceptionGuid);
        public Task<StaffScheduleExceptionCreateResponseDTO> StaffCreateScheduleExceptionAsync(StaffScheduleExceptionCreateDTO createDTO);
        public Task<StaffScheduleExceptionEditResponseDTO> StaffEditScheduleExceptionAsync(StaffScheduleExceptionEditDTO editDTO);
        public Task<StaffScheduleExceptionDeleteResponseDTO> StaffDeleteScheduleExceptionAsync(string exceptionGuid);

    }
}
