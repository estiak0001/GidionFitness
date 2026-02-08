using BlazorAdminTemplate.Application.DTOs.Chips;
using BlazorAdminTemplate.Application.DTOs.Classes;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Flurl;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class TrainingClassesService : ITrainingClassesService
    {

        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly ITokenService _tokenService;
        private const string TrainingClassesListEndpoint = "/MemberClassesListAvailableClasses";
        private const string TrainingClassesBookEndpoint = "/MemberClassesBookClass";
        private const string TrainingClassesCancelEndpoint = "/MemberClassesCancelClass";
        private const string MyClassesListEndpoint = "/MemberClassesListMyClasses";
        private const string StaffCreateSingleClassEndpoint = "/StaffTrainingClassCreateSingle/CreateSingleClass";
        private const string StaffCreateMultipleClassesEndpoint = "/StaffTrainingClassCreateMultible/CreateMultibleClass";
        private const string StaffEditClassEndpoint = "/StaffTrainingClassCreateSingle/UpdateSingleClass";
        private const string StaffDeleteClassEndpoint = "/StaffTrainingClassCreateSingle/DeleteSingleClass";
        private const string StaffGetClassListEndpoint = "/StaffTrainingClassCreateSingle/ListClasses";
        private const string StaffCancelClassEndpoint = "/StaffTrainingClassCancellation/cancel";
        private const string StaffTrainerListEndpoint = "/StaffTrainingclassTrainerList/TrainerList";


        public TrainingClassesService(HttpClient httpClient, ApiConfiguration apiConfiguration, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
            _tokenService = tokenService;
        }

        public async Task<MemberClassesListAvailableClassesResponseDTO> GetMemberClassesListAsync(int? weekNumber = null, int? fromWeekNumber = null, int? toWeekNumber = null, DateTime? startDate = null, DateTime? endDate = null, string? orgSubGuid = null, string? trainingClassTypesGUID = null)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var queryParams = new List<string>();

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

                if (!string.IsNullOrEmpty(orgSubGuid))
                    queryParams.Add($"orgSubGuid={Uri.EscapeDataString(orgSubGuid)}");

                if (!string.IsNullOrEmpty(trainingClassTypesGUID))
                    queryParams.Add($"trainingClassTypesGUID={Uri.EscapeDataString(trainingClassTypesGUID)}");

                var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var requestUrl = $"{_apiConfiguration.BaseUrl}{TrainingClassesListEndpoint}{queryString}";

                var response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<MemberClassesListAvailableClassesResponseDTO>();
                    return result ?? new MemberClassesListAvailableClassesResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching training classes: {errorMessage}");
                    return new MemberClassesListAvailableClassesResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching training classes: {ex.Message}");
                return new MemberClassesListAvailableClassesResponseDTO();
            }
        }

        public async Task<MemberClassesBookClassResponseDTO> PostMemberClassBookingAsync(string classGUID)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var requestUrl = $"{_apiConfiguration.BaseUrl}{TrainingClassesBookEndpoint}/{Uri.EscapeDataString(classGUID)}";

                var response = await _httpClient.PostAsync(requestUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<MemberClassesBookClassResponseDTO>();
                    return result ?? new MemberClassesBookClassResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error booking class: {errorMessage}");
                    return new MemberClassesBookClassResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error booking class: {ex.Message}");
                return new MemberClassesBookClassResponseDTO();
            }
        }

        public async Task<MemberClassesCancelClassResponseDTO> PostMemberClassCancelAsync(string classGUID)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var requestUrl = $"{_apiConfiguration.BaseUrl}{TrainingClassesCancelEndpoint}/{Uri.EscapeDataString(classGUID)}";

                var response = await _httpClient.PostAsync(requestUrl, null);

                // Read response regardless of status code since both success and "error" return valid JSON
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(responseContent))
                {
                    var result = JsonSerializer.Deserialize<MemberClassesCancelClassResponseDTO>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (result != null)
                    {
                        // Log based on response type
                        if (result.IsSuccess)
                        {
                            Console.WriteLine($"Class cancelled successfully: {result.CancelledEnrollmentGUID}");
                        }
                        else if (result.IsCancellationNotAllowed)
                        {
                            Console.WriteLine($"Cancellation not allowed: {result.Message}");
                        }

                        return result;
                    }
                }

                Console.WriteLine($"Error cancelling class: {response.StatusCode} - {responseContent}");
                return new MemberClassesCancelClassResponseDTO { Message = "Unknown error occurred" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling class: {ex.Message}");
                return new MemberClassesCancelClassResponseDTO { Message = $"Exception: {ex.Message}" };
            }
        }

        public async Task<MemberBookedClassesResponseDTO> GetMyClassesListAsync(int? weekNumber = null, int? fromWeekNumber = null, int? toWeekNumber = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var queryParams = new List<string>();

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
                var requestUrl = $"{_apiConfiguration.BaseUrl}{MyClassesListEndpoint}{queryString}";

                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<MemberBookedClassesResponseDTO>();
                    return result ?? new MemberBookedClassesResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching booked classes: {errorMessage}");
                    return new MemberBookedClassesResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching booked classes: {ex.Message}");
                return new MemberBookedClassesResponseDTO();
            }
        }

        public async Task<StaffTrainingClassActionResponseDTO> StaffCreateSingleClass(StaffTrainingClassAddDTO staffTrainingClassAddDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffTrainingClassActionResponseDTO { Success = false, Message = "User is not authenticated." };
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                // Log the request data
                Console.WriteLine($"=== Creating Training Class ===");
                Console.WriteLine($"Name: {staffTrainingClassAddDTO.TrainingClassClassesName}");
                Console.WriteLine($"Type GUID: {staffTrainingClassAddDTO.TrainingClassTypesGUID}");
                Console.WriteLine($"Location GUID: {staffTrainingClassAddDTO.TrainingClassLocationGUID}");
                Console.WriteLine($"Start DateTime: {staffTrainingClassAddDTO.TrainingClassClassesStartDateTime:yyyy-MM-ddTHH:mm:ss.fffZ}");
                Console.WriteLine($"Duration: {staffTrainingClassAddDTO.TrainingClassClassesDuration} minutes");
                Console.WriteLine($"Max: {staffTrainingClassAddDTO.TrainingClassClassesMax}, Min: {staffTrainingClassAddDTO.TrainingClassClassesMin}");
                Console.WriteLine($"Description: {staffTrainingClassAddDTO.TrainingClassClassesDescription}");
                Console.WriteLine($"Access: {staffTrainingClassAddDTO.TrainingClassClassesAccess}");
                Console.WriteLine($"AccessGUIDs: [{string.Join(", ", staffTrainingClassAddDTO.TrainingClassClassesAccessGUIDs)}]");
                
                // Serialize with proper options
                var jsonOptions = new System.Text.Json.JsonSerializerOptions 
                { 
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var json = System.Text.Json.JsonSerializer.Serialize(staffTrainingClassAddDTO, jsonOptions);
                Console.WriteLine($"JSON Payload:\n{json}");
                
                var url = $"{_apiConfiguration.BaseUrl}{StaffCreateSingleClassEndpoint}";
                Console.WriteLine($"Endpoint: {url}");
                
                var response = await _httpClient.PostAsJsonAsync(url, staffTrainingClassAddDTO);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"Response Status: {response.StatusCode}");
                Console.WriteLine($"Response Content: {responseContent}");
                Console.WriteLine($"=== End Create Training Class ===");
                
                if (response.IsSuccessStatusCode)
                {
                    return new StaffTrainingClassActionResponseDTO { Success = true, Message = "Class created successfully." };
                }
                else
                {
                    // Try to parse error details
                    string errorDetail = responseContent;
                    try
                    {
                        var errorObj = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
                        if (errorObj != null && errorObj.ContainsKey("message"))
                        {
                            errorDetail = errorObj["message"].ToString() ?? responseContent;
                        }
                    }
                    catch { }
                    
                    return new StaffTrainingClassActionResponseDTO 
                    { 
                        Success = false, 
                        Message = $"Request failed ({response.StatusCode}): {errorDetail}" 
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating member chip: {ex.Message}");
                return new StaffTrainingClassActionResponseDTO { Success = false, Message = "An error occurred while creating the class." };
            }
        }

        public async Task<StaffTrainingClassAddMultiResponseDTO> StaffCreateMultipleClasses(StaffTrainingClassAddMultiDTO staffTrainingClassAddMultiDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffTrainingClassAddMultiResponseDTO { Success = false, Message = "User is not authenticated." };
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(staffTrainingClassAddMultiDTO,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine($"[StaffCreateMultipleClasses] Sending payload:\n{jsonPayload}");

                var response = await _httpClient.PostAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffCreateMultipleClassesEndpoint}", staffTrainingClassAddMultiDTO);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[StaffCreateMultipleClasses] Response ({response.StatusCode}): {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    // Try to parse message from API response
                    try
                    {
                        var apiResponse = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                        var message = apiResponse.TryGetProperty("message", out var msgProp) ? msgProp.GetString() : "Classes created successfully.";
                        return new StaffTrainingClassAddMultiResponseDTO { Success = true, Message = message ?? "Classes created successfully." };
                    }
                    catch
                    {
                        return new StaffTrainingClassAddMultiResponseDTO { Success = true, Message = "Classes created successfully." };
                    }
                }
                else
                {
                    // Try to parse error message from API response
                    string errorMessage;
                    try
                    {
                        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                        errorMessage = errorResponse.TryGetProperty("message", out var msgProp) ? msgProp.GetString() ?? responseContent : responseContent;
                    }
                    catch
                    {
                        errorMessage = responseContent;
                    }
                    return new StaffTrainingClassAddMultiResponseDTO { Success = false, Message = $"Request failed ({response.StatusCode}): {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating classes: {ex.Message}");
                return new StaffTrainingClassAddMultiResponseDTO { Success = false, Message = "An error occurred while creating the classes." };
            }
        }

        public async Task<StaffTrainingClassActionResponseDTO> StaffEditClass(StaffTrainingClassEditDTO staffTrainingClassEditDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffTrainingClassActionResponseDTO { Success = false, Message = "User is not authenticated." };
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PutAsJsonAsync($"{_apiConfiguration.BaseUrl}{StaffEditClassEndpoint}", staffTrainingClassEditDTO);
                var responseContent = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode
                    ? new StaffTrainingClassActionResponseDTO { Success = true, Message = "Classes created successfully." }
                    : new StaffTrainingClassActionResponseDTO { Success = false, Message = $"Request failed ({response.StatusCode})" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing class: {ex.Message}");
                return new StaffTrainingClassActionResponseDTO { Success = false, Message = "An error occurred while editing the class." };
            }
        }

        public async Task<StaffTrainingClassActionResponseDTO> StaffDeleteClass(string trainingClassClassesGUID)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return new StaffTrainingClassActionResponseDTO { Message = "User is not authenticated." };
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"{_apiConfiguration.BaseUrl}{StaffDeleteClassEndpoint}/{trainingClassClassesGUID}");
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[StaffDeleteClass] Response ({response.StatusCode}): {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    return new StaffTrainingClassActionResponseDTO { Success = true, Message = "Class deleted successfully." };
                }
                else
                {
                    // Parse error message from API (e.g. members enrolled)
                    string errorMessage;
                    try
                    {
                        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseContent);
                        errorMessage = errorResponse.TryGetProperty("message", out var msgProp) ? msgProp.GetString() ?? responseContent : responseContent;
                    }
                    catch
                    {
                        errorMessage = responseContent;
                    }
                    return new StaffTrainingClassActionResponseDTO { Success = false, Message = errorMessage };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting class: {ex.Message}");
                return new StaffTrainingClassActionResponseDTO { Message = "An error occurred while deleting the class." };
            }
        }

        public async Task<StaffTrainingClassGetListResponseDTO> StaffGetClassListAsync(StaffTrainingClassGetDTO filters)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var url = $"{_apiConfiguration.BaseUrl}{StaffGetClassListEndpoint}"
                    .SetQueryParams(new
                    {
                        page = filters.Page,
                        pageSize = filters.PageSize,
                        weekNumber = filters.WeekNumber,
                        fromWeekNumber = filters.FromWeekNumber,
                        toWeekNumber = filters.ToWeekNumber,
                        startDate = filters.StartDate?.ToString("yyyy-MM-dd"),
                        endDate = filters.EndDate?.ToString("yyyy-MM-dd"),
                        trainingClassTypesGUID = filters.TrainingClassTypesGUID,
                        trainingClassLocationGUID = filters.TrainingClassLocationGUID
                    });
              

                var requestUrl = url;
                var response = await _httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainingClassGetListResponseDTO>();
                    return result ?? new StaffTrainingClassGetListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching staff training classes: {errorMessage}");
                    return new StaffTrainingClassGetListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching staff training classes: {ex.Message}");
                return new StaffTrainingClassGetListResponseDTO();
            }
        }

        public async Task<StaffTrainingClassCancelResponseDTO> StaffCancelClassAsync(string classGUID)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var requestUrl = $"{_apiConfiguration.BaseUrl}{StaffCancelClassEndpoint}/{Uri.EscapeDataString(classGUID)}";
                var response = await _httpClient.PostAsync(requestUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainingClassCancelResponseDTO>();
                    return result ?? new StaffTrainingClassCancelResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error cancelling staff class: {errorMessage}");
                    return new StaffTrainingClassCancelResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cancelling staff class: {ex.Message}");
                return new StaffTrainingClassCancelResponseDTO();
            }
        }

        public async Task<StaffTrainerListResponseDTO> GetTrainerListAsync()
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var requestUrl = $"{_apiConfiguration.BaseUrl}{StaffTrainerListEndpoint}";
                var response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StaffTrainerListResponseDTO>();
                    return result ?? new StaffTrainerListResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching trainer list: {errorMessage}");
                    return new StaffTrainerListResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching trainer list: {ex.Message}");
                return new StaffTrainerListResponseDTO();
            }
        }
    }
}
