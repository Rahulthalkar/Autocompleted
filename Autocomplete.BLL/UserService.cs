using ANM.Domain;
using Autocomplete.DAL.Interface;
using Autocomplete.Domain;

namespace Autocomplete.BLL
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public LoginInfo Login(LoginModel model)
        {

            APIResult<LoginInfo> result = new APIResult<LoginInfo>();
            try
            {
                result.Value = _userRepository.Login(model);
                result.IsSuccess = result.Value != null;
                if (result.Value.ID == 0)
                {
                    result.ExceptionInfo = "UserName or Password Incorrect";
                    result.IsSuccess = false;
                }
            }
            catch (Exception exception)
            {
                result.ExceptionInfo = exception.Message;
                result.IsSuccess = false;
            }

            return result.Value;

        }

    }
}
