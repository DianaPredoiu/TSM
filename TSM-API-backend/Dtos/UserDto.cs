
namespace WebApi.Dtos
{
    public class UserDto
    {
       
        public int IdUser { get; set; }

       
        public int IdRole { get; set; }

        
        public int IdTeam { get; set; }

       
        public string Username { get; set; }

        
        public string Password { get; set; }

           
        public string Email { get; set; }

        
        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

    }//CLASS UserDto

}//NAMESPACE WebApi.Dtos