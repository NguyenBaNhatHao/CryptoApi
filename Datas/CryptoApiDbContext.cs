using CryptoApi.Models;
using Microsoft.EntityFrameworkCore;
namespace CryptoApi.Datas{
    public class CryptoApiDbContext: DbContext{
        public CryptoApiDbContext(DbContextOptions<CryptoApiDbContext> opt):base(opt){

        }
        public DbSet<Address> AddressCrypto{get;set;}
        public DbSet<SendCoin> TransactionRequest { get; set; }
    }
}