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
    public class RecipeIngredientBinder
    {
        [Key]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        public Guid RecipeId { get; set; }
        
        [ForeignKey("RecipeId")]
        public RecipePublicModel Recipe { get; set; }

        [Required]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        public Guid IngredientId { get; set; }

        [ForeignKey("IngredientId")]
        public IngredientModel Ingredient { get; set; }
    }
}
