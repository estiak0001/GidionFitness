using BlazorAdminTemplate.Application.DTOs.Authentication;
using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Domain.Configuration;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class ImageUploadService : IImageUploadService
    {

        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;
        private const string ImageUploadEndpoint = "/ImageUpload/upload";
        private const string ImageUploadWithMemberGuidEndpoint = "/ImageUpload/member/";

        private static readonly string[] SupportedImageTypes = new[]
        {
            "image/jpeg",
            "image/jpg",
            "image/png",
            "image/gif",
            "image/webp"
        };

        public ImageUploadService(HttpClient httpClient, ApiConfiguration apiConfiguration)
        {
            _httpClient = httpClient;
            _apiConfiguration = apiConfiguration;
        }


        public async Task<MemberImageUploadRequestDTO> PrepareImageFromBrowserFileAsync(IBrowserFile browserFile, long maxFileSize = 5242880)
        {
            if(browserFile == null)
                throw new ArgumentNullException(nameof(browserFile));

            if (browserFile.Size > maxFileSize)
                throw new InvalidOperationException($"File size exceeds the maximum limit of {maxFileSize} bytes.");

            if(!IsValidImageType(browserFile.ContentType))
                throw new InvalidOperationException($"Unsupported image type: {browserFile.ContentType}");

            using var stream = browserFile.OpenReadStream(maxFileSize);
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            var fileBytes = memoryStream.ToArray();

            return new MemberImageUploadRequestDTO
            {
                ImageFile = Convert.ToBase64String(fileBytes)
            };
        }

        public MemberImageUploadRequestDTO PrepareImageFromBytes(byte[] imageBytes, string contentType, string? fileName = null)
        {
            if(imageBytes == null || imageBytes.Length == 0)
                throw new ArgumentException("Image bytes cannot be null or empty.", nameof(imageBytes));

            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException("Content type is required.", nameof(contentType));

            if (!IsValidImageType(contentType))
                throw new InvalidOperationException($"Content type '{contentType}' is not supported.");

            return new MemberImageUploadRequestDTO
            {
                ImageFile = Convert.ToBase64String(imageBytes)
            };
        }

        public bool IsValidImageType(string contentType)
        {
            if(string.IsNullOrWhiteSpace(contentType))
                return false;

            return SupportedImageTypes.Contains(contentType.ToLowerInvariant());
        }

        public bool HasValidImageData(MemberImageUploadRequestDTO dto)
        {
            return dto != null &&
                !string.IsNullOrWhiteSpace(dto.ImageFile);

        }

        public async Task<MemberImageUploadResponseDTO> UploadMemberImageAsync(MemberImageUploadRequestDTO request)
        {
            try
            {
                if (!HasValidImageData(request))
                    throw new ArgumentException("Invalid image data provided.", nameof(request));

                

                // Convert base64 back to bytes for binary upload
                var imageBytes = Convert.FromBase64String(request.ImageFile);

                using var formData = new MultipartFormDataContent();
                var byteContent = new ByteArrayContent(imageBytes);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                formData.Add(byteContent, "imageFile", "image.jpg");

                var response = await _httpClient.PostAsync($"{_apiConfiguration.BaseUrl}{ImageUploadEndpoint}", formData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<MemberImageUploadResponseDTO>();
                    Console.WriteLine($"Image uploaded successfully: {result?.ImageUrl}");

                    // Ensure Success property is set if the DTO has one
                    if (result != null)
                    {
                        
                        return result;
                    }

                    return new MemberImageUploadResponseDTO
                    {
                        Message = "Upload successful"
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Image upload failed: {response.StatusCode} - {errorContent}");
                    return new MemberImageUploadResponseDTO
                    {
                        Message = $"Upload failed: {response.StatusCode} - {errorContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while uploading the image: {ex.Message}");
                return new MemberImageUploadResponseDTO
                {
                    Message = $"An error occurred while uploading the image: {ex.Message}"
                };
            }
        }

        public async Task<MemberImageUploadResponseDTO> UploadMemberImageOnMemberGuidAsync(MemberImageUploadRequestDTO request, string memberGuid)
        {
            try
            {
                if (!HasValidImageData(request))
                    throw new ArgumentException("Invalid image data provided.", nameof(request));



                // Convert base64 back to bytes for binary upload
                var imageBytes = Convert.FromBase64String(request.ImageFile);

                using var formData = new MultipartFormDataContent();
                var byteContent = new ByteArrayContent(imageBytes);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                formData.Add(byteContent, "imageFile", "image.jpg");

                var response = await _httpClient.PostAsync($"{_apiConfiguration.BaseUrl}{ImageUploadWithMemberGuidEndpoint}{memberGuid}", formData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<MemberImageUploadResponseDTO>();
                    Console.WriteLine($"Image uploaded successfully: {result?.ImageUrl}");

                    // Ensure Success property is set if the DTO has one
                    if (result != null)
                    {
                        // Note: The Success property might not exist in the actual DTO based on your interface signatures
                        // If you need it, you'll need to add it to the DTO
                        return result;
                    }

                    return new MemberImageUploadResponseDTO
                    {
                        Message = "Upload successful"
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Image upload failed: {response.StatusCode} - {errorContent}");
                    return new MemberImageUploadResponseDTO
                    {
                        Message = $"Upload failed: {response.StatusCode} - {errorContent}"
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while uploading the image: {ex.Message}");
                return new MemberImageUploadResponseDTO
                {
                    Message = $"An error occurred while uploading the image: {ex.Message}"
                };
            }
        }
    }
}
