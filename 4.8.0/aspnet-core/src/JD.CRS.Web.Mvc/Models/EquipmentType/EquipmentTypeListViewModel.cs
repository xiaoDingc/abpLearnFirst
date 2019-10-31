using System.Collections.Generic;
using JD.CRS.EquipmentType.Dto;

namespace JD.CRS.Web.Models.EquipmentType
{
    public class EquipmentTypeListViewModel
    {
        public  IReadOnlyList<EquipmentTypeDto> EquipmentTypeDtos{get;set;}
    }
}