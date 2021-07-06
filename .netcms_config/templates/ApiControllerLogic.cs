//_NETCMS_HEADER_

using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Shared Resources
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Controllers;
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Models;
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages;

// Server Resources
using _NAMESPACE_BASE_SERVER_.Database;
using _NAMESPACE_BASE_SERVER_.Api._API_NAME_.Services;
using _NAMESPACE_BASE_SERVER_.Api._API_NAME_.Models;

namespace _NAMESPACE_BASE_SERVER_.Api._API_NAME_.Controllers
{   
    /// <summary>
    /// This class provides you with fully functional common api CRUD logic.<br/>
    /// You can also inherit this class and override the logic to use your own custom logic. (Not Recommended)
    /// </summary>
    public class _API_NAME_Controller : I_API_NAME_Controller
    {
        public virtual async Task<_API_NAME_PublicModel> Create(_API_NAME_CreateRequest request, CallContext context = default)
        {         
            throw new NotImplementedException();
        }

        public virtual async Task<_API_NAME_PublicModel> Delete(_API_NAME_DeleteRequest request, CallContext context = default)
        {
            throw new NotImplementedException();
        }

        public virtual  async Task<List<_API_NAME_PublicModel>> Fetch(_API_NAME_FetchRequest request, CallContext context = default)
        {
            // // Permissions and Access Control are being handled in the Wrapper of this class.
            // var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            // var db = provider.GetRequiredService<ApiDatabase>();
            // var myService = provider.GetRequiredService<_API_NAME_MyService>();

            // // Building Predicate Conditions
            // // Note: Reflection is being used and can be heavy when very large list of condition is given.
            // // you may want to restrict the number of condition, duplicate is allowed so the intent to slow the system can be used.
            // // By Default we allow 20 conditions longer than that will throw an error to protect the default system.
            // var predicate = myService.ConditionsPredicateBuilder(request.Conditions);

            // // Execute Predicate on Database.
            // var result = db._API_NAME_.Where(p => predicate(p)).Select(p=>(_API_NAME_PublicModel)p).ToList();
            // return result;
             throw new NotImplementedException();
        }

        public virtual async Task<_API_NAME_PublicModel> FetchOne(_API_NAME_FetchOneRequest request, CallContext context = default)
        {
            // var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            // var db = provider.GetRequiredService<ApiDatabase>();
            // var result = db._API_NAME_.Where(p => p.Id.Equals(request.Id)).FirstOrDefault();
            // return result;
             throw new NotImplementedException();
        }

        public virtual async Task<_API_NAME_PublicModel> Update(_API_NAME_UpdateOneRequest request, CallContext context = default)
        {
            // var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            // var db = provider.GetRequiredService<ApiDatabase>();
            // var myService = provider.GetRequiredService<_API_NAME_MyService>();
            // var updating = db._API_NAME_.Where(p => p.Id.Equals(request.Id)).FirstOrDefault();
            // // Map Fields One by One (No JSON, No Serializer, Simply Hard Handed... but Generated so... :D)
            // updating = myService.FilterUpdate(updating, (_API_NAME_Model)request.Element);
            // // Process References Update, Remove Unwanted, Add Wanted, Keep the Keeper
            // updating = myService.FilterUpdateReferences(db, updating, request);
            // return updating;
             throw new NotImplementedException();
        }

        public virtual async Task<List<_API_NAME_PublicModel>> UpdateMany(_API_NAME_UpdateManyRequest request, CallContext context = default)
        {
            throw new NotImplementedException();
        }
    }
}
