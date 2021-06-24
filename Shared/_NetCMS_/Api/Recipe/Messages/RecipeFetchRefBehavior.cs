using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared._NetCMS_.Api.Recipe.Messages
{
    // Generated File
    [ProtoContract]
    public class RecipeFetchRefBehavior
    {
        /// <summary>
        /// Ref1 = List of Available Reference.
        /// </summary>
        public ReferenceFetchBehavior Ref1 { get; set; } = ReferenceFetchBehavior.Ignore;
    }
}
