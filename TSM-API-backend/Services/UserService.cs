/*********************************************************************************
 * \file 
 * 
 * UserService.cs file contains the UserService class, which is included in 
 * Services namespace.
 * 
 ********************************************************************************/

//list of namespcaes used in UserService class
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
//list of namespaces


namespace WebApi
{
    /*****************************************************************************************************************************
     * 
     * \namespace
     * 
     * Services namespace is included in WebApi namespace and contains all the classes and functions that represent database
     * manipulation(CRUD operations, queries etc..) made for each specific model.The concept of dependency injection is used in 
     * every class of this namespace.
     * 
     * For more details go to: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1
     * 
     *****************************************************************************************************************************/
    namespace Services
    {
        /***********************************************************
         * 
         * \interface
         * 
         * IUserService interface is included in Services namespace
         * and contains all the signatures of the methods used in 
         * UserService class.
         * 
         **********************************************************/
        public interface IUserService
        {
            User Authenticate(string username, string password);
            IEnumerable<User> GetAll();
            User GetById(int id);
            User Create(User user, string password);
            void Update(User user, string password = null);
            void Delete(int id);
            bool VerifyPassword(string password, int id);
            IEnumerable<User> GetAllByIdTeam(int id);
            IEnumerable<User> GetAllByIdProject(int id);


        }//INTERFACE IUserService

        /*******************************************************
        * 
        * \class
        * 
        * UserService class implements all the methods declared 
        * in IUserService inteface.
        * 
        ******************************************************/
        public class UserService : IUserService
        {
            //CONTEXT
            private DataContext _context; //!< The _context filed is the dependency of UserService class.

            /*******************************************************
             * 
             * UserService Constructor takes a DataContext variable
             * as parameter and executes the injection of the service.
             * 
             ******************************************************/
            public UserService(DataContext context)
            {
                _context = context;

            }//CONSTRUCTOR

            /***********************************************************************************
             * 
             * Authenticate method:
             *     + Return type: User.
             *     + @param username: first argument,type string.
             *     + @param password: second argument, type string.
             *     + It is used when the user logs in the application. 
             *       Verifies if the parameters are null, and then if the user 
             *       and password coincide with the data form the database.If 
             *       authentication is successful it returns the user.
             *     + Exceptions: + One or both arguments are null: returns null.
             *                   + Does not find the username in the database: returns null.
             *                   + The password is incorrect: returns null.
             * 
             * 
             ***********************************************************************************/
            public User Authenticate(string username, string password)
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return null;

                var user = _context.Users.SingleOrDefault(x => x.Username == username);

                // check if username exists
                if (user == null)
                    return null;

                // check if password is correct
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                // authentication successful
                return user;
                 
            }//METHOD Authenticate

            /*********************************************************
             * 
             * GetAll method:
             *   + Returns a list of all the registered users.
             *   + Return type: IEnumerable type list of User objects.
             *   + No parameters.
             *   + No exceptions.
             * 
             ********************************************************/
            public IEnumerable<User> GetAll()
            {
                return _context.Users;

            }//METHOD GetAll

            /*********************************************************
             * 
             * GetById method:
             *   + Returns a user by id.
             *   + Return type: User.
             *   + @param id: first argument, type int.
             *   + No exceptions.
             * 
             ********************************************************/
            public User GetById(int id)
            {
                return _context.Users.Find(id);

            }//METHOD GetById

            /****************************************************************
             * 
             * VerifyPassword method:
             *   + It is used in a specific situation in the front-end app. 
             *   + Return type: bool.
             *   + @param password: first argument, type string
             *   + @param id: second argument, type int.
             *   + Exceptions: + If user is not found: returns false.
             *                 + If password is not correct: returns false.
             * 
             ****************************************************************/
            public bool VerifyPassword(string password, int id)
            {
                var user = _context.Users.SingleOrDefault(x => x.IdUser == id);

                // check if user exists
                if (user == null)
                    return false;

                // check if password is correct
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                { Console.WriteLine("pass is bad"); return false; }

                // validation successful

                return true;

            }//METHOD VerifyPassword

            /****************************************************************
             * 
             * Create method:
             *   + It creates a new user and adds it to the database. 
             *   + Return type: User.
             *   + @param user: first argument, type User.
             *   + @param password: second argument, type string.
             *   + Exceptions: + If password is null: throws AppException.
             *                 + If username is taken: throws AppException.
             * 
             ****************************************************************/
            public User Create(User user, string password)
            {
                // validation
                if (string.IsNullOrWhiteSpace(password))
                    throw new AppException("Password is required");

                if (_context.Users.Any(x => x.Username == user.Username))
                    throw new AppException("Username \"" + user.Username + "\" is already taken");

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.IsActive = true;
                user.IsAdmin = false;

                _context.Users.Add(user);
                _context.SaveChanges();

                return user;

            }//METHOD Create

