using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HateoasFilter.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;

namespace HateoasFilter
{
    public class HateoasFilter : IActionFilter
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private IEnumerable<ActionDescriptor> collectionofcontroller;

        /// <summary>
        /// Constructor with IActionDescriptorCollectionProvider DI
        /// </summary>
        /// <param name="actionDescriptorCollectionProvider"></param>
        public HateoasFilter(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        /// <summary>
        /// will be called before the execution of the mehod which is using this filter
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllername = ((ControllerActionDescriptor)context.ActionDescriptor).ControllerName;

            collectionofcontroller = _actionDescriptorCollectionProvider?.ActionDescriptors?.Items?.Where(x =>
            {
                return x.RouteValues.Values.ToList()[1].Equals(controllername);
            });
        }

        /// <summary>
        /// will be called after the execution of the mehod which is using this filter
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var resultCollection = (((ObjectResult)context.Result)?.Value);
            foreach (var coll in collectionofcontroller)
            {
                Model.Model ln = new Model.Model(coll.AttributeRouteInfo.Template, ((ControllerActionDescriptor)coll).ActionName,
                    ((HttpMethodActionConstraint)coll.ActionConstraints.First()).HttpMethods.First());

                if (!(resultCollection is null))
                {
                    if ((resultCollection as IEnumerable) is null)
                    {
                        ((ModelWrapper)resultCollection).Links.Add(ln);
                    }
                    else
                    {
                        foreach (var result in (IEnumerable)resultCollection)
                        {
                            ((ModelWrapper)result).Links.Add(ln);
                        }
                    }
                }

            }
        }
    }
}
