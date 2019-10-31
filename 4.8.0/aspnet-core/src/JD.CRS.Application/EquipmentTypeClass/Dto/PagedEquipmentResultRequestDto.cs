using Abp.Application.Services.Dto;

namespace JD.CRS.EquipmentType.Dto
{
    public class PagedEquipmentResultRequestDto:PagedResultRequestDto
    {
        public  string Name{get;set;}

        public  string Code{get;set;}

        public bool? IsActive { get; set; }
    }
}