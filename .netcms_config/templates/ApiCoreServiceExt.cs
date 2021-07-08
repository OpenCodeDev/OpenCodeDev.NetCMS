//_NETCMS_HEADER_

using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
    /// Provide service with vital extension for Search System, Ordering System, Filter System and much more.
    /// </summary>
    public static class _API_NAME_CoreServiceExt
    {
        public static IQueryable<_API_NAME_Model> WhereConditionsMet(this IQueryable<_API_NAME_Model> query, List<_API_NAME_PredicateConditions> conditions)
        {
            bool nextFollowsLogic = false;
            ApiServiceBase myServiceBase = new ApiServiceBase();
            LogicTypes? nextBreakingLogic = null;
            Expression<Func<_API_NAME_PublicModel, bool>> expr = null;
            Expression<Func<_API_NAME_PublicModel, bool>> currentExpr = null;
            foreach (var item in conditions)
            {
                Expression<Func<_API_NAME_PublicModel, bool>> nonRelationField = p => myServiceBase.ConditionTypeDelegator(item.Conditions,
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
                    currentExpr = Expression.Lambda<Func<_API_NAME_PublicModel, bool>>(
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
            // If no Condition load any 
            expr = expr == null ? p => p != null : expr;
            Func<_API_NAME_PublicModel, bool> predFunc = expr.Compile();
            return query.Where(p => predFunc(p));
        }

        public static IEnumerable<_API_NAME_Model> WhereConditionsMet(this IEnumerable<_API_NAME_Model> query, List<_API_NAME_PredicateConditions> conditions)
        {
            bool nextFollowsLogic = false;
            ApiServiceBase myServiceBase = new ApiServiceBase();
            LogicTypes? nextBreakingLogic = null;
            Expression<Func<_API_NAME_PublicModel, bool>> expr = null;
            Expression<Func<_API_NAME_PublicModel, bool>> currentExpr = null;
            foreach (var item in conditions)
            {
                Expression<Func<_API_NAME_PublicModel, bool>> nonRelationField = p => myServiceBase.ConditionTypeDelegator(item.Conditions,
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
                    currentExpr = Expression.Lambda<Func<_API_NAME_PublicModel, bool>>(
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
            // If no Condition load any 
            expr = expr == null ? p => p != null : expr;
            Func<_API_NAME_PublicModel, bool> predFunc = expr.Compile();
            return query.Where(p => predFunc(p));
        }

    }


}
