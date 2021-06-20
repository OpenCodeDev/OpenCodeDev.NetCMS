using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.NetCms.Core.Plugin
{
    public class PluginBase
    {  
        /// <summary>
        /// Load Plugin's Reference
        /// </summary>
        protected PluginBase(){

        }


        public virtual void Setup(){

        }


        /// <summary>
        /// Initialize Plugin and Registering All Action, Filter
        /// </summary>
        public virtual void Initialize(){
            
        }
    }
}
