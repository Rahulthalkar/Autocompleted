using ANM.Domain;
using Autocomplete.DAL.Interface;
using Autocomplete.DB;
using Autocomplete.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly string ConnectionString;
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = Convert.ToString(configuration.GetSection("ConnectionStrings:AC").Value);
        }
        public LoginInfo Login(LoginModel model)
        {
            APIResult<LoginInfo> result = new APIResult<LoginInfo>();

            using (var dbcontext = new ACEntities(ConnectionString))
            {
                var user = (from db in dbcontext.tblUsers
                            join ds in dbcontext.tblUsersType on db.UserTypeId equals ds.Id
                            where db.UserName == model.Username
                            select new
                            {
                                db.Id,
                                db.FirstName,
                                db.LastName,
                                db.Password,
                                UserType = ds.TypeOfUser, // Assuming 'Name' is the property in tblUsersType representing user type
                                db.UserTypeId
                            }).FirstOrDefault();

                if (user != null)
                {
                    if (String.Compare(user.Password, model.Password, StringComparison.Ordinal) == 0)
                    {
                        return new LoginInfo
                        {
                            ID = user.Id,
                            FullName = user.FirstName + " " + user.LastName,
                            UserType = user.UserType,
                            UserTypeId = user.UserTypeId
                        };
                    }
                    else
                    {
                        return new LoginInfo();
                    }
                }
                else
                {
                    return new LoginInfo();
                }
            }
        }


        /*        public LoginInfo Login(LoginModel model)
                {
                    APIResult<LoginInfo> result = new APIResult<LoginInfo>();

                    using (var dbcontext=new ACEntities(ConnectionString))
                    {
                        var user = (from db in dbcontext.tblUsers
                                    join ds in dbcontext.tblUsersType on db.UserTypeId equals ds.Id
                                    where db.UserName == model.Username
                                    select db).FirstOrDefault();
                        if (user != null)
                        {
                            if (String.Compare(user.Password, model.Password, StringComparison.Ordinal) == 0)
                            {
                                return new LoginInfo
                                {
                                    ID = user.Id,
                                    FullName = user.FirstName + " " + user.LastName,
                                    UserType=user.,
                                    UserTypeId = user.UserTypeId
                                };
                            }
                            else
                            {
                                return new LoginInfo();
                            }
                        }
                        else
                        {
                            return new LoginInfo();
                        }
                    }
                }
        */


    }
}
