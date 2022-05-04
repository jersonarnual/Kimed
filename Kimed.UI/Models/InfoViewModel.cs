using Kimed.Infraestructure.DTO;
using Kimed.UI.Models.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kimed.UI.Models
{
    public class InfoViewModel
    {
        public Guid Id { get; set; }

        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        [Required(ErrorMessage ="Debe de diligenciar el campo")]
        public string Name { get; set; }

        public string File { get; set; }

        [Display(Name = "Image")]
        public IFormFile FileByte{ get; set; }

        [Required(ErrorMessage = "Debe de diligenciar el campo")]
        public string Description { get; set; }

        public List<InfoDTO> ListInfo{ get; set; }
    }


}
