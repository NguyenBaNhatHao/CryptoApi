using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CryptoApi.Dtos{
    public class SendCoinDTO{
        public string? context {get; set;}
        public string walletid {get; set;}
        public string blockchain {get; set;}
        public string network {get; set;}
        public string address {get; set;}
        public DataSendCoin data {get; set;}
        
    }
    [Keyless]
    [NotMapped]
    public class DataSendCoin {
        public ItemSendCoin item{ get; set;}
    }
    [Keyless]
    [NotMapped]
    public class ItemSendCoin {
        public string? amount{get; set;}
        public string note {get; set;}
        public string? recipientAddress {get; set;}
    }
}