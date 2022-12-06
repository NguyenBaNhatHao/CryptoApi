using CryptoApi.Dtos;
using System.ComponentModel.DataAnnotations;
namespace CryptoApi.Models{
    public class SendCoin{
        public string context {get; set;}
        public string? amount{get; set;}
        public string walletid {get; set;}
        public string blockchain {get; set;}
        public string network {get; set;}
        public string address {get; set;}
        public string? note {get; set;}
        public string? recipientAddress {get; set;}
        public DataSendCoin? data {get; set;}
        public ItemSendCoin? item{ get; set;}
    }
}