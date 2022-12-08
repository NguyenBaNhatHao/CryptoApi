using System.Net;
using AutoMapper;
using System.Text;
using CryptoApi.Profiles;
using Microsoft.AspNetCore.Mvc;
using CryptoApi.Models;
using CryptoApi.Dtos;
using CryptoApi.Datas;
using Newtonsoft.Json;
namespace CryptoApi.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateAddressBCONBOController : ControllerBase{
        public readonly CryptoApiDbContext _context;
        public readonly HttpClient _http;
        private readonly IConfiguration _configuration;
        public GenerateAddressBCONBOController(HttpClient http, CryptoApiDbContext context,IConfiguration configuration){
            _http = http;
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<ActionResult> CreateAddressBCONGO(AddressBCONGO address){
            var config = new MapperConfiguration(cfg =>{
                cfg.AddProfile(new CrytoApiProfile());
            });
            var mapper = config.CreateMapper();
            
            var ApiKey = _configuration.GetValue<string>("ApiKey");
            var addressBCONGODTO = mapper.Map<AddressBCONGO,AddressBCONGODTO>(address);
            string url = "http://localhost:5280/api/GenerateAddress";
            _http.DefaultRequestHeaders.Add("X-API-Key",ApiKey);
            Address address1 = new Address();
            address1.blockchain = address.blockchain;
            address1.network = address.network;
            address1.context = address.data.Item.email;
            address1.data.item.label = address.data.Item.email;
            
            string data = JsonConvert.SerializeObject(addressBCONGODTO);
            HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _http.PostAsync(url, c).GetAwaiter().GetResult();
            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            var sc = HttpStatusCode.OK;
            // if(httpResponse.StatusCode == sc){
            //     address.label = addressBCONGODTO.Data.Item.email;
            //     _context.AddressCrypto.Add(address);
            //     _context.SaveChanges();
            // }
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            return Ok(responseString);                          
        }
    }
}