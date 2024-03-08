using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class SubGeographicalRegion : IdentifiedObject
    {
        private List<long> lines = new List<long>();
        public SubGeographicalRegion(long globalId) : base(globalId)
        {

        }
        public List<long> Lines { get => lines; set => lines = value; }

        public override bool Equals(object obj)
        {
            return obj is SubGeographicalRegion region &&
                   base.Equals(obj) &&
                   EqualityComparer<List<long>>.Default.Equals(lines, region.lines);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.SUBGEOGRAPHICAL_REGION_LINES:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SUBGEOGRAPHICAL_REGION_LINES:
                    property.SetValue(this.lines);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }
        #region IReference Implementation
        public override bool IsReferenced
        {
            get
            {
                return lines.Count > 0 || base.IsReferenced;
            }

        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (lines != null && lines.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SUBGEOGRAPHICAL_REGION_LINES] = lines.GetRange(0, lines.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.LINE_REGION:
                    lines.Add(globalId);
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
                case ModelCode.LINE_REGION:
                    if (lines.Contains(globalId))
                    {
                        lines.Remove(globalId);
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
