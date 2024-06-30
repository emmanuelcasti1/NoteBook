using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NotePad.Models;

namespace NotePad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateContentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CreateContentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("createContent")]

        public string CreateContent(Content content)
        {
            string query = "INSERT INTO Content (Title,Body) VALUES (@title,@body)";
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("NoteCone")!.ToString());
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", content.title);
            cmd.Parameters.AddWithValue("@body", content.body);
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
