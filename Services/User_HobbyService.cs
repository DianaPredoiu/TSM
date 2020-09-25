using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IUser_HobbyService
    {
        IEnumerable<User_Hobby> GetAll();
        User_Hobby Get(int id);
        void Add(User_Hobby entity);
        void Update(User_Hobby entity);
        void Delete(int id);

    }//INTERFACE IUser_HobbyService
    public class User_HobbyService: IUser_HobbyService
    {
        //CONTEXT
        readonly DataContext _userhobbyContext;

        //CONSTRUCTOR
        public User_HobbyService(DataContext context)
        {
            _userhobbyContext = context;
        }

        //ADD
        public void Add(User_Hobby entity)
        {
            _userhobbyContext.User_Hobbies.Add(entity);
            _userhobbyContext.SaveChanges();
        }

        //DELETE
        public void Delete(int id)
        {
            var user_hobby = _userhobbyContext.User_Hobbies.Find(id);

            _userhobbyContext.User_Hobbies.Remove(user_hobby);
            _userhobbyContext.SaveChanges();

        }

        //GET
        public User_Hobby Get(int id)
        {
            return _userhobbyContext.User_Hobbies.Find(id);
        }

        //GETALL
        public IEnumerable<User_Hobby> GetAll()
        {
            return _userhobbyContext.User_Hobbies.ToList();
        }

        //UPDATE
        public void Update(User_Hobby entity)
        {
            var _userhobby = _userhobbyContext.User_Hobbies.Find(entity.Id);

            if (_userhobby == null)
                throw new AppException("Hobby not found");

            if (_userhobby.HobbyId != entity.HobbyId)
            {
                if (_userhobbyContext.User_Hobbies.Any(x => x.HobbyId == entity.HobbyId) && _userhobbyContext.User_Hobbies.Any(x => x.UserId == entity.UserId))
                    throw new AppException("This combination is already taken");
            }

            _userhobby.HobbyId = entity.HobbyId;
            _userhobby.UserId = entity.UserId;
            _userhobbyContext.User_Hobbies.Update(_userhobby);
            _userhobbyContext.SaveChanges();
        }

    }//CLASS User_HobbyService

}//NAMESPACE WebApi.Services
