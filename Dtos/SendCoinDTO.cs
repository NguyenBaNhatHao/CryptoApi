namespace CryptoApi.Dtos{
    public class SendCoinDTO{
        public string context {get; set;}
        public Data data {get; set;}
        public class Data {
            public Item item{ get; set;}
        }
        public class Item {
            public string amount{get; set;}
            public string callbackSecretKey {get; set;}
            public string callbackUrl {get; set;}
            public string note {get; set;}
            public string recipientAddress {get; set;}
        }
    }
}