using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public partial class ChatRooms
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string GetLastMessage { get; set; }
    }
}
