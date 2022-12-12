using System.ComponentModel.DataAnnotations;

namespace CryptoApi.Models{
    public class SymbolCoin{
        [Key]
        [Required]
        public int id {get; set;}
        public string Blockchain {get; set;}
        public string Symbol {get; set;}
        public Network? Network {get; set;}
    }
}