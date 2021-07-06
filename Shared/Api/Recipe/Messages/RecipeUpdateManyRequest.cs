using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Messages
{
    /// <summary>
    /// Update in Bulk, One Bucket One Request Many Changes. <br/>
    /// When one fails, server will return an object with a list of all changes if succeded or not.
    /// </summary>
    [ProtoContract]
    public partial class RecipeUpdateManyRequest
    {
        /// <summary>
        /// Default Limit is 20 Items per request... beyond that, server will return resource exhausted.
        /// </summary>
        [ProtoMember(1)]
        public List<RecipeUpdateOneRequest> BulkUpdate { get; set; }
        public RecipeUpdateManyRequest()
        { }

        public RecipeUpdateManyRequest(List<RecipeUpdateOneRequest> bulkUpdate) : this()
        { BulkUpdate = bulkUpdate; }

    }
}
