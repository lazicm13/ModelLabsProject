//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTN_68 {
    using System;
    using FTN_68;
    using System.Collections.Generic;
    
    
    /// A subset of a geographical region of a power system network model.
    public class SubGeographicalRegion : IdentifiedObject {
        
        /// The lines within the sub-geographical region.
        private List<Line> cim_Lines = new List<Line>();
        
        private const bool isLinesMandatory = false;
        
        private const string _LinesPrefix = "cim";
        
        public virtual List<Line> Lines {
            get {
                return this.cim_Lines;
            }
            set {
                this.cim_Lines = value;
            }
        }
        
        public virtual bool LinesHasValue {
            get {
                return this.cim_Lines != null;
            }
        }
        
        public static bool IsLinesMandatory {
            get {
                return isLinesMandatory;
            }
        }
        
        public static string LinesPrefix {
            get {
                return _LinesPrefix;
            }
        }
    }
}