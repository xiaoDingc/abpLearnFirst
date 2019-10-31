using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace JD.CRS.BaseModel
{
    public class BaseEntity:FullAuditedEntity<int>,IPassivable
    {
        public const int MaxCodeLength = 64;
        public const int MaxNameLength = 256;
        public const int MaxBriefNameLength = 64;
        public const int MaxRemarkLength = 64;

        [Required]
        [StringLength(MaxCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public string Name { get; set; }

        [StringLength(64)]
        public string BriefName { get; set; }

        [StringLength(512)]
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}