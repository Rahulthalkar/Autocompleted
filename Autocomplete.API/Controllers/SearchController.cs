using ANM.Domain;
using Autocomplete.BLL;
using Autocomplete.Domain;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Autocomplete.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly AutoSerachService _autoSerachService;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SearchController(AutoSerachService autoSerachService)
        {
            _autoSerachService = autoSerachService;
        }
        /// <summary>
        /// This function is use for Add Search .
        /// </summary>
        /// <param name="addItemRequestModel">Please provide details to AddSearch.</param>
        /// <returns>Returns true, if Search Added</returns>
        [Route("AddSearch")]
        [HttpPost]
        [ProducesResponseType(typeof(APIResult<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResult<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSearch(AddItemRequestModel addItemRequestModel)
        {
            log.Info($"Request: {nameof(AddSearch)} method called with value{JsonConvert.SerializeObject(addItemRequestModel)}.");

            if (ModelState.IsValid)
            {
                var AddAutoSearch = _autoSerachService.AddSearch(addItemRequestModel);
                if (AddAutoSearch !=null)
                {
                    log.Info($"Response: {nameof(AddSearch)} method {JsonConvert.SerializeObject(AddAutoSearch)}");
                    return Ok(AddAutoSearch);
                }
                else
                {
                    log.Info($"Response: {nameof(AddSearch)} method {JsonConvert.SerializeObject(AddAutoSearch)}");
                    return BadRequest(AddAutoSearch);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest("Model is Invalid. Validation Errors: " + message);
            }


        }
        /// <summary>
        ///  auto Search  
        /// </summary>
        /// <returns>if auto Search...</returns>
        /// 
        [Route("GetAll")]
        [HttpGet]
        [ProducesResponseType(typeof(APIResult<AddItemRequestModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResult<AddItemRequestModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            log.Info($"Request: {nameof(GetAll)} method is called.");

            var SearchData = _autoSerachService.GetAll();

            if (SearchData.IsSuccess)
            {
                log.Info($"Response: {nameof(GetAll)} method {JsonConvert.SerializeObject(SearchData)}");
                return Ok(SearchData);
            }
            else
            {
                log.Info($"Response: {nameof(GetAll)} method {JsonConvert.SerializeObject(SearchData)}");
                return BadRequest(SearchData);
            }
        }

        /// <summary>
        ///  search company details
        /// </summary>      
        /// <returns cref="ItemResponseMode">Returns list of filtered company details</returns>
        [HttpGet]
        [Route("SearchItems")]
        [ProducesResponseType(typeof(APIResult<List<ItemResponseMode>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResult<List<ItemResponseMode>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchItems(string searchCriteria)
        {
            log.Info($"{nameof(SearchItems)} method called");

            APIResult<List<ItemResponseMode>> lstResponse = new APIResult<List<ItemResponseMode>>();
            lstResponse = _autoSerachService.SearchItems(searchCriteria);

            if (lstResponse != null)
            {
                return Ok(lstResponse);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
