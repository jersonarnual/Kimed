using AutoMapper;
using kimed.Business.Interface;
using Kimed.Infraestructure.DTO;
using Kimed.Infraestructure.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Kimed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {

        #region Members
        private readonly IInfoBusiness _infoBusiness;
        private readonly IMapper _mapper; 
        #endregion

        #region Ctor
        public InfoController(IInfoBusiness infoBusiness)
        {
            _infoBusiness = infoBusiness;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IEnumerable<object> Get()
        {
            Result model = _infoBusiness.GetAll();
            if (object.Equals(model.ListModel, null))
                return null;
            return model.ListModel;
        }

        [HttpGet("{id}")]
        public object GetById(Guid id)
        {
            Result model = _infoBusiness.GetById(id);
            if (object.Equals(model.ListModel, null))
                return null;
            return model;
        }

        [HttpPost]
        public Result Post(InfoDTO model)
        {
            Result result = new();
            try
            {
                result = _infoBusiness.Insert(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        [HttpPut("{id}")]
        public Result Put(InfoDTO model)
        {
            Result result = new();
            try
            {
                result = _infoBusiness.Update(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        // DELETE api/<InfoController>/5
        [HttpDelete("{id}")]
        public Result Delete(InfoDTO model)
        {
            Result result = new();
            try
            {
                result = _infoBusiness.Delete(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        } 
        #endregion
    }
}
