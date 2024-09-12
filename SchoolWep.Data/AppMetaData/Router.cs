using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.AppMetaData
{
    public static class Router
    {
        public const string SingelId = "{Id}";
        public const string SingelName = "{Name}";

        public const string Root = "Api";
        public const string Version = "V1";
        public const string Role = Root+"/"+Version+"/";


        public static class StudentRouting
        {
            public const string Prefix = Role + "Student/";
            public const string List = Prefix + "List";
            public const string Add = Prefix + "Add";
            public const string Update = Prefix + "Update";
            public const string Pagination = Prefix + "Pagination";
            public const string Delete = Prefix + SingelId;
            public const string GetById = Prefix + SingelId;
        }

        public static class DepartmentRouting
        {
            public const string Prefix = Role + "Department/";
            public const string List = Prefix + "List";
            public const string Add = Prefix + "Add";
            public const string Update = Prefix + "Update";
            public const string Pagination = Prefix + "Pagination";
            public const string Delete = Prefix + SingelId;
            public const string GetById = Prefix + SingelId;
            public const string GetByIdPagination = Prefix + "GetById";
        }
        public static class AuthenticationRouting
        {
            public const string Prefix = Role + " Authentication/";
            public const string Register = Prefix + "Register";
            public const string ConfirmEmail = Prefix + "ConfirmEmail";
            public const string ConfirmOtpResetPass = Prefix + "ConfirmOtpResetPassword";
            public const string Login = Prefix + "Login";
            public const string RefreshToken = Prefix + "Refresh_Token";
          
        }
        public static class AuthorizationRouting
        {
            public const string Prefix = Role + "Authorization/";
            public static class RoleRouting
            {
                public const string SubPrefix = Prefix + "Role/";
                public const string List = SubPrefix + "List";
                public const string Add = SubPrefix + "Add";
                public const string Update = SubPrefix + "Update";
                public const string UpdateCalim = SubPrefix + "Claims/Update";
                public const string Delete = SubPrefix + SingelName;
                public const string GetByName = SubPrefix + SingelName;
            }
            public static class AuthUserRouting
            {
                public const string SubPrefix = Prefix + "AuthUser/";
                public const string GetRoles = SubPrefix + "Roles/"+ SingelId;
                public const string Add = SubPrefix + "Roles/Add";
                public const string Delete = SubPrefix + "Roles/Delete";
                public const string GetClaims = SubPrefix + "Claims/" + SingelId;
                public const string UpdateCalim = SubPrefix + "Claims/Update";

            }

        }

        public static class UserRouting
        {
            public const string Prefix = Role + "User/";
            public const string List = Prefix + "List";
            public const string Update = Prefix + "Update";
            public const string ChangePassword = Prefix + "ChangePassword";
            public const string Pagination = Prefix + "Pagination";
            public const string Delete = Prefix + SingelId;
            public const string GetById = Prefix + SingelId;
            public const string GetByIdPagination = Prefix + "GetById";
            public const string SendOtpToResetPassword = Prefix + "SendOtpToResetPassword";
            public const string ResetPassword = Prefix + "ResetPassword";
        }

    }
}
