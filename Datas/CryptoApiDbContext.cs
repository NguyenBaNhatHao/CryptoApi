using CryptoApi.Models;
using Microsoft.EntityFrameworkCore;
namespace CryptoApi.Datas{
    public class CryptoApiDbContext: DbContext{
        public CryptoApiDbContext(DbContextOptions<CryptoApiDbContext> opt):base(opt){

        }
        public DbSet<AddressParameter> AddressCrypto{get;set;}
        public DbSet<SendCoin> TransactionRequest { get; set; }
        public DbSet<SymbolCoin> SymbolCoin {get; set;}
        public DbSet<Network> Network {get; set;}
        public DbSet<Address> ResponseAddress {get; set;}
    }
}