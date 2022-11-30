using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using CryptoApi.Models;

namespace CryptoApi.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositAddressController : ControllerBase{

        public readonly HttpClient _http;

        public DepositAddressController(HttpClient http){
            _http = http;
        }

        [HttpPost]
        public ActionResult GetDiemdanh(Address address){

            string url = "https://rest.cryptoapis.io";
            // var authenticationString = $"{identifer}:{key}";
            // var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            // _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic",base64EncodedAuthenticationString);
            // _http.DefaultRequestHeaders.Add("X-Crisp-Tier","plugin");
            return null;                          
        }
    }
}