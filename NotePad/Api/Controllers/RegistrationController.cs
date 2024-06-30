using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NotePad.Models;

namespace NotePad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]

        public string registration(Registration registration)
        {
            string query = "INSERT INTO Registration(Username, Password) VALUES (@username, @password)";

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("NoteCone")!.ToString());
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", registration.username);
            cmd.Parameters.AddWithValue("@password", registration.password);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                return "Data inserted";
            }
            else
            {
                return "Error";
            }
           
        }
    }
}
