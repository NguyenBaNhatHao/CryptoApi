using CryptoApi.Dtos;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace CryptoApi.Models{
    public class SendCoin{
        public int id {get; set;}
        public string? amount{get; set;}
        public string currencycode {get; set;}
        public string? note {get; set;}
        public string? recipientAddress {get; set;}
    }
}