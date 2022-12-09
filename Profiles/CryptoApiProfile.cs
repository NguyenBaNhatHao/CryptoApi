using AutoMapper;
using CryptoApi.Dtos;
using CryptoApi.Models;
namespace CryptoApi.Profiles{
    public class CrytoApiProfile : Profile{
        public CrytoApiProfile(){
            CreateMap<AddressDTO, Address>()
            .ForMember(dest=>dest.context, act=>act.MapFrom(src=>src.context))
            .ForMember(dest=>dest.walletid, act=>act.MapFrom(src=>src.walletid))
            .ForMember(dest=>dest.blockchain, act=>act.MapFrom(src=>src.blockchain))
            .ForMember(dest=>dest.network, act=>act.MapFrom(src=>src.network))
            .ForMember(dest=>dest.label, act=>act.MapFrom(src=>src.data.item.label));
            CreateMap<Address,AddressDTO>();

            CreateMap<AddressParameter,AddressDTO>()
            .ForMember(dest=>dest.blockchain, act=>act.MapFrom(src=>src.blockchain))
            .ForMember(dest=>dest.network, act=>act.MapFrom(src=>src.network))
            .ForMember(dest=>dest.data, act=>act.MapFrom(src=> new Data{ item = new Item{label = src.email}}));

            CreateMap<AddressBCONGODTO, AddressBCONGO>()
            .ForMember(dest=>dest.blockchain, act=>act.MapFrom(src=>src.blockchain))
            .ForMember(dest=>dest.network, act=>act.MapFrom(src=>src.network))
            .ForMember(dest=>dest.data, act=>act.MapFrom(src=>src.Data))
            .ForMember(dest=>dest.item, act=>act.MapFrom(src=>src.Data.Item))
            .ForMember(dest=>dest.label, act=>act.MapFrom(src=>src.Data.Item.email));
            CreateMap<AddressBCONGO,AddressBCONGODTO>();

            CreateMap<SendCoin, SendCoinDTO>()
            .ForMember(dest=>dest.blockchain, act=>act.MapFrom(src=>src.blockchain))
            .ForMember(dest=>dest.network, act=>act.MapFrom(src=>src.network))
            .ForMember(dest=>dest.address, act=>act.MapFrom(src=>src.address))
            .ForMember(dest=>dest.data, act=>act.MapFrom(src=> new DataSendCoin{item = new ItemSendCoin{amount = src.amount, recipientAddress = src.recipientAddress, note = src.note}}));
        }
    }
}