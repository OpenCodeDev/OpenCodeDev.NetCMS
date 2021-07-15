//_NETCMS_HEADER_

using ProtoBuf;
using Grpc.Core;
using System;


// Core System
using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;


namespace _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages
{
    [ProtoContract()]
    public class _API_NAME_PredicateOrdering : OrderByBase
    {
        [ProtoMember(1)] 
        public Fields Field { get; set; }
        public enum Fields
        {
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
    
