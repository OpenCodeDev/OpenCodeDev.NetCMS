using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api._Core.Messages
{
   
    public static class MessageService
    {
        /// <summary>
        /// Return the underlying type defined by the system to ensure 100% maatch when it occurs
        /// </summary>
        public static string ToSystemString(this FieldTypes type){
            switch (type)
            {
                case FieldTypes.String:
                    return typeof(string).ToString();
                case FieldTypes.Int:
                    return typeof(string).ToString();
                case FieldTypes.Float:
                    return typeof(float).ToString();
                case FieldTypes.Double:
                    return typeof(double).ToString();
                case FieldTypes.Bool:
                    return typeof(bool).ToString();
                case FieldTypes.Guid:
                    return typeof(Guid).ToString();
                case FieldTypes.Long:
                    return typeof(long).ToString();
                default:
                    Console.WriteLine($@"ERROR (ConditionHandler): The Field Type ${type} isn't supported by NetCMS.");
                    throw new RpcException(new Status(StatusCode.Internal, "An unknown error has occured, the admin has been notified. try again later or contact support."));

            }
        }
    }
}
