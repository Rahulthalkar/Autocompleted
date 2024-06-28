using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.Domain
{
    public class AddItemRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ItemResponseMode
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}
