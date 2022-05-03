using AutoMapper;
using Kimed.Data.Models;
using Kimed.Infraestructure.DTO;

namespace Kimed.Infraestructure.Util
{
    public class KimedProfile : Profile
    {
        public KimedProfile()
        {
            CreateMap<InfoDTO, Info>();
        }
    }
}
