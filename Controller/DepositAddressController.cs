using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using CryptoApi.Models;
using Newtonsoft.Json;
using DotNetEnv;

namespace CryptoApi.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositAddressController : ControllerBase{

        public readonly HttpClient _http;

        public DepositAddressController(HttpClient http){
            _http = http;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAddress(Address address){
            Env.Load();
            var walletid = Environment.GetEnvironmentVariable("walletId");
            var blockchain = Environment.GetEnvironmentVariable("blockchain");
            var network = Environment.GetEnvironmentVariable("network");
            var ApiKey = Environment.GetEnvironmentVariable("ApiKey");
            string url = "https://rest.cryptoapis.io/wallet-as-a-service/wallets/"+walletid+"/"+blockchain+"/"+network+"/addresses";
            _http.DefaultRequestHeaders.Add("X-API-Key",ApiKey);
            string data = JsonConvert.SerializeObject(address);
            HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _http.PostAsync(url, c).GetAwaiter().GetResult();
            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            return Ok(responseString);                          
        }
    }
}