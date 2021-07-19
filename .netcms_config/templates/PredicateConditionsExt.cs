//_NETCMS_HEADER_

using ProtoBuf;
using Grpc.Core;
using System;


// Core System
using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;


namespace _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages
{
    public static class _API_NAME_PredicateConditionExt
    {
        public static List<_API_NAME_PredicateConditions> Parse(this List<_API_NAME_PredicateConditions> list, string code)
        {
            bool capturing = false;
            string capturingStr = "";
            _API_NAME_PredicateConditions currentPred = null;
            char? lastChar = null;
            foreach (var item in code)
            {
                // Field Start
                if (item == '[' && (lastChar == null || lastChar != '\\'))
                {
                    if (capturing) { throw new Exception("Cannot parse search condition: syntax error."); }
                    if (currentPred == null) { currentPred = new _API_NAME_PredicateConditions(); }
                    capturing = true;
                }
                else if (item == '&' && (lastChar == null || lastChar != '&' && lastChar != '\\'))
                {
                    if (currentPred == null) { currentPred = new _API_NAME_PredicateConditions(); }
                    currentPred.LogicalOperator = LogicTypes.And;
                }
                else if (item == '&' && (lastChar == null || lastChar == '&'))
                {
                    if (currentPred == null) { currentPred = new _API_NAME_PredicateConditions(); }
                    currentPred.LogicalOperator = LogicTypes.AndAlso;
                }
                else if (item == '|' && (lastChar == null || lastChar != '|' && lastChar != '\\'))
                {
                    if (currentPred == null) { currentPred = new _API_NAME_PredicateConditions(); }
                    currentPred.LogicalOperator = LogicTypes.Or;
                }
                else if (item == '|' && (lastChar != null && lastChar == '|'))
                {
                    if (currentPred == null) { currentPred = new _API_NAME_PredicateConditions(); }
                    currentPred.LogicalOperator = LogicTypes.OrElse;
                }
                else if (capturing && item == ']' && (lastChar == null || lastChar != '\\'))
                {
                    bool foundField = false;
                    foreach (var field in ((_API_NAME_PredicateConditions.Fields[])Enum.GetValues(typeof(_API_NAME_PredicateConditions.Fields))).Distinct())
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
                else if (capturing && item == '(' && (lastChar == null || lastChar != '\\'))
                { // Finish Reading Function and Starting Reading Value

                    bool foundFunc = false;
                    foreach (var func in ((ConditionTypes[])Enum.GetValues(typeof(ConditionTypes))).Distinct())
                    {
                        if (func.ToString().Equals(capturingStr)) { currentPred.Conditions = func; foundFunc = true; }
                        if (foundFunc) { break; }
                    }
                    capturingStr = ""; // reset for Value intake
                }
                else if (capturing && item == ')' && (lastChar == null || lastChar != '\\'))
                {
                    currentPred.Value = capturingStr;
                    list.Add(currentPred);
                    currentPred = null;
                    capturingStr = ""; // reset for Value intake
                    capturing = false;
                }
                else if (capturing)
                {
                    capturingStr += item;
                }
                lastChar = item;
            }
            return list;
        }
    }
}