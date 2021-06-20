using Microsoft.EntityFrameworkCore;
using OpenCodeDev.NetCms.Server.Api.Recipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Server
{
    public class RecipeDatabase : DbContext
    {
        public DbSet<RecipeModel> Recipes { get; set; }

        public RecipeDatabase(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }

        /// <summary>
        /// Mock Up Test Data
        /// </summary>
        public void MockUp (){

        }
    }
}
