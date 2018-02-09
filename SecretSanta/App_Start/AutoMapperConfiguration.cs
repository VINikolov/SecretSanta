using AutoMapper;
using AutoMapper.Configuration;
using Models.ApiResponseModels;
using Models.DataTransferModels;

namespace SecretSanta
{
    public static class AutoMapperConfiguration
    {
        public static void Register()
        {
            var config = new MapperConfigurationExpression();
            config.CreateMap<UserResponse, User>();
            Mapper.Initialize(config);
        }
    }
}