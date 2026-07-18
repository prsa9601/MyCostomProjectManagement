using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.Role
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<RolePermission>? RolePermissions { get; set; } = new();


        private Role()
        {
            
        }
        public Role(string name, List<RolePermission>? rolePermissions)
        {
            Name = name;
            if (rolePermissions.Count() > 0)
            {
                rolePermissions.ForEach(i => i.RoleId = Id);
                RolePermissions = rolePermissions;
            }
        }

        public void Edit(string name, List<RolePermission>? rolePermissions)
        {
            Name = name;
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
