using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CryptoApi.Dtos;
namespace CryptoApi.Models{
    public class Energy{
        public int id {get; set;}
        public string sender {get; set;}
        public string currencycode {get;set;}
        public string amount {get; set;}
        public string recipient {get;set;}
        public string resource {get;set;}
        
    }
}