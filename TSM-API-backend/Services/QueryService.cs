using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    internal class HobbyIdComparer : IEqualityComparer<Hobby>
    {
        public bool Equals(Hobby x, Hobby y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.IdHobby == y.IdHobby && x.HobbyName == y.HobbyName&& x._HobbyType==y._HobbyType;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Hobby hobby)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(hobby, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashHobbyName = hobby.HobbyName == null ? 0 : hobby.HobbyName.GetHashCode();

            int hashHobbyType = hobby._HobbyType == null ? 0 : hobby._HobbyType.GetHashCode();

            //Get hash code for the Code field.
            int hashHobbyId = hobby.IdHobby.GetHashCode();

            //Calculate the hash code for the product.
            return hashHobbyName ^ hashHobbyType ^ hashHobbyId;
        }
    }
    public interface IQueryService
    {
        IEnumerable<Hobby> getList(int id);

        IEnumerable<Hobby> getOptions(int id);

        IEnumerable<User> getUsers(int id);

    }//INTERFACE IQueryService
    public class QueryService : IQueryService
    {
        //CONTEXT
        private DataContext _context;

        //CONTRUCTOR
        public QueryService(DataContext context)
        {
            _context = context;
        }

        //GET HOBBIES LIST FOR SPECIFIED USER
        public IEnumerable<Hobby> getList(int id)
        {
            var users = _context.Users;
            var hobbies = _context.Hobbies;
            var user_hobbies = _context.User_Hobbies;

            var userHobbiesList = from uh in user_hobbies
                                  join u in users on uh.UserId equals u.IdUser
                                  join h in hobbies on uh.HobbyId equals h.IdHobby
                                  where uh.UserId==id
                                  select new Hobby
                                  {
                                      IdHobby = h.IdHobby,
                                      HobbyName = h.HobbyName,
                                      _HobbyType = h._HobbyType
                                  };

            return userHobbiesList;
        }

        //GET HOBBIES OPTIONS FOR SPECIFIED USER
        public IEnumerable<Hobby> getOptions(int id)
        {

            IEnumerable<Hobby> hobbyList = this.getList(id);
            IEnumerable<Hobby> hobbies = _context.Hobbies;

            IEnumerable<Hobby> optionsList = hobbies.Except(hobbyList, new HobbyIdComparer());

            return optionsList;

        }

        public IEnumerable<User> getUsers(int id)
        {
            var users = _context.Users;
            var hobbies = _context.Hobbies;
            var user_hobbies = _context.User_Hobbies;

            var userHobbiesList = from uh in user_hobbies
                                  join u in users on uh.UserId equals u.IdUser
                                  join h in hobbies on uh.HobbyId equals h.IdHobby
                                  where uh.HobbyId == id
                                  select new User
                                  {
                                      IdUser = u.IdUser,
                                      Username = u.Username,
                                      FirstName=u.FirstName,
                                      LastName=u.LastName
                                  };

            return userHobbiesList;
        }

    }//CLASS QueryService

}//NAMENSPACE WebApi.Services
