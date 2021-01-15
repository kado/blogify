using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using BlogifyWebApp.Models.Interfaces;
using BlogifyWebApp.Models.EF;
using BlogifyWebApp.Models.Helpers;

namespace BlogifyWebApp.Models.Providers
{

    public class AuthProvider : IAuthProvider
    {

        private DBBlogifyContext db;
        ILogger<BlogProvider> _logger;

        //2021-01-13 - Kadel D. Lacatt
        //This class provides functions for checking users and retrive Users profiles.
        public AuthProvider(ILogger<BlogProvider> logger)
        {
            //Initialize the logger
            _logger = logger;

            //Initialize the dbcontext
            if (db == null)
            {
                db = new DBBlogifyContext();
            }
        }

        //2021-01-13 - Kadel D. Lacatt
        //Retrives the user profile given a username (string)
        //Returns IUser 
        IUser IAuthProvider.GetUser(string username)
        {
            try
            {
                return db.Users.Find(username);
            }catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return null;
            }
        }

        //2021-01-13 - Kadel D. Lacatt
        //Verify user credentials against db stored credentials
        //Return true if the user is authenticated or false otherwise.
        bool IAuthProvider.VerifyUser(string username, string password)
        {
            try
            {
                IUser user = db.Users.Find(username);
                if (user != null)
                {
                    if (user.Password.Trim() == password)
                    {
                        return true;
                    }else
                    {
                        throw new Exception("Invalid Password.");
                    }

                }else
                {
                    throw new Exception("User does not exist on the db.");
                }
            }catch (Exception ex)
            {
                _logger.LogError(GeneralHelper.GetMessageFromException(this.GetType().ToString(), ex));
                return false;
            }
        }
    }
}
