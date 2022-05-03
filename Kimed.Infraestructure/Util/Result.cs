using System.Collections.Generic;

namespace Kimed.Infraestructure.Util
{
    public  class Result
    {
        public bool State { get; set; }
        public string Message { get; set; }
        public string MessageException { get; set; }
        public object Model { get; set; }
        public IEnumerable<object> ListModel { get; set; }
    }
}
