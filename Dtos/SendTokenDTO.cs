using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CryptoApi.Dtos{
    public class SendTokenDTO{
        public string blockchain {get; set;}
        public string network {get; set;}
        public string SenderAddress {get; set;}
        public DataSendToken data {get; set;}
        
    }
    [Keyless]
    [NotMapped]
    public class DataSendToken {
        public ItemSendToken item{ get; set;}
    }
    [Keyless]
    [NotMapped]
    public class ItemSendToken {
        public string? amount{get; set;}
        public string note {get; set;}
        public string? feeLimit {get;set;}
        public string? tokenIdentifier{get;set;}
        public string? recipientAddress {get; set;}
    }
}