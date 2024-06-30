using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NotePad.Models;

namespace NotePad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditContentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EditContentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("edit")]

        public string EditContent(Content content)
        {
            string query = "UPDATE Content SET Title = @title, Body = @body WHERE IdNote = @idNote";
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("NoteCone")!.ToString());
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@idNote", content.idNote);
            cmd.Parameters.AddWithValue("@title", content.title);
            cmd.Parameters.AddWithValue("@body", content.body);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                return "Content updated successfully";
            }
            else
            {
                return "Error";
            }

        }
    }
}

