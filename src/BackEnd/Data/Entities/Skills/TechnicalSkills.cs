using BackEnd.Shared.DataShared;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data.Entities.Skills
{
    public class TechnicalSkills : BaseEntity
    {
        public string Title { get; set; }
        public int SkillPercentage { get; set; }
        public string Icon { get; set; } = "fas fa-code";
        public bool IsActive { get; set; }
        public SkillTypes SkillTypes { get; set; }

        public TechnicalSkills(string title, int skillPercentage, string icon, SkillTypes skillTypes,
            bool isActive)
        {
            Title = title;
            SkillPercentage = skillPercentage;
            Icon = icon;
            SkillTypes = skillTypes;
            IsActive = isActive;
        }
        public void Edit(string title, int skillPercentage, string icon, SkillTypes skillTypes, 
            bool isActive)
        {
            Title = title;
            SkillPercentage = skillPercentage;
            Icon = icon;
            SkillTypes = skillTypes;
            IsActive = isActive;
        }

    }
    public enum SkillTypes
    {
        [Display(Name ="فرانت اند")]
        BackEnd,
        [Display(Name ="بک اند")]
        FrontEnd,
        [Display(Name ="دوآپس")]
        DevOps,
        [Display(Name ="پایگاه داده")]
        DataBase,
        [Display(Name ="سایر")]
        Others
    }
}
