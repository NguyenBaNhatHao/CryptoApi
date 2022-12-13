using CryptoApi.Dtos;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace CryptoApi.Models{
    public class SendToken{
        public int id {get; set;}
        public string currencycode {get;set;}
        public string? amount{get; set;}
        public string? note {get; set;}
        public string? SenderAddress {get; set;}
    }
}