using System;

namespace Kimed.Infraestructure.DTO
{
    public class InfoDTO
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public string Description { get; set; }
    }
}
