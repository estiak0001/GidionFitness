using BlazorAdminTemplate.Application.DTOs.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IImageUploadService
    {
        Task<MemberImageUploadRequestDTO> PrepareImageFromBrowserFileAsync(IBrowserFile browserFile, long maxFileSize = 5 * 1024 * 1024);

        MemberImageUploadRequestDTO PrepareImageFromBytes(byte[] imageBytes, string contentType, string? fileName = null);

        bool IsValidImageType(string contentType);

        bool HasValidImageData(MemberImageUploadRequestDTO dto);

        Task<MemberImageUploadResponseDTO> UploadMemberImageAsync(MemberImageUploadRequestDTO request);
        Task<MemberImageUploadResponseDTO> UploadMemberImageOnMemberGuidAsync(MemberImageUploadRequestDTO request, string memberGuid);
    }
}
