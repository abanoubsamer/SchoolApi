using SchoolWep.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Constans
{
    public static class Permission
    {

        public static List<string> GeneratePermissionsFormModule(string module)
        {
            return new List<string>
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
        public static List<string> PermissionList()
        {
            var AllPermission = new List<string>();
            foreach (var item in Enum.GetValues(typeof(Helpers.EPermissionModuleName)))
            {
                AllPermission.AddRange(GeneratePermissionsFormModule(item.ToString()));
            }
            return AllPermission;
        }
        public static class Studnet
        {
            public const string View = "Permissions.Studnet.View";
            public const string Create = "Permissions.Studnet.Create";
            public const string Edit = "Permissions.Studnet.Edit";
            public const string Delete = "Permissions.Studnet.Delete";
        }
        public static class Department
        {
            public const string View = "Permissions.Department.View";
            public const string Create = "Permissions.Department.Create";
            public const string Edit = "Permissions.Department.Edit";
            public const string Delete = "Permissions.Department.Delete";
        }
        public static class User
        {
            public const string View = "Permissions.User.View";
            public const string Create = "Permissions.User.Create";
            public const string Edit = "Permissions.User.Edit";
            public const string Delete = "Permissions.User.Delete";
        }
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
        }
        public static class Permissions
        {
            public const string View = "Permissions.Permission.View";
            public const string Create = "Permissions.Permission.Create";
            public const string Edit = "Permissions.Permission.Edit";
            public const string Delete = "Permissions.Permission.Delete";
        }

    }
}
