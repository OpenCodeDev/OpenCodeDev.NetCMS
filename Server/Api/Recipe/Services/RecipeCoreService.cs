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

/*
 * From NetCMS Template
 * Created by Max Samson
 * AUTO GENERATED (DO NOT EDIT)
 * Base on Core System Api Services.
 */
namespace OpenCodeDev.NetCms.Server.Api.Recipe.Services
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
        public virtual Predicate<RecipePublicModel> RecipeConditionHandler(List<RecipeCondition> conditions)
        {
            Expression<Func<RecipePublicModel, bool>> predicate = PredicateBuilder.True<RecipePublicModel>();
            Type model = typeof(RecipePublicModel);
            LogicTypes nextLogic = LogicTypes.And;
            bool nextFollowLogic = false;

            foreach (var item in conditions)
            {
                PropertyInfo fieldInfo = null;
                try { fieldInfo = model.GetProperty(item.Field.ToString()); } catch (Exception) { fieldInfo = null; }
                if (fieldInfo == null)
                {
                    Console.WriteLine($@"ERROR (RecipeConditionHandler): The Field ${item.Field} doesn't match any field. Most likely you didn't use NetCMS Compiler (netcms build ""netcms.json"")");
                    throw new RpcException(new Status(StatusCode.Internal, "An unknown error has occured, the admin has been notified. try again later or contact support."));
                }

                Type fieldType = null;
                try { fieldType = Nullable.GetUnderlyingType(fieldInfo.PropertyType); } catch (Exception) { fieldType = null; }
                if (fieldType == null) { fieldType = fieldInfo.PropertyType; }
                if (fieldType == null)
                {
                    Console.WriteLine($@"ERROR (RecipeConditionHandler): The Field ${item.Field} was found but the type of the field doesn't return anything, which is an internal problem.");
                    throw new RpcException(new Status(StatusCode.Internal, "An unknown error has occured, the admin has been notified. try again later or contact support."));
                }

                // Check if condition type doesn't match the model type
                if (!fieldType.ToString().Equals(item.Type.ToSystemString()))
                {
                    Console.WriteLine($"ERROR (RecipeConditionHandler): The Field ${item.Field} has the wrong type {{Given: ${item.Type.ToSystemString()}, Reality: {fieldType}}}");
                    throw new RpcException(new Status(StatusCode.Internal, "An unknown error has occured, the admin has been notified. try again later or contact support."));
                }
                if (nextFollowLogic)
                {
                    switch (nextLogic)
                    {
                        case LogicTypes.And:
                            predicate = predicate.And(p => ConditionTypeDelegator(item.Conditions, p.GetType().GetProperty(item.Field.ToString()).GetValue(p), item.Value, item.Type));
                            break;
                        case LogicTypes.Or:
                            predicate = predicate.Or(p => ConditionTypeDelegator(item.Conditions, p.GetType().GetProperty(item.Field.ToString()).GetValue(p), item.Value, item.Type));
                            break;
                    }
                }
                else
                {
                    predicate = predicate.And(p => ConditionTypeDelegator(item.Conditions, p.GetType().GetProperty(item.Field.ToString()).GetValue(p), item.Value, item.Type));
                }

                nextFollowLogic = true; // Next Loop will use nextLogic as predicate behavior
            }

            Func<RecipePublicModel, bool> predFunc = predicate.Compile();
            return p => predFunc(p);
        }


    }


}
