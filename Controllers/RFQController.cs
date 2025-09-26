using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Unibouw.Models;
using RestSharp;


namespace Unibouw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RFQController : ControllerBase
    {
        private readonly IConfiguration _config;

        public RFQController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] Email request)
        {
            if (string.IsNullOrEmpty(request.To) || string.IsNullOrEmpty(request.Subject))
                return BadRequest("To and Subject are required");

            var apiKey = _config["Mailgun:ApiKey"];
            var domain = _config["Mailgun:Domain"];
            var fromEmail = _config["Mailgun:FromEmail"];
            var fromName = _config["Mailgun:FromName"];

            var client = new RestClient($"https://api.mailgun.net/v3/{domain}/messages");
            var restRequest = new RestRequest()
                .AddHeader("Authorization", "Basic " +
                    Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"api:{apiKey}")))
                .AddParameter("from", $"{fromName} <{fromEmail}>")
                .AddParameter("to", request.To)
                .AddParameter("subject", request.Subject)
                .AddParameter("text", request.Body)
                .AddParameter("html", request.Body);

            var response = await client.PostAsync(restRequest);

            if (response.IsSuccessful)
                return Ok(new { success = true, message = "Email sent successfully" });
            else
                return StatusCode((int)response.StatusCode, new { success = false, error = response.Content });
        }
    }
}

