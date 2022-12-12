using System.ComponentModel.DataAnnotations.Schema;
namespace CryptoApi.Models{
    public class Network{
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Networkid {get; set;}
        public string NetworkName {get; set;}
    }
}