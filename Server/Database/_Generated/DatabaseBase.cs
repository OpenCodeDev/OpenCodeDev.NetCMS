using Microsoft.EntityFrameworkCore;
using OpenCodeDev.NetCms.Server.Api.Recipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models._Generated;
namespace OpenCodeDev.NetCms.Server.Database._Generated
{
    // Auto Generated DO NOT EDIT
    public class DatabaseBase : DbContext
    {
        public virtual DbSet<RecipeModel> Recipes { get; set; }
        public virtual DbSet<OneToZeroRecipeIngredients> OneToZeroRecipe_Ingredients { get; set; }

        public DatabaseBase(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Many To Zero
            builder.Entity<RecipeModel>().HasMany(c => c.Ingredients).WithOne(p => (RecipeModel) p.Recipe);
        }

        /// <summary>
        /// Mock Up Test Data
        /// </summary>
        public void MockUp (){
            
        }
    }
}
