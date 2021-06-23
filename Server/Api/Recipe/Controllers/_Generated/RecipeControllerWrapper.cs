using Grpc.Core;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Controllers;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages._Generated;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Server.Api.Recipe.Controllers._Generated
{
    //AUTO-GENERATED DO NOT EDIT.
    public class RecipeControllerWrapper : IRecipeController
    {
        public virtual async Task<RecipePublicModel> Create(RecipeCreateRequest request, CallContext context = default)
        {
            // Check for Permissions, Check for Field Permission
            // Check for ApiKey Permission, Check for Account Role Permissions
            // Check for Form Validation
            // If everything checks out, call the real thing.
            try
            {
                var api = new RecipeController();
                return await api.Create(request, context);
            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(ex.Status.StatusCode, ex.Status.Detail));
            }
            catch (NullReferenceException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Entry was not found."));
            }
            catch (Exception ex)
            {
                //TODO: IDEA ?  Create a grpc error handler at core base, whenever error is thrown during request,
                // error is related to the core system and every plugin register all error handling with messages then core return grpc error to throw.
                // Example: Stripe would register StripeException, so whenever a plugin uses stripe and throw that error, catch this to print a more accurate msg for user.
                Console.WriteLine(ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, "Unknown error occured"));
            }
        }

        public virtual async Task<RecipePublicModel> Delete(RecipeDeleteRequest request, CallContext context = default)
        {
            try
            {
                var api = new RecipeController();
                return await api.Delete(request, context);
            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(ex.Status.StatusCode, ex.Status.Detail));
            }
            catch (NullReferenceException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Entry was not found."));
            }
            catch (Exception ex)
            {
                //TODO: IDEA ?  Create a grpc error handler at core base, whenever error is thrown during request,
                // error is related to the core system and every plugin register all error handling with messages then core return grpc error to throw.
                // Example: Stripe would register StripeException, so whenever a plugin uses stripe and throw that error, catch this to print a more accurate msg for user.
                Console.WriteLine(ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, "Unknown error occured"));
            }
        }

        public virtual async Task<List<RecipePublicModel>> Fetch(RecipeFetchRequest request, CallContext context = default)
        {
            try
            {
                var api = new RecipeController();
                return await api.Fetch(request, context);
            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(ex.Status.StatusCode, ex.Status.Detail));
            }
            catch (NullReferenceException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Entry was not found."));
            }
            catch (Exception ex)
            {
                //TODO: IDEA ?  Create a grpc error handler at core base, whenever error is thrown during request,
                // error is related to the core system and every plugin register all error handling with messages then core return grpc error to throw.
                // Example: Stripe would register StripeException, so whenever a plugin uses stripe and throw that error, catch this to print a more accurate msg for user.
                Console.WriteLine(ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, "Unknown error occured"));
            }
        }

        public virtual async Task<RecipePublicModel> FetchOne(RecipeFetchOneRequest request, CallContext context = default)
        {
            try
            {
                var api = new RecipeController();
                return await api.FetchOne(request, context);
            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(ex.Status.StatusCode, ex.Status.Detail));
            }
            catch (NullReferenceException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Entry was not found."));
            }
            catch (Exception ex)
            {
                //TODO: IDEA ?  Create a grpc error handler at core base, whenever error is thrown during request,
                // error is related to the core system and every plugin register all error handling with messages then core return grpc error to throw.
                // Example: Stripe would register StripeException, so whenever a plugin uses stripe and throw that error, catch this to print a more accurate msg for user.
                Console.WriteLine(ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, "Unknown error occured"));
            }
        }

        public virtual async Task<RecipePublicModel> Update(RecipeUpdateOneRequest request, CallContext context = default)
        {
            try
            {
                var api = new RecipeController();
                return await api.Update(request, context);
            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(ex.Status.StatusCode, ex.Status.Detail));
            }
            catch (NullReferenceException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Entry was not found."));
            }
            catch (Exception ex)
            {
                //TODO: IDEA ?  Create a grpc error handler at core base, whenever error is thrown during request,
                // error is related to the core system and every plugin register all error handling with messages then core return grpc error to throw.
                // Example: Stripe would register StripeException, so whenever a plugin uses stripe and throw that error, catch this to print a more accurate msg for user.
                Console.WriteLine(ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, "Unknown error occured"));
            }
        }

        public virtual async Task<List<RecipePublicModel>> UpdateMany(RecipeUpdateManyRequest request, CallContext context = default)
        {
            try
            {
                var api = new RecipeController();
                return await api.UpdateMany(request, context);
            }
            catch (RpcException ex)
            {
                throw new RpcException(new Status(ex.Status.StatusCode, ex.Status.Detail));
            }
            catch (NullReferenceException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Entry was not found."));
            }
            catch (Exception ex)
            {
                //TODO: IDEA ?  Create a grpc error handler at core base, whenever error is thrown during request,
                // error is related to the core system and every plugin register all error handling with messages then core return grpc error to throw.
                // Example: Stripe would register StripeException, so whenever a plugin uses stripe and throw that error, catch this to print a more accurate msg for user.
                Console.WriteLine(ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, "Unknown error occured"));
            }
        }
    }
}
