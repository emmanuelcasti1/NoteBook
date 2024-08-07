using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace NotePad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteContentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DeleteContentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteContent(int id)
        {
            string query = "DELETE FROM Content WHERE IdNote = @id";
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("NoteCone")!);
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            if (rowsAffected > 0)
            {
                return Ok("Content deleted");
            }
            else
            {
                return BadRequest("Content not delete");
            }
        }
    }
}
