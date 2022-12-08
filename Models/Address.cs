using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CryptoApi.Dtos;
namespace CryptoApi.Models{
    public class Address{
        public int id {get; set;}
        public string? context {get; set;}
       
        public string? walletid {get; set;}
       
        public string blockchain {get; set;}
        
        public string network {get; set;}
        public string? label {get; set;}
        public Data? data {get; set;} 
        public Item? item {get; set;}
    }
}