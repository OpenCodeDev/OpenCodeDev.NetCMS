using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages;
using OpenCodeDev.NetCms.Shared._NetCMS_.Api.Recipe.Messages;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<RecipePredicateCondition> Conditions { get; set; }

        /// <summary>
        /// Limit the number of result (Max: 400)
        /// </summary>
        [ProtoMember(2)]
        [Required]
        [Range(10, 400, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int Limit { get; set ; }

        /// <summary>
        /// List All References to be Loaded. Will return object with relation. <br/>
        /// Note: We do not support deep nesting, in fact we only support 1 level nest for now.
        /// </summary>
        [ProtoMember(3)]
        public RecipeFetchRefBehavior IncludeReferences { get; set; }

        public RecipeFetchRequest()
        { }

        public RecipeFetchRequest(int limit) : this()
        { Limit = limit; }

        public RecipeFetchRequest(int limit, RecipeFetchRefBehavior includeReferences) : this(limit)
        { IncludeReferences = includeReferences; }

        public RecipeFetchRequest(int limit, RecipeFetchRefBehavior includeReferences, List<RecipePredicateCondition> conditions) : this(limit, includeReferences)
        { Conditions = conditions; }
    }
}
