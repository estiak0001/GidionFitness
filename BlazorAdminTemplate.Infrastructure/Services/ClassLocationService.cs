using BlazorAdminTemplate.Application.DTOs;
using BlazorAdminTemplate.Application.DTOs.Authentication;
using BlazorAdminTemplate.Application.DTOs.Contracts;
using BlazorAdminTemplate.Application.DTOs.Location;
using BlazorAdminTemplate.Application.DTOs.ClassTypes;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class ClassLocationService : IClassLocationService
    {
        public readonly ApiConfiguration _configuration;
        public readonly HttpClient _httpClient;
        public ITokenService _tokenService;
        private const string staffTrainingClassLocation = "/StaffTrainingclassLocation/TrainingclassLocationList";
        private const string staffTrainingClassNewLocation = "/StaffTrainingclassLocation/TrainingclassLocationAdd";
        private const string staffTrainingClassDeleteLocation = "/StaffTrainingclassLocation/TrainingclassLocationDelete/";
        private const string staffTrainingClassUpdateLocation = "/StaffTrainingclassLocation/TrainingclassLocationEdit";

        private const string staffTrainingClassType = "/StaffTrainingclassTypes/TrainingclassTypeList";
        private const string staffTrainingClassNewType = "/StaffTrainingclassTypes/TrainingclassTypeAdd";
        private const string staffTrainingClassDeleteType = "/StaffTrainingclassTypes/TrainingclassTypeDelete/";
        private const string staffTrainingClassUpdateType = "/StaffTrainingclassTypes/TrainingclassTypeEdit";

        public ClassLocationService(ApiConfiguration configuration, HttpClient httpClient, ITokenService tokenService)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _tokenService = tokenService;
        }
        #region Location
        public async Task<ResponseDTO<List<StaffTrainingClassLocationDTO>>> GetStaffTrainingClassLocationListAsync(int page, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{_configuration.BaseUrl}{staffTrainingClassLocation}?page={page}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var contract = await response.Content.ReadFromJsonAsync<ResponseDTO<List<StaffTrainingClassLocationDTO>>>();
                    contract?.Initialize();
                    return contract ?? new ResponseDTO<List<StaffTrainingClassLocationDTO>>();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ResponseDTO<List<StaffTrainingClassLocationDTO>>();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error fetching contract: {ex.Message}");
                return new ResponseDTO<List<StaffTrainingClassLocationDTO>>();
            }
        }

        public async Task<StaffTrainingClassNewLocationResponseDTO> PostStaffTrainingClassNewLocationAsync(AddNewLocationDTO newClassName)
        {

            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{_configuration.BaseUrl}{staffTrainingClassNewLocation}", newClassName);
                if (response.IsSuccessStatusCode)
                {
                    var contract = await response.Content.ReadFromJsonAsync<StaffTrainingClassNewLocationResponseDTO>();
                    return contract ?? new StaffTrainingClassNewLocationResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassNewLocationResponseDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Adding new Location: {ex.Message}");
                return new StaffTrainingClassNewLocationResponseDTO();
            }
        }

        public async Task<StaffTrainingClassDeleteResponseDTO> DeleteStaffTrainingClassLocationAsync(StaffTrainingClassLocationDTO staffTrainingClassLocationDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"{_configuration.BaseUrl}{staffTrainingClassDeleteLocation}{staffTrainingClassLocationDTO.TrainingClassLocationGUID}");
                if (response.IsSuccessStatusCode)
                {
                    return new StaffTrainingClassDeleteResponseDTO()
                    {
                        Message = "Delete successful"
                    };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorMessage);
                    return new StaffTrainingClassDeleteResponseDTO()
                    {
                        Message = $"Delete failed {response.StatusCode} - {errorMessage}"
                    };

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Deleteing Location: {ex.Message}");
                return new StaffTrainingClassDeleteResponseDTO();
            }
        }

        public async Task<StaffTrainingClassUpdateResponseDTO> UpdateStaffTrainingClassLocationAsync(StaffTrainingClassLocationDTO StaffTrainingClassLocationDTO)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync($"{_configuration.BaseUrl}{staffTrainingClassUpdateLocation}", StaffTrainingClassLocationDTO);
                if (response.IsSuccessStatusCode)
                {
                    StaffTrainingClassUpdateResponseDTO updateResponseDTO = await response.Content.ReadFromJsonAsync<StaffTrainingClassUpdateResponseDTO>();
                    return updateResponseDTO ?? new StaffTrainingClassUpdateResponseDTO();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new StaffTrainingClassUpdateResponseDTO();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Editing Location: {ex.Message}");
                return new StaffTrainingClassUpdateResponseDTO();
            }
        }
        #endregion



        #region Type
        public async Task<ResponseDTO<List<StaffTrainingClassTypeDTO>>> GetStaffTrainingClassTypeListAsync(int page, int pageSize)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"{_configuration.BaseUrl}{staffTrainingClassType}?page={page}&pageSize={pageSize}");
                if (response.IsSuccessStatusCode)
                {
                    var contract = await response.Content.ReadFromJsonAsync<ResponseDTO<List<StaffTrainingClassTypeDTO>>>();
                    contract?.Initialize();
                    return contract ?? new ResponseDTO<List<StaffTrainingClassTypeDTO>>();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ResponseDTO<List<StaffTrainingClassTypeDTO>>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching Types: {ex.Message}");
                return new ResponseDTO<List<StaffTrainingClassTypeDTO>>();

            }


           
        }

        public async Task<ResponseDTO<StaffTrainingClassTypeDTO>> PostStaffTrainingClassNewTypeAsync(StaffTrainingClassTypeDTO newClassType)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{_configuration.BaseUrl}{staffTrainingClassNewType}", newClassType);
                if (response.IsSuccessStatusCode)
                {
                    var contract = await response.Content.ReadFromJsonAsync<ResponseDTO<StaffTrainingClassTypeDTO>>();
                    return contract ?? new ResponseDTO<StaffTrainingClassTypeDTO>();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ResponseDTO<StaffTrainingClassTypeDTO>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Adding new Type: {ex.Message}");
                return new ResponseDTO<StaffTrainingClassTypeDTO>();
            }

        }

        public async Task<ResponseDTO<StaffTrainingClassTypeDTO>> DeleteStaffTrainingClassTypeAsync(StaffTrainingClassTypeDTO ClassType)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync($"{_configuration.BaseUrl}{staffTrainingClassDeleteType}{ClassType.TrainingClassTypesGUID}");
                if (response.IsSuccessStatusCode)
                {
                    return new ResponseDTO<StaffTrainingClassTypeDTO>()
                    {
                        Message = "Delete successful"
                    };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorMessage);
                    return new ResponseDTO<StaffTrainingClassTypeDTO>()
                    {
                        Message = $"Delete failed {response.StatusCode} - {errorMessage}"
                    };
                }


            }
            catch (Exception ex)
            {


                Console.WriteLine($"Error Deleteing Type: {ex.Message}");
                return new ResponseDTO<StaffTrainingClassTypeDTO>();
            }


        }

        public async Task<ResponseDTO<StaffTrainingClassTypeDTO>> UpdateStaffTrainingClassTypeAsync(StaffTrainingClassTypeDTO ClassType)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("User is not authenticated.");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PutAsJsonAsync($"{_configuration.BaseUrl}{staffTrainingClassUpdateType}", ClassType);
                if (response.IsSuccessStatusCode)
                {
                    return new ResponseDTO<StaffTrainingClassTypeDTO>()
                    {
                        Message = "Delete successful"
                    };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorMessage);
                    return new ResponseDTO<StaffTrainingClassTypeDTO>()
                    {
                        Message = $"Delete failed {response.StatusCode} - {errorMessage}"
                    };
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Editing Type: {ex.Message}");
                return new ResponseDTO<StaffTrainingClassTypeDTO>();
            }
        }
        #endregion
    }
}
