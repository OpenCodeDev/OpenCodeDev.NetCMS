using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Shared.Api._Core.Messages
{
    [ProtoContract]
    public class ConditionBase
    {

        /// <summary>
        /// Supports Json 
        /// [{field:"Id", Type: "string, int, float, double", ConditionType: "Contains, EndsWith, StartWith, Equals, GreaterThan, LesserThan", Value:"Hello"}]
        /// </summary>
        [ProtoMember(1)]
        public ConditionTypes Conditions { get; set; }

        /// <summary>
        /// This is important for transpiling behavior.<br/>
        /// Example: (Field (ID) == Value(2) NextLogic (And->&&) [The Next Item FetchGenericRequest item])
        /// </summary>
        [ProtoMember(2)]
        public LogicTypes NextLogic { get; set; }

        /// <summary>
        /// Value to compare to, ("Name", "1", "1.0", "0.001", "00000000-0000-0000-0000-000000000000", "True", "False")
        /// </summary>
        [ProtoMember(3)]
        public string Value { get; set; }

        /// <summary>
        /// Type: string, int, float, double <br/>
        /// If cannot parse to provided type, throw error
        /// </summary>
        [ProtoMember(4)]
        public FieldTypes Type { get; set; }
    }
}
