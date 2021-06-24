﻿using OpenCodeDev.NetCms.Shared.Api.Recipe.Models._Generated;
using OpenCodeDev.NetCMS.Core.Shared.DataAnnotation;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Models
{
    /// <summary>
    /// Public Field of Recipe Api (UnAllowed Field will be null)
    /// </summary>

    [Serializable]
    [ProtoContract]
    public partial class RecipePublicModel
    {
        [Key][Column][Required][ProtoMember(1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]        
        public Guid Id { get; set; }
        
        [Required]
        [Column]
        [ProtoMember(2)]
        public string Name { get; set; }

        [Required]
        [Column]
        [ProtoMember(3)]
        public int Duration { get; set; }

        [ProtoMember(4)]
        public List<IngredientModel> Ingredients { get; set; }

    }
}
