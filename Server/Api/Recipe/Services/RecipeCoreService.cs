using Grpc.Core;
using OpenCodeDev.NetCms.Shared.Api._Core.Messages;
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
    internal class RecipeCoreService
    {
    
        public bool Comparer (ConditionTypes function, object a, object b, FieldTypes dataType)
        {
            
            switch (function)
            {
                case ConditionTypes.Contains:
                case ConditionTypes.Equals:
                case ConditionTypes.EndsWith:
                case ConditionTypes.StartsWith:
                

                case ConditionTypes.LesserThan:
                case ConditionTypes.GreaterEqualThan:
                case ConditionTypes.LesserEqualThan:

                case ConditionTypes.GreaterThan:
                    return GreaterThan(a, b, dataType);
            }
            return false;
        }
        public virtual Predicate<RecipePublicModel> RecipeConditionHandler(List<RecipeCondition> conditions){
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
                if (!fieldType.ToString().Equals(item.Type.ToSystemString())){
                    Console.WriteLine($"ERROR (RecipeConditionHandler): The Field ${item.Field} has the wrong type {{Given: ${item.Type.ToSystemString()}, Reality: {fieldType}}}");
                    throw new RpcException(new Status(StatusCode.Internal, "An unknown error has occured, the admin has been notified. try again later or contact support."));
                }
                if (nextLogic == LogicTypes.And)
                {

                }

                if (item.Conditions == ConditionTypes.Equals)
                {
                    predicate = predicate.Or(p=>p.GetType().GetProperty(item.Field.ToString()).GetValue(p).Equals(int.Parse("")));
                }
                nextFollowLogic = true; // Next Loop will use nextLogic as predicate behavior
            }

            Func<RecipePublicModel, bool> predFunc = predicate.Compile();
            return p=> predFunc(p);
        }
        
        public bool GreaterThan(int a, int b)
        {
            return a > b;
        }
        public bool GreaterThan(float a, float b)
        {
            return a > b;
        }

        public bool GreaterThan(bool a, bool b)
        {
            return (a == true && b == false);
        }

        public bool GreaterThan(double a, double b)
        {
            return a > b;
        }

        public bool GreaterThan(Guid a, Guid b)
        {
            return a.GetHashCode() > b.GetHashCode();
        }

        public bool GreaterThan(long a, long b)
        {
            return a > b;
        }

        public bool GreaterThan(string a, string b)
        {
            return a.Length > b.Length;
        }
        
        public bool GreaterThan(object a, object b, FieldTypes dataType)
        {
            switch (dataType)
            {
                case FieldTypes.String:
                    return GreaterThan((string)a, (string)b);
                case FieldTypes.Int:
                    return GreaterThan((int)a, (int)b);
                case FieldTypes.Float:
                    return GreaterThan((float)a, (float)b);
                case FieldTypes.Double:
                    return GreaterThan((double)a, (double)b);
                case FieldTypes.Bool:
                    return GreaterThan((bool)a, (bool)b);
                case FieldTypes.Guid:
                    return GreaterThan((Guid)a, (Guid)b);
                case FieldTypes.Long:
                    return GreaterThan((long)a, (long)b);
            }
            return false;
        }
    }
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

}
