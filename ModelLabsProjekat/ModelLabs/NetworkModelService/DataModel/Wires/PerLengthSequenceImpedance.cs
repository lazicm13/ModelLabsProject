using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PerLengthSequenceImpedance : PerLengthImpedance
    {
        public PerLengthSequenceImpedance(long globalId) : base(globalId)
        {

        }
        private float b0ch;
        private float bch;
        private float g0ch;
        private float gch;
        private float r;
        private float r0;
        private float x;
        private float x0;
        private long perLengthImpedance;

        public float B0ch { get => b0ch; set => b0ch = value; }
        public float Bch { get => bch; set => bch = value; }
        public float G0ch { get => g0ch; set => g0ch = value; }
        public float Gch { get => gch; set => gch = value; }
        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }

        public override bool Equals(object obj)
        {
            return obj is PerLengthSequenceImpedance imp &&
                   base.Equals(obj) &&
                   b0ch == imp.b0ch &&
                   bch == imp.bch &&
                   g0ch == imp.g0ch &&
                   gch == imp.gch &&
                   r == imp.r &&
                   r0 == imp.r0 &&
                   x == imp.x &&
                   x0 == imp.x0 &&
                   perLengthImpedance == imp.perLengthImpedance;
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
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_B0CH:
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_BCH:
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_G0CH:
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_GCH:
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_X:
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_X0:
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_R:
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_R0:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_B0CH:
                    property.SetValue(this.B0ch);
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_BCH:
                    property.SetValue(this.Bch);
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_G0CH:
                    property.SetValue(this.G0ch);
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_GCH:
                    property.SetValue(this.Gch);
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_X:
                    property.SetValue(this.X);
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_X0:
                    property.SetValue(this.X0);
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_R:
                    property.SetValue(this.R);
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_R0:
                    property.SetValue(this.R0);
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
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_B0CH:
                    this.B0ch = property.AsFloat();
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_BCH:
                    this.Bch = property.AsFloat();
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_G0CH:
                    this.G0ch = property.AsFloat();
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_GCH:
                    this.Gch = property.AsFloat();
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_X:
                    this.X = property.AsFloat();
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_X0:
                    this.X0 = property.AsFloat();
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_R:
                    this.R = property.AsFloat();
                    break;
                case ModelCode.PER_LENGTH_SEQ_IMPEDANCE_R0:
                    this.R0 = property.AsFloat();
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
            base.GetReferences(references, refType);
        }


        #endregion
    }
}
