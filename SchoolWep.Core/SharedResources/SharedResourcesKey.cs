using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.SharedResources
{
    public static class SharedResourcesKey
    {

        public static class Operations
        {
            public const string Deleted = "Deleted";
            public const string Updated = "Updated";
            public const string Added = "Added";
            public const string Successed = "Successed";
        }
     
      

       
        //Validation
        public static class Valdiation
        {
            public const string ValidationError = "ValidationError";
            public const string NotNull = "NotNull";
            public const string MaxLength = "MaxLength";
            public const string MinLength = "MaxLength";
            public const string Required = "Required";
            public const string NotFound = "NotFound";
            public const string Must_Start_With_uppercase_Letter= "Must_Start_With_uppercase_Letter";
            public const string Must_Be_More_Than_18_Years= "Must_Be_More_Than_18_Years";
            public const string Must_Be_More_Than= "Must_Be_More_Than";
            public const string Range_Between= "Range_Between";
            public const string Student_Is_Exist = "Student_Is_Exist";
            public const string Department_Is_Exist = "Department_Is_Exist";
            public const string Invalid = "Invalid";
            public const string PasswordsDoNotMatch = "PasswordsDoNotMatch";
            public const string Email_Is_Already_Exist = "Email_Is_Already_Exist";
            public const string UserName_Is_Already_Exist = "UserName_Is_Already_Exist";
        }

    }
}
