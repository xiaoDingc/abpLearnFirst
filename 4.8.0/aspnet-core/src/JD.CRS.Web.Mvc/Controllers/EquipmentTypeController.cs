using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using JD.CRS.Authorization;
using JD.CRS.Controllers;
using JD.CRS.EquipmentType;
using JD.CRS.EquipmentType.Dto;
using JD.CRS.Web.Models.Course;
using JD.CRS.Web.Models.EquipmentType;
using Microsoft.AspNetCore.Mvc;

namespace JD.CRS.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages_Department)]
    public class EquipmentTypeController : CRSControllerBase
    {
        private  readonly  IEquipmentTypeAppService _equipmentTypeAppService;

        public EquipmentTypeController(IEquipmentTypeAppService iEquipmentTypeAppService)
        {
            _equipmentTypeAppService=iEquipmentTypeAppService;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            //_equipmentTypeAppService.Create()
            var modelDto=(await _equipmentTypeAppService.GetAll(new PagedEquipmentResultRequestDto(){MaxResultCount = int.MaxValue})
                ).Items;
            var viewModel=new EquipmentTypeListViewModel()
            {
                EquipmentTypeDtos=modelDto,
            };

            return View(viewModel);
        }

        public  async  Task<IActionResult> EditEquipmentTypeModal(int EquipmentTypeId)
        {
            Logger.Debug("good");
             var equipmentTypes=await  _equipmentTypeAppService.Get(new EntityDto<int>(EquipmentTypeId));
            var model=new EditEquipmentTypeViewModel()
            {
                EquipmentTypeDto = equipmentTypes,
            };
            return  View("_EditEquipmentTypeModal",model);
        }

    }
}