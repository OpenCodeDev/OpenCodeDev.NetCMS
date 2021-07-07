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
            bool nextFollowsLogic = false;
            LogicTypes? nextBreakingLogic  = null;
            Expression<Func<_API_NAME_PublicModel, bool>> expr = null;
            Expression<Func<_API_NAME_PublicModel, bool>> currentExpr = null;
            foreach (var item in conditions)
            {
                Expression<Func<_API_NAME_PublicModel, bool>> nonRelationField = p => ConditionTypeDelegator(item.Conditions,
                    p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value,
                    p.GetType().GetProperty(item.Field.ToString()).GetUnderlyingPropertyTypeIfPossible());

                if (!nextFollowsLogic)
                {
                    currentExpr = nonRelationField;

                }
                else if (item.LogicalOperator == LogicTypes.And || item.LogicalOperator == LogicTypes.Or)
                {
                    if (expr == null) { expr = currentExpr; }
                    else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.And)
                    {
                        expr = Expression.Lambda<Func<_API_NAME_PublicModel, bool>>(Expression.And(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                    }
                    else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.Or)
                    {
                        expr = Expression.Lambda<Func<_API_NAME_PublicModel, bool>>(Expression.Or(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                    }
                    currentExpr = nonRelationField;
                    nextBreakingLogic = item.LogicalOperator == LogicTypes.And ? LogicTypes.And : LogicTypes.Or;
                }
                else if (item.LogicalOperator == LogicTypes.AndAlso)
                {
                    currentExpr = Expression.Lambda<Func<_API_NAME_PublicModel, bool>>(
                    Expression.AndAlso(currentExpr.Body,
                    new ExpressionParameterReplacer(nonRelationField.Parameters, currentExpr.Parameters)
                        .Visit(nonRelationField.Body)), currentExpr.Parameters);
                }
                else if (item.LogicalOperator == LogicTypes.OrElse)
                {
                    currentExpr =  Expression.Lambda<Func<_API_NAME_PublicModel, bool>>(
                    Expression.OrElse(currentExpr.Body,
                    new ExpressionParameterReplacer(nonRelationField.Parameters, currentExpr.Parameters)
                        .Visit(nonRelationField.Body)), currentExpr.Parameters);
                }
                nextFollowsLogic = true; // Next Loop will use nextLogic as predicate behavior
            }

            if (currentExpr != null)
            {
                if (expr == null) { expr = currentExpr; }
                else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.And)
                {
                    expr = Expression.Lambda<Func<_API_NAME_PublicModel, bool>>(Expression.And(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                }
                else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.Or)
                {
                    expr = Expression.Lambda<Func<_API_NAME_PublicModel, bool>>(Expression.Or(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                }
            }
                Func<_API_NAME_PublicModel, bool> predFunc = expr.Compile();
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
