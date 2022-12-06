namespace CryptoApi.Dtos{
    public class AddressDTO{
        public string? context {get; set;}
        public string walletid {get; set;}
        public string blockchain {get; set;}
        public string network {get; set;}
        public Data data {get;set;}
        
    }
    public class Data{
            public Item item {get; set;}
        }
        public class Item{
            public string? label {get; set;}
        }
}