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
    public class RecipeDeleteRequest
    {
        [ProtoMember(1)]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        public Guid Id { get; set; }
    }
}
