using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.NewsDTO
{
    public class StaffNewsDTO
    {
        [JsonPropertyName("memberNewsGUID")]
        public string MemberNewsGUID { get; set; }

        [JsonPropertyName("memberNewsSubject")]
        public string MemberNewsSubject { get; set; }

        [JsonPropertyName("memberNewsText")]
        public string MemberNewsText { get; set; }

        [JsonPropertyName("memberNewsDate")]
        public DateTime? MemberNewsDate { get; set; }

        [JsonPropertyName("memberNewsCreated")]
        public DateTime? MemberNewsCreated { get; set; }


        public StaffNewsDTO()
        {
            
        }

        public StaffNewsDTO(StaffNewsDTO newsDTO)
        {
            MemberNewsGUID = newsDTO.MemberNewsGUID;
            MemberNewsSubject = newsDTO.MemberNewsSubject;
            MemberNewsText = newsDTO.MemberNewsText;
            MemberNewsDate = newsDTO.MemberNewsDate;
            MemberNewsCreated = newsDTO.MemberNewsCreated;
        }


    }
}
