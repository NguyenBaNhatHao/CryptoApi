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

namespace CryptoApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateAddressController : ControllerBase{
        public readonly CryptoApiDbContext _context;
        public readonly HttpClient _http;
        private readonly IConfiguration _configuration;

        public GenerateAddressController(HttpClient http, CryptoApiDbContext context,IConfiguration configuration){
            _http = http;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("testnetwork")]
        public async Task<ActionResult> CreateAddress(AddressParameter address){
            var config = new MapperConfiguration(cfg =>{
                cfg.AddProfile(new CrytoApiProfile());
            });
            var mapper = config.CreateMapper();
            string network="";
            var ApiKey = _configuration.GetValue<string>("ApiKey");
            var walletid = _configuration.GetValue<string>("walletid");
            // var Listnetwork = _configuration.GetSection("listNetWork");
            // if(address.currencycode == "BTC"){
            //     network = Listnetwork.GetValue<string>("testnet");
            // }
            var SymbolCode = new SqlParameter("@SymbolCoin", address.currencycode);
            var addressDTO = mapper.Map<AddressParameter,AddressDTO>(address);
            using (var cmd = _context.Database.GetDbConnection().CreateCommand()) {
                cmd.CommandText = "sp_api_currencycode";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                cmd.Parameters.Add(SymbolCode);
                var reader = cmd.ExecuteReader();
                while(reader.Read()){
                    if(addressDTO.blockchain.Equals(reader[2].ToString())){
                        addressDTO.blockchain = reader[1].ToString();
                        network = reader[3].ToString();
                    }else{
                        continue;
                    }
                }
                cmd.Connection.Close();
            }

            
            using (var cmd = _context.Database.GetDbConnection().CreateCommand()) {
                cmd.CommandText = "sp_api_GetEmail";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                while(reader.Read()){
                    if(addressDTO.data.item.label.Equals(reader[0].ToString())){
                        return Ok(addressDTO);
                    }else{
                        continue;
                    }
                }
                cmd.Connection.Close();
            }
            string url = "https://rest.cryptoapis.io/wallet-as-a-service/wallets/"+walletid+"/"+addressDTO.blockchain+"/"+network+"/addresses";
            _http.DefaultRequestHeaders.Add("X-API-Key",ApiKey);
            string data = JsonConvert.SerializeObject(addressDTO);
            HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _http.PostAsync(url, c).GetAwaiter().GetResult();
            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            var sc = HttpStatusCode.OK;
            if(httpResponse.StatusCode == sc){
                address.email = addressDTO.data.item.label;
                _context.AddressCrypto.Add(address);
                _context.SaveChanges();
            }
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(responseString);
            string requestId = jObject["requestId"].Value<string>();
            string addresses = jObject["data"]["item"]["address"].Value<string>();
            string createdTimestamp = jObject["data"]["item"]["createdTimestamp"].Value<string>();
            string email = jObject["data"]["item"]["label"].Value<string>();
            var dictionary = new OrderedDictionary();
            dictionary.Add("requestId",requestId);
            dictionary.Add("address",addresses);
            dictionary.Add("createdTimestamp",createdTimestamp);
            dictionary.Add("email",email);
            var jsonData = JsonConvert.SerializeObject( dictionary, Formatting.Indented );
            Console.WriteLine(jsonData);
            return Ok(jsonData);                          
        }

        [HttpPost("mainnetwork")]
        public async Task<ActionResult> CreateAddressMain(AddressParameter address){
            var config = new MapperConfiguration(cfg =>{
                cfg.AddProfile(new CrytoApiProfile());
            });
            var mapper = config.CreateMapper();
            string network="";
            var ApiKey = _configuration.GetValue<string>("ApiKey");
            var walletid = _configuration.GetValue<string>("MainWallet");
            var Listnetwork = _configuration.GetSection("listNetWork");
            network = Listnetwork.GetValue<string>("mainnet");
            
            var SymbolCode = new SqlParameter("@SymbolCoin", address.currencycode);
            var MainNet = new SqlParameter("@Network", network);
            var addressDTO = mapper.Map<AddressParameter,AddressDTO>(address);
            using (var cmd = _context.Database.GetDbConnection().CreateCommand()) {
                cmd.CommandText = "sp_api_currencycode_mainnet";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                cmd.Parameters.Add(SymbolCode);
                cmd.Parameters.Add(MainNet);
                var reader = cmd.ExecuteReader();
                while(reader.Read()){
                    if(addressDTO.blockchain.Equals(reader[2].ToString())){
                        addressDTO.blockchain = reader[1].ToString();
                        network = reader[3].ToString();
                    }else{
                        continue;
                    }
                }
                cmd.Connection.Close();
            }

            
            using (var cmd = _context.Database.GetDbConnection().CreateCommand()) {
                cmd.CommandText = "sp_api_GetEmail";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (cmd.Connection.State != System.Data.ConnectionState.Open) cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                while(reader.Read()){
                    if(addressDTO.data.item.label.Equals(reader[0].ToString())){
                        return Ok(addressDTO);
                    }else{
                        continue;
                    }
                }
                cmd.Connection.Close();
            }
            string url = "https://rest.cryptoapis.io/wallet-as-a-service/wallets/"+walletid+"/"+addressDTO.blockchain+"/"+network+"/addresses";
            _http.DefaultRequestHeaders.Add("X-API-Key",ApiKey);
            string data = JsonConvert.SerializeObject(addressDTO);
            HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _http.PostAsync(url, c).GetAwaiter().GetResult();
            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            var sc = HttpStatusCode.OK;
            if(httpResponse.StatusCode == sc){
                address.email = addressDTO.data.item.label;
                _context.AddressCrypto.Add(address);
                _context.SaveChanges();
            }
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(responseString);
            string requestId = jObject["requestId"].Value<string>();
            string addresses = jObject["data"]["item"]["address"].Value<string>();
            string createdTimestamp = jObject["data"]["item"]["createdTimestamp"].Value<string>();
            string email = jObject["data"]["item"]["label"].Value<string>();
            var dictionary = new OrderedDictionary();
            dictionary.Add("requestId",requestId);
            dictionary.Add("address",addresses);
            dictionary.Add("createdTimestamp",createdTimestamp);
            dictionary.Add("email",email);
            var jsonData = JsonConvert.SerializeObject( dictionary, Formatting.Indented );
            Console.WriteLine(jsonData);
            return Ok(jsonData);                          
        }

        [HttpPost("sendcoin")]
        public async Task<ActionResult> SendCoin (SendCoin sendCoin){
            Env.Load();
            var config = new MapperConfiguration(cfg =>{
                cfg.AddProfile(new CrytoApiProfile());
            });
            var mapper = config.CreateMapper();
            var sendCoinDto = mapper.Map<SendCoin,SendCoinDTO>(sendCoin);
            var ApiKey = _configuration.GetValue<string>("ApiKey");
            var walletid = _configuration.GetValue<string>("walletid");
            
            string url = "https://rest.cryptoapis.io/wallet-as-a-service/wallets/"+walletid+"/"+sendCoinDto.blockchain+"/"+sendCoinDto.network+"/addresses/"+sendCoinDto.address+"/feeless-transaction-requests";
            _http.DefaultRequestHeaders.Add("X-API-Key",ApiKey);
            string data = JsonConvert.SerializeObject(sendCoinDto);
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