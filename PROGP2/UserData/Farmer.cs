using PROGP2.Models;

namespace PROGP2.UserData
{
    
    public class Farmer : User
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();
        List<Products> products = new List<Products>();

        public Farmer(int userID, string username, string password) : base( username, password)
        {

        }

        public void addProducts()
        {
            //products.Add()
        } 
    }
}
