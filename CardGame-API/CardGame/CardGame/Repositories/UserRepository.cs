using CardGame.ExceptionService;
using CardGame.Identity;
using CardGame.Interfaces;
using CardGame.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<User> _manager;
        public UserRepository(UserManager<User> manager)
        {
            _manager = manager;
        }

        public async Task GoogleLogin(string idToken, string username, string googleEmail)
        {
            var payload = await JwtHelper.VerifyGoogleTokenAsync(idToken);

            if (payload == null)
                throw new ApiException(Exceptions.InvalidGoogleToken, System.Net.HttpStatusCode.BadRequest, "Could not verify Google idToken");

            if (payload.Email != googleEmail)
                throw new ApiException(Exceptions.GoogleEmailDoesNotMatch, System.Net.HttpStatusCode.BadRequest, "Emails do not match!");

            User duplicateUser = await _manager.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (duplicateUser != null)
                throw new ApiException(Exceptions.UsernameExists, System.Net.HttpStatusCode.BadRequest, "Username exists!");

            duplicateUser = await _manager.Users.FirstOrDefaultAsync(x => x.Email == googleEmail);

            //This time, also check if the user already has registered with a google account with this email ID
            //If yes, then the user is already registered and we only need to check the ID Token to ensure that
            //the login was correct and then send a JWT
            if (duplicateUser != null)
            {
                //To check if user was registered previously with Google
                var result = duplicateUser.ExternalInfo.FirstOrDefault(x => x.LoginProvider == "Google");
                if (result != null)
                {
                    return;
                }

                //User exists with some other login provider
                //Add Google as login provider, verify token and then send JWT
                var externalLogin = duplicateUser.ExternalInfo;
                externalLogin.Add(new ExternalLogin
                {
                    LoginProvider = "Google",
                    LoginProderKey = payload.Subject
                });

                duplicateUser.ExternalInfo = externalLogin;
                await _manager.UpdateAsync(duplicateUser);
            }

            //Control reaches here if user is new
            User newUser = new User
            {
                UserName = username,
                Email = googleEmail,
                Id = payload.Subject,
                Name = payload.Name,
                ExternalInfo = new List<ExternalLogin>
                {
                    new ExternalLogin
                    {
                        LoginProvider = "Google",
                        LoginProderKey = payload.Subject
                    }
                }
            };

            await _manager.CreateAsync(newUser);
        }
    }
}
