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
using OpenCodeDev.NetCms.Server.Api.Recipe.Services;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages._Generated;
using OpenCodeDev.NetCms.Server.Database;
using OpenCodeDev.NetCms.Server.Api.Recipe.Models;

namespace OpenCodeDev.NetCms.Server.Api.Recipe.Controllers
{
    public class RecipeController : IRecipeController
    {
        public async Task<RecipePublicModel> Create(RecipeCreateRequest request, CallContext context = default)
        {
            throw new NotImplementedException();
        }

        public async Task<RecipePublicModel> Delete(RecipeDeleteRequest request, CallContext context = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RecipePublicModel>> Fetch(RecipeFetchRequest request, CallContext context = default)
        {
            // Permissions and Access Control are being handled in the Wrapper of this class.
            var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            var db = provider.GetRequiredService<ApiDatabase>();
            var myService = provider.GetRequiredService<RecipeMyService>();

            // Building Predicate Conditions
            // Note: Reflection is being used and can be heavy when very large list of condition is given.
            // you may want to restrict the number of condition, duplicate is allowed so the intent to slow the system can be used.
            // By Default we allow 20 conditions longer than that will throw an error to protect the default system.
            var predicate = myService.RecipeConditionHandler(request.Conditions);

            // Execute Predicate on Database.
            var result = db.Recipes.Where(p => predicate(p)).Select(p=>(RecipePublicModel)p).ToList();
            return result;
        }

        public async Task<RecipePublicModel> FetchOne(RecipeFetchOneRequest request, CallContext context = default)
        {
            var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            var db = provider.GetRequiredService<ApiDatabase>();
            var result = db.Recipes.Where(p => p.Id.Equals(request.Id)).FirstOrDefault();
            return result;
        }

        public async Task<RecipePublicModel> Update(RecipeUpdateOneRequest request, CallContext context = default)
        {
            var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            var db = provider.GetRequiredService<ApiDatabase>();
            var myService = provider.GetRequiredService<RecipeMyService>();
            var updating = db.Recipes.Where(p => p.Id.Equals(request.Id)).FirstOrDefault();
            // Map Fields One by One (No JSON, No Serializer, Simply Hard Handed... but Generated so... :D)
            updating = myService.RecipeFilterUpdate(updating, (RecipeModel)request.Element);
            // Process References Update, Remove Unwanted, Add Wanted, Keep the Keeper
            updating = myService.RecipeFilterUpdateReferences(db, updating, request);
            return updating;
        }

        public async Task<List<RecipePublicModel>> UpdateMany(RecipeUpdateManyRequest request, CallContext context = default)
        {
            throw new NotImplementedException();
        }
    }
}
