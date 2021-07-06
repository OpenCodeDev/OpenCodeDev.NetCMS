//_NETCMS_HEADER_

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//_USINGS_

namespace _NAMESPACE_BASE_SERVER_.Database
{
    // Auto Generated DO NOT EDIT
    public class DatabaseBase : DbContext
    {
        //_DBSET_

        public DatabaseBase(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Relation Code

            // Link Multiple Ingredients One-Way
            // Many To Zero (If Recipe is Link to Removed)
            // builder.Entity<RecipeModel>()
            // .HasMany(p => p.Ingredients)
            // .WithOne(p => (RecipeModel)p.Recipe)
            // .HasForeignKey(p => p.RecipeId)
            // .OnDelete(DeleteBehavior.Cascade);

            // // One to Zero (If Ingredient is Removed, Link is Removed)
            // builder.Entity<RecipeIngredientBinder>()
            // .HasOne(p => p.Ingredient)
            // .WithOne()
            // .HasForeignKey<RecipeIngredientBinder>(p => p.IngredientId).HasPrincipalKey<RecipeIngredientBinder>(p=>p.IngredientId)
            // .OnDelete(DeleteBehavior.Cascade);

            
    

        }

        /// <summary>
        /// Mock Up Test Data
        /// </summary>
        public void MockUp (){
            
        }
    }
}
