using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using OpenCodeDev.NetCms.Shared.Api._Core.Messages;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Controllers;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;

using ProtoBuf.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Admin.Pages
{
    public partial class Index
    {
        public async Task Call()
        {
            var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
            var channel = GrpcChannel.ForAddress("https://localhost:5001/", new GrpcChannelOptions { HttpHandler = httpHandler });
            var server= channel.CreateGrpcService<IRecipeController>();
            var headers = new Metadata();

            //RecipePublicModel model = new RecipePublicModel() { Name = "Test" };
            ////RecipeCondition request = new RecipeCondition();
            
            ////request.Query = (Predicate<object>)new Predicate<RecipeModel>(p=>p.Name.Equals("Max"));
            //var list = await server.Fetch(request, new CallOptions());
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.Test.Count);
            //}

        }
    }
}
