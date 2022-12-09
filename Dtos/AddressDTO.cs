using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CryptoApi.Dtos{
    public class AddressDTO{
        public string? context {get; set;}
        public string walletid {get; set;}
        public string blockchain {get; set;}
        public string network {get; set;}
        public Data data {get;set;}
        
    }
    [Keyless]
    [NotMapped]
    public class Data{
        
        public Item item {get; set;}
    }
    [Keyless]
    [NotMapped]
    public class Item{
        public string? label {get; set;}
    }
}