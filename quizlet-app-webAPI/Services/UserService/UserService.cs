using System.Security.Claims;

namespace quizlet_app_webAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        
        public string GetUserName()
        {
            var result  = string.Empty;
            if (httpContextAccessor.HttpContext!= null)
            {
                result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
        public string GetUserId()
        {
            var result  = string.Empty;
            if (httpContextAccessor.HttpContext!= null)
            {
                result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return result;
        }
    }
}
