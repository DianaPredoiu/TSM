using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IHobbyService
    {
        IEnumerable<Hobby> GetAll();
        Hobby Get(int id);
        void Add(Hobby entity);
        void Update(Hobby entity);
        void Delete(int id);

    }//INTERAFACE IHobbyService
    public class HobbyService: IHobbyService
    {
        //CONTEXT
        readonly DataContext _hobbyContext;

        //CONSTRUCTOR
        public HobbyService(DataContext context)
        {
            _hobbyContext = context;
        }

        //ADD
        public void Add(Hobby entity)
        {
            _hobbyContext.Hobbies.Add(entity);
            _hobbyContext.SaveChanges();
        }

        //DELETE
        public void Delete(int id)
        {
            var hobby = _hobbyContext.Hobbies.Find(id);

            _hobbyContext.Hobbies.Remove(hobby);
            _hobbyContext.SaveChanges();

        }

        //GET
        public Hobby Get(int id)
        {
            return _hobbyContext.Hobbies.Find(id);
        }

        //GETALL
        public IEnumerable<Hobby> GetAll()
        {
            return _hobbyContext.Hobbies.ToList();
        }

        //UPDATE
        public void Update(Hobby entity)
        {
            var _hobby = _hobbyContext.Hobbies.Find(entity.IdHobby);

            if (_hobby == null)
                throw new AppException("Hobby not found");

            if (_hobby.HobbyName != entity.HobbyName)
            {
                if (_hobbyContext.Hobbies.Any(x => x.HobbyName == entity.HobbyName))
                    throw new AppException("Username " + entity.HobbyName + " is already listed");
            }

            _hobby.HobbyName = entity.HobbyName;
            _hobby._HobbyType = entity._HobbyType;
            _hobbyContext.Hobbies.Update(_hobby);
            _hobbyContext.SaveChanges();
        }

    }//CLASS HobbyService

}//NAMESAPCE WebApi.Services
