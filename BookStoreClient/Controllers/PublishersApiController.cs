using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using OdataBookStore.entity;

namespace OdataBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PublishersApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<ActionResult<List<Publisher>>> GetPublishers()
        {
            var publishers = await _httpClient.GetFromJsonAsync<List<Publisher>>("https://localhost:7159/odata/PublishersOData");
            return publishers ?? new List<Publisher>();
        }
    }
}
