//_NETCMS_HEADER_

 using ProtoBuf.Grpc;
 using System.Collections.Generic;
 using System.ServiceModel;
 using System.Threading.Tasks;

 // Shared Resources
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Models;
using _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages;

 namespace _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Controllers
 { 
    [ServiceContract()]
    public interface I_API_NAME_Controller { 
        /// <summary>
        /// Call to create a new _API_NAME_ through GRPC.
        /// </summary>
        [OperationContract()]
        Task<_API_NAME_PublicModel> Create (_API_NAME_CreateRequest request, CallContext context = default); 

        /// <summary>
        /// Call to Fetch all _API_NAME_ matching given conditions through GRPC.
        /// </summary>

        [OperationContract()]
        Task<List<_API_NAME_PublicModel>> Fetch (_API_NAME_FetchRequest request, CallContext context = default); 

        /// <summary>
        /// Call to Fetch  One _API_NAME_ matching given id through GRPC.
        /// </summary>
        [OperationContract()]
        Task<_API_NAME_PublicModel> FetchOne (_API_NAME_FetchOneRequest request, CallContext context = default); 

        /// <summary>
        /// Call to Update One _API_NAME_ matching given id through GRPC.
        /// </summary>
        [OperationContract()]
        Task<_API_NAME_PublicModel> Update (_API_NAME_UpdateOneRequest request, CallContext context = default); 


        /// <summary>
        /// Call to Process many updates of _API_NAME_ in a single call through GRPC.
        /// </summary>
        [OperationContract()]
        Task<List<_API_NAME_PublicModel>> UpdateMany (_API_NAME_UpdateManyRequest request, CallContext context = default); 


        /// <summary>
        /// Call to Delete a _API_NAME_ matching given id through GRPC.
        /// </summary>
        [OperationContract()]
        Task<_API_NAME_PublicModel> Delete (_API_NAME_DeleteRequest request, CallContext context = default); 
        
    } 
 }