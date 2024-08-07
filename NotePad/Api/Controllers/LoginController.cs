using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NotePad.Models;
using System.Data;

namespace NotePad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]

        public IActionResult Login(Registration login)
        {

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("NoteCone")!.ToString());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Registration WHERE Username = " +
                "'"+login.username+ "' AND Password = '"+login.password+"' ", connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                return Ok("Valid User");
            }
            else
            {
                return BadRequest("Invalid User");
            }
        }
        
    }
}
