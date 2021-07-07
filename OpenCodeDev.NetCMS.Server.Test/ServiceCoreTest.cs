using Grpc.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCodeDev.NetCMS.Core.Server.Api;
using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;
using OpenCodeDev.NetCMS.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OpenCodeDev.NetCMS.Server.Test
{
    [TestClass]
    public class ServiceCoreTest : ApiServiceBase
    {



        [TestMethod]
        public void Test_Predicate_Builder(){
            Guid predicedID = Guid.NewGuid();
            List<TestModel> DbSet = new List<TestModel>() { 
                new TestModel() { Id = predicedID, Duration = 1, Name = "Half-Crooks" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "NetCMS.OpenCodeDev.com" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "Half-Way-Crooked" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 2, Name = "Fork-Repos" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "MS-Test" },
                new TestModel() { Id = Guid.NewGuid(), Duration = 1, Name = "Test-Name" }
            };
            TestFetchRequest conditions = new TestFetchRequest() { 
                Conditions = new List<TestPredicateConditions>() {
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.GreaterEqualThan,
                         Field = TestPredicateConditions.Fields.Duration,
                         LogicalOperator = LogicTypes.And, Value = "1"
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.And, Value = "Crooks"
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.Equals,
                         Field = TestPredicateConditions.Fields.Id,
                         LogicalOperator = LogicTypes.Or, Value = predicedID.ToString()
                    },

                },
                Limit = 10
            };

            var predicate = ConditionsPredicateBuilder(conditions.Conditions);
            List<TestModel> result = DbSet.Where(p => predicate(p)).ToList();
            Assert.AreEqual<int>(1, result.Count);

        }

        [TestMethod]
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
                         Value = "1"
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.EndsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.And, Value = "Crooks"
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.Equals,
                         Field = TestPredicateConditions.Fields.Id,
                         LogicalOperator = LogicTypes.Or, Value = predicedID.ToString()
                    },
                 new TestPredicateConditions(){
                         Conditions = ConditionTypes.StartsWith,
                         Field = TestPredicateConditions.Fields.Name,
                         LogicalOperator = LogicTypes.And, Value = "Half"
                    },
                },
                Limit = 10
            };

            var predicate = ConditionsPredicateBuilder(conditions.Conditions);
            try
            {
                List<TestModel> result = DbSet.Where(p => predicate(p)).ToList();
                List<TestModel> resultw = DbSet.Where(p => p.Duration >= int.Parse("1") && p.Name.EndsWith("Crooks") || p.Id.Equals(Guid.Parse(predicedID.ToString())) && p.Name.StartsWith("Half")).ToList();
                Assert.AreEqual<int>(1, result.Count);
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        /// <summary>
        /// Condition Search Predicate Builder for Context of RecipeController
        /// </summary>
        /// <param name="conditions">List of condition</param>
        public virtual Predicate<TestPublicModel> ConditionsPredicateBuilder(List<TestPredicateConditions> conditions)
        {
            Type _PublicModel = typeof(TestPublicModel);
            Expression<Func<TestPublicModel, bool>> predicate = p => false;
            bool nextFollowsLogic = false;
            // AndAlso and OrElse currently broken down
            Expression<Func<TestPublicModel, bool>> e1 = p => p.Duration == 1; 
            Expression<Func<TestPublicModel, bool>> e2 = p => p.Name == "dasd";
            Expression<Func<TestPublicModel, bool>> e4 = p => p.Name == "dascc";

            Expression<Func<TestPublicModel, bool>> e3 = Expression.Lambda<Func<TestPublicModel, bool>>(
                Expression.And(
                    e1.Body, new ExpressionParameterReplacer(e2.Parameters, e1.Parameters).Visit(e2.Body)),
                e1.Parameters);

            Expression<Func<TestPublicModel, bool>> e5 = Expression.Lambda<Func<TestPublicModel, bool>>(
            Expression.OrElse(
                e3.Body, new ExpressionParameterReplacer(e4.Parameters, e3.Parameters).Visit(e4.Body)),
            e3.Parameters);
            Func<TestPublicModel, bool> predFunc = e3.Compile();
            foreach (var item in conditions)
            {
                //if (!nextFollowsLogic)
                //{
                //    ParameterExpression param = Expression.Parameter(typeof(TestPublicModel), "p");
                //    Expression<Func<TestPublicModel, bool>> tmpExp = p => ConditionTypeDelegator(item.Conditions,
                //    p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value,
                //    p.GetType().GetProperty(item.Field.ToString()).GetUnderlyingPropertyTypeIfPossible());
                //    try
                //    {
                //        bracket = Expression.Lambda<Func<TestPublicModel, bool>>(tmpExp, param);
                //    }
                //    catch (Exception ex)
                //    {

                //        throw;
                //    }
                    
                //}
                //else if (item.LogicalOperator == LogicTypes.And)
                //{
                //    // Condition Expression
                //    Expression<Func<TestPublicModel, bool>> tmpExp = p => ConditionTypeDelegator(item.Conditions,
                //    p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value,
                //    p.GetType().GetProperty(item.Field.ToString()).GetUnderlyingPropertyTypeIfPossible());

                //    ParameterExpression param = Expression.Parameter(typeof(TestPublicModel), "p");
                //    // Combining Current Braket Expression.
                //    try
                //    {
                //        bracket = Expression
                //        .Lambda<Func<TestPublicModel, bool>>(
                //        Expression.AndAlso(bracket.Body, tmpExp.Body),
                //       param);
                //    }
                //    catch (Exception ex)
                //    {

                //        throw;
                //    }
        

                //}
                //else if (item.LogicalOperator == LogicTypes.Or)
                //{
                //    var parameter = Expression.Parameter(typeof(TestPublicModel));

                //    // Condition Expression
                //    Expression<Func<TestPublicModel, bool>> tmpExp = p => ConditionTypeDelegator(item.Conditions,
                //    p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value,
                //    p.GetType().GetProperty(item.Field.ToString()).GetUnderlyingPropertyTypeIfPossible());
                //    ParameterExpression param = Expression.Parameter(typeof(TestPublicModel), "p");
                //    // Combining Current Braket Expression.
                //    bracket = Expression
                //    .Lambda<Func<TestPublicModel, bool>>(
                //    Expression.OrElse(bracket.Body, tmpExp.Body), param);

                //}
                //nextFollowsLogic = true; // Next Loop will use nextLogic as predicate behavior
            }
            //try
            //{
            //    Func<TestPublicModel, bool> predFunc = bracket.Compile();
            //    return p => predFunc(p);
            //}
            //catch (Exception ed)
            //{

            //    throw;
            //}

            return null;
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
