using OpenCodeDev.NetCms.Shared.Api._Core.Messages;
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
        Task<object> Create(object request, CallContext context = default);

        /// <summary>
        /// Fetch List with given filter
        /// </summary>
        [OperationContract]
        Task<List<RecipePublicModel>> Fetch(RecipeFetchRequest request, CallContext context = default);

        /// <summary>
        /// Fetch One by Given ID
        /// </summary>
        [OperationContract]
        Task<object> FetchOne(object request, CallContext context = default);

        /// <summary>
        /// Update a single entry
        /// </summary>
        [OperationContract]
        Task<object> Update(object request, CallContext context = default);

        /// <summary>
        /// Delete one entry
        /// </summary>
        [OperationContract]
        Task Delete(object request, CallContext context = default);
    }
}
