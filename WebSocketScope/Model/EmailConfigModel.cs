using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketScope.Model
{
    public class EmailConfigModel
    {
        public required string smtp { get; set; }
        public required string usuario { get; set; }
        public required string senha { get; set; }
    }
}
