using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Castle.Core.Internal;
using JD.CRS.Authorization;
using JD.CRS.Course.Dto;

namespace JD.CRS.Course
{
     [AbpAuthorize(PermissionNames.Pages_Course)]
    public class CourseAppService : AsyncCrudAppService<Entitys.Course, CourseDto, int, GetAllCoursesInput, CreateUpdateCourseDto, CreateUpdateCourseDto>, ICourseAppService
    {
        private readonly ICacheManager cacheManager;
        public CourseAppService(IRepository<Entitys.Course, int> repository,ICacheManager _cacheManager):base(repository)
        {
            cacheManager=_cacheManager;
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<CourseDto>> GetAll(GetAllCoursesInput input)
        {
        //       var query = base.CreateFilteredQuery(input)
        //.WhereIf(input.Status.HasValue, t => t.Status == input.Status.Value)
        //.WhereIf(
        //!input.Keyword.IsNullOrEmpty(), t =>
        //t.Code.ToLower().Contains((input.Keyword ?? string.Empty).ToLower()) //按编号查询
        //|| t.DepartmentCode.ToLower().Contains((input.Keyword ?? string.Empty).ToLower()) //按院系编号查询
        //|| t.Name.ToLower().Contains((input.Keyword ?? string.Empty).ToLower()) //按名称查询
        //|| t.Credits.ToString().Contains((input.Keyword ?? string.Empty).ToLower()) //按学分查询
        //|| t.Remarks.ToLower().Contains((input.Keyword ?? string.Empty).ToLower()) //按备注查询
        //);

        var query=base.CreateFilteredQuery(input).WhereIf(input.Status.HasValue,t=>t.Status==input.Status.Value)
            .WhereIf(!input.KeyWord.IsNullOrEmpty(),t=>t.Code.ToLower().Contains((input.KeyWord ?? String.Empty).ToLower())//按编号查询
          ||t.DepartmentCode.ToLower().Contains((input.KeyWord ?? string.Empty).ToLower())//按院系编号查找
            || t.Name.ToLower().Contains((input.KeyWord ?? String.Empty).ToLower()) //按名称查询
          || t.Credits.ToString().Contains((input.KeyWord ?? String.Empty)) //按学分查询
         || t.Remarks.ToLower().Contains((input.KeyWord ?? String.Empty).ToLower()) //按备注查询
            );
        //  var query=base.CreateFilteredQuery(input).WhereIf(input.Status.HasValue,t=>t.Status==input.Status.Value);
            var courseCount=query.Count();
            var courseList=query.ToList();
            return  new PagedResultDto<CourseDto>()
            {
                TotalCount = courseCount,
                //Items = ObjectMapper.Map<List<CourseDto>>(courseList),
                Items=ObjectMapper.Map<List<CourseDto>>(courseList),
            };
        }

        
    }

}
