using Grpc.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCodeDev.NetCMS.Core.Server.Api;
using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;
using OpenCodeDev.NetCMS.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OpenCodeDev.NetCMS.Server.Test
{
    [TestClass]
    public class ServiceCoreTest
    {


        /// <summary>
        /// Get row by ID
        /// </summary>
        [TestMethod("Test 1 Set of Condition")]
        [TestCategory("Condition Builder")]
        public void Test_Predicate_Builder()
        {
            Guid predicedID = Guid.NewGuid();
            List<_API_NAME_Model> DbSet = new List<_API_NAME_Model>() {
                new _API_NAME_Model() { Id = predicedID, Duration = 1, Name = "Half-Crooks" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "NetCMS.OpenCodeDev.com" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "Half-Way-Crooked" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "Fork-Repos" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "MS-Test" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "Test-Name" }
            };
            _API_NAME_FetchRequest conditions = new _API_NAME_FetchRequest()
            {
                Conditions = new List<_API_NAME_PredicateConditions>() {
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.Equals,
                         Field = _API_NAME_PredicateConditions.Fields.Id,
                         Value = predicedID.ToString()
                    },

                },
                Limit = 10
            };

            _API_NAME_Model result = DbSet.WhereConditionsMet(conditions.Conditions).Take(1).First();
            _API_NAME_Model resultCorrect = DbSet.Where(p => p.Id.Equals(predicedID)).Take(1).First();
            Assert.AreEqual<_API_NAME_Model>(result, resultCorrect);

        }

        /// <summary>
        /// Test 2 Sets of condition using OR statement
        /// </summary>
        [TestMethod("Test 2 Sets of Condition OR")]
        [TestCategory("Condition Builder")]
        public void Test_Predicate_Builder_2()
        {
            Guid predicedID = Guid.NewGuid();
            List<_API_NAME_Model> DbSet = new List<_API_NAME_Model>() {
                new _API_NAME_Model() { Id = predicedID, Duration = 1, Name = "Half-Filled" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "NetCMS.OpenCodeDev.com" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "T-Way-Crooks" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "Fork-Repos" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "MS-Test" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "Test-Crooks" }
            };
            _API_NAME_FetchRequest conditions = new _API_NAME_FetchRequest()
            {
                Conditions = new List<_API_NAME_PredicateConditions>() {
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.GreaterEqualThan,
                         Field = _API_NAME_PredicateConditions.Fields.Duration,
                         Value = "1", LogicalOperator = LogicTypes.And
                    },
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = _API_NAME_PredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.AndAlso, Value = "Crooks"
                    },
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.Equals,
                         Field = _API_NAME_PredicateConditions.Fields.Id,
                         LogicalOperator = LogicTypes.Or, Value = predicedID.ToString()
                    },
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.StartsWith,
                         Field = _API_NAME_PredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.AndAlso, Value = "Half"
                    },
                },
                Limit = 10
            };

            List<_API_NAME_Model> result = DbSet.WhereConditionsMet(conditions.Conditions).ToList();
            List<_API_NAME_Model> result_correct = DbSet.Where(p => p.Duration >= int.Parse("1") && p.Name.EndsWith("Crooks")
            || p.Id.Equals(Guid.Parse(predicedID.ToString())) && p.Name.StartsWith("Half")).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            foreach (var cRez in result_correct) { Assert.IsTrue(result.Contains(cRez)); }

        }

        /// <summary>
        /// Test 2 Sets of condition with sub condition like (x = "1" && y = "2") || (x = "1" && (y = "4" || y = "5"))
        /// </summary>
        [TestMethod("Test 2 Sets of Condition + SubConditions")]
        [TestCategory("Condition Builder")]
        public void Test_Predicate_Builder_3()
        {
            Guid predicedID = Guid.NewGuid();
            List<_API_NAME_Model> DbSet = new List<_API_NAME_Model>() {
                new _API_NAME_Model() { Id = predicedID, Duration = 1, Name = "Max Samson" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "David Of Israel" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "Rabbi Jeremy Ishiakel" },
            };
            _API_NAME_FetchRequest conditions = new _API_NAME_FetchRequest()
            {
                Conditions = new List<_API_NAME_PredicateConditions>() {
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.StartsWith, // Review Greater Equal from String
                         Field = _API_NAME_PredicateConditions.Fields.Name,
                         Value = "Max"
                    },
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = _API_NAME_PredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.AndAlso, Value = "Of Israel"
                    },
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.StartsWith,
                         Field = _API_NAME_PredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.Or, Value = "Max"
                    },
                 new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = _API_NAME_PredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.AndAlso, Value = "Ishiakel"
                    },
                    new _API_NAME_PredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = _API_NAME_PredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.OrElse, Value = "Samson"
                    },
                },
                Limit = 10
            };

            List<_API_NAME_Model> result = DbSet.WhereConditionsMet(conditions.Conditions).ToList();
            List<_API_NAME_Model> result_correct = DbSet.Where(p => p.Name.StartsWith("Max") && p.Name.EndsWith("Of Israel") ||
            p.Name.StartsWith("Max") && p.Name.EndsWith("Ishiakel") || p.Name.EndsWith("Samson")).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            foreach (var cRez in result_correct) { Assert.IsTrue(result.Contains(cRez)); }

        }

        /// <summary>
        /// Test 2 Sets of condition with sub condition like (x = "1" && y = "2") || (x = "1" && (y = "4" || y = "5"))
        /// </summary>
        [TestMethod("Test 0 Set of Condition, Return All")]
        [TestCategory("Condition Builder")]
        public void Test_Predicate_Builder_4()
        {
            Guid predicedID = Guid.NewGuid();
            List<_API_NAME_Model> DbSet = new List<_API_NAME_Model>() {
                new _API_NAME_Model() { Id = predicedID, Duration = 1, Name = "Max Samson" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "David Of Israel" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "Rabbi Jeremy Ishiakel" },
            };
            _API_NAME_FetchRequest conditions = new _API_NAME_FetchRequest()
            {
                Conditions = new List<_API_NAME_PredicateConditions>() { },
                Limit = 10
            };


            List<_API_NAME_Model> result = DbSet.WhereConditionsMet(conditions.Conditions).ToList();
            List<_API_NAME_Model> result_correct = DbSet.Where(p => p != null).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            foreach (var cRez in result_correct) { Assert.IsTrue(result.Contains(cRez)); }

        }

        [TestMethod("OrderBy Name=>ASC Then Duration=>DESC")]
        [TestCategory("OrderBy Builder")]
        public void Test_Predicate_OrderBy_Builder_1()
        {
            Guid predicedID = Guid.NewGuid();
            List<_API_NAME_Model> DbSet = new List<_API_NAME_Model>() {
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 3, Name = "A" },
            };

            _API_NAME_FetchRequest conditions = new _API_NAME_FetchRequest()
            {
                Conditions = new List<_API_NAME_PredicateConditions>() { },
                OrderBy = new List<_API_NAME_PredicateOrdering>() { 
                new _API_NAME_PredicateOrdering() { Field = _API_NAME_PredicateOrdering.Fields.Name, Order = OrderType.Ascending }, 
                new _API_NAME_PredicateOrdering() { Field = _API_NAME_PredicateOrdering.Fields.Duration, Order = OrderType.Descending },
                },
                Limit = 10
            };


            List<_API_NAME_Model> result = DbSet.WhereConditionsMet(conditions.Conditions).OrderByMatching(conditions.OrderBy).ToList();
            List<_API_NAME_Model> result_correct = DbSet.Where(p => p != null).OrderBy(p=>p.Name).ThenByDescending(p=>p.Duration).ToList();
            Assert.AreEqual<int>(result.Count, result_correct.Count);
            foreach (var cRez in result_correct)
            {
                Assert.IsTrue(result.Contains(cRez));
                Assert.IsTrue(result.IndexOf(cRez) == result_correct.IndexOf(cRez));
            }

        }

        [TestMethod("OrderBy Name=>DESC Then Duration=>ASC")]
        [TestCategory("OrderBy Builder")]
        public void Test_Predicate_OrderBy_Builder_2()
        {
            Guid predicedID = Guid.NewGuid();
            List<_API_NAME_Model> DbSet = new List<_API_NAME_Model>() {
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 3, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "B" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "B" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 3, Name = "B" },
            };

            _API_NAME_FetchRequest conditions = new _API_NAME_FetchRequest()
            {
                Conditions = new List<_API_NAME_PredicateConditions>() { },
                OrderBy = new List<_API_NAME_PredicateOrdering>() {
                new _API_NAME_PredicateOrdering() { Field = _API_NAME_PredicateOrdering.Fields.Name, Order = OrderType.Descending },
                new _API_NAME_PredicateOrdering() { Field = _API_NAME_PredicateOrdering.Fields.Duration, Order = OrderType.Ascending },
                },
                Limit = 10
            };


            List<_API_NAME_Model> result = DbSet.WhereConditionsMet(conditions.Conditions).OrderByMatching(conditions.OrderBy).ToList();
            List<_API_NAME_Model> result_correct = DbSet.Where(p => p != null).OrderByDescending(p => p.Name).ThenBy(p => p.Duration).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            Assert.AreEqual<int>(result.Count, result_correct.Count);
            foreach (var cRez in result_correct) {
                Assert.IsTrue(result.Contains(cRez));
                Assert.IsTrue(result.IndexOf(cRez) == result_correct.IndexOf(cRez));
            }

        }

        [TestMethod("OrderBy Name=>DESC Then Duration=>DESC")]
        [TestCategory("OrderBy Builder")]
        public void Test_Predicate_OrderBy_Builder_3()
        {
            Guid predicedID = Guid.NewGuid();
            List<_API_NAME_Model> DbSet = new List<_API_NAME_Model>() {
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 3, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "B" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "B" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 3, Name = "B" },
            };

            _API_NAME_FetchRequest conditions = new _API_NAME_FetchRequest()
            {
                Conditions = new List<_API_NAME_PredicateConditions>() { },
                OrderBy = new List<_API_NAME_PredicateOrdering>() {
                new _API_NAME_PredicateOrdering() { Field = _API_NAME_PredicateOrdering.Fields.Name, Order = OrderType.Descending },
                new _API_NAME_PredicateOrdering() { Field = _API_NAME_PredicateOrdering.Fields.Duration, Order = OrderType.Descending },
                },
                Limit = 10
            };


            List<_API_NAME_Model> result = DbSet.WhereConditionsMet(conditions.Conditions).OrderByMatching(conditions.OrderBy).ToList();
            List<_API_NAME_Model> result_correct = DbSet.Where(p => p != null).OrderByDescending(p => p.Name).ThenByDescending(p => p.Duration).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            Assert.AreEqual<int>(result.Count, result_correct.Count);
            foreach (var cRez in result_correct)
            {
                Assert.IsTrue(result.Contains(cRez));
                Assert.IsTrue(result.IndexOf(cRez) == result_correct.IndexOf(cRez));
            }

        }

        [TestMethod("OrderBy Duration=>DESC Then Name=>ASC")]
        [TestCategory("OrderBy Builder")]
        public void Test_Predicate_OrderBy_Builder_4()
        {
            Guid predicedID = Guid.NewGuid();
            List<_API_NAME_Model> DbSet = new List<_API_NAME_Model>() {
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 3, Name = "A" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 1, Name = "B" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 2, Name = "B" },
                new _API_NAME_Model() { Id = Guid.NewGuid(), Duration = 3, Name = "B" },
            };

            _API_NAME_FetchRequest conditions = new _API_NAME_FetchRequest()
            {
                Conditions = new List<_API_NAME_PredicateConditions>() { },
                OrderBy = new List<_API_NAME_PredicateOrdering>() {
                new _API_NAME_PredicateOrdering() { Field = _API_NAME_PredicateOrdering.Fields.Duration, Order = OrderType.Descending },
                new _API_NAME_PredicateOrdering() { Field = _API_NAME_PredicateOrdering.Fields.Name, Order = OrderType.Ascending },
                
                },
                Limit = 10
            };


            List<_API_NAME_Model> result = DbSet.WhereConditionsMet(conditions.Conditions).OrderByMatching(conditions.OrderBy).ToList();
            List<_API_NAME_Model> result_correct = DbSet.Where(p => p != null).OrderByDescending(p => p.Duration).ThenBy(p => p.Name).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            Assert.AreEqual<int>(result.Count, result_correct.Count);
            foreach (var cRez in result_correct)
            {
                Assert.IsTrue(result.Contains(cRez));
                Assert.IsTrue(result.IndexOf(cRez) == result_correct.IndexOf(cRez));
            }

        }

    }

    public static class API_NAME_SearchExt
    {
        /// <summary>
        /// Query where given field condition are met.
        /// </summary>
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

        /// <summary>
        /// Query where given field condition are met.
        /// </summary>
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

       
        
        /// <summary>
        /// Convert user given field to real backing field from model.json and sort it by given direction.
        /// </summary>
        public static IOrderedEnumerable<_API_NAME_Model> OrderFieldConvert(this IEnumerable<_API_NAME_Model> query, _API_NAME_PredicateOrdering.Fields field, OrderType orderType)
        {
            switch (field)
            {
                //_FOR_EACH_MODEL_PUBLIC_ORDERABLE_FIELD_
                // [{ 
                // case _API_NAME_PredicateOrdering.Fields._FIELD_NAME_:
                // return orderType == OrderType.Ascending ? query.OrderBy(p => p._FIELD_NAME_) : query.OrderByDescending(p => p._FIELD_NAME_);
                // }]
                case _API_NAME_PredicateOrdering.Fields.Id:
                    return orderType == OrderType.Ascending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                case _API_NAME_PredicateOrdering.Fields.Name:
                    return orderType == OrderType.Ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                case _API_NAME_PredicateOrdering.Fields.Duration:
                    return orderType == OrderType.Ascending ? query.OrderBy(p => p.Duration) : query.OrderByDescending(p => p.Duration);
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
                //_FOR_EACH_MODEL_PUBLIC_ORDERABLE_FIELD_
                // [{ 
                // case _API_NAME_PredicateOrdering.Fields._FIELD_NAME_:
                // return orderType == OrderType.Ascending ? query.OrderBy(p => p._FIELD_NAME_) : query.OrderByDescending(p => p._FIELD_NAME_);
                // }]
                case _API_NAME_PredicateOrdering.Fields.Id:
                    return orderType == OrderType.Ascending ? query.ThenBy(p => p.Id) : query.ThenByDescending(p => p.Id);
                case _API_NAME_PredicateOrdering.Fields.Name:
                    return orderType == OrderType.Ascending ? query.ThenBy(p => p.Name) : query.ThenByDescending(p => p.Name);
                case _API_NAME_PredicateOrdering.Fields.Duration:
                    return orderType == OrderType.Ascending ? query.ThenBy(p => p.Duration) : query.ThenByDescending(p => p.Duration);
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
                //_FOR_EACH_MODEL_PUBLIC_ORDERABLE_FIELD_
                // [{ 
                // case _API_NAME_PredicateOrdering.Fields._FIELD_NAME_:
                // return orderType == OrderType.Ascending ? query.OrderBy(p => p._FIELD_NAME_) : query.OrderByDescending(p => p._FIELD_NAME_);
                // }]
                case _API_NAME_PredicateOrdering.Fields.Id:
                    return orderType == OrderType.Ascending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
                case _API_NAME_PredicateOrdering.Fields.Name:
                    return orderType == OrderType.Ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                case _API_NAME_PredicateOrdering.Fields.Duration:
                    return orderType == OrderType.Ascending ? query.OrderBy(p => p.Duration) : query.OrderByDescending(p => p.Duration);
            }
            return null;
        }

        /// <summary>
        /// Convert user given field to real backing field from model.json and sort it by given direction.
        /// </summary>
        public static IOrderedQueryable<_API_NAME_Model> OrderFieldConvert(this IOrderedQueryable<_API_NAME_Model> query, _API_NAME_PredicateOrdering.Fields field, OrderType orderType)
        {
            switch (field)
            {
                //_FOR_EACH_MODEL_PUBLIC_ORDERABLE_FIELD_
                // [{ 
                // case _API_NAME_PredicateOrdering.Fields._FIELD_NAME_:
                // return orderType == OrderType.Ascending ? query.OrderBy(p => p._FIELD_NAME_) : query.OrderByDescending(p => p._FIELD_NAME_);
                // }]
                case _API_NAME_PredicateOrdering.Fields.Id:
                    return orderType == OrderType.Ascending ? query.ThenBy(p => p.Id) : query.ThenByDescending(p => p.Id);
                case _API_NAME_PredicateOrdering.Fields.Name:
                    return orderType == OrderType.Ascending ? query.ThenBy(p => p.Name) : query.ThenByDescending(p => p.Name);
                case _API_NAME_PredicateOrdering.Fields.Duration:
                    return orderType == OrderType.Ascending ? query.ThenBy(p => p.Duration) : query.ThenByDescending(p => p.Duration);
                default:
                    return query;
            }
        }

        /// <summary>
        /// Order List by a given sets of rules.
        /// </summary>
        public static IQueryable<_API_NAME_Model> OrderByMatching(this IQueryable<_API_NAME_Model> query, List<_API_NAME_PredicateOrdering> order)
        {
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


    public class _API_NAME_PublicModel
    {
        public System.String Name { get; set; }
        public System.Int32 Duration { get; set; }
        public System.Guid Id { get; set; }

    }

    public class _API_NAME_Model : _API_NAME_PublicModel
    {

    }
    public class _API_NAME_FetchRequest
    {
        public List<_API_NAME_PredicateConditions> Conditions { get; set; }
        public List<_API_NAME_PredicateOrdering> OrderBy { get; set; }

        public System.Int32 Limit { get; set; }
    }
    public class _API_NAME_PredicateConditions : ConditionBase
    {
        public Fields Field { get; set; }
        public enum Fields
        {
            Id,
            Name,
            Duration
        }

        public Type GetFieldType()
        {
            switch (Field)
            {
                case Fields.Id:
                    return typeof(System.Guid);
                case Fields.Name:
                    return typeof(System.String);
                case Fields.Duration:
                    return typeof(System.Int32);
            }
            throw new RpcException(new Status(StatusCode.Unknown, "Server misconfiguration tries to pass un supported type of data, contact support."));
        }


    }

    public class _API_NAME_PredicateOrdering : OrderByBase
    {
        public Fields Field { get; set; }
        public enum Fields
        {
            Id,
            Name,
            Duration
        }

        public Type GetFieldType()
        {
            switch (Field)
            {
                case Fields.Id:
                    return typeof(System.Guid);
                case Fields.Name:
                    return typeof(System.String);
                case Fields.Duration:
                    return typeof(System.Int32);
            }
            throw new RpcException(new Status(StatusCode.Unknown, "Server misconfiguration tries to pass un supported type of data, contact support."));
        }


    }



}
