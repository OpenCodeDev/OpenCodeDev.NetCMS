//_NETCMS_HEADER_

using ProtoBuf;
using Grpc.Core;
using System;


// Core System
using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;


namespace _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages
{
    /// <summary>
    /// Advanced Conditional Search System for _API_NAME_ Model.<br/>
    /// Provided by NetCMS :)
    /// </summary>
    [ProtoContract()]
    public class _API_NAME_PredicateConditions
    {
            /// <summary>
        /// Supports Json 
        /// [{field:"Id", Type: "string, int, float, double", ConditionType: "Contains, EndsWith, StartWith, Equals, GreaterThan, LesserThan", Value:"Hello"}]
        /// </summary>
        [ProtoMember(1)]
        public ConditionTypes Conditions { get; set; }

        /// <summary>
        /// This is important for transpiling behavior.<br/>
        /// Example: (Field (ID) == Value(2) NextLogic (And->&&) [The Next Item FetchGenericRequest item])
        /// </summary>
        [ProtoMember(2)]
        public LogicTypes LogicalOperator { get; set; }

        /// <summary>
        /// Value to compare to, ("Name", "1", "1.0", "0.001", "00000000-0000-0000-0000-000000000000", "True", "False")
        /// </summary>
        [ProtoMember(3)]
        public string Value { get; set; }

        /// <summary>
        /// Type: string, int, float, double <br/>
        /// If cannot parse to provided type, throw error
        /// </summary>
        [ProtoMember(4)]
        public FieldTypes Type { get; set; }

        [ProtoMember(5)] 
        public Fields Field { get; set; }
        public enum Fields { 
            //_FIELDS_ENUM_
         }

        public Type GetFieldType()
        {
            switch (Field) 
            { 
                //_GET_TYPE_SWITCH_
            }
            throw new RpcException(new Status(StatusCode.Unknown, "Server misconfiguration tries to pass un supported type of data, contact support."));
        }


    }
}