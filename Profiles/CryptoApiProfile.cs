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
            .ForMember(dest=>dest.address, act=>act.MapFrom(src=>src.address))
            .ForMember(dest=>dest.data, act=>act.MapFrom(src=> new DataSendCoin{item = new ItemSendCoin{amount = src.amount, recipientAddress = src.recipientAddress, note = src.note}}));

            CreateMap<Energy, EnergyDTO>()
            .ForMember(dest=>dest.blockchain, act=>act.MapFrom(src=>src.currencycode))
            .ForMember(dest=>dest.data, act=>act.MapFrom(src=> new DataEnergy{item = new ItemEnergy{recipient = src.recipient, resource = src.resource}}));

            CreateMap<SendToken, SendTokenDTO>()
            .ForMember(dest=>dest.blockchain, act=>act.MapFrom(src=>src.currencycode))
            .ForMember(dest=>dest.SenderAddress, act=>act.MapFrom(src=>src.SenderAddress))
            .ForMember(dest=>dest.data, act=>act.MapFrom(src=> new DataSendToken{item = new ItemSendToken{amount = src.amount, note = src.note}}));
        }
    }
}