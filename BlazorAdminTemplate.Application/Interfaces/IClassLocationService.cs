using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAdminTemplate.Application.DTOs.Location;
using BlazorAdminTemplate.Application.DTOs.ClassTypes;
using BlazorAdminTemplate.Application.DTOs;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IClassLocationService
    {
        #region Location
        Task<ResponseDTO<List<StaffTrainingClassLocationDTO>>> GetStaffTrainingClassLocationListAsync(int page,int pageSize);
        Task<StaffTrainingClassNewLocationResponseDTO> PostStaffTrainingClassNewLocationAsync(AddNewLocationDTO newClassName);
        Task<StaffTrainingClassDeleteResponseDTO> DeleteStaffTrainingClassLocationAsync(StaffTrainingClassLocationDTO staffTrainingClassLocationDTO);
        Task<StaffTrainingClassUpdateResponseDTO> UpdateStaffTrainingClassLocationAsync(StaffTrainingClassLocationDTO StaffTrainingClassLocationDTO);
        #endregion

        #region Type
        Task<ResponseDTO<List<StaffTrainingClassTypeDTO>>> GetStaffTrainingClassTypeListAsync(int page, int pageSize);
        Task<ResponseDTO<StaffTrainingClassTypeDTO>> PostStaffTrainingClassNewTypeAsync(StaffTrainingClassTypeDTO newClassType);
        Task<ResponseDTO<StaffTrainingClassTypeDTO>> DeleteStaffTrainingClassTypeAsync(StaffTrainingClassTypeDTO ClassType);
        Task<ResponseDTO<StaffTrainingClassTypeDTO>> UpdateStaffTrainingClassTypeAsync(StaffTrainingClassTypeDTO ClassType);


        #endregion


    }
}
