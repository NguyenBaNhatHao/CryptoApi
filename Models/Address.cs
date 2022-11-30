namespace CryptoApi.Models{
    public class Address{
        public string? context {get; set;}
        public Data data {get;set;}
    }
    public class Data{
        public Item item {get; set;}
    }
    public class Item{
        public string? label {get; set;}
    }
}