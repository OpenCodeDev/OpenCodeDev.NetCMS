using OpenCodeDev.NetCMS.Core.Shared.Api.Messages;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Messages._Generated
{
    // Generated File
    [ProtoContract]
    public class RecipeUpdateRefBehavior
    {

        /// <summary>
        /// How to Behave? Default: Ignore.
        /// </summary>
        [ProtoMember(1)]
        public ReferenceEditBehavior Ingredients { get; set; } = ReferenceEditBehavior.Ignore;

        List<Guid> RefX_ToUnlink { get; set; } = new List<Guid>();
        List<Guid> RefX_ToLink { get; set; } = new List<Guid>();

    }
}
