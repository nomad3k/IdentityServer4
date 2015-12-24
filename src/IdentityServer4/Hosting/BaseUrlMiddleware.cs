﻿using IdentityServer4.Core.Extensions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace IdentityServer4.Core.Hosting
{
    public class BaseUrlMiddleware
    {
        private readonly RequestDelegate _next;
        
        public BaseUrlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IdentityServerContext idsrvContext)
        {
            var request = context.Request;

            var origin = idsrvContext.Options.PublicOrigin;
            if (origin.IsMissing())
            {
                origin = context.Request.Scheme + "://" + request.Host.Value;
            }

            idsrvContext.SetHost(origin);
            idsrvContext.SetBasePath(request.PathBase.Value.RemoveTrailingSlash());

            await _next(context);
        }
    }
}