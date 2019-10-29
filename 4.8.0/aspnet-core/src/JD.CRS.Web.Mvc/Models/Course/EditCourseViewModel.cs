using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Localization;
using JD.CRS.Course.Dto;
using JD.CRS.Entitys;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace JD.CRS.Web.Models.Course
{
    public class EditCourseViewModel
    {
        public CourseDto Course{get;set;}

        public StatusCode? SelectStatusCode{get;set;}

        public List<SelectListItem> GetStatusList(ILocalizationManager localizationManager)
        {
            var list=new List<SelectListItem>();

            list.AddRange(Enum.GetValues(typeof(StatusCode))
                .Cast<StatusCode>()
                .Select(status=>new SelectListItem()
                {
                    Text = localizationManager.GetString(CRSConsts.LocalizationSourceName,$"StatusCode_{status}"),
                    Value = status.ToString(),
                    Selected = status==SelectStatusCode
                })
            );

            return  list;
        }
    }
}
