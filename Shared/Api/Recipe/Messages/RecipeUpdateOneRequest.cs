using OpenCodeDev.NetCms.Shared._NetCMS_.Api.Recipe.Messages;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Messages
{
    public class RecipeUpdateOneRequest
    {
        /// <summary>
        /// Element Id
        /// </summary>
        [Required]
        [ProtoMember(1)]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        public Guid Id { get; set; }

        /// <summary>
        /// Note 1: Update Reference is Not Supported, you should do multiple bulk requests on targeted reference api.<br/>
        /// Note 2: Support for Reference will not happen due to complexity nature, performance (reflection) and risks of error, but we plan to support ZillionRequests to allow you to pass multiple request to multiple api at once.
        /// </summary>
        [Required]
        [ProtoMember(2)]
        public RecipePublicModel Element { get; set; }

        /// <summary>
        /// Note 1: Will remove any missing entities (Break Link, Auto-Delete if dependent) or entity (single reference and delete it)<br/>
        /// Note 2: One to Zero will link entity if exist.<br/>
        /// Note 3: Many to Zero will link entities by creating a binder which will be broken if any sides is deleted.
        /// </summary>
        [ProtoMember(3)]
        public RecipeUpdateRefBehavior ReferenceBehavior { get; set; } = new RecipeUpdateRefBehavior();

        /// <summary>
        /// Choose which reference to include at success update. 
        /// </summary>
        [ProtoMember(4)]
        public RecipeFetchRefBehavior ReturnedReference { get; set; } = new RecipeFetchRefBehavior();



    }
}
