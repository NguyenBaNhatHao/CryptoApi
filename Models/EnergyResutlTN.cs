using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CryptoApi.Dtos;
namespace CryptoApi.Models{
    public class EnergyResutlTN{
        public int id {get; set;}
        public string amount {get; set;}
        public string currencycode {get;set;}
        public string address {get; set;}
        public string resource {get;set;}
        public string status {get;set;}
    }
}