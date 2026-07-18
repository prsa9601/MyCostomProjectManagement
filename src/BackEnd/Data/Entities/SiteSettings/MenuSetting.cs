using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.SiteSettings
{
    public class MenuSetting : BaseEntity
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public ImplementationStyle ImplementationStyle { get; set; } = ImplementationStyle.RightToLeft;

        public MenuSetting(string name, int sequence, ImplementationStyle implementationStyle)
        {
            Name = name;
            Sequence = sequence;
            this.ImplementationStyle = implementationStyle;
        }
        
        public void Edit(string name, int sequence, ImplementationStyle implementationStyle)
        {
            Name = name;
            Sequence = sequence;
            this.ImplementationStyle = implementationStyle;
        }


    }
    public enum ImplementationStyle
    {
        RightToLeft,
        LeftToRight
    }
}
