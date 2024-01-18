using AutoMapper;

using BlazorApp.Server.Models;
using BlazorApp.Shared.DTO;

namespace BlazorApp.Server
{
    /// <summary>
    /// Default profile for AutoMapper
    /// </summary>
    /// <remarks>See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#automapper</remarks>
    public class DefaultMapperProfile : Profile
    {
        public DefaultMapperProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, Order>();
            CreateMap<OrderDto, OrderDto>();

            CreateMap<Window, WindowDto>();
            CreateMap<WindowDto, Window>();
            CreateMap<Window, Window>();
            CreateMap<WindowDto, WindowDto>();

            CreateMap<SubElement, SubElementDto>();
            CreateMap<SubElementDto, SubElement>();
            CreateMap<SubElement, SubElement>();
            CreateMap<SubElementDto, SubElementDto>();
        }
    }
}
