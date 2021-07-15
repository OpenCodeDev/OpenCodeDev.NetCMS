
 using ProtoBuf;
 using System;
 using System.ComponentModel.DataAnnotations;
 using System.Collections.Generic;


 namespace _NAMESPACE_BASE_SHARED_.Api._API_NAME_.Messages { 

    [ProtoContract()]
    public partial class _API_NAME_FetchRequest  { 
        [ProtoMember(1)]
        public  List<_API_NAME_PredicateConditions> Conditions {  get;  set;}  
        
        [ProtoMember(2)]
        public  List<_API_NAME_PredicateOrdering> OrderBy {  get;  set;}  
        
        [ProtoMember(3)]
        [Required()]
        [Range(10, 400, ErrorMessage = "The field {0} must be greater than {1}.")]
        public  System.Int32 Limit {  get;  set;}  
        
        
    
    } 
  }