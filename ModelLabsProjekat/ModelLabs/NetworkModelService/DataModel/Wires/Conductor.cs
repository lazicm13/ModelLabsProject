using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Conductor : ConductingEquipment
    {
        private float length;
        public Conductor(long globalId) : base(globalId)
        {

        }

        public float Length { get => length; set => length = value; }

        public override bool Equals(object obj)
        {
            return obj is Conductor conductor &&
                   base.Equals(obj) &&
                   length == conductor.length;
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
                case ModelCode.CONDUCTOR_LENGTH:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONDUCTOR_LENGTH:
                    property.SetValue(this.Length);
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
                case ModelCode.CONDUCTOR_LENGTH:
                    this.Length = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }


        #endregion

        #region IReference Implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            base.GetReferences(references, refType);
        }
        #endregion
    }
}
