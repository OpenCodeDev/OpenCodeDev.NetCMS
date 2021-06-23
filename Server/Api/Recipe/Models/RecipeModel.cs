using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCodeDev.NetCms.Server.Api.Recipe.Controllers;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models._Generated;
using ProtoBuf;

namespace OpenCodeDev.NetCms.Server.Api.Recipe.Models
{
    public class RecipeModel: RecipePublicModel
    {
        /// <summary>
        /// Binder for Many to Zero
        /// </summary>
        public ICollection<RecipeIngredientBinder> Ingredients { get; set; }

        /// <summary>
        /// Called at 
        /// </summary>
        public void Mock(){

        }
    }
}
