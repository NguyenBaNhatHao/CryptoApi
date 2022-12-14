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
        public DbSet<AddressTestNet> ResponseAddressTestNet {get; set;}
        public DbSet<EnergyResutlTN> EnergyTN {get;set;}
        public DbSet<SendTokenResutlTN> TokenTN {get; set;}
    }
}