using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.Domain
{
    public class LoginInfo
    {
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserType {get; set; }
        public int UserTypeId { get; set; }
    }
}
