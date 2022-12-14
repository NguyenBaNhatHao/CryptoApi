using System.Net;
using AutoMapper;
using System.Text;
using CryptoApi.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptoApi.Models;
using CryptoApi.Dtos;
using Microsoft.Extensions.Configuration;
using CryptoApi.Datas;
using Newtonsoft.Json;
using DotNetEnv;
using Microsoft.Data.SqlClient;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;

namespace CryptoApi.Controller{
    [Route("api/[controller]")]
    [ApiController]
    public class SendCoinController : ControllerBase{
        public readonly CryptoApiDbContext _context;
        public readonly HttpClient _http;
        private readonly IConfiguration _configuration;

        public SendCoinController(HttpClient http, CryptoApiDbContext context,IConfiguration configuration){
            _http = http;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> SendCoin (SendCoin sendCoin){
            Env.Load();
            var config = new MapperConfiguration(cfg =>{
                cfg.AddProfile(new CrytoApiProfile());
            });
            string network="";
            var mapper = config.CreateMapper();
            var sendCoinDto = mapper.Map<SendCoin,SendCoinDTO>(sendCoin);
            var ApiKey = _configuration.GetValue<string>("ApiKey");
            var walletid = _configuration.GetValue<string>("walletid");
            var SymbolCode = new SqlParameter("@SymbolCoin", sendCoin.currencycode);
            using (var cmd = _context.Database.GetDbConnection().CreateCommand()) {
                cmd.CommandText = "sp_api_currencycode";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                cmd.Parameters.Add(SymbolCode);
                var reader = cmd.ExecuteReader();
                while(reader.Read()){
                    if(sendCoinDto.blockchain.Equals(reader[2].ToString())){
                        sendCoinDto.blockchain = reader[1].ToString();
                        network = reader[3].ToString();
                    }else{
                        continue;
                    }
                }
                cmd.Connection.Close();
            }

            string url = "https://rest.cryptoapis.io/wallet-as-a-service/wallets/"+walletid+"/"+sendCoinDto.blockchain+"/"+network+"/addresses/"+sendCoinDto.address+"/feeless-transaction-requests";
            _http.DefaultRequestHeaders.Add("X-API-Key",ApiKey);
            string data = JsonConvert.SerializeObject(sendCoinDto);
            HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _http.PostAsync(url, c).GetAwaiter().GetResult();
            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            // if((int)httpResponse.StatusCode == 201){
            //     sendCoin.amount = sendCoinDto.data.item.amount;
            //     sendCoin.note = sendCoinDto.data.item.note;
            //     sendCoin.recipientAddress = sendCoinDto.data.item.recipientAddress;
            //     _context.TransactionRequest.Add(sendCoin);
            //     _context.SaveChanges();
            // }
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            return Ok(responseString);              
        }
    }
}