using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using JD.CRS.Course.Dto;

namespace JD.CRS.Course
{
    public class CourseAppService : AsyncCrudAppService<Entitys.Course, CourseDto, int, GetAllCoursesInput, CreateUpdateCourseDto, CreateUpdateCourseDto>, ICourseAppService
    {
        public CourseAppService(IRepository<Entitys.Course, int> repository):base(repository)
        {
            
        }

        public override async Task<PagedResultDto<CourseDto>> GetAll(GetAllCoursesInput input)
        {
            var query=base.CreateFilteredQuery(input).WhereIf(input.Status.HasValue,t=>t.Status==input.Status.Value);
            var courseCount=query.Count();
            var courseList=query.ToList();
            return  new PagedResultDto<CourseDto>()
            {
                TotalCount = courseCount,
                Items = ObjectMapper.Map<List<CourseDto>>(courseList),
            };
        }

        
    }

}
