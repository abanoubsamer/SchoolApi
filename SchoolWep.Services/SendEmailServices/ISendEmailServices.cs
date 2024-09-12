using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.SendEmailServices
{
    public interface ISendEmailServices
    {
        public Task<bool> SendConfirmEmail(string EmailTo,string linkConfirm);
        public Task<bool> SendOTPResetPassword(string EmailTo,string OTP);

    }
}
