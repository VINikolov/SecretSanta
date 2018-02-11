using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using BusinessLogic.Interfaces;
using SecretSanta.Controllers;

namespace SecretSanta.CrossDomain
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute, IAutofacActionFilter
    {
        private readonly IUsersManager _usersManager;

        public AuthenticationFilterAttribute(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            IEnumerable<string> authValues;
            if (!actionContext.Request.Headers.TryGetValues("AuthenticationToken", out authValues))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var user = await _usersManager.GetUserByAuthenticationToken(authValues.First());

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            if (controller == "Groups")
            {
                ((GroupsController)actionContext.ControllerContext.Controller).SetCurrentUser(user);
            }
            else if (controller == "Invitations")
            {
                ((InvitationsController) actionContext.ControllerContext.Controller).SetCurrentUser(user);
            }

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}