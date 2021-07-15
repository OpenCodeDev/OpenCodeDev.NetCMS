using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Controllers;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCMS.Admin.Pages
{
    public partial class Index
    {

        public async Task GrpcTest(){
    
                var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
                var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
                {
                    HttpHandler = httpHandler,
                });
                var authService = channel.CreateGrpcService<IRecipeController>();
            RecipeFetchRequest request = new RecipeFetchRequest() { Limit = 10, 
            Conditions = new List<RecipePredicateConditions>() { 
                { new RecipePredicateConditions() {  LogicalOperator = Core.Shared.Api.Messages.LogicTypes.And, Conditions = Core.Shared.Api.Messages.ConditionTypes.Equals, Field = RecipePredicateConditions.Fields.Duration, Value = "5"} }, 
                { new RecipePredicateConditions() 
                {  LogicalOperator = Core.Shared.Api.Messages.LogicTypes.Or, Conditions = Core.Shared.Api.Messages.ConditionTypes.Equals, Field = RecipePredicateConditions.Fields.Duration, Value = "1", } }
            } 
            };

            CallContext callContext = new CallOptions();

            try
            {
                var list = await authService.Fetch(request, callContext);
                Console.WriteLine("OK");
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"{ex.Status.Detail}");

            }


        }
    }
}
