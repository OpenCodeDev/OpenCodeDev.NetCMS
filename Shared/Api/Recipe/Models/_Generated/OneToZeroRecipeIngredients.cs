using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Models._Generated
{
    // AUTO-GENERATED, DO NOT EDIT.
    [ProtoContract]
    public class OneToZeroRecipeIngredients
    {
        [Required]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        public Guid RecipeIdentifier { get; set; }
        
        [ForeignKey("RecipeIdentifier")]
        public RecipePublicModel Recipe { get; set; }

        [Required]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        public Guid IngredientIdentifier { get; set; }

        [NotMapped]
        public RecipePublicModel Ingredient { get; set; }
    }
}
