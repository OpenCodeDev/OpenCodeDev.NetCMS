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
        public void Test_Greather_Than_True_1()
        {
            Assert.IsTrue(GreaterThan(true, false));
        }

        [TestMethod]
        public void Test_Greather_Than_True_2()
        {
            Assert.IsFalse(GreaterThan(false, true));
        }

        [TestMethod]
        public void Test_Greather_Than_Int_1()
        {
            Assert.IsTrue(GreaterThan(3, 2));
        }

        [TestMethod]
        public void Test_Greather_Than_Int_2()
        {
            Assert.IsFalse(GreaterThan(3, 6));
        }

        [TestMethod]
        public void Test_Greather_Than_Int_3()
        {
            Assert.IsFalse(GreaterThan(3, 3));
        }

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
                 new TestPredicateConditions(){ Type= FieldTypes.Int, Conditions = ConditionTypes.GreaterThan, Field = TestPredicateConditions.Fields.Duration, NextLogic = LogicTypes.And, Value = "1" }
                },
                Limit = 10
            };

            var predicate = ConditionsPredicateBuilder(conditions.Conditions);
            try
            {
                List<TestModel> result = DbSet.Where(p => predicate(p)).ToList();
                throw new Exception(result.Count.ToString());
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
            Expression<Func<TestPublicModel, bool>> predicate = null;
            LogicTypes nextLogicToFollow = LogicTypes.And;
            bool nextFollowsLogic = false;
            foreach (var item in conditions)
            {
                PropertyInfo fieldInfo = _PublicModel.GetPropertyInfoByName(item.Field.ToString()) // Get Property by Name
                .ThrowWhenNull<PropertyInfo>(StatusCode.Unknown, $"The condition field {item.Field} is not available in the condition of fetch model. (You most likely need to update your client or wait several hours for next update deployment.");
                fieldInfo.ValidationPropTypeAllowed() // Ensure Underlying type is allowed to be considered a condition.
                .ThrowWhenFalse(StatusCode.InvalidArgument, $"The condition field {item.Field} is not supported type for condition of fetch.");

                if (!nextFollowsLogic)
                {
                    predicate = p => ConditionTypeDelegator(item.Conditions, p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value, p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetType());
                }
                else if (nextLogicToFollow == LogicTypes.And)
                {
                    predicate = predicate.And(p => ConditionTypeDelegator(item.Conditions, p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value, p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetType()));
                }
                else
                {
                    predicate = predicate.Or(p => ConditionTypeDelegator(item.Conditions, p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetValue(p), item.Value, p.GetType().GetPropertyInfoByName(item.Field.ToString()).GetType()));

                }

                nextFollowsLogic = true; // Next Loop will use nextLogic as predicate behavior
            }

            Func<TestPublicModel, bool> predFunc = predicate.Compile();
            return p => predFunc(p);
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
