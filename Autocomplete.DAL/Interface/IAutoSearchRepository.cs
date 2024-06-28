using Autocomplete.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.DAL.Interface
{
    public interface IAutoSearchRepository
    {
        int AddSearch(AddItemRequestModel addItemRequestModel);
        List<AddItemRequestModel> GetAll();
        public List<ItemResponseMode> SearchItems(string searchCriteria);

    }
}
