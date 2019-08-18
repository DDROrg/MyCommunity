using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MyCommunity.DAL.Models;
using MyCommunity.DAL.DTO;

namespace MyCommunity.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, ObjectId>().ConvertUsing(new ObjectIdConverterForString());
            CreateMap<ObjectId, string>().ConvertUsing(new StringConverterForObjectId());
            CreateMap<Appartment, AppartmentDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();

        }
    }

    public class ObjectIdConverterForString : ITypeConverter<string, ObjectId>
    {
        public ObjectId Convert(string source, ObjectId destination, ResolutionContext context)
        {
            return ObjectId.Parse(source);
        }
    }
    public class StringConverterForObjectId : ITypeConverter<ObjectId, string>
    {
        public string Convert(ObjectId source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }
}
