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

        [HttpPost("registration")]
        public IActionResult RegisterUser([FromBody] Registration registration)
        {
            string query = "INSERT INTO Registration(Username, Password) VALUES (@username, @password)";

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("NoteCone")!))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", registration.username);
                cmd.Parameters.AddWithValue("@password", registration.password);
                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    return Ok("Data inserted");
                }
                else
                {
                    return BadRequest("Usuario no registrado");
                }
            }
        }
    }
}
