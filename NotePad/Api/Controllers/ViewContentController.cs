using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NotePad.Models;

namespace NotePad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewContentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ViewContentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("view")]

        public List<Content> ViewContent()
        {
            string query = "SELECT * FROM Content";
            List<Content> contents = new List<Content>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("NoteCone")!.ToString()))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Content content = new Content
                        {
                            idNote =reader.GetInt32(reader.GetOrdinal("IdNote")),
                            title = reader.GetString(reader.GetOrdinal("Title")),
                            body = reader.GetString(reader.GetOrdinal("Body"))
                        };
                        contents.Add(content);
                    }
                }
            }
            return contents;
        }
    }
}
