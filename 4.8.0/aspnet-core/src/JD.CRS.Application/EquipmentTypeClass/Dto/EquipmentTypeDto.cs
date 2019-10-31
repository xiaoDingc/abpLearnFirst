using Abp.AutoMapper;
using JD.CRS.BaseModel;

namespace JD.CRS.EquipmentType.Dto
{
    [AutoMapFrom(typeof(Entitys.EquipmentType))]
    public class EquipmentTypeDto:BaseEntityDto
    {
        
    }
}