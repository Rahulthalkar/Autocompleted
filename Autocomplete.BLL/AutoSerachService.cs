using ANM.Domain;
using Autocomplete.DAL.Interface;
using Autocomplete.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.BLL
{
    public class AutoSerachService
    {
        private readonly IAutoSearchRepository _autoSearchRepository;

        public AutoSerachService(IAutoSearchRepository autoSearchRepository)
        {
            _autoSearchRepository = autoSearchRepository;
        }
        public bool AddSearch(AddItemRequestModel addItemRequestModel)
        {
            APIResult<bool> result = new APIResult<bool>();
            try
            {
             
                var retValue = _autoSearchRepository.AddSearch(addItemRequestModel);
                if (retValue == 0)
                {
                    result.Value = false;
                }
                else
                {
                    result.Value = true;
                }
                result.IsSuccess = true;

            }
            catch (Exception exception)
            {
                result.ExceptionInfo = exception.Message;
                result.IsSuccess = false;
            }
            return result.Value;

        }

        public APIResult<List<AddItemRequestModel>> GetAll()
        {
            APIResult<List<AddItemRequestModel>> result = new APIResult<List<AddItemRequestModel>>();
            try
            {
                var SearchList = _autoSearchRepository.GetAll();
                result.Value = SearchList;
                result.IsSuccess = true;
            }
            catch (Exception exception)
            {
                result.ExceptionInfo = exception.Message;
                result.IsSuccess = false;
            }
            return result;
        }

        /// <summary>
        /// filters items data
        /// </summary>      
        /// <param name="searchCriteria">Please provide search Criteria to get items list.</param>
        /// <returns cref="ItemResponseMode">Returns filtered item list details</returns>
        public APIResult<List<ItemResponseMode>> SearchItems(string searchCriteria)
        {
            APIResult<List<ItemResponseMode>> response = new APIResult<List<ItemResponseMode>>();
            try
            {
                var lstObj = _autoSearchRepository.SearchItems(searchCriteria);
                response.Value = lstObj;
                response.IsSuccess = true;
                return response;
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Value = null;
                response.ErrorMessageKey = "ConnectionFailed";
                response.ExceptionInfo = JsonConvert.SerializeObject(exception);
                return response;
            }
        }

    }
}
