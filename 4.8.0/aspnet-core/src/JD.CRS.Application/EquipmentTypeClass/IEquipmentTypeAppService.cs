using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JD.CRS.EquipmentType.Dto;

namespace JD.CRS.EquipmentType
{
    public interface IEquipmentTypeAppService:IAsyncCrudAppService<EquipmentTypeDto,int,
        PagedEquipmentResultRequestDto,CreateUpdateEquipmentTypeDto,CreateUpdateEquipmentTypeDto>
    {
        
    }
}