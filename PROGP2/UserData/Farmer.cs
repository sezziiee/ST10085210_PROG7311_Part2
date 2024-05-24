using PROGP2.Models;

namespace PROGP2.UserData
{
    
    public class Farmer : User
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();
        List<Products> products = new List<Products>();

        public Farmer(string username, string password, string roleID) : base( username, password)
        {

        }

        public void addProducts()
        {
            //products.Add()
        } 
    }
}
