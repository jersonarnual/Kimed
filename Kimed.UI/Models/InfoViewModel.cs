using Kimed.Infraestructure.DTO;
using System;
using System.Collections.Generic;

namespace Kimed.UI.Models
{
    public class InfoViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public byte FileByte{ get; set; }
        public string Description { get; set; }
        public List<InfoDTO> ListInfo{ get; set; }
    }
}
