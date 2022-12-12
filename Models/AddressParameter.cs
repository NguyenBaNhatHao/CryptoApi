using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using CryptoApi.Dtos;
namespace CryptoApi.Models{
    public class AddressParameter{
        public int id {get; set;}
       
        public string currencycode {get; set;}
        public string? email {get; set;}
    }
}