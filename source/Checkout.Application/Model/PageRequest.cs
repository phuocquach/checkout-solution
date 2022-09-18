using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Application.Model
{
    public class PagedRequest
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 5;
        public string OrderBy { get; set; }
        public string OrderByDirection { get; set; }
        public string Search { get; set; }
    }
}
