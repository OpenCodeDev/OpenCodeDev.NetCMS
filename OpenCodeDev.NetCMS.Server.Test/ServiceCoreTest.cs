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
        [TestCategory("Predicate Builder")]
        public void Test_Predicate_Builder()
        {
            Guid predicedID = Guid.NewGuid();
            List<TestModel> DbSet = new List<TestModel>() {
                new TestModel() { Id = predicedID, Duration = 1, Name = "Half-Crooks" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "NetCMS.OpenCodeDev.com" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "Half-Way-Crooked" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 2, Name = "Fork-Repos" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "MS-Test" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "Test-Name" }
            };
            TestFetchRequest conditions = new TestFetchRequest()
            {
                Conditions = new List<TestPredicateConditions>() {
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.Equals,
                         Field = TestPredicateConditions.Fields.Id,
                         Value = predicedID.ToString()
                    },

                },
                Limit = 10
            };

            TestModel result = DbSet.WhereConditionsMet(conditions.Conditions).Take(1).First();
            TestModel resultCorrect = DbSet.Where(p => p.Id.Equals(predicedID)).Take(1).First();
            Assert.AreEqual<TestModel>(result, resultCorrect);

        }

        /// <summary>
        /// Test 2 Sets of condition using OR statement
        /// </summary>
        [TestMethod("Test 2 Sets of Condition OR")]
        [TestCategory("Predicate Builder")]
        public void Test_Predicate_Builder_2()
        {
            Guid predicedID = Guid.NewGuid();
            List<TestModel> DbSet = new List<TestModel>() {
                new TestModel() { Id = predicedID, Duration = 1, Name = "Half-Filled" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "NetCMS.OpenCodeDev.com" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "T-Way-Crooks" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 2, Name = "Fork-Repos" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "MS-Test" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "Test-Crooks" }
            };
            TestFetchRequest conditions = new TestFetchRequest()
            {
                Conditions = new List<TestPredicateConditions>() {
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.GreaterEqualThan,
                         Field = TestPredicateConditions.Fields.Duration,
                         Value = "1", LogicalOperator = LogicTypes.And
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.AndAlso, Value = "Crooks"
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.Equals,
                         Field = TestPredicateConditions.Fields.Id,
                         LogicalOperator = LogicTypes.Or, Value = predicedID.ToString()
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.StartsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.AndAlso, Value = "Half"
                    },
                },
                Limit = 10
            };

            List<TestModel> result = DbSet.WhereConditionsMet(conditions.Conditions).ToList();
            List<TestModel> result_correct = DbSet.Where(p => p.Duration >= int.Parse("1") && p.Name.EndsWith("Crooks")
            || p.Id.Equals(Guid.Parse(predicedID.ToString())) && p.Name.StartsWith("Half")).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            foreach (var cRez in result_correct) { Assert.IsTrue(result.Contains(cRez)); }

        }

        /// <summary>
        /// Test 2 Sets of condition with sub condition like (x = "1" && y = "2") || (x = "1" && (y = "4" || y = "5"))
        /// </summary>
        [TestMethod("Test 2 Sets of Condition + SubConditions")]
        [TestCategory("Predicate Builder")]
        public void Test_Predicate_Builder_3()
        {
            Guid predicedID = Guid.NewGuid();
            List<TestModel> DbSet = new List<TestModel>() {
                new TestModel() { Id = predicedID, Duration = 1, Name = "Max Samson" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "David Of Israel" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "Rabbi Jeremy Ishiakel" },
            };
            TestFetchRequest conditions = new TestFetchRequest()
            {
                Conditions = new List<TestPredicateConditions>() {
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.StartsWith, // Review Greater Equal from String
                         Field = TestPredicateConditions.Fields.Name,
                         Value = "Max"
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.AndAlso, Value = "Of Israel"
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.StartsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.Or, Value = "Max"
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.AndAlso, Value = "Ishiakel"
                    },
                    new TestPredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.OrElse, Value = "Samson"
                    },
                },
                Limit = 10
            };

            List<TestModel> result = DbSet.WhereConditionsMet(conditions.Conditions).ToList();
            List<TestModel> result_correct = DbSet.Where(p => p.Name.StartsWith("Max") && p.Name.EndsWith("Of Israel") ||
            p.Name.StartsWith("Max") && p.Name.EndsWith("Ishiakel") || p.Name.EndsWith("Samson")).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            foreach (var cRez in result_correct) { Assert.IsTrue(result.Contains(cRez)); }

        }

        /// <summary>
        /// Test 2 Sets of condition with sub condition like (x = "1" && y = "2") || (x = "1" && (y = "4" || y = "5"))
        /// </summary>
        [TestMethod("Test 0 Set of Condition, Return All")]
        [TestCategory("Predicate Builder")]
        public void Test_Predicate_Builder_4()
        {
            Guid predicedID = Guid.NewGuid();
            List<TestModel> DbSet = new List<TestModel>() {
                new TestModel() { Id = predicedID, Duration = 1, Name = "Max Samson" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "David Of Israel" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "Rabbi Jeremy Ishiakel" },
            };
            TestFetchRequest conditions = new TestFetchRequest()
            {
                Conditions = new List<TestPredicateConditions>() { },
                Limit = 10
            };


            List<TestModel> result = DbSet.WhereConditionsMet(conditions.Conditions).ToList();
            List<TestModel> result_correct = DbSet.Where(p => p != null).ToList();
            // Ensure Consistent Result between COndition Builder and Actual Linq Facts
            foreach (var cRez in result_correct) { Assert.IsTrue(result.Contains(cRez)); }

        }


        // public virtual IQueryable<RecipePublicModel> RecipeLoadReference(IQueryable<NAMESPACE_BASE_SHARED.Api.Recipe.Models.RecipePublicModel> model, RecipeReferences references){
        //     return model.Include(p => p.Ingredients);
        // }


        public virtual TestPublicModel FilterUpdate(TestModel current, TestModel changed)
        {
            //_UPDATE_FILTER_BODY_
            return current;
        }
    }
    
    public static class TestSearchExt{
        public static IQueryable<TestModel> WhereConditionsMet(this IQueryable<TestModel> query, List<TestPredicateConditions> conditions)
        {
            bool nextFollowsLogic = false;
            ApiServiceBase myServiceBase = new ApiServiceBase();
            LogicTypes? nextBreakingLogic = null;
            Expression<Func<TestPublicModel, bool>> expr = null;
            Expression<Func<TestPublicModel, bool>> currentExpr = null;
            foreach (var item in conditions)
            {
                Expression<Func<TestPublicModel, bool>> nonRelationField = p => myServiceBase.ConditionTypeDelegator(item.Conditions,
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
                        expr = Expression.Lambda<Func<TestPublicModel, bool>>(Expression.And(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                    }
                    else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.Or)
                    {
                        expr = Expression.Lambda<Func<TestPublicModel, bool>>(Expression.Or(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                    }
                    currentExpr = nonRelationField;
                    nextBreakingLogic = item.LogicalOperator == LogicTypes.And ? LogicTypes.And : LogicTypes.Or;
                }
                else if (item.LogicalOperator == LogicTypes.AndAlso)
                {
                    currentExpr = Expression.Lambda<Func<TestPublicModel, bool>>(
                    Expression.AndAlso(currentExpr.Body,
                    new ExpressionParameterReplacer(nonRelationField.Parameters, currentExpr.Parameters)
                        .Visit(nonRelationField.Body)), currentExpr.Parameters);
                }
                else if (item.LogicalOperator == LogicTypes.OrElse)
                {
                    currentExpr = Expression.Lambda<Func<TestPublicModel, bool>>(
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
                    expr = Expression.Lambda<Func<TestPublicModel, bool>>(Expression.And(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                }
                else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.Or)
                {
                    expr = Expression.Lambda<Func<TestPublicModel, bool>>(Expression.Or(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                }
            }
            // If no Condition load any 
            expr = expr == null ? p => p != null : expr;
            Func<TestPublicModel, bool> predFunc = expr.Compile();
            return query.Where(p => predFunc(p));
        }

        public static IEnumerable<TestModel> WhereConditionsMet(this IEnumerable<TestModel> query, List<TestPredicateConditions> conditions)
        {
            bool nextFollowsLogic = false;
            ApiServiceBase myServiceBase = new ApiServiceBase();
            LogicTypes? nextBreakingLogic = null;
            Expression<Func<TestPublicModel, bool>> expr = null;
            Expression<Func<TestPublicModel, bool>> currentExpr = null;
            foreach (var item in conditions)
            {
                Expression<Func<TestPublicModel, bool>> nonRelationField = p => myServiceBase.ConditionTypeDelegator(item.Conditions,
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
                        expr = Expression.Lambda<Func<TestPublicModel, bool>>(Expression.And(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                    }
                    else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.Or)
                    {
                        expr = Expression.Lambda<Func<TestPublicModel, bool>>(Expression.Or(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                    }
                    currentExpr = nonRelationField;
                    nextBreakingLogic = item.LogicalOperator == LogicTypes.And ? LogicTypes.And : LogicTypes.Or;
                }
                else if (item.LogicalOperator == LogicTypes.AndAlso)
                {
                    currentExpr = Expression.Lambda<Func<TestPublicModel, bool>>(
                    Expression.AndAlso(currentExpr.Body,
                    new ExpressionParameterReplacer(nonRelationField.Parameters, currentExpr.Parameters)
                        .Visit(nonRelationField.Body)), currentExpr.Parameters);
                }
                else if (item.LogicalOperator == LogicTypes.OrElse)
                {
                    currentExpr = Expression.Lambda<Func<TestPublicModel, bool>>(
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
                    expr = Expression.Lambda<Func<TestPublicModel, bool>>(Expression.And(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                }
                else if (expr != null && currentExpr != null && nextBreakingLogic != null && nextBreakingLogic == LogicTypes.Or)
                {
                    expr = Expression.Lambda<Func<TestPublicModel, bool>>(Expression.Or(expr.Body, new ExpressionParameterReplacer(currentExpr.Parameters, expr.Parameters).Visit(currentExpr.Body)), expr.Parameters);
                }
            }
            // If no Condition load any 
            expr = expr == null ? p => p != null : expr;
            Func<TestPublicModel, bool> predFunc = expr.Compile();
            return query.Where(p => predFunc(p));
        }

    }


    public class TestPublicModel
    {
        public System.String Name { get; set; }
        public System.Int32 Duration { get; set; }
        public System.Guid Id { get; set; }

    }

    public class TestModel : TestPublicModel
    {

    }
    public class TestFetchRequest
    {
        public List<TestPredicateConditions> Conditions { get; set; }
        public System.Int32 Limit { get; set; }
    }
    public class TestPredicateConditions : ConditionBase
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
