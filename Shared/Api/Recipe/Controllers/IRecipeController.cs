using ProtoBuf.Grpc;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api.Recipe.Controllers
{
    [ServiceContract()]
    public partial interface IRecipeController
    {
        /// <summary>
        /// Create one entry (Do not use for register AuthenticationService provides it)
        /// </summary>
        [OperationContract()]
        Task<Models.RecipePublicModel> Create(_NetCMS_.Api.Recipe.Messages.RecipeCreateRequest request, CallContext context = default);

        /// <summary>
        /// Fetch List with given filter
        /// </summary>
        [OperationContract]
        Task<List<Models.RecipePublicModel>> Fetch(Messages.RecipeFetchRequest request, CallContext context = default);

        /// <summary>
        /// Fetch One by Given ID
        /// </summary>
        [OperationContract]
        Task<Models.RecipePublicModel> FetchOne(Messages.RecipeFetchOneRequest request, CallContext context = default);

        /// <summary>
        /// Update a single entry
        /// </summary>
        [OperationContract]
        Task<Models.RecipePublicModel> Update(Messages.RecipeUpdateOneRequest request, CallContext context = default);

        /// <summary>
        /// Update Many Entries
        /// </summary>
        [OperationContract]
        Task<List<Models.RecipePublicModel>> UpdateMany(Messages.RecipeUpdateManyRequest request, CallContext context = default);

        /// <summary>
        /// Delete one entry
        /// </summary>
        [OperationContract]
        Task<Models.RecipePublicModel> Delete(Messages.RecipeDeleteRequest request, CallContext context = default);
    }
}
