//_NETCMS_HEADER_

using Grpc.Core;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Code Namespaces
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Models;
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages;
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Controllers;

// DO NOT EDIT, REGENERATED AT EACH BUILD.

namespace _NAMESPACE_BASE_SERVER_.Api._API_NAME_.Controllers
{
    /// <summary>
    /// Provides with Api Controller's wrapper mainly for permission, authentication and other operation required to process request. <br/>
    /// If you use a custom endpoint, you will have to see the official documentation to call permission and auth functions
    /// </summary>
    public partial class _API_NAME_ControllerEndpoints : I_API_NAME_Controller
    {
        public virtual async Task<_API_NAME_PublicModel> Create(_API_NAME_CreateRequest request, CallContext context = default)
        {
            // Check for Permissions, Check for Field Permission
            // Check for ApiKey Permission, Check for Account Role Permissions
            // Check for Form Validation
            // If everything checks out, call the real thing.
            try
            {
                var api = new _API_NAME_Controller();
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

        public virtual async Task<_API_NAME_PublicModel> Delete(_API_NAME_DeleteRequest request, CallContext context = default)
        {
            try
            {
                var api = new _API_NAME_Controller();
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
                // error is relayed to the core system and every plugin register all error handling with messages then core return grpc error to throw.
                // Example: Stripe would register StripeException, so whenever a plugin uses stripe and throw that error, catch this to print a more accurate msg for user from the plugin in question.
                Console.WriteLine(ex.Message);
                throw new RpcException(new Status(StatusCode.Unknown, "Unknown error occured"));
            }
        }

        public virtual async Task<List<_API_NAME_PublicModel>> Fetch(_API_NAME_FetchRequest request, CallContext context = default)
        {
            try
            {
                var api = new _API_NAME_Controller();
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

        public virtual async Task<_API_NAME_PublicModel> FetchOne(_API_NAME_FetchOneRequest request, CallContext context = default)
        {
            try
            {
                var api = new _API_NAME_Controller();
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

        public virtual async Task<_API_NAME_PublicModel> Update(_API_NAME_UpdateOneRequest request, CallContext context = default)
        {
            try
            {
                var api = new _API_NAME_Controller();
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

        public virtual async Task<List<_API_NAME_PublicModel>> UpdateMany(_API_NAME_UpdateManyRequest request, CallContext context = default)
        {
            try
            {
                var api = new _API_NAME_Controller();
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
