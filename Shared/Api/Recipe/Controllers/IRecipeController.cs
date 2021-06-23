using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Messages._Generated;
using OpenCodeDev.NetCms.Shared.Api.Recipe.Models;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Controllers
{
    [ServiceContract]
    public interface IRecipeController
    {
        /// <summary>
        /// Create one entry (Do not use for register AuthenticationService provides it)
        /// </summary>
        [OperationContract]
        Task<RecipePublicModel> Create(RecipeCreateRequest request, CallContext context = default);

        /// <summary>
        /// Fetch List with given filter
        /// </summary>
        [OperationContract]
        Task<List<RecipePublicModel>> Fetch(RecipeFetchRequest request, CallContext context = default);

        /// <summary>
        /// Fetch One by Given ID
        /// </summary>
        [OperationContract]
        Task<RecipePublicModel> FetchOne(RecipeFetchOneRequest request, CallContext context = default);

        /// <summary>
        /// Update a single entry
        /// </summary>
        [OperationContract]
        Task<RecipePublicModel> Update(RecipeUpdateOneRequest request, CallContext context = default);

        /// <summary>
        /// Update Many Entries
        /// </summary>
        [OperationContract]
        Task<List<RecipePublicModel>> UpdateMany(RecipeUpdateManyRequest request, CallContext context = default);

        /// <summary>
        /// Delete one entry
        /// </summary>
        [OperationContract]
        Task<RecipePublicModel> Delete(RecipeDeleteRequest request, CallContext context = default);
    }
}
