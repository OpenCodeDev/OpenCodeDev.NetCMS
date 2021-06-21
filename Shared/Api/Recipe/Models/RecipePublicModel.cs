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
    public class RecipePublicModel
    {
        [Key]
        [Column]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column]
        public string Name { get; set; }

        [Required]
        [Column]
        public int Duration { get; set; }
    }
}
