using Grpc.Core;
using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;
using OpenCodeDev.NetCMS.Core.Server.Api;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using OpenCodeDev.NetCms.Shared._NetCMS_.Api.Recipe.Messages;
using OpenCodeDev.NetCMS.Core.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using OpenCodeDev.NetCms.Server.Database._Generated;
using OpenCodeDev.NetCms.Server.Api.Recipe.Models;

/*
 * From NetCMS Template
 * Created by Max Samson
 * AUTO GENERATED (DO NOT EDIT)
 * Base on Core System Api Services.
 */
namespace OpenCodeDev.NetCms.Server.Api.Recipe.Services._Generated
{

    // AUTO-GENERATE DO NOT EDIT

    /// <summary>
    /// This class is auto gen by NetCMS, it enables context specific core system... to reduce reflection use at runtime, we pre-generate at build time the core model.<br/>
    /// The function available should ALWAYS be available unless breaking update occure at the NetCMS-CLI and Core Level which you will have to responsability to check>br/>
    /// Note: your RecipeService will inherit this class any Core function can be overiden in your own way if a function if later remove it will be tag obselete several version prior and switch to throw Unimplemented as removal! if you override it you will be able to keep it running in the event of breaking update.
    /// </summary>
    internal class RecipeCoreService : ApiServiceBase
    {
        /// <summary>
        /// Condition Search Predicate Builder for Context of RecipeController
        /// </summary>
        /// <param name="conditions">List of condition</param>
        public virtual Predicate<RecipePublicModel> RecipeConditionHandler(List<RecipePredicateCondition> conditions)
        {
            Type _PublicModel = typeof(RecipePublicModel);
            Expression<Func<RecipePublicModel, bool>> predicate = PredicateBuilder.True<RecipePublicModel>();
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

            Func<RecipePublicModel, bool> predFunc = predicate.Compile();
            return p => predFunc(p);
        }


        public virtual IQueryable<RecipePublicModel> RecipeLoadReference(IQueryable<RecipePublicModel> model, RecipeReferences references){
            return model.Include(p => p.Ingredients);
        }


        public virtual RecipeModel RecipeFilterUpdate(RecipeModel current, RecipeModel changed)
        {
            // Map Field allowed to change ignore everything else.
            current.Duration = changed.Duration;
            current.Name = changed.Name;
            return current;
        }

        public virtual RecipeModel RecipeFilterUpdateReferences(DatabaseBase db, RecipeModel current, RecipeUpdateOneRequest request)
        {
            var _current = db.Recipes.Where(p => p.Id.Equals(current.Id)).First();

            // List Each Reference, See Behavior
            db.Entry(_current).Collection(p => p.Ingredients).Load();
            if (request.ReferenceBehavior.Ingredients == ReferenceEditBehavior.Process)
            {
                
            }
            return current;
        }

    }


}
