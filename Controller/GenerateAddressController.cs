using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using System.Text;
using System.Data;
using CryptoApi.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using CryptoApi.Models;
using System.Collections.Generic;
using CryptoApi.Dtos;
using CryptoApi.Datas;
using Newtonsoft.Json;
using DotNetEnv;

namespace CryptoApi.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateAddressController : ControllerBase{
        public readonly CryptoApiDbContext _context;
        public readonly HttpClient _http;

        public GenerateAddressController(HttpClient http, CryptoApiDbContext context){
            _http = http;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAddress(Address address){
            var config = new MapperConfiguration(cfg =>{
                cfg.AddProfile(new CrytoApiProfile());
            });
            var mapper = config.CreateMapper();
            Env.Load();
            
            var ApiKey = Environment.GetEnvironmentVariable("ApiKey");
            var addressDTO = mapper.Map<Address,AddressDTO>(address);
            string url = "https://rest.cryptoapis.io/wallet-as-a-service/wallets/"+addressDTO.walletid+"/"+addressDTO.blockchain+"/"+addressDTO.network+"/addresses";
            _http.DefaultRequestHeaders.Add("X-API-Key",ApiKey);
            string data = JsonConvert.SerializeObject(addressDTO);
            HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _http.PostAsync(url, c).GetAwaiter().GetResult();
            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            var sc = HttpStatusCode.OK;
            if(httpResponse.StatusCode == sc){
                address.label = addressDTO.data.item.label;
                _context.AddressCrypto.Add(address);
                _context.SaveChanges();
            }
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            return Ok(responseString);                          
        }
        [HttpPost("sendcoin")]
        public async Task<ActionResult> SendCoin (SendCoin sendCoin){
            Env.Load();
            var config = new MapperConfiguration(cfg =>{
                cfg.AddProfile(new CrytoApiProfile());
            });
            var mapper = config.CreateMapper();
            var sendCoinDto = mapper.Map<SendCoin,SendCoinDTO>(sendCoin);
            var ApiKey = Environment.GetEnvironmentVariable("ApiKey");
            string url = "https://rest.cryptoapis.io/wallet-as-a-service/wallets/"+sendCoinDto.walletid+"/"+sendCoinDto.blockchain+"/"+sendCoinDto.network+"/addresses/"+sendCoinDto.address+"/feeless-transaction-requests";
            _http.DefaultRequestHeaders.Add("X-API-Key",ApiKey);
            string data = JsonConvert.SerializeObject(sendCoin);
            HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _http.PostAsync(url, c).GetAwaiter().GetResult();
            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            if((int)httpResponse.StatusCode == 201){
                sendCoin.amount = sendCoinDto.data.item.amount;
                sendCoin.note = sendCoinDto.data.item.note;
                sendCoin.recipientAddress = sendCoinDto.data.item.recipientAddress;
                _context.TransactionRequest.Add(sendCoin);
                _context.SaveChanges();
            }
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            return Ok(responseString);              
        }
    }
}