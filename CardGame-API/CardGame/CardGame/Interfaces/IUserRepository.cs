using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// This method logs in a user using Google credentials as an external provider
        /// This is the only method required to login/signup a user using Google
        /// </summary>
        /// <param name="idToken"></param>
        /// <param name="username"></param>
        /// <param name="googleEmail"></param>
        /// <returns></returns>
        Task GoogleLogin(string idToken, string username, string googleEmail);
    }
}