            /****************************************************************
              * 
              * Update method:
              *   + It updates a user in the database. 
              *   + Return type: void.
              *   + @param userParam: first argument, type User.
              *   + @param password: second argument, type string.
              *   + Exceptions: + If user doest not exist : throws AppException.
              *                 + If username is taken: throws AppException.
              * 
              ****************************************************************/
            public void Update(User userParam, string password = null)
            {
                var user = _context.Users.Find(userParam.IdUser);

                if (user == null)
                    throw new AppException("User not found");

                if (userParam.Username != user.Username)
                {
                    // username has changed so check if the new username is already taken
                    if (_context.Users.Any(x => x.Username == userParam.Username))
                        throw new AppException("Username " + userParam.Username + " is already taken");
                }

                // update user properties
                user.Email = userParam.Email;
                user.IdRole = userParam.IdRole;
                user.Username = userParam.Username;
                user.IsActive = userParam.IsActive;
                user.IdTeam = userParam.IdTeam;
                user.IsAdmin = userParam.IsAdmin;

                // update password if it was entered
                if (!string.IsNullOrWhiteSpace(password))
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                _context.Users.Update(user);
                _context.SaveChanges();

            }//METHOD Update

            /****************************************************************
              * 
              * Delete method:
              *   + It deletes a user in the database. 
              *   + Return type: void.
              *   + @param id: first argument, type int.
              *   + No exceptions.
              * 
              ****************************************************************/
            public void Delete(int id)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }

            }//METHOD Delete

            /*****************************************************************************************
              * 
              * CreatePasswordHash method:
              *   + It encrypts a string. 
              *   + Return type: void.
              *   + @param password: first argument, type string-the string that is to be encrypted.
              *   + @param passwordHash: second argument, type byte-the encrypted password.
              *   + @param password: first argument, type string-the key.
              *   + Exceptions: + If password is null: throw ArgumentNullException.
              * 
              ****************************************************************************************/
            private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
            {
                if (password == null) throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }

            }//METHOD CreatePasswordHash


            /*****************************************************************************************
              * 
              * VerifyPasswordHash method:
              *   + It verifies if a string coincides with a stroed hash and a stored salt. 
              *   + Return type: bool.
              *   + @param password: first argument, type string-the string that is to be verified.
              *   + @param passwordHash: second argument, type byte-the stored hash.
              *   + @param password: first argument, type string-the stored salt.
              *   + Exceptions: + If password is null: throw ArgumentNullException.
              * 
              ****************************************************************************************/
            private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
            {
                if (password == null) throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
                if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
                if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

                using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    for (int i = 0; i < computedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i]) return false;
                    }
                }

                return true;
            }

            public IEnumerable<User> GetAllByIdTeam(int IdTeam)
            {
                var users = _context.Users;
                var teams = _context.Teams;

                var teamMembers = from u in users
                                  join t in teams on u.IdTeam equals t.IdTeam
                                  where u.IdTeam == IdTeam
                                  select new User
                                  {
                                      IdUser = u.IdUser,
                                      IdRole = u.IdRole,
                                      IdTeam = u.IdTeam,
                                      Email = u.Email,
                                      IsActive = u.IsActive,
                                      IsAdmin = u.IsAdmin,
                                      Username = u.Username,

                                  };

                return teamMembers;
            }

            public IEnumerable<User> GetAllByIdProject(int id)
            {
                var users = _context.Users;
                var projects = _context.Projects;
                var timesheetActivity = _context.TimesheetActivities;
                var timesheet = _context.Timesheets;
                var projectManager = _context.ProjectManagers;
                var projAssign = _context.ProjectAssignments;

                var projectMembers = from pm in projectManager
                                     join p in projects on pm.IdProject equals p.IdProject
                                     join pa in projAssign on pm.IdProject equals pa.IdProject
                                     join u in users on pa.IdUser equals u.IdUser
                                     where pm.IdUser == id
                                     select new User
                                     {
                                         IdUser = u.IdUser,
                                         IdRole = u.IdRole,
                                         IdTeam = u.IdTeam,
                                         Email = u.Email,
                                         IsActive = u.IsActive,
                                         IsAdmin = u.IsAdmin,
                                         Username = u.Username,

                                     };

                return projectMembers;
            }


        }//CLASS UserService
    }
    

}//NAMESPACE WebApi.Services