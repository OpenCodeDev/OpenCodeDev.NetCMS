//_NETCMS_HEADER_

using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

//NetCMS Server Core System
using OpenCodeDev.NetCMS.Core.Server.Api;

//NetCMS Shared Core System
using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;
using OpenCodeDev.NetCMS.Core.Shared.Extensions;

// Code Namespaces
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Models;
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages;

// Server Resources
using _NAMESPACE_BASE_SERVER_.Database;
using _NAMESPACE_BASE_SERVER_.Api._API_NAME_.Models;

namespace _NAMESPACE_BASE_SERVER_.Api._API_NAME_.Services
{

    // AUTO-GENERATE DO NOT EDIT

    /// <summary>
    /// This class is auto gen by NetCMS, it enables context specific core system... to reduce reflection use at runtime, we pre-generate at build time the core model.<br/>
    /// The function available should ALWAYS be available unless breaking update occure at the NetCMS-CLI and Core Level which you will have to responsability to check>br/>
    /// Note: your RecipeService will inherit this class any Core function can be overiden in your own way if a function if later remove it will be tag obselete several version prior and switch to throw Unimplemented as removal! if you override it you will be able to keep it running in the event of breaking update.
    /// </summary>
    internal class _API_NAME_CoreService : ApiServiceBase
    {
      
        // public virtual IQueryable<_API_NAME_PublicModel> RecipeLoadReference(IQueryable<NAMESPACE_BASE_SHARED.Api._API_NAME_.Models._API_NAME_PublicModel> model, RecipeReferences references){
        //     return model.Include(p => p.Ingredients);
        // }

        
        public virtual _API_NAME_Model FilterUpdate(_API_NAME_Model current, _API_NAME_Model changed)
        {
            //_UPDATE_FILTER_BODY_
            return current;
        }

        public virtual _API_NAME_Model FilterUpdateReferences(DatabaseBase db, _API_NAME_Model current, _API_NAME_UpdateOneRequest request)
        {
            // var _current = db._API_NAME_.Where(p => p.Id.Equals(current.Id)).First();

            // List Each Reference, See Behavior
            // db.Entry(_current).Collection(p => p.Ingredients).Load();
            // if (request.ReferenceBehavior.Ingredients == ReferenceEditBehavior.Process)
            // {
                
            // }
            return current;
        }

    }


}
