using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;    // For the [Key] annotation for identity column
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor
{
    public class Agency
    {
        [Key]
        public long AGENCY_CODE { get; set; }
        public string AGENCY_NANE { get; set; }
        public string AGENCY_DOMAIN_NAME { get; set; }
        public string AGENCY_TYPE { get; set; }
    }
}
