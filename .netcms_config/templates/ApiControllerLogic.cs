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

// NetCMS Core Sever
using OpenCodeDev.NetCMS.Core.Server.Extensions;

namespace _NAMESPACE_BASE_SERVER_.Api._API_NAME_.Controllers
{   
    /// <summary>
    /// This class provides you with fully functional common api CRUD logic.<br/>
    /// You can also inherit this class and override the logic to use your own custom logic. (Not Recommended)
    /// </summary>
    public class _API_NAME_Controller
    {
        public virtual async Task<_API_NAME_PublicModel> Create(_API_NAME_CreateRequest request, CallContext context = default)
        {         
            throw new NotImplementedException();
        }

        public virtual async Task<_API_NAME_PublicModel> Delete(_API_NAME_DeleteRequest request, CallContext context = default)
        {
            var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            var db = provider.GetRequiredService<ApiDatabase>();
            var result = db._API_NAME_.Where(p => p.Id.Equals(request.Id)).FirstOrDefault();
            if (result != null) { db._API_NAME_.Remove(result); }
            else{
                throw new RpcException(new Status(StatusCode.NotFound, "Cannot delete because entry wasn't found."));
            }            
            await db.SaveChangesAsync();
            return result;
        }

        public virtual  async Task<List<_API_NAME_PublicModel>> Fetch(_API_NAME_FetchRequest request, CallContext context = default)
        {
            var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            var db = provider.GetRequiredService<ApiDatabase>();

            var result = db._API_NAME_
            .WhereConditionsMet(request.Conditions)
            .OrderByMatching(request.OrderBy)
            .Take(request.Limit).Select(p=>(_API_NAME_PublicModel)p).ToList();

            return result;
        }

        public virtual async Task<_API_NAME_PublicModel> FetchOne(_API_NAME_FetchOneRequest request, CallContext context = default)
        {
            var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            var db = provider.GetRequiredService<ApiDatabase>();
            var result = db._API_NAME_.Where(p => p.Id.Equals(request.Id)).FirstOrDefault();
            if (result == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Cannot find {request.Id} because entry wasn't found."));
            }
            return result;
        }

        public virtual async Task<_API_NAME_PublicModel> Update(_API_NAME_UpdateOneRequest request, CallContext context = default)
        {
            var provider = context.ServerCallContext.GetHttpContext().RequestServices;
            var db = provider.GetRequiredService<ApiDatabase>();
            var myService = provider.GetRequiredService<_API_NAME_MyService>();
            var updating = db._API_NAME_.Where(p => p.Id.Equals(request.Id)).FirstOrDefault();
            if(updating == null){
                throw new RpcException(new Status(StatusCode.NotFound, $"Cannot update {request.Id} because entry wasn't found."));
            }
            updating = myService.FilterUpdate(updating, (_API_NAME_Model)request.Element);
            // updating = myService.FilterUpdateReferences(db, updating, request);
            db._API_NAME_.Add(updating);
            await db.SaveChangesAsync();
            return updating;
        }
    }
}
