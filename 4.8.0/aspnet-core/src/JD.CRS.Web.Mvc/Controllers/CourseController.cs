using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using JD.CRS.Authorization;
using JD.CRS.Controllers;
using JD.CRS.Course;
using JD.CRS.Course.Dto;
using JD.CRS.Web.Models.Course;
using Microsoft.AspNetCore.Mvc;

namespace JD.CRS.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Course)]
    public class CourseController : CRSControllerBase
    {
        private  readonly  ICourseAppService _courseAppService;

        const  int MaxNum=10;

        public CourseController(ICourseAppService courseAppService)
        {
            this._courseAppService=courseAppService;
        }
       
        public async Task<ActionResult> Index(GetAllCoursesInput input)
        {
            //异步获取所有的course
          var courses=(await _courseAppService.GetAll(new GetAllCoursesInput(){Status = input.Status,KeyWord = input.KeyWord})).Items;

          var model=new CourseListViewModel()
          {
              Courses = courses,
              //源代码讲解到此的时候没有添加这一句,导致一刷新就显示全部
              SelectStatusCode = input.Status,
              KeyWord = input.KeyWord,
          };
            return  View(model);
        }
        public  async  Task<ActionResult> EditCourseModal(int courseId)
        {
            var course=await  _courseAppService.Get(new EntityDto<int>(courseId));
            var model=new EditCourseViewModel()
            {
                Course = course
            };
            return  View("_EditCourseModal",model);
        }

    }
}