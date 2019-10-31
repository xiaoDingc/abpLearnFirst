using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Castle.Core.Internal;
using JD.CRS.Authorization;
using JD.CRS.EquipmentType.Dto;
using Microsoft.EntityFrameworkCore;


namespace JD.CRS.EquipmentType
{
     [AbpAuthorize(PermissionNames.Pages_Course)]
    public class EquipmentTypeAppService : AsyncCrudAppService<Entitys.EquipmentType,
        EquipmentTypeDto,
        int,
        PagedEquipmentResultRequestDto,
    CreateEquipmentTypeDto, UpdateEquipmentTypeDto>, IEquipmentTypeAppService
    {
        
         private readonly IRepository<Entitys.EquipmentType> _equipmentTypeRepository;

        public EquipmentTypeAppService(IRepository<Entitys.EquipmentType> equipmentTypeRepository)
            : base(equipmentTypeRepository)
        {
            _equipmentTypeRepository = equipmentTypeRepository;

            LocalizationSourceName = JD.CRS.CRSConsts.LocalizationSourceName;
        }

        public override async Task<EquipmentTypeDto> Get(EntityDto<int> input)
        {
            var equipmentType = await _equipmentTypeRepository
                .GetAsync(input.Id);

            return ObjectMapper.Map<EquipmentTypeDto>(equipmentType);
        }

        public override async Task<PagedResultDto<EquipmentTypeDto>> GetAll(PagedEquipmentResultRequestDto input)
        {
            var equipmentTypes = Repository.GetAll()
                .WhereIf(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name))
                .WhereIf(!input.Code.IsNullOrEmpty(), x => x.Code.Contains(input.Code))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                .OrderBy(x => x.Code);

            return await Task.FromResult(new PagedResultDto<EquipmentTypeDto>(equipmentTypes.Count(),
                ObjectMapper.Map<List<EquipmentTypeDto>>(equipmentTypes)
            ));
        }

        public  async Task<EquipmentTypeDto> Update(EquipmentTypeDto input)
        {
            var entity = await _equipmentTypeRepository.GetAsync(input.Id);

            ObjectMapper.Map(input, entity);

            await _equipmentTypeRepository.UpdateAsync(entity);

            return MapToEntityDto(entity);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            var entity = await _equipmentTypeRepository.GetAsync(input.Id);
            await _equipmentTypeRepository.DeleteAsync(entity);
        }

        public async Task<EquipmentTypeDto> Create(UpdateEquipmentTypeDto input)
        {
           //判断CODE是否已存在
            var model = await Repository.GetAllIncluding().FirstOrDefaultAsync(x => x.Code == input.Code);
            if (model != null)
            {
                throw new UserFriendlyException(L("EquipmentType code already exists"));
            }

            //检查是否已被软删除，已经软删除的数据，无法通过
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                //判断CODE是否已存在
                var model0 = await Repository.GetAllIncluding().FirstOrDefaultAsync(x => x.Code == input.Code);
                if (model0 != null)
                {
                    throw new UserFriendlyException(L("EquipmentType code is deleted"));
                }
            }

            var entity = ObjectMapper.Map<Entitys.EquipmentType>(input);
            await _equipmentTypeRepository.InsertAsync(entity);
            return MapToEntityDto(entity);
        }
    }
}