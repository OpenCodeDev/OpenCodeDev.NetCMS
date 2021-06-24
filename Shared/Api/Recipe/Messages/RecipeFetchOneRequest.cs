using OpenCodeDev.NetCms.Shared._NetCMS_.Api.Recipe.Messages;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Messages
{
    [ProtoContract]
    public class RecipeFetchOneRequest
    {
        [ProtoMember(1)]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        public Guid Id { get; set; }

        /// <summary>
        /// List All References to be Loaded. Will return object with relation. <br/>
        /// Note: We do not support deep nesting, in fact we only support 1 level nest for now.
        /// </summary>
        [ProtoMember(2)]
        public RecipeFetchRefBehavior IncludeReferences { get; set; }

        public RecipeFetchOneRequest()
        { }

        public RecipeFetchOneRequest(Guid id) : this()
        { Id = id; }

        public RecipeFetchOneRequest(Guid id, RecipeFetchRefBehavior includeReferences) : this(id) 
        { IncludeReferences = includeReferences; }
    }
}
