using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class Equipment : PowerSystemResource
	{
		private bool aggregate;
		private bool normallyInService;
		private long equipmentContainer;
						
		public Equipment(long globalId) : base(globalId) 
		{
		}

        public bool Aggregate { get => aggregate; set => aggregate = value; }
        public bool NormallyInService { get => normallyInService; set => normallyInService = value; }
        public long EquipmentContainer { get => equipmentContainer; set => equipmentContainer = value; }

        public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Equipment x = (Equipment)obj;
				return ((x.Aggregate == this.Aggregate) &&
						(x.NormallyInService == this.NormallyInService) &&
						x.EquipmentContainer == this.equipmentContainer);
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

		#region IAccess implementation

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.EQUIPMENT_AGGREGATE:
				case ModelCode.EQUIPMENT_NORM_IN_SERV:
				case ModelCode.EQUIPMENT_EQUIPCONT:
		
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.EQUIPMENT_AGGREGATE:
					property.SetValue(aggregate);
					break;

				case ModelCode.EQUIPMENT_NORM_IN_SERV:
					property.SetValue(normallyInService);
					break;

				case ModelCode.EQUIPMENT_EQUIPCONT:
					property.SetValue(equipmentContainer);
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
				case ModelCode.EQUIPMENT_AGGREGATE:					
					aggregate = property.AsBool();
					break;

				case ModelCode.EQUIPMENT_NORM_IN_SERV:
					normallyInService = property.AsBool();
					break;

				case ModelCode.EQUIPMENT_EQUIPCONT:
					equipmentContainer = property.AsReference();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (equipmentContainer != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.EQUIPMENT_EQUIPCONT] = new List<long> { equipmentContainer };
			}

			base.GetReferences(references, refType);
		}
	}
}
