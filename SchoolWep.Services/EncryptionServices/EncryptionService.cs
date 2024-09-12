using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Infrustructure.Encryption
{
    public class EncryptionService
    {
        private readonly IDataProtector _dataProtector;

        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("SIsInJvbGUiOlsiU3VwZXJBZG1pbiIsIlVzZXIiXSwiUGVybWlzc2lvbiI6WyJQZXJtaXNzaW9ucy5TdHVkZW50LlZpZXciLCJQZXJtaXNzaW9ucy5TdHVkZW50LkNyZWF0ZSIsIlBlcm1pc3Npb25zLlN0dWRlbnQuRWRpdCIsIlBlcm1pc3Npb25zLlN0dWRlbnQuRGVsZXRlIiwiUGVybWlzc2lvbnMuRGVwYXJ0bWVudC5WaWV3IiwiUGVybWlzc2lvbnMuRGVwYXJ0bWVudC5DcmVhdGUiLCJQZXJtaXNzaW9ucy5EZXBhcnRtZW50LkVkaXQiLCJQZXJtaXNzaW9ucy5EZXBhcnRtZW50LkRlbGV0ZSIsIlBlcm1pc3Npb2");
        }

        public string Encrypt(string plaintext)
        {
            return _dataProtector.Protect(plaintext);
        }

        public string Decrypt(string encryptedText)
        {
            return _dataProtector.Unprotect(encryptedText);
        }
    }
}
