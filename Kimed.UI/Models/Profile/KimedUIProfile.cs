using AutoMapper;
using Kimed.Infraestructure.DTO;

namespace Kimed.UI.Models
{
    public class KimedUIProfile : Profile
    {
        public KimedUIProfile()
        {
            CreateMap<InfoDTO, InfoViewModel>();
            CreateMap<InfoViewModel, InfoDTO>();
        }
    }
}
