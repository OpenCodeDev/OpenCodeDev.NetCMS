using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages._Generated;
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
        /// Unlink = Delete when One-to-One and Many-to-One Relationship, Unlist when (this)One-To-Many(other) and Many-to-Many. <br/>
        /// Link = Only works for Many-to-Many and One-to-Many. Others are linked at the creation of the reference.<br/>
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
