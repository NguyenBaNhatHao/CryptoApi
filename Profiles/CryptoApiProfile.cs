using AutoMapper;
using CryptoApi.Dtos;
using CryptoApi.Models;
namespace CryptoApi.Profiles{
    public class CrytoApiProfile : Profile{
        public CrytoApiProfile(){

            CreateMap<AddressParameter,AddressDTO>()
            .ForMember(dest=>dest.blockchain, act=>act.MapFrom(src=>src.currencycode))
            .ForMember(dest=>dest.data, act=>act.MapFrom(src=> new Data{ item = new Item{label = src.email}}));

            CreateMap<SendCoin, SendCoinDTO>()
            .ForMember(dest=>dest.blockchain, act=>act.MapFrom(src=>src.currencycode))
            .ForMember(dest=>dest.data, act=>act.MapFrom(src=> new DataSendCoin{item = new ItemSendCoin{amount = src.amount, recipientAddress = src.recipientAddress, note = src.note}}));
        }
    }
}