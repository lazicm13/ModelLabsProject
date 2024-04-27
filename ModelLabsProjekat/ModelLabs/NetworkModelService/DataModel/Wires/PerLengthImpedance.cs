using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PerLengthImpedance : IdentifiedObject
    {
		public PerLengthImpedance(long globalId) : base(globalId) { }

		private List<long> acLineSegments = new List<long>();

		public List<long> AcLineSegments { get => acLineSegments; set => acLineSegments = value; }



		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				PerLengthImpedance x = (PerLengthImpedance)obj;
				return CompareHelper.CompareLists(x.AcLineSegments, this.AcLineSegments, true);
			}
			else
			{
				return false;
			}
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
				case ModelCode.PER_LENGTH_IMPEDANCE_ACLSEG:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.PER_LENGTH_IMPEDANCE_ACLSEG:
					property.SetValue(this.AcLineSegments);
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
		#endregion

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return (AcLineSegments.Count > 0) || base.IsReferenced;
			}
		}


        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (AcLineSegments != null && AcLineSegments.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.PER_LENGTH_IMPEDANCE_ACLSEG] = AcLineSegments.GetRange(0, AcLineSegments.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.AC_LINE_SEGMENT_PERLENGTHIMP:
					acLineSegments.Add(globalId);
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

					if (AcLineSegments.Contains(globalId))
					{
						AcLineSegments.Remove(globalId);
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

		#endregion IReference implementation

	}
}
