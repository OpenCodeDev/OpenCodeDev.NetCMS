using OpenCodeDev.NetCms.Shared.Api.Recipe.Controllers;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OpenCodeDev.NetCms.Server.Api.Recipe.Controllers
{
    public class RecipeController : IRecipeController
    {

        

        public async Task<object> Create(object request, CallContext context = default)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(object request, CallContext context = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RecipePublicModel>> Fetch(RecipeFetchRequest request, CallContext context = default)
        {
           var provider =  context.ServerCallContext.GetHttpContext().RequestServices;
           var db = provider.GetRequiredService<RecipeDatabase>();

            Predicate<Models.RecipeModel> test1 = p => p.Id.Equals("");
            Predicate<Models.RecipeModel> test2 = p => p.Duration.Equals("");
            Predicate<Models.RecipeModel> test = p=> test1(p) && test2(p);
        }



        public async Task<object> FetchOne(object request, CallContext context = default)
        {
            throw new NotImplementedException();
        }

        public async Task<object> Update(object request, CallContext context = default)
        {
            throw new NotImplementedException();
        }

    }
}
