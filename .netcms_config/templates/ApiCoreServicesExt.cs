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
    public static class _API_NAME_CoreServicesExt
    {
                /// <summary>
        /// Query where given field condition are met.
        /// </summary>
        public static IQueryable<_API_NAME_Model> WhereConditionsMet(this IQueryable<_API_NAME_Model> query, List<_API_NAME_PredicateConditions> conditions)
        {
            if (conditions == null) { return query; }
            bool nextFollowsLogic = false;
            ApiServiceBase myServiceBase = new ApiServiceBase();
            LogicTypes? nextBreakingLogic = null;
            Expression<Func<_API_NAME_Model, bool>> expr = null;
            Expression<Func<_API_NAME_Model, bool>> currentExpr = null;
            foreach (var item in conditions)
            {
                Expression<Func<_API_NAME_Model, bool>> nonRelationField = null;
                switch (item.Field)
                {
                    //_WHERE_CONDITION_PUBLIC_FETCH_FIELDS_
                    // case RecipePredicateConditions.Fields.Duration:
                    //     nonRelationField = p => myServiceBase.ConditionTypeDelegator(item.Conditions, p.Duration, item.Value, typeof(Int32));
                    //     break;
                    // case RecipePredicateConditions.Fields.Name:
                    //     nonRelationField = p => myServiceBase.ConditionTypeDelegator(item.Conditions, p.Duration, item.Value, typeof(Int32));
                    //     break;
                    default:
                        break;
                }


                if (!nextFollowsLogic)
                {
                    currentExpr = nonRelationField;

                }
                else if (item.LogicalOperator == LogicTypes.And || item.LogicalOperator == LogicTypes.Or)
                {
                    if (expr == null) { expr = currentExpr; }
                    else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.And)
                    {
                        expr = Expression.Lambda<Func<_API_NAME_Model, bool>>(Expression.And(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                    }
                    else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.Or)
                    {
                        expr = Expression.Lambda<Func<_API_NAME_Model, bool>>(Expression.Or(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                    }
                    currentExpr = nonRelationField;
                    nextBreakingLogic = item.LogicalOperator == LogicTypes.And ? LogicTypes.And : LogicTypes.Or;
                }
                else if (item.LogicalOperator == LogicTypes.AndAlso)
                {
                    currentExpr = Expression.Lambda<Func<_API_NAME_Model, bool>>(
                    Expression.AndAlso(currentExpr.Body,
                    new ExpressionParameterReplacer(nonRelationField.Parameters, currentExpr.Parameters)
                        .Visit(nonRelationField.Body)), currentExpr.Parameters);
                }
                else if (item.LogicalOperator == LogicTypes.OrElse)
                {
                    currentExpr = Expression.Lambda<Func<_API_NAME_Model, bool>>(
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
                    expr = Expression.Lambda<Func<_API_NAME_Model, bool>>(Expression.And(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                }
                else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.Or)
                {
                    expr = Expression.Lambda<Func<_API_NAME_Model, bool>>(Expression.Or(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                }
            }
            // If no Condition load any 
            expr = expr == null ? p => p != null : expr;
            return query.Where(expr);
        }

        /// <summary>
        /// Query where given field condition are met.
        /// </summary>
        public static IEnumerable<_API_NAME_Model> WhereConditionsMet(this IEnumerable<_API_NAME_Model> query, List<_API_NAME_PredicateConditions> conditions)
        {
            if (conditions == null) { return query; }
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

        /// <summary>
        /// Convert user given field to real backing field from model.json and sort it by given direction.
        /// </summary>
        public static IOrderedEnumerable<_API_NAME_Model> OrderFieldConvert(this IEnumerable<_API_NAME_Model> query, _API_NAME_PredicateOrdering.Fields field, OrderType orderType)
        {
            switch (field)
            {
                //_MODEL_ORDERABLE_FIELD_ORDERBY_

                //[FOREACH]=>[_FIELD_PUBLIC_FETCH_]
                //[{ 
                // case _API_NAME_PredicateOrdering.Fields._FIELD_NAME_:
                // return orderType == OrderType.Ascending ? query.OrderBy(p => p._FIELD_NAME_) : query.OrderByDescending(p => p._FIELD_NAME_);
                //}]
            }
            return null;
        }

        /// <summary>
        /// Convert user given field to real backing field from model.json and sort it by given direction.
        /// </summary>
        public static IOrderedEnumerable<_API_NAME_Model> OrderFieldConvert(this IOrderedEnumerable<_API_NAME_Model> query, _API_NAME_PredicateOrdering.Fields field, OrderType orderType)
        {
            switch (field)
            {

                //_MODEL_ORDERABLE_FIELD_THENBY_

                //case _API_NAME_PredicateOrdering.Fields.Id:
                    //return orderType == OrderType.Ascending ? query.ThenBy(p => p.Id) : query.ThenByDescending(p => p.Id);
                default:
                    return query;
            }
        }

        /// <summary>
        /// Convert user given field to real backing field from model.json and sort it by given direction.
        /// </summary>
        public static IOrderedQueryable<_API_NAME_Model> OrderFieldConvert(this IQueryable<_API_NAME_Model> query, _API_NAME_PredicateOrdering.Fields field, OrderType orderType)
        {
            switch (field)
            {
                //_MODEL_ORDERABLE_FIELD_ORDERBY_
                //_FOR_EACH_MODEL_PUBLIC_ORDERABLE_FIELD_
                //[{ 
                // case _API_NAME_PredicateOrdering.Fields._FIELD_NAME_:
                // return orderType == OrderType.Ascending ? query.OrderBy(p => p._FIELD_NAME_) : query.OrderByDescending(p => p._FIELD_NAME_);
                //}]
                default:
                    return null;
            }
        }

        /// <summary>
        /// Convert user given field to real backing field from model.json and sort it by given direction.
        /// </summary>
        public static IOrderedQueryable<_API_NAME_Model> OrderFieldConvert(this IOrderedQueryable<_API_NAME_Model> query, _API_NAME_PredicateOrdering.Fields field, OrderType orderType)
        {
            switch (field)
            {
                //_MODEL_ORDERABLE_FIELD_THENBY_

                // case _API_NAME_PredicateOrdering.Fields.Id:
                //     return orderType == OrderType.Ascending ? query.ThenBy(p => p.Id) : query.ThenByDescending(p => p.Id);
                default:
                    return query;
            }
        }

        /// <summary>
        /// Order List by a given sets of rules.
        /// </summary>
        public static IQueryable<_API_NAME_Model> OrderByMatching(this IQueryable<_API_NAME_Model> query, List<_API_NAME_PredicateOrdering> order)
        {
            if (order == null) { return query; }
            bool notFirst = false;
            ApiServiceBase myServiceBase = new ApiServiceBase();
            IOrderedQueryable<_API_NAME_Model> ordering = null;
            foreach (var item in order)
            {
                if (!notFirst)
                {
                    ordering = query.OrderFieldConvert(item.Field, item.Order);
                    notFirst = true;
                }
                else
                {
                    ordering = ordering.OrderFieldConvert(item.Field, item.Order);
                }
            }

            if (ordering == null)
            {
                return query;
            }

            return ordering;
        }

        /// <summary>
        /// Order List by a given sets of rules.
        /// </summary>
        public static IEnumerable<_API_NAME_Model> OrderByMatching(this IEnumerable<_API_NAME_Model> query, List<_API_NAME_PredicateOrdering> order)
        {
            if (order == null) { return query; }
            bool notFirst = false;
            ApiServiceBase myServiceBase = new ApiServiceBase();
            IOrderedEnumerable<_API_NAME_Model> ordering = null;
            foreach (var item in order)
            {
                if (!notFirst)
                {
                    ordering = query.OrderFieldConvert(item.Field, item.Order);
                    notFirst = true;
                }
                else
                {
                    ordering = ordering.OrderFieldConvert(item.Field, item.Order);
                }
            }

            if (ordering == null)
            {
                return query;
            }

            return ordering;
        }
    }

}
