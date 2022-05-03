using Kimed.Data.Models.Base;

namespace Kimed.Data.Models
{
    public class Info : BaseEntity
    {
        public string Name { get; set; }
        public string File { get; set; }
        public string Description { get; set; }
    }
}
