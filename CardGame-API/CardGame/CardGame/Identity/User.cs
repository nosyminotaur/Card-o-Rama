using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace CardGame.Identity
{
    /// <summary>
    /// User of the API
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Holds data regarding external logins of the user.
        /// This is a complex class so we will be using an external type convertor
        /// in the OnModelCreating method of the DbContext
        /// </summary>
        public List<ExternalLogin> ExternalInfo { get; set; }
        /// <summary>
        /// Stores name of User.
        /// Used because Identity does not have a default implementation
        /// of the name of a user
        /// </summary>
        public string Name { get; set; }
        /// <summary>
    }
}
