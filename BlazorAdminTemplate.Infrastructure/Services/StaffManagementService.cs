using BlazorAdminTemplate.Application.DTOs.Access;
using BlazorAdminTemplate.Application.DTOs.Chips;
using BlazorAdminTemplate.Application.DTOs.Classes;
using BlazorAdminTemplate.Application.DTOs.ClassTypes;
using BlazorAdminTemplate.Application.DTOs.Contracts;
using BlazorAdminTemplate.Application.DTOs.Location;
using BlazorAdminTemplate.Application.DTOs.Members;
using BlazorAdminTemplate.Application.DTOs.Memberships;
using BlazorAdminTemplate.Application.DTOs.Notes;
using BlazorAdminTemplate.Application.DTOs.Payment;
using BlazorAdminTemplate.Application.DTOs.Templates;
using BlazorAdminTemplate.Application.DTOs.Training;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using Flurl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class StaffManagementService : IStaffManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private readonly ApiConfiguration _apiConfiguration;
        private const string StaffGetMemberClassesListEndpoint = "/StaffMemberManagement/member-booking-list-member-class";
        private const string StaffMemberPaymentListEndpoint = "/StaffMemberManagement/staff-member-payment-list";
        private const string StaffMembershipInfoEndpoint = "/StaffMemberManagement/staff-member-membership-info";
        private const string MemberTotalAccessLogsEndpoint = "/StaffMemberManagement/member-access-logs-total";
        private const string MemberAccessLogsListEndpoint = "/StaffMemberManagement/member-access-logs-list";
        private const string StaffShowSignedMemberContractEndpoint = "/StaffMemberManagement/member-contract-show-signed-contract";
        private const string StaffGetClassLocationsEndpoint = "/StaffTrainingclassLocation/TrainingclassLocationList";
        private const string StaffAddClassLocationEndpoint = "/StaffTrainingclassLocation/TrainingclassLocationAdd";
        private const string StaffUpdateClassLocationEndpoint = "/StaffTrainingclassLocation/TrainingclassLocationEdit";
        private const string StaffDeleteClassLocationEndpoint = "/StaffTrainingclassLocation/TrainingclassLocationDelete";
        private const string StaffGetClassTypesEndpoint = "/StaffTrainingclassTypes/TrainingclassTypeList";
        private const string StaffAddClassTypeEndpoint = "/StaffTrainingclassTypes/TrainingclassTypeAdd";
        private const string StaffUpdateClassTypeEndpoint = "/StaffTrainingclassTypes/TrainingclassTypeEdit";
        private const string StaffDeleteClassTypeEndpoint = "/StaffTrainingclassTypes/TrainingclassTypeDelete";
        private const string StaffChangeMemberPasswordEndpoint = "/StaffMemberManagement/member-change-password";
        private const string StaffCancelMembershipCheckPolicyEndpoint = "/StaffMembershipCancellation/earliest-cancellation-date";
        private const string StaffCancelMembershipEndpoint = "/StaffMembershipCancellation/cancel-membership";
        private const string StaffGetMemberPaymentCardsEndpoint = "/StaffMemberPaymentCardInfo/member/{memberGuid}/cards";
        private const string StaffGetMemberNotesEndpoint = "/StaffMemberNotes/MemberNotesList";
        private const string StaffAddMemberNoteEndpoint = "/StaffMemberNotes/MemberNotesAdd";
        private const string StaffEditMemberNoteEndpoint = "/StaffMemberNotes/MemberNotesEdit";
        private const string StaffDeleteMemberNoteEndpoint = "/StaffMemberNotes/MemberNotesDelete";
        private const string StaffGetMemberNoteEndpoint = "/StaffMemberNotes/MemberNotesGet";
        private const string StaffGetMemberChipEndpoint = "/StaffMemberChip";
        private const string StaffCreateMemberChipEndpoint = "/StaffMemberChip/Create";
        private const string StaffEditMemberChipEndpoint = "/StaffMemberChip/Edit";
        private const string StaffDeleteMemberChipEndpoint = "/StaffMemberChip/Delete";
        private const string StaffGetChipTypesEndpoint = "/StaffChipTypes/ListChipTypes";
        private const string StaffHoldMemberSubscriptionEndpoint = "/StaffMemberSubscriptionSetOnHold/set-hold";
        private const string StaffMemberMembershipChangeEndpoint = "/StaffMemberMembershipChange/ChangeMembership";
        private const string StaffGetAccessControllerGroupEndpoint = "/StaffAccessControllerGroups";
        private const string StaffAccessControllerGroupListEndpoint = "/StaffAccessControllerGroups";
        private const string StaffAccessControllerGroupAddEndpoint = "/StaffAccessControllerGroups";
        private const string StaffAccessControllerGroupEditEndpoint = "/StaffAccessControllerGroups";
        private const string StaffAccessControllerGroupDeleteEndpoint = "/StaffAccessControllerGroups";
        private const string StaffGetAccessControllerLevelsEndpoint = "/StaffAccessControllerGroups/controller-levels";
        private const string StaffGetScheduleDayConfigsEndpoint = "/StaffAccessControllerScheduleDayConfig";
        private const string StaffToggleScheduleDayConfigEndpoint = "/StaffAccessControllerScheduleDayConfig";
        private const string StaffGetScheduleTimeSlotEndpoint = "/StaffAccessControllerScheduleTimeSlot";
        private const string StaffDeleteScheduleTimeSlotEndpoint = "/StaffAccessControllerScheduleTimeSlot";
        private const string StaffAddScheduleTimeSlotEndpoint = "/StaffAccessControllerScheduleTimeSlot";
        private const string StaffEditScheduleTimeSlotEndpoint = "/StaffAccessControllerScheduleTimeSlot";
        private const string StaffGetScheduleExceptionsEndpoint = "/StaffAccessControllerScheduleException";
        private const string StaffAddScheduleExceptionEndpoint = "/StaffAccessControllerScheduleException";
        private const string StaffEditScheduleExceptionEndpoint = "/StaffAccessControllerScheduleException";
        private const string StaffDeleteScheduleExceptionEndpoint = "/StaffAccessControllerScheduleException";

        public StaffManagementService(HttpClient httpClient, ITokenService tokenService, ApiConfiguration apiConfiguration)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _apiConfiguration = apiConfiguration;
        }

        public async Task<MemberBookedClassesResponseDTO> GetMembersBookedClassesAsync(string memberGuid, int? weekNumber = null, int? fromWeekNumber = null, int? toWeekNumber = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var queryParams = new List<string>();

                // Add memberGuid as the first parameter
                queryParams.Add($"memberGuid={Uri.EscapeDataString(memberGuid)}");

                if (weekNumber.HasValue)
                    queryParams.Add($"weekNumber={weekNumber.Value}");
                if (fromWeekNumber.HasValue)
                    queryParams.Add($"fromWeekNumber={fromWeekNumber.Value}");
                if (toWeekNumber.HasValue)
                    queryParams.Add($"toWeekNumber={toWeekNumber.Value}");
                if (startDate.HasValue)
                    queryParams.Add($"startDate={startDate.Value:yyyy-MM-dd}");
                if (endDate.HasValue)
                    queryParams.Add($"endDate={endDate.Value:yyyy-MM-dd}");

                var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var requestUrl = $"{_apiConfiguration.BaseUrl}{StaffGetMemberClassesListEndpoint}{queryString}";

                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<MemberBookedClassesResponseDTO>();
                    return result ?? new MemberBookedClassesResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching member's booked classes: {errorMessage}");
                    return new MemberBookedClassesResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member's booked classes: {ex.Message}");
                return new MemberBookedClassesResponseDTO();
            }
        }

        public async Task<MemberAccessLogsTotalResponseDTO> GetTotalMemberAccessLogsAsync(string memberGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{MemberTotalAccessLogsEndpoint}?memberGuid={memberGuid}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var accessLogs = await response.Content.ReadFromJsonAsync<MemberAccessLogsTotalResponseDTO>();
                    return accessLogs ?? new MemberAccessLogsTotalResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberAccessLogsTotalResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member access logs: {ex.Message}");
                return new MemberAccessLogsTotalResponseDTO();
            }
        }

        public async Task<AccessLogsListReponseDTO> GetMemberAccessLogsAsync(string memberGuid, int page, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{MemberAccessLogsListEndpoint}?memberGuid={memberGuid}&page={page}&pageSize={pageSize}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var accessLogs = await response.Content.ReadFromJsonAsync<AccessLogsListReponseDTO>();
                    return accessLogs ?? new AccessLogsListReponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new AccessLogsListReponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member access logs: {ex.Message}");
                return new AccessLogsListReponseDTO();
            }
        }

        public async Task<MemberPaymentMembershipInfoResponseDTO> StaffGetMembershipInfo(string memberGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffMembershipInfoEndpoint}?memberGuid={memberGuid}");

                if (response.IsSuccessStatusCode)
                {
                    var membershipInfo = await response.Content.ReadFromJsonAsync<MemberPaymentMembershipInfoResponseDTO>();
                    return membershipInfo ?? new MemberPaymentMembershipInfoResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberPaymentMembershipInfoResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching staff membership info: {ex.Message}");
                return new MemberPaymentMembershipInfoResponseDTO();
            }
        }

        public async Task<MemberPaymentListReponseDTO> StaffGetMemberPaymentsAsync(string memberGuid, int page, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffMemberPaymentListEndpoint}?memberGuid={memberGuid}&page={page}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var paymentList = await response.Content.ReadFromJsonAsync<MemberPaymentListReponseDTO>();
                    return paymentList ?? new MemberPaymentListReponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberPaymentListReponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching staff member payments: {ex.Message}");
                return new MemberPaymentListReponseDTO();
            }
        }

        public async Task<StaffShowSignedMemberContractResponseDTO> StaffGetSignedMemberContracts(string memberGuid, int page, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffShowSignedMemberContractEndpoint}?memberGuid={memberGuid}&page={page}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var contracts = await response.Content.ReadFromJsonAsync<StaffShowSignedMemberContractResponseDTO>();
                    return contracts ?? new StaffShowSignedMemberContractResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffShowSignedMemberContractResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching signed member contracts: {ex.Message}");
                return new StaffShowSignedMemberContractResponseDTO();
            }
        }

        public async Task<StaffTrainingClassLocationListResponseDTO> StaffGetClassLocationsAsync(int page, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetClassLocationsEndpoint}?page={page}&pageSize={pageSize}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var locations = await response.Content.ReadFromJsonAsync<StaffTrainingClassLocationListResponseDTO>();
                    return locations ?? new StaffTrainingClassLocationListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassLocationListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching class locations: {ex.Message}");
                return new StaffTrainingClassLocationListResponseDTO();
            }
        }

        public async Task<StaffTrainingClassNewLocationResponseDTO> StaffAddClassLocationAsync(AddNewLocationDTO addNewLocationDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffAddClassLocationEndpoint}", addNewLocationDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainingClassNewLocationResponseDTO>();
                    return result ?? new StaffTrainingClassNewLocationResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassNewLocationResponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding class location: {ex.Message}");
                return new StaffTrainingClassNewLocationResponseDTO { Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<StaffTrainingClassUpdateResponseDTO> StaffUpdateClassLocationAsync(StaffTrainingClassLocationDTO locationDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffUpdateClassLocationEndpoint}", locationDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainingClassUpdateResponseDTO>();
                    return result ?? new StaffTrainingClassUpdateResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassUpdateResponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating class location: {ex.Message}");
                return new StaffTrainingClassUpdateResponseDTO { Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<StaffTrainingClassDeleteResponseDTO> StaffDeleteClassLocationAsync(string locationGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"{_apiConfiguration.BaseUrl}{StaffDeleteClassLocationEndpoint}/{locationGuid}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainingClassDeleteResponseDTO>();
                    return result ?? new StaffTrainingClassDeleteResponseDTO { Message = "Location deleted successfully" };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassDeleteResponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting class location: {ex.Message}");
                return new StaffTrainingClassDeleteResponseDTO { Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<StaffTrainingClassTypeListResponseDTO> StaffGetClassTypesAsync(int page, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetClassTypesEndpoint}?page={page}&pageSize={pageSize}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var types = await response.Content.ReadFromJsonAsync<StaffTrainingClassTypeListResponseDTO>();
                    return types ?? new StaffTrainingClassTypeListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassTypeListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching class types: {ex.Message}");
                return new StaffTrainingClassTypeListResponseDTO();
            }
        }

        public async Task<StaffTrainingClassTypeAddResponseDTO> StaffAddClassTypeAsync(AddNewTypeDTO addNewTypeDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Log the DTO before sending
                Console.WriteLine($"AddNewTypeDTO - Name: {addNewTypeDTO.TrainingClassTypesName}, Description: {addNewTypeDTO.TrainingClassTypesDescription}, Max: {addNewTypeDTO.TrainingClassTypesMax}");
                Console.WriteLine($"Description Length: {addNewTypeDTO.TrainingClassTypesDescription?.Length ?? 0}");
                
                var jsonOptions = new System.Text.Json.JsonSerializerOptions 
                { 
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase 
                };
                var json = System.Text.Json.JsonSerializer.Serialize(addNewTypeDTO, jsonOptions);
                Console.WriteLine($"JSON being sent: {json}");

                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffAddClassTypeEndpoint}", addNewTypeDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainingClassTypeAddResponseDTO>();
                    return result ?? new StaffTrainingClassTypeAddResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassTypeAddResponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding class type: {ex.Message}");
                return new StaffTrainingClassTypeAddResponseDTO { Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<StaffTrainingClassTypeUpdateResponseDTO> StaffUpdateClassTypeAsync(StaffTrainingClassTypeDTO typeDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Log the DTO before sending
                Console.WriteLine($"StaffTrainingClassTypeDTO - Name: {typeDTO.TrainingClassTypesName}, Description: {typeDTO.TrainingClassTypesDescription}, Max: {typeDTO.TrainingClassTypesMax}");
                Console.WriteLine($"Description Length: {typeDTO.TrainingClassTypesDescription?.Length ?? 0}");
                
                var jsonOptions = new System.Text.Json.JsonSerializerOptions 
                { 
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase 
                };
                var json = System.Text.Json.JsonSerializer.Serialize(typeDTO, jsonOptions);
                Console.WriteLine($"JSON being sent: {json}");

                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffUpdateClassTypeEndpoint}", typeDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainingClassTypeUpdateResponseDTO>();
                    return result ?? new StaffTrainingClassTypeUpdateResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassTypeUpdateResponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating class type: {ex.Message}");
                return new StaffTrainingClassTypeUpdateResponseDTO { Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<StaffTrainingClassTypeDeleteResponseDTO> StaffDeleteClassTypeAsync(string typeGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"{_apiConfiguration.BaseUrl}{StaffDeleteClassTypeEndpoint}/{typeGuid}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainingClassTypeDeleteResponseDTO>();
                    return result ?? new StaffTrainingClassTypeDeleteResponseDTO { Message = "Class type deleted successfully" };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassTypeDeleteResponseDTO { Message = $"Error: {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting class type: {ex.Message}");
                return new StaffTrainingClassTypeDeleteResponseDTO { Message = $"Error: {ex.Message}" };
            }
        }

        public async Task<StaffChangeMemberPasswordResponseDto> StaffChangeMemberPasswordAsync(StaffChangeMemberPasswordDto staffChangeMemberPasswordDto)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffChangeMemberPasswordEndpoint}", staffChangeMemberPasswordDto);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffChangeMemberPasswordResponseDto>();
                    return result ?? new StaffChangeMemberPasswordResponseDto();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffChangeMemberPasswordResponseDto { Message = "Failed to change member password." };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing member password: {ex.Message}");
                return new StaffChangeMemberPasswordResponseDto { Message = "An error occurred while changing the member password." };
            }
        }

        public async Task<StaffCancelMembershipCheckResponseDTO> StaffCancelMembershipCheckPolicyAsync(StaffCancelMembershipCheckDTO staffCancelMembershipCheckDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Construct the URL with the memberGuid in the path
                string url = $"{_apiConfiguration.BaseUrl}{StaffCancelMembershipCheckPolicyEndpoint}/{staffCancelMembershipCheckDTO.MemberGuid}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffCancelMembershipCheckResponseDTO>();
                    return result ?? new StaffCancelMembershipCheckResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Fetching Member Cancellation Policy: {errorMessage}");
                    
                    // Try to parse error response as JSON to extract message
                    try
                    {
                        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(errorMessage);
                        if (errorResponse != null && errorResponse.ContainsKey("message"))
                        {
                            return new StaffCancelMembershipCheckResponseDTO 
                            { 
                                CanCancel = false, 
                                Note = errorResponse["message"] 
                            };
                        }
                    }
                    catch { }
                    
                    return new StaffCancelMembershipCheckResponseDTO 
                    { 
                        CanCancel = false, 
                        Note = "Failed to retrieve cancellation policy." 
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching Member Cancellation Policy: {ex.Message}");
                return new StaffCancelMembershipCheckResponseDTO 
                { 
                    CanCancel = false, 
                    Note = "An error occurred while checking cancellation eligibility." 
                };
            }
        }


        public async Task<StaffCancelMembershipResponseDTO> StaffCancelMembershipAsync(StaffCancelMembershipDTO staffCancelMembershipDTO)
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("User is not authenticated.");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            string url = $"{_apiConfiguration.BaseUrl}{StaffCancelMembershipEndpoint}";

            var response = await _httpClient.PostAsJsonAsync(url, staffCancelMembershipDTO);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<StaffCancelMembershipResponseDTO>();
                return result ?? new StaffCancelMembershipResponseDTO { Message = "Membership cancelled successfully." };
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Cancelling Member's Membership: {errorMessage}");
                throw new HttpRequestException($"Failed to cancel membership: {errorMessage}");
            }
        }

        public async Task<MemberPaymentCardInfoResponseDTO> StaffGetMemberPaymentCardsAsync(string memberGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffGetMemberPaymentCardsEndpoint.Replace("{memberGuid}", memberGuid)}");
                if (response.IsSuccessStatusCode)
                {
                    var cardInfo = await response.Content.ReadFromJsonAsync<MemberPaymentCardInfoResponseDTO>();
                    return cardInfo ?? new MemberPaymentCardInfoResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new MemberPaymentCardInfoResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member payment cards: {ex.Message}");
                return new MemberPaymentCardInfoResponseDTO();
            }
        }

        public async Task<StaffMemberNotesResponseDTO> StaffGetMemberNotesAsync(StaffMemberNotesRequestDTO staffMemberNotesRequestDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffGetMemberNotesEndpoint}?memberGuid={staffMemberNotesRequestDTO.MemberGuid}&page={staffMemberNotesRequestDTO.Page}&pageSize={staffMemberNotesRequestDTO.PageSize}&search={staffMemberNotesRequestDTO.Search}");
                if (response.IsSuccessStatusCode)
                {
                    var cardInfo = await response.Content.ReadFromJsonAsync<StaffMemberNotesResponseDTO>();
                    return cardInfo ?? new StaffMemberNotesResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffMemberNotesResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member notes: {ex.Message}");
                return new StaffMemberNotesResponseDTO();
            }
        }

        public async Task<StaffMemberNoteActionsResponseDTO> StaffAddMemberNoteAsync(StaffAddMemberNoteDTO staffAddMemberNoteDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffMemberNoteActionsResponseDTO { Success = false, Message = "User is not authenticated." };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //var content = new StringContent(JsonConvert.SerializeObject(staffAddMemberNoteDTO), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffAddMemberNoteEndpoint}", staffAddMemberNoteDTO);

                //var responseContent = await response.Content.ReadFromJsonAsync;

                // Try to deserialize regardless of status code (many APIs return {Success, Message} even on errors)
                //if (!string.IsNullOrWhiteSpace(response.Content.ReadAsStringAsync()))
                //{
                //    var result = responseContent.re<StaffMemberNoteActionsResponseDTO>(responseContent);
                //    if (result != null)
                //        return result;
                //}

                // Fallback based on HTTP status if no body or deserialization failed
                return response.IsSuccessStatusCode
                    ? new StaffMemberNoteActionsResponseDTO { Success = true, Message = "Note added successfully." }
                    : new StaffMemberNoteActionsResponseDTO { Success = false, Message = $"Request failed ({response.StatusCode})" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding member note: {ex.Message}");
                return new StaffMemberNoteActionsResponseDTO { Success = false, Message = "An error occurred while adding the note." };
            }
        }

        public async Task<StaffMemberNoteActionsResponseDTO> StaffEditMemberNoteAsync(StaffEditMemberNoteDTO staffEditMemberNoteDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffMemberNoteActionsResponseDTO { Success = false, Message = "User is not authenticated." };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //var content = new StringContent(JsonConvert.SerializeObject(staffEditMemberNoteDTO), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffEditMemberNoteEndpoint}", staffEditMemberNoteDTO);

                var responseContent = await response.Content.ReadAsStringAsync();

                //if (!string.IsNullOrWhiteSpace(responseContent))
                //{
                //    var result = JsonConvert.DeserializeObject<StaffMemberNoteActionsResponseDTO>(responseContent);
                //    if (result != null)
                //        return result;
                //}

                return response.IsSuccessStatusCode
                    ? new StaffMemberNoteActionsResponseDTO { Success = true, Message = "Note updated successfully." }
                    : new StaffMemberNoteActionsResponseDTO { Success = false, Message = $"Request failed ({response.StatusCode})" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing member note: {ex.Message}");
                return new StaffMemberNoteActionsResponseDTO { Success = false, Message = "An error occurred while updating the note." };
            }
        }

        public async Task<StaffMemberNoteActionsResponseDTO> StaffDeleteMemberNoteAsync(string memberNotesGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffMemberNoteActionsResponseDTO { Success = false, Message = "User is not authenticated." };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"{_apiConfiguration.BaseUrl}{StaffDeleteMemberNoteEndpoint}/{memberNotesGuid}");

                var responseContent = await response.Content.ReadAsStringAsync();

                //if (!string.IsNullOrWhiteSpace(responseContent))
                //{
                //    var result = JsonConvert.DeserializeObject<StaffMemberNoteActionsResponseDTO>(responseContent);
                //    if (result != null)
                //        return result;
                //}

                // DELETE often returns 204 No Content on success
                return response.IsSuccessStatusCode
                    ? new StaffMemberNoteActionsResponseDTO { Success = true, Message = "Note deleted successfully." }
                    : new StaffMemberNoteActionsResponseDTO { Success = false, Message = $"Request failed ({response.StatusCode})" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting member note: {ex.Message}");
                return new StaffMemberNoteActionsResponseDTO { Success = false, Message = "An error occurred while deleting the note." };
            }
        }
        public async Task<StaffMemberNoteResponseDTO> StaffGetMemberNoteAsync(string memberNotesGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffGetMemberNoteEndpoint}/{memberNotesGuid}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffMemberNoteResponseDTO>();
                    return result ?? new StaffMemberNoteResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffMemberNoteResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member note: {ex.Message}");
                return new StaffMemberNoteResponseDTO();
            }
        }

        public async Task<StaffGetMemberChipResponseDTO> StaffGetMemberChipsAsync(StaffGetMemberChipDTO staffGetMemberChipDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffGetMemberChipEndpoint}?memberGuid={staffGetMemberChipDTO.MemberGuid}&page={staffGetMemberChipDTO.Page}&pageSize={staffGetMemberChipDTO.PageSize}&search={staffGetMemberChipDTO.Search}");
                if (response.IsSuccessStatusCode)
                {
                    var chipInfo = await response.Content.ReadFromJsonAsync<StaffGetMemberChipResponseDTO>();
                    return chipInfo ?? new StaffGetMemberChipResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffGetMemberChipResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member notes: {ex.Message}");
                return new StaffGetMemberChipResponseDTO();
            }
        }

        public async Task<StaffMemberChipActionResponseDTO> StaffCreateMemberChipAsync(StaffMemberChipCreateDTO staffMemberChipCreateDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffMemberChipActionResponseDTO { Success = false, Message = "User is not authenticated." };
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffCreateMemberChipEndpoint}", staffMemberChipCreateDTO);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                    return new StaffMemberChipActionResponseDTO { Success = true, Message = "Member chip created successfully." };
                
                // Try to parse error response for detailed message
                try
                {
                    var errorResponse = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                    if (errorResponse.TryGetProperty("message", out var messageElement))
                    {
                        var errorMessage = messageElement.GetString();
                        
                        // Check if this is a duplicate chip error with member info
                        if (errorResponse.TryGetProperty("memberName", out var memberNameElement))
                        {
                            var memberName = memberNameElement.GetString();
                            return new StaffMemberChipActionResponseDTO 
                            { 
                                Success = false, 
                                Message = $"{errorMessage} (Already assigned to: {memberName})" 
                            };
                        }
                        
                        return new StaffMemberChipActionResponseDTO { Success = false, Message = errorMessage ?? $"Request failed ({response.StatusCode})" };
                    }
                }
                catch { /* Ignore JSON parse errors */ }
                
                return new StaffMemberChipActionResponseDTO { Success = false, Message = $"Request failed ({response.StatusCode})" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating member chip: {ex.Message}");
                return new StaffMemberChipActionResponseDTO { Success = false, Message = "An error occurred while creating the member chip." };
            }
        }

        public async Task<StaffMemberChipActionResponseDTO> StaffEditMemberChipAsync(StaffMemberChipEditDTO staffMemberChipEditDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffMemberChipActionResponseDTO { Success = false, Message = "User is not authenticated." };
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffEditMemberChipEndpoint}", staffMemberChipEditDTO);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                    return new StaffMemberChipActionResponseDTO { Success = true, Message = "Member chip updated successfully." };
                
                // Try to parse error response for detailed message
                try
                {
                    var errorResponse = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                    if (errorResponse.TryGetProperty("message", out var messageElement))
                    {
                        var errorMessage = messageElement.GetString();
                        
                        // Check if this is a duplicate chip error with member info
                        if (errorResponse.TryGetProperty("memberName", out var memberNameElement))
                        {
                            var memberName = memberNameElement.GetString();
                            return new StaffMemberChipActionResponseDTO 
                            { 
                                Success = false, 
                                Message = $"{errorMessage} (Already assigned to: {memberName})" 
                            };
                        }
                        
                        return new StaffMemberChipActionResponseDTO { Success = false, Message = errorMessage ?? $"Request failed ({response.StatusCode})" };
                    }
                }
                catch { /* Ignore JSON parse errors */ }
                
                return new StaffMemberChipActionResponseDTO { Success = false, Message = $"Request failed ({response.StatusCode})" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing member chip: {ex.Message}");
                return new StaffMemberChipActionResponseDTO { Success = false, Message = "An error occurred while updating the member chip." };
            }
        }

        public async Task<StaffMemberChipDeleteResponseDTO> StaffDeleteMemberChipAsync(string memberChipGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffMemberChipDeleteResponseDTO { Message = "User is not authenticated." };
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"{_apiConfiguration.BaseUrl}{StaffDeleteMemberChipEndpoint}/{memberChipGuid}");
                var responseContent = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode
                    ? new StaffMemberChipDeleteResponseDTO { Message = "Member chip deleted successfully." }
                    : new StaffMemberChipDeleteResponseDTO { Message = $"Request failed ({response.StatusCode})" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting member chip: {ex.Message}");
                return new StaffMemberChipDeleteResponseDTO { Message = "An error occurred while deleting the member chip." };
            }

        }

        public async Task<StaffGetChipTypeResponseDTO> StaffGetChipTypesAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_apiConfiguration.BaseUrl}{StaffGetChipTypesEndpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var chipTypes = await response.Content.ReadFromJsonAsync<StaffGetChipTypeResponseDTO>();
                    return chipTypes ?? new StaffGetChipTypeResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffGetChipTypeResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching chip types: {ex.Message}");
                return new StaffGetChipTypeResponseDTO();
            }
        }

        public async Task<StaffHoldMemberSubscriptionResponseDTO> StaffHoldMemberSubscriptionAsync(StaffHoldMemberSubscriptionDTO staffHoldMemberSubscriptionDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffHoldMemberSubscriptionEndpoint}";

                var requestUrl = url;
                var response = await _httpClient.PostAsJsonAsync(requestUrl, staffHoldMemberSubscriptionDTO);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffHoldMemberSubscriptionResponseDTO>();
                    if (result != null)
                    {
                        result.Success = true;
                    }
                    return result ?? new StaffHoldMemberSubscriptionResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<StaffHoldMemberSubscriptionResponseDTO>();

                    Console.WriteLine($"Error holding subscription: {errorMessage}");
                    return new StaffHoldMemberSubscriptionResponseDTO { Success = false, Message = errorMessage?.Message ?? "Failed to hold subscription." };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error holding subscription: {ex.Message}");
                return new StaffHoldMemberSubscriptionResponseDTO { Success = false, Message = ex.Message };
            }
        }

        public async Task<StaffMemberMembershipChangeResponseDTO> StaffChangeMemberMembershipAsync(StaffMemberMembershipChangeDTO staffMemberMembershipChangeDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffMemberMembershipChangeEndpoint}";

                var requestUrl = url;
                var response = await _httpClient.PostAsJsonAsync(requestUrl, staffMemberMembershipChangeDTO);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffMemberMembershipChangeResponseDTO>();
                    if (result != null)
                    {
                        result.Success = true;
                    }
                    return result ?? new StaffMemberMembershipChangeResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<StaffMemberMembershipChangeResponseDTO>();

                    Console.WriteLine($"Error changing membership: {errorMessage}");

                    if (errorMessage != null)
                        errorMessage.Success = false;

                    return errorMessage ?? new StaffMemberMembershipChangeResponseDTO { Success = false, Message = errorMessage?.Message ?? "Failed to hold subscription." };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error holding subscription: {ex.Message}");
                return new StaffMemberMembershipChangeResponseDTO { Success = false, Message = ex.Message };
            }
        }

        public async Task<StaffAccessControllerGroupListResponseDTO> StaffGetAccessControllerGroupListAsync(StaffAccessControllerGroupDTO query)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffAccessControllerGroupListEndpoint}"
                    .SetQueryParams(new
                    {
                        page = query.Page,
                        pageSize = query.PageSize,
                        search = query.Search
                    });


                var requestUrl = url;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffAccessControllerGroupListResponseDTO>();
                    return result ?? new StaffAccessControllerGroupListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching access controller groups: {errorMessage}");
                    return new StaffAccessControllerGroupListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching access controller groups: {ex.Message}");
                return new StaffAccessControllerGroupListResponseDTO();
            }
        }

        public async Task<StaffAccessControllerGroupAddResponseDTO> StaffGetAccessControllerGroupAddAsync(StaffAccessControllerGroupAddDTO addDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffAccessControllerGroupAddEndpoint}";

                var requestUrl = url;
                var response = await _httpClient.PostAsJsonAsync(requestUrl, addDTO);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffAccessControllerGroupAddResponseDTO>();
                    return result ?? new StaffAccessControllerGroupAddResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error creating access controller group: {errorMessage}");
                    return new StaffAccessControllerGroupAddResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating access controller groups: {ex.Message}");
                return new StaffAccessControllerGroupAddResponseDTO();
            }
        }

        public async Task<StaffAccessControllerGroupEditResponseDTO> StaffGetAccessControllerGroupEditAsync(StaffAccessControllerGroupEditDTO editDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffAccessControllerGroupEditEndpoint}";

                var requestUrl = url;
                var response = await _httpClient.PutAsJsonAsync(requestUrl, editDTO);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffAccessControllerGroupEditResponseDTO>();
                    return result ?? new StaffAccessControllerGroupEditResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error editing access controller group: {errorMessage}");
                    return new StaffAccessControllerGroupEditResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing access controller group: {ex.Message}");
                return new StaffAccessControllerGroupEditResponseDTO();
            }
        }

        public async Task<StaffAccessControllerGroupDeleteResponseDTO> StaffGetAccessControllerGroupDeleteAsync(string GroupGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffAccessControllerGroupDeleteEndpoint}/{GroupGuid}";

                var requestUrl = url;
                var response = await _httpClient.DeleteAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffAccessControllerGroupDeleteResponseDTO>();
                    return result ?? new StaffAccessControllerGroupDeleteResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error deleting access controller group: {errorMessage}");
                    return new StaffAccessControllerGroupDeleteResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting access controller group: {ex.Message}");
                return new StaffAccessControllerGroupDeleteResponseDTO();
            }
        }

        public async Task<AccessControllerLevelsResponseDTO> StaffGetAccessControllerLevelsAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetAccessControllerLevelsEndpoint}";


                var requestUrl = url;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AccessControllerLevelsResponseDTO>();
                    return result ?? new AccessControllerLevelsResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching access controller levels: {errorMessage}");
                    return new AccessControllerLevelsResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching access controller levels: {ex.Message}");
                return new AccessControllerLevelsResponseDTO();
            }
        }

        public async Task<StaffAccessControllerGroupResponseDTO> StaffGetAccessControllerGroupAsync(string groupGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetAccessControllerGroupEndpoint}/{groupGuid}";


                var requestUrl = url;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffAccessControllerGroupResponseDTO>();
                    return result ?? new StaffAccessControllerGroupResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching access controller group: {errorMessage}");
                    return new StaffAccessControllerGroupResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching access controller group: {ex.Message}");
                return new StaffAccessControllerGroupResponseDTO();
            }
        }

        // Continuing inside class StaffManagementService : IStaffManagementService

        public async Task<StaffScheduleDayConfigResponseDTO> StaffGetScheduleDayConfigsAsync(string accessGroupsGUID)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetScheduleDayConfigsEndpoint}?accessControllerAccessGroupsGUID={accessGroupsGUID}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleDayConfigResponseDTO>();
                    return result ?? new StaffScheduleDayConfigResponseDTO();
                }

                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching schedule day configs: {error}");
                return new StaffScheduleDayConfigResponseDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in StaffGetScheduleDayConfigsAsync: {ex.Message}");
                return new StaffScheduleDayConfigResponseDTO();
            }
        }

        public async Task<StaffScheduleDayConfigToggleResponseDTO> StaffToggleScheduleDayConfigAsync(StaffScheduleDayConfigToggleDTO toggleDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffToggleScheduleDayConfigEndpoint}";

                var response = await _httpClient.PutAsJsonAsync(url, toggleDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleDayConfigToggleResponseDTO>();
                    if (result != null)
                    {
                        result.Success = true;
                    }
                    return result ?? new StaffScheduleDayConfigToggleResponseDTO { Success = true };
                }

                var errorContent = await response.Content.ReadFromJsonAsync<StaffScheduleDayConfigToggleResponseDTO>();
                return errorContent ?? new StaffScheduleDayConfigToggleResponseDTO
                {
                    Success = false,
                    Message = $"Toggle failed ({response.StatusCode})"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling schedule day config: {ex.Message}");
                return new StaffScheduleDayConfigToggleResponseDTO
                {
                    Success = false,
                    Message = "An error occurred while toggling day config"
                };
            }
        }

        public async Task<StaffScheduleTimeSlotResponseDTO> StaffGetScheduleTimeSlotAsync(string dayConfigGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetScheduleTimeSlotEndpoint}/{dayConfigGuid}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleTimeSlotResponseDTO>();
                    return result ?? new StaffScheduleTimeSlotResponseDTO();
                }

                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching time slot: {error}");
                return new StaffScheduleTimeSlotResponseDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in StaffGetScheduleTimeSlotAsync: {ex.Message}");
                return new StaffScheduleTimeSlotResponseDTO();
            }
        }

        public async Task<StaffScheduleTimeSlotListResponseDTO> StaffGetScheduleTimeSlotsAsync(string dayConfigGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetScheduleTimeSlotEndpoint}?dayConfigGuid={dayConfigGuid}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleTimeSlotListResponseDTO>();
                    return result ?? new StaffScheduleTimeSlotListResponseDTO();
                }

                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching time slots list: {error}");
                return new StaffScheduleTimeSlotListResponseDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in StaffGetScheduleTimeSlotsAsync: {ex.Message}");
                return new StaffScheduleTimeSlotListResponseDTO();
            }
        }

        public async Task<StaffScheduleTimeSlotCreateResponseDTO> StaffCreateScheduleTimeSlotAsync(StaffScheduleTimeSlotCreateDTO createDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffAddScheduleTimeSlotEndpoint}";

                var response = await _httpClient.PostAsJsonAsync(url, createDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleTimeSlotCreateResponseDTO>();
                    return result ?? new StaffScheduleTimeSlotCreateResponseDTO();
                }

                var errorContent = await response.Content.ReadFromJsonAsync<StaffScheduleTimeSlotCreateResponseDTO>();
                return errorContent ?? new StaffScheduleTimeSlotCreateResponseDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating time slot: {ex.Message}");
                return new StaffScheduleTimeSlotCreateResponseDTO();
            }
        }

        public async Task<StaffScheduleTimeSlotUpdateResponseDTO> StaffUpdateScheduleTimeSlotAsync(StaffScheduleTimeSlotUpdateDTO updateDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");


                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffEditScheduleTimeSlotEndpoint}";

                var response = await _httpClient.PutAsJsonAsync(url, updateDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleTimeSlotUpdateResponseDTO>();
                    return result ?? new StaffScheduleTimeSlotUpdateResponseDTO();
                }

                var errorContent = await response.Content.ReadFromJsonAsync<StaffScheduleTimeSlotUpdateResponseDTO>();
                return errorContent ?? new StaffScheduleTimeSlotUpdateResponseDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating time slot: {ex.Message}");
                return new StaffScheduleTimeSlotUpdateResponseDTO();
            }
        }

        public async Task<StaffScheduleTimeSlotDeleteResponseDTO> StaffDeleteScheduleTimeSlotAsync(string timeSlotGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");


                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffDeleteScheduleTimeSlotEndpoint}/{timeSlotGuid}";

                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Many APIs return 204 No Content on successful DELETE
                    return new StaffScheduleTimeSlotDeleteResponseDTO
                    {
                        Success = true,
                        Message = "Time slot deleted successfully"
                    };
                }

                var errorContent = await response.Content.ReadFromJsonAsync<StaffScheduleTimeSlotDeleteResponseDTO>();
                return errorContent ?? new StaffScheduleTimeSlotDeleteResponseDTO
                {
                    Success = false,
                    Message = $"Delete failed ({response.StatusCode})"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting time slot: {ex.Message}");
                return new StaffScheduleTimeSlotDeleteResponseDTO
                {
                    Success = false,
                    Message = "Error while deleting time slot"
                };
            }
        }

        public async Task<StaffScheduleExceptionListResponseDTO> StaffGetScheduleExceptionsAsync(string accessGroupGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetScheduleExceptionsEndpoint}"
                    .SetQueryParam("accessGroupGuid", accessGroupGuid);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleExceptionListResponseDTO>();
                    return result ?? new StaffScheduleExceptionListResponseDTO();
                }

                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching schedule exceptions: {error}");
                return new StaffScheduleExceptionListResponseDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in StaffGetScheduleExceptionsAsync: {ex.Message}");
                return new StaffScheduleExceptionListResponseDTO();
            }
        }

        public async Task<StaffScheduleExceptionResponseDTO> StaffGetScheduleExceptionAsync(string exceptionGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetScheduleExceptionsEndpoint}/{exceptionGuid}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleExceptionResponseDTO>();
                    return result ?? new StaffScheduleExceptionResponseDTO();
                }

                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching schedule exception: {error}");
                return new StaffScheduleExceptionResponseDTO();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in StaffGetScheduleExceptionAsync: {ex.Message}");
                return new StaffScheduleExceptionResponseDTO();
            }
        }

        public async Task<StaffScheduleExceptionCreateResponseDTO> StaffCreateScheduleExceptionAsync(StaffScheduleExceptionCreateDTO createDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffAddScheduleExceptionEndpoint}";
                var response = await _httpClient.PostAsJsonAsync(url, createDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleExceptionCreateResponseDTO>();
                    return result ?? new StaffScheduleExceptionCreateResponseDTO();
                }

                var errorContent = await response.Content.ReadFromJsonAsync<StaffScheduleExceptionCreateResponseDTO>();
                return errorContent ?? new StaffScheduleExceptionCreateResponseDTO
                {
                    Message = $"Creation failed ({response.StatusCode})"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating schedule exception: {ex.Message}");
                return new StaffScheduleExceptionCreateResponseDTO
                {
                    Message = "An error occurred while creating the schedule exception"
                };
            }
        }

        public async Task<StaffScheduleExceptionEditResponseDTO> StaffEditScheduleExceptionAsync(StaffScheduleExceptionEditDTO editDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffEditScheduleExceptionEndpoint}";
                var response = await _httpClient.PutAsJsonAsync(url, editDTO);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffScheduleExceptionEditResponseDTO>();
                    return result ?? new StaffScheduleExceptionEditResponseDTO();
                }

                var errorContent = await response.Content.ReadFromJsonAsync<StaffScheduleExceptionEditResponseDTO>();
                return errorContent ?? new StaffScheduleExceptionEditResponseDTO
                {
                    Message = $"Update failed ({response.StatusCode})"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating schedule exception: {ex.Message}");
                return new StaffScheduleExceptionEditResponseDTO
                {
                    Message = "An error occurred while updating the schedule exception"
                };
            }
        }

        public async Task<StaffScheduleExceptionDeleteResponseDTO> StaffDeleteScheduleExceptionAsync(string exceptionGuid)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffDeleteScheduleExceptionEndpoint}/{exceptionGuid}";
                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Typical DELETE → 204 No Content or 200 with simple response
                    return new StaffScheduleExceptionDeleteResponseDTO
                    {
                        Success = true,
                        Message = "Schedule exception deleted successfully"
                    };
                }

                var errorContent = await response.Content.ReadFromJsonAsync<StaffScheduleExceptionDeleteResponseDTO>();
                return errorContent ?? new StaffScheduleExceptionDeleteResponseDTO
                {
                    Success = false,
                    Message = $"Delete failed ({response.StatusCode})"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting schedule exception: {ex.Message}");
                return new StaffScheduleExceptionDeleteResponseDTO
                {
                    Success = false,
                    Message = "An error occurred while deleting the schedule exception"
                };
            }
        }
    }
}