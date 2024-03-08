using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Line : EquipmentContainer
    {
        private long region;
        public Line(long globalId) : base(globalId)
        {

        }

        public long Region { get => region; set => region = value; }

        public override bool Equals(object obj)
        {
            return obj is Line line &&
                   base.Equals(obj) &&
                   region == line.region;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


		#region IAccess implementation

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.LINE_REGION:

					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.LINE_REGION:
					property.SetValue(region);
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
				case ModelCode.LINE_REGION:
					region = property.AsReference();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (region != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.LINE_REGION] = new List<long> { region };
			}

			base.GetReferences(references, refType);
		}
	}
}
