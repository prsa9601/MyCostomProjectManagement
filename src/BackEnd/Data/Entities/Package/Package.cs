using BackEnd.Shared.DataShared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data.Entities.Package
{
    public class Package : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Badge Badge { get; set; }

        public DateTime ExpiresAt { get; set; }
        public List<string> Features { get; set; }
    }

    public enum Badge
    {
        [Display(Name = "محبوب")]
        Popular = 0,

        [Display(Name = "جدید")]
        IsNew = 1,
        
        [Display(Name = "پیشنهادی")]
        Recomended = 2
    }
}
