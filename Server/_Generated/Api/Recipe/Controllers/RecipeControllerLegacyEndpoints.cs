using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using Microsoft.AspNetCore.Http;
using ProtoBuf.Grpc;
using Grpc.Core;


namespace OpenCodeDev.NetCms.Server.Api.Recipe.Controllers
{
    [ApiController]
    [Route("/api/recipe")]
    public partial class RecipeControllerLegacyEndpoints: ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RecipeControllerLegacyEndpoints(IHttpContextAccessor httpContextAccessor){
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IEnumerable<RecipePublicModel> Fetch(){
            var httpcontext = _httpContextAccessor.HttpContext;
            var headers = new Metadata();
            foreach (var item in httpcontext.Request.Headers) {
                try
                {
                    headers.Add(item.Key, item.Value);
                }
                catch (Exception)
                {  // Silent Ignore 
                }
            
            }
            CallContext cContext = new CallOptions(headers);
            RecipeController grpcController = new RecipeController();
            return new List<RecipePublicModel>();
        }
    }
}
