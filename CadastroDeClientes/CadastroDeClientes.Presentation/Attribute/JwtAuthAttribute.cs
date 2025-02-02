using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace CadastroDeClientes.Presentation.Attribute
{
    public class JwtAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["AuthToken"]; 

            if (cookie == null || string.IsNullOrEmpty(cookie) || !IsTokenValid(cookie))
            {
                filterContext.Result = new RedirectResult("~/Account/Login"); 
            }

            base.OnActionExecuting(filterContext);
        }

        private bool IsTokenValid(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken.ValidTo < DateTime.UtcNow)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
