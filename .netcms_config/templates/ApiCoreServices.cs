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
        /// <summary>
        /// Condition Search Predicate Builder for Context of RecipeController
        /// </summary>
        /// <param name="conditions">List of condition</param>
        public virtual Predicate<_API_NAME_PublicModel> ConditionsPredicateBuilder(List<_API_NAME_PredicateConditions> conditions)
        {
            Type _PublicModel = typeof(_API_NAME_PublicModel);
            Expression<Func<_API_NAME_PublicModel, bool>> predicate = PredicateBuilder.True<_API_NAME_PublicModel>();
            LogicTypes nextLogicToFollow = LogicTypes.And;
            bool nextFollowsLogic = false;
            foreach (var item in conditions) {
                PropertyInfo fieldInfo = _PublicModel.GetPropertyInfoByName(item.Field.ToString()) // Get Property by Name
                .ThrowWhenNull<PropertyInfo>(StatusCode.Unknown, $"The condition field {item.Field} is not available in the condition of fetch model. (You most likely need to update your client or wait several hours for next update deployment.");
                fieldInfo.ValidationPropTypeAllowed() // Ensure Underlying type is allowed to be considered a condition.
                .ThrowWhenFalse(StatusCode.InvalidArgument, $"The condition field {item.Field} is not supported type for condition of fetch.");
               
                if (!nextFollowsLogic || nextLogicToFollow == LogicTypes.And)
                {
                    predicate = predicate.And(p => ConditionTypeDelegator(item.Conditions, p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value, item.Type));
                } else {
                    predicate = predicate.Or(p => ConditionTypeDelegator(item.Conditions, p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value, item.Type));

                }

                nextFollowsLogic = true; // Next Loop will use nextLogic as predicate behavior
            }

            Func<_API_NAME_PublicModel, bool> predFunc = predicate.Compile();
            return p => predFunc(p);
        }


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
