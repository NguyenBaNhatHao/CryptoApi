using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CryptoApi.Dtos;
namespace CryptoApi.Models{
    public class Address{
        public int id {get; set;}
        public string currencycode {get; set;}
       
        public string email {get; set;}
       
        public string requestId {get; set;}
        
        public string address {get; set;}
        public string createdTimestamp {get; set;}
        
    }
}