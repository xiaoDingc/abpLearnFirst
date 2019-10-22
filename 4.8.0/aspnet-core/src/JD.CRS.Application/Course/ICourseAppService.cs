using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JD.CRS.Course.Dto;

namespace JD.CRS.Course
{
     
   public interface ICourseAppService:IAsyncCrudAppService<CourseDto,
       int
       , GetAllCoursesInput//获取课程的时候用于分页
       ,CreateUpdateCourseDto
       ,CreateUpdateCourseDto>
   {

   }
  
}
