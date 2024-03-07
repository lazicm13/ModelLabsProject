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
    
    
    /// Common type for per-length impedance electrical catalogues.
    public class PerLengthImpedance : IdentifiedObject {
        
        /// All line segments described by this per-length impedance.
        private List<ACLineSegment> cim_ACLineSegments = new List<ACLineSegment>();
        
        private const bool isACLineSegmentsMandatory = false;
        
        private const string _ACLineSegmentsPrefix = "cim";
        
        public virtual List<ACLineSegment> ACLineSegments {
            get {
                return this.cim_ACLineSegments;
            }
            set {
                this.cim_ACLineSegments = value;
            }
        }
        
        public virtual bool ACLineSegmentsHasValue {
            get {
                return this.cim_ACLineSegments != null;
            }
        }
        
        public static bool IsACLineSegmentsMandatory {
            get {
                return isACLineSegmentsMandatory;
            }
        }
        
        public static string ACLineSegmentsPrefix {
            get {
                return _ACLineSegmentsPrefix;
            }
        }
    }
}