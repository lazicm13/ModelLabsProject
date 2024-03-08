using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SeriesCompensator : ConductingEquipment
    {
        private float r;
        private float r0;
        private float x;
        private float x0;

        public SeriesCompensator(long globalId) : base(globalId)
        {

        }

        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }

        public override bool Equals(object obj)
        {
            return obj is SeriesCompensator compensator &&
                   base.Equals(obj) &&
                   r == compensator.r &&
                   r0 == compensator.r0 &&
                   x == compensator.x &&
                   x0 == compensator.x0;
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
                case ModelCode.SERIES_COMPENSATOR_R:
                case ModelCode.SERIES_COMPENSATOR_X:
                case ModelCode.SERIES_COMPENSATOR_X0:
                case ModelCode.SERIES_COMPENSATOR_R0:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SERIES_COMPENSATOR_X:
                    property.SetValue(this.X);
                    break;
                case ModelCode.SERIES_COMPENSATOR_X0:
                    property.SetValue(this.X0);
                    break;
                case ModelCode.SERIES_COMPENSATOR_R:
                    property.SetValue(this.R);
                    break;
                case ModelCode.SERIES_COMPENSATOR_R0:
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
                case ModelCode.SERIES_COMPENSATOR_X:
                    this.X = property.AsFloat();
                    break;
                case ModelCode.SERIES_COMPENSATOR_X0:
                    this.X0 = property.AsFloat();
                    break;
                case ModelCode.SERIES_COMPENSATOR_R:
                    this.R = property.AsFloat();
                    break;
                case ModelCode.SERIES_COMPENSATOR_R0:
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

        public override void AddReference(ModelCode referenceId, long globalId)
        {

            base.AddReference(referenceId, globalId);

        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {

            base.RemoveReference(referenceId, globalId);

        }

        #endregion
    }
}
