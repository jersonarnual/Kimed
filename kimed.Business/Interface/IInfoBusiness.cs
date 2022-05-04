using Kimed.Infraestructure.DTO;
using Kimed.Infraestructure.Util;
using System;

namespace kimed.Business.Interface
{
    public interface IInfoBusiness
    {
        Result GetAll();
        Result GetById(Guid id);
        Result Insert(InfoDTO entity);
        Result Update(InfoDTO entity);
        Result Delete(Guid id);
    }
}
