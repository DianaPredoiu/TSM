using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
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

    public class UserService : IUserService
    {
        //CONTEXT
        private DataContext _context;

        //CONSTRUCTOR
        public UserService(DataContext context)
        {
            _context = context;
        }

        //Authentication function
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
        }

        //GETALL USERS
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        //GET USER BY ID
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        //VERFIY PASSWORD
        public bool VerifyPassword(string password,int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.IdUser == id);

            // check if user exists
            if (user == null)
                return false;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            { Console.WriteLine("pass is bad");  return false; }

                // validation successful

                return true;
        }
        //ADD USER
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
        }

        //UPDATE USER
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
        }

        //DELETE USER
        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

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
                              where u.IdTeam==IdTeam
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
                                 where pm.IdUser==id
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

}//NAMESPACE WebApi.Services