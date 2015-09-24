using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Models
{
    public class RespondObj
    {
        public int status { get; set; }

        public string data { get; set; }

        public string message { get; set; }
    }
}
