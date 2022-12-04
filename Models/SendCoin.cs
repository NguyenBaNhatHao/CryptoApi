using System.ComponentModel.DataAnnotations;
namespace CryptoApi.Models{
    public class SendCoin{
        public string context {get; set;}
        [Required]
        public Data data {get; set;}
        public class Data {
            [Required]
            public Item item{ get; set;}
        }
        public class Item {
            [Required]
            public string amount{get; set;}
            public string note {get; set;}
            [Required]
            public string recipientAddress {get; set;}
        }
    }
}