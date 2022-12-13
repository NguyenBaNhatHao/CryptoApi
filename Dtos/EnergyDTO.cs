namespace CryptoApi.Dtos{
    public class EnergyDTO{
        public string blockchain {get; set;}
        public string network {get; set;}
        public DataEnergy data {get;set;}
    }
    public class DataEnergy{
        public ItemEnergy item{get;set;}
    }
    public class ItemEnergy{
        public string amount {get;set;}
        public string recipient {get; set;}
        public string resource {get; set;}
    }
}