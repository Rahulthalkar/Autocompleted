using Autocomplete.DAL.Interface;
using Autocomplete.DB;
using Autocomplete.Domain;
using Autocomplete.Domain.Tables;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.DAL
{
    public class SearchRepository : IAutoSearchRepository
    {
        private readonly string ConnectionString;
        private readonly IConfiguration _configuration;

        public SearchRepository(IConfiguration configuration)
        {

            _configuration = configuration;
            ConnectionString = Convert.ToString(configuration.GetSection("ConnectionStrings:AC").Value);
        }

        public int AddSearch(AddItemRequestModel addItemRequestModel)
        {
            using (var dbContext = new ACEntities(ConnectionString))
            {

                var addSearchItem = new Tableitem
                {
                    Id = 0,
                    Name = addItemRequestModel.Name
                };
                dbContext.Add(addSearchItem);
               return dbContext.SaveChanges();

            }
        }

        public string ExistItems(string items)
        {
            throw new NotImplementedException();
        }

        public List<AddItemRequestModel> GetAll()
        {
            using (var dbContext = new ACEntities(ConnectionString))
            {
                var serachData = (from db in dbContext.tableItems
                                  select new AddItemRequestModel
                                  {
                                      Id=db.Id,
                                      Name=db.Name,
                                  }).ToList();

                return serachData;
                

            }
        }

        public List<ItemResponseMode> SearchItems(string searchCriteria)
        {
            using (var dbContext = new ACEntities(ConnectionString))
            {
                var data = (from db in dbContext.tableItems
                            where (db.Name.Contains(searchCriteria))
                            orderby db.Name
                            select new ItemResponseMode()
                            {
                                Id = db.Id,
                                Name = db.Name,
                            }).Distinct().ToList();

                return data;
            }
        }
    }
}
