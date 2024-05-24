using PROGP2.Models;

namespace PROGP2.UserData
{

    public class Products 
    {
        public Products(string productID, string productName, string description, string categoryID, DateOnly productionDate)
        {
            this.productID = productID;
            this.productName = productName;
            this.description = description;
            this.categoryID = categoryID;
            this.productionDate = productionDate;
        }

        string productID {  get; set; }
       string productName {  get; set; }
       string description { get; set; }
        string categoryID { get; set; }
        DateOnly productionDate { get; set; }

       
    }
}
