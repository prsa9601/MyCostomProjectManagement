using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.Role
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }
        public string Icon { get; set; } = "fa-crown";
        public string IconColorCode { get; set; } = "#CCFFCC";
        public string? Description { get; set; }
        public bool IsDefault { get; set; } = false;

        private Role()
        {
            RolePermissions = new();
        }
      
        public void ChangeVisibilityIsDefault(bool isDefault)
        {
            IsDefault = isDefault;
        }
        public Role(string name, List<RolePermission>? rolePermissions,
            string icon, string iconColorCode, string description)
        {
            Name = name;
            Icon = icon;
            IconColorCode = iconColorCode;
            Description = description;
            if (rolePermissions.Count() > 0)
            {
                rolePermissions.ForEach(i => i.RoleId = Id);
                RolePermissions = rolePermissions;
            }
        }

        public void Edit(string name, List<RolePermission>? rolePermissions, 
            string icon, string iconColorCode, string description)
        {
            Name = name;
            Icon = icon;
            IconColorCode = iconColorCode;
            Description = description;
            if (rolePermissions.Count() > 0)
            {
                rolePermissions.ForEach(i => i.RoleId = Id);
                RolePermissions = rolePermissions;
            }
        }

        public void SetRolePermission(List<RolePermission> rolePermissions)
        {
            RolePermissions = rolePermissions;
        }

    }
}
