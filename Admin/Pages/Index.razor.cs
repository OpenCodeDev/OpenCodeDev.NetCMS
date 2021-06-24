using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Controllers;
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
                Guid MyGuidTest = Guid.NewGuid();

                //CallContext callContext = new CallOptions();

                //try
                //{
                //    await authService.FetchOne(MyGuidTest, callContext);
                //}
                //catch (RpcException ex)
                //{
                //    Console.WriteLine($"{ex.Status.Detail}");
    
                //}


        }
    }
}
