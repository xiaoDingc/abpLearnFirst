using Abp.AutoMapper;
using JD.CRS.BaseModel;

namespace JD.CRS.EquipmentType.Dto
{
    [AutoMapTo(typeof(Entitys.EquipmentType))]
    public class CreateUpdateEquipmentTypeDto:BaseEntityDto
    {
        
    }
}