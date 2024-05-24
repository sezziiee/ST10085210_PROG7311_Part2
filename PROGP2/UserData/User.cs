using Microsoft.EntityFrameworkCore;
using PROGP2.Models;

namespace PROGP2.UserData
{
    
    public class User
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();
        int userID { get; set; }
        public string Username { get; set; }
        string password { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public User()
        {


        }

        public User( string username, string password)
        {
            
            Username = username;
            this.password = password;
          
        }
        public void deleteUser()
        {
            var user = context.Users.Where(x => x.UserId == userID).FirstOrDefault();
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
    

}
