using Autocomplete.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.DAL.Interface
{
    public interface IUserRepository
    {
        public LoginInfo Login(LoginModel model);
    }
}
