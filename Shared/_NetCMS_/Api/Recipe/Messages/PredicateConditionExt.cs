using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared._NetCMS_.Api.Recipe.Messages
{
    public static class PredicateConditionExt
    {
        public static List<RecipePredicateConditions> Parse(this List<RecipePredicateConditions> list, string code)
        {
            bool capturing = false;
            string capturingStr = "";
            List<RecipePredicateConditions> ListOfPred = new List<RecipePredicateConditions>();
            RecipePredicateConditions currentPred = null;
            char lastChar = 'e';
            foreach (var item in code)
            {
                // Field Start
                if (item == '[' && lastChar != '\\')
                {
                    if (capturing) { throw new Exception("Cannot parse search condition: syntax error."); }
                    if (currentPred == null) { currentPred = new RecipePredicateConditions(); }
                }
                else if (capturing && item == '&' && lastChar == '&')
                {
                    if (currentPred == null) { currentPred = new RecipePredicateConditions(); }
                    currentPred.LogicalOperator = LogicTypes.AndAlso;
                }
                else if (capturing && item == '&' && lastChar != '&' && lastChar != '\\')
                {
                    if (currentPred == null) { currentPred = new RecipePredicateConditions(); }
                    currentPred.LogicalOperator = LogicTypes.And;
                }
                else if (capturing && item == '|' && lastChar == '|')
                {
                    if (currentPred == null) { currentPred = new RecipePredicateConditions(); }
                    currentPred.LogicalOperator = LogicTypes.OrElse;
                }
                else if (capturing && item == '|' && lastChar != '|' && lastChar != '\\')
                {
                    if (currentPred == null) { currentPred = new RecipePredicateConditions(); }
                    currentPred.LogicalOperator = LogicTypes.Or;
                }
                else if (capturing && item == ']' && lastChar != '\\')
                {
                    bool foundField = false;
                    foreach (var field in ((RecipePredicateConditions.Fields[])Enum.GetValues(typeof(RecipePredicateConditions.Fields))).Distinct())
                    {
                        if (field.ToString().Equals(capturingStr)) { currentPred.Field = field; foundField = true; }
                        if (foundField) { break; }
                    }
                    capturingStr = "";
                    capturing = false;
                }
                else if (item == '>') // Start Reading Function
                {
                    capturing = true;
                }
                else if (capturing && item == '(' && lastChar != '\\')
                { // Finish Reading Function and Starting Reading Value

                    bool foundFunc = false;
                    foreach (var func in ((ConditionTypes[])Enum.GetValues(typeof(ConditionTypes))).Distinct())
                    {
                        if (func.ToString().Equals(capturingStr)) { currentPred.Conditions = func; foundFunc = true; }
                        if (foundFunc) { break; }
                    }
                    capturingStr = ""; // reset for Value intake
                }
                else if (capturing && item == ')' && lastChar != '\\')
                {
                    currentPred.Value = capturingStr;
                    ListOfPred.Add(currentPred);
                    currentPred = null;
                    capturingStr = ""; // reset for Value intake
                }
                else if (capturing)
                {
                    capturingStr += item;
                }
            }
            return ListOfPred;
        }
    }
}
