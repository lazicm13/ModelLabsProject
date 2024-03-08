using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class DCLineSegment : Conductor
    {
        private float inductanse;
        private float resistance;
        public DCLineSegment(long globalId) : base(globalId)
        {

        }

        public float Inductanse { get => inductanse; set => inductanse = value; }
        public float Resistance { get => resistance; set => resistance = value; }

        public override bool Equals(object obj)
        {
            return obj is DCLineSegment segment &&
                   base.Equals(obj) &&
                   inductanse == segment.inductanse &&
                   resistance == segment.resistance;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.DC_LINE_SEGMENT_INDUCTANCE:
                case ModelCode.DC_LINE_SEGMENT_RESISTANCE:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.DC_LINE_SEGMENT_INDUCTANCE:
                    property.SetValue(inductanse);
                    break;

                case ModelCode.DC_LINE_SEGMENT_RESISTANCE:
                    property.SetValue(resistance);
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
                case ModelCode.DC_LINE_SEGMENT_INDUCTANCE:
                    inductanse = property.AsFloat();
                    break;

                case ModelCode.DC_LINE_SEGMENT_RESISTANCE:
                    resistance = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            base.GetReferences(references, refType);
        }
    }
}
