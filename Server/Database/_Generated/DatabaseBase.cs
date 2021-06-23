using Microsoft.EntityFrameworkCore;
using OpenCodeDev.NetCms.Server.Api.Recipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models._Generated;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;

namespace OpenCodeDev.NetCms.Server.Database._Generated
{
    // Auto Generated DO NOT EDIT
    public class DatabaseBase : DbContext
    {
        public virtual DbSet<RecipeModel> Recipes { get; set; }
        public virtual DbSet<RecipeIngredientBinder> RecipeIngredientBinders { get; set; }

        public virtual DbSet<TestModel> TestModel { get; set; }
        public virtual DbSet<IngredientModel> Ingredients { get; set; }
        public DatabaseBase(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Link Multiple Ingredients One-Way
            // Many To Zero (If Recipe is Link to Removed)
            builder.Entity<RecipeModel>()
            .HasMany(p => p.Ingredients)
            .WithOne(p => (RecipeModel)p.Recipe)
            .HasForeignKey(p => p.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

            // One to Zero (If Ingredient is Removed, Link is Removed)
            builder.Entity<RecipeIngredientBinder>()
            .HasOne(p => p.Ingredient)
            .WithOne()
            .HasForeignKey<RecipeIngredientBinder>(p => p.IngredientId).HasPrincipalKey<RecipeIngredientBinder>(p=>p.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

            
    

        }

        /// <summary>
        /// Mock Up Test Data
        /// </summary>
        public void MockUp (){
            
        }
    }
}
