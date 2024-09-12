using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Helper
{
    public static class Helpers
    {

        public const string Permission = "Permission";


        public enum EPermissionModuleName
        {
            Student,
            Department,
            Roles,
            Registers,
            User,
            Permission,
        }
    }
}
