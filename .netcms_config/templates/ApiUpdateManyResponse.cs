
 using ProtoBuf;
 using System;
 using System.ComponentModel.DataAnnotations;
 using System.Collections.Generic;


 namespace _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages { 

    [ProtoContract()]
    public partial class _API_NAME_UpdateManyResponse  { 

        [ProtoMember(1)]
        public  _API_NAME_PublicModel Entry {  get;  set;}  
        
        [ProtoMember(2)]
        public  bool HasError {  get;  set;}  = false;

        [ProtoMember(3)]
        public  RpcException Error {  get;  set; }  = null;
        
        
    
    } 
  }