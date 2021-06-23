using OpenCodeDev.NetCms.Shared.Api.Recipe.Models._Generated;
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
    public class RecipePublicModel
    {
        [Key]
        [Column]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Id cannot be empty.")]
        [ProtoMember(1)]
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
        public ICollection<OneToZeroRecipeIngredients> Ingredients { get; set; }
    }
}
