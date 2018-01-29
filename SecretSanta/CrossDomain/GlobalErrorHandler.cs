using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;

namespace SecretSanta.CrossDomain
{
    public class GlobalErrorHandler : ExceptionFilterAttribute, IAutofacExceptionFilter
    {
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            //Log, trace
            if (!(actionExecutedContext.Exception is HttpResponseException))
            {
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}