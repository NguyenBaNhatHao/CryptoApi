using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CryptoApi.Dtos{
    public class AddressBCONGODTO{
        public string blockchain {get; set;}
        public string network {get; set;}
        public DataBCONGO Data {get; set;}
    }
    [Keyless]
    [NotMapped]
    public class DataBCONGO{
        public ItemBCONGO Item {get; set;}
    }
    [Keyless]
    [NotMapped]
    public class ItemBCONGO{
        public string? email {get; set;}
    }
}