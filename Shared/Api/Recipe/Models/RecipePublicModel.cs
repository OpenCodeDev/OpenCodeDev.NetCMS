using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Models
{
    public partial class RecipePublicModel
    {
        
        [ProtoMember(4)]
        public List<IngredientModel> Ingredients { get; set; }
    }
}
