using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularInventoryManagement.AuthenticationManagement.Business
{
    class CheckLoginActivity : AuthenticationActivity
    {
        // 1st param is username, 2nd param is plaintext password
        public override async Task<object> PerformAsync(params object[] inputParams)
        {
            var username = inputParams[0] as string;
            var password = inputParams[1] as string;
            var matchingUser = await userRepository.FindFirst(user => user.Username == username);
            if (matchingUser is null) return null;
            var isValidPass = OpenBsdBCrypt.CheckPassword(matchingUser.Password, password.ToCharArray());
            if (!isValidPass) return null;
            return matchingUser.Role.RolePermission?
                    .Select(rolePermission => rolePermission.Permission)
                    .ToList();
        }
    }
}
