using AutoMapper;
using DOTNETRELATIONS.Data.Entities;
using DOTNETRELATIONS.DTOs;

namespace DOTNETRELATIONS;

public class AppMapperConfig : Profile
{
    public AppMapperConfig()
    {
        CreateMap<CustomerDto, Customer>();
        CreateMap<CustomerAddressesDto, CustomerAddresses>();

        //CreateMap<CustomerDto, Customer>().ReverseMap();   //For Two Way Mapping.

    }
}