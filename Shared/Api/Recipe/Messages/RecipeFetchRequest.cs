using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Messages
{

    [ProtoContract]
    public class RecipeFetchRequest
    {
        /// <summary>
        /// List of Condition on which will be use to build the predicate on EF CORE.
        /// </summary>
        [ProtoMember(1)]
        public List<RecipeCondition> Conditions { get; set; }

        /// <summary>
        /// Limit the number of result (Max: 400)
        /// </summary>
        [ProtoMember(2)]
        public int Limit { get; set; }

        /// <summary>
        /// List All References to be Loaded. Will return object with relation. <br/>
        /// Note: We do not support deep nesting, in fact we only support 1 level nest for now.
        /// </summary>
        [ProtoMember(3)]
        public List<RecipeReference> LoadReferences { get; set; }
    }
}
