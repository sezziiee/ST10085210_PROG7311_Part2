using Microsoft.AspNetCore.Mvc;
using PROGP2.Models;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgriEnergyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();
        [HttpPost]
        public string PostLogin(Login login)
        {
            if (login != null)
            {
                //add login logic with try catch 
                var userlogged = context.Users.Where(x => x.Username == login.username && x.Password == login.password).FirstOrDefault();

                if (userlogged != null)
                {
                    return "success";
                }
                else return "invalid stuff!!!";
            }
            else return "error!!!";
        }
    }
}
