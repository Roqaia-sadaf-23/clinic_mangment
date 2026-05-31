using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Common.Moduls;


    public class PaginatedList<T>
    {
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
         public int totalCount { get; set; }
         public int totalpages { get; set; }

    public IReadOnlyCollection<T> items { get; set; }
     


}
