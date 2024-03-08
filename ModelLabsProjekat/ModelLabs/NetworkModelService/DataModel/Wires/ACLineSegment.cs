using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ACLineSegment : Conductor
    {
        private float b0ch;
        private float bch;
        private float g0ch;
        private float gch;
        private float r;
        private float r0;
        private float x;
        private float x0;
        private long perLengthImpedance;
        public ACLineSegment(long globalId) : base(globalId)
        {

        }

        public float B0ch { get => b0ch; set => b0ch = value; }
        public float Bch { get => bch; set => bch = value; }
        public float G0ch { get => g0ch; set => g0ch = value; }
        public float Gch { get => gch; set => gch = value; }
        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }
        public long PerLengthImpedance { get => perLengthImpedance; set => perLengthImpedance = value; }

        public override bool Equals(object obj)
        {
            return obj is ACLineSegment segment &&
                   base.Equals(obj) &&
                   b0ch == segment.b0ch &&
                   bch == segment.bch &&
                   g0ch == segment.g0ch &&
                   gch == segment.gch &&
                   r == segment.r &&
                   r0 == segment.r0 &&
                   x == segment.x &&
                   x0 == segment.x0 &&
                   perLengthImpedance == segment.perLengthImpedance;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess Implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.AC_LINE_SEGMENT_B0CH:
                case ModelCode.AC_LINE_SEGMENT_BCH:
                case ModelCode.AC_LINE_SEGMENT_G0CH:
                case ModelCode.AC_LINE_SEGMENT_GCH:
                case ModelCode.AC_LINE_SEGMENT_X:
                case ModelCode.AC_LINE_SEGMENT_X0:
                case ModelCode.AC_LINE_SEGMENT_R:
                case ModelCode.AC_LINE_SEGMENT_R0:
                case ModelCode.AC_LINE_SEGMENT_PERLENGTHIMP:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.AC_LINE_SEGMENT_B0CH:
                    property.SetValue(this.B0ch);
                    break;
                case ModelCode.AC_LINE_SEGMENT_BCH:
                    property.SetValue(this.Bch);
                    break;
                case ModelCode.AC_LINE_SEGMENT_G0CH:
                    property.SetValue(this.G0ch);
                    break;
                case ModelCode.AC_LINE_SEGMENT_GCH:
                    property.SetValue(this.Gch);
                    break;
                case ModelCode.AC_LINE_SEGMENT_X:
                    property.SetValue(this.X);
                    break;
                case ModelCode.AC_LINE_SEGMENT_X0:
                    property.SetValue(this.X0);
                    break;
                case ModelCode.AC_LINE_SEGMENT_R:
                    property.SetValue(this.R);
                    break;
                case ModelCode.AC_LINE_SEGMENT_R0:
                    property.SetValue(this.R0);
                    break;
                case ModelCode.AC_LINE_SEGMENT_PERLENGTHIMP:
                    property.SetValue(this.PerLengthImpedance);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.AC_LINE_SEGMENT_B0CH:
                    this.B0ch = property.AsFloat();
                    break;
                case ModelCode.AC_LINE_SEGMENT_BCH:
                    this.Bch = property.AsFloat();
                    break;
                case ModelCode.AC_LINE_SEGMENT_G0CH:
                    this.G0ch = property.AsFloat();
                    break;
                case ModelCode.AC_LINE_SEGMENT_GCH:
                    this.Gch = property.AsFloat();
                    break;
                case ModelCode.AC_LINE_SEGMENT_X:
                    this.X = property.AsFloat();
                    break;
                case ModelCode.AC_LINE_SEGMENT_X0:
                    this.X0 = property.AsFloat();
                    break;
                case ModelCode.AC_LINE_SEGMENT_R:
                    this.R = property.AsFloat();
                    break;
                case ModelCode.AC_LINE_SEGMENT_R0:
                    this.R0 = property.AsFloat();
                    break;
                case ModelCode.AC_LINE_SEGMENT_PERLENGTHIMP:
                    this.PerLengthImpedance = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion

        #region IReference


        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (perLengthImpedance != 0 && (refType == TypeOfReference.Both || refType == TypeOfReference.Target))
            {
                references[ModelCode.AC_LINE_SEGMENT_PERLENGTHIMP] = new List<long> { perLengthImpedance };
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.AC_LINE_SEGMENT_PERLENGTHIMP:
                    perLengthImpedance = globalId;
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.AC_LINE_SEGMENT_PERLENGTHIMP:
                    if (perLengthImpedance == globalId)
                    {
                        perLengthImpedance = 0;
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }
                    break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion
    }
}
