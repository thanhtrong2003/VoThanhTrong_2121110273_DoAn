using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerResBO
    {
        public int cid { get; set; }
        public string cname { get; set; }
        public long mobile { get; set; }
        public string nationality { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string idproof { get; set; }
        public string address { get; set; }
        public string checkin { get; set; }
        public string checkout { get; set; } // Sử dụng DateTime? vì Checkout có thể là NULL
        public string chekout { get; set; } // Sử dụng bool thay vì BIT
        public int roomid { get; set; }
    }
}
