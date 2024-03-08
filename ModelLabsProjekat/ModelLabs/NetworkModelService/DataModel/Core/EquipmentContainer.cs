using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class EquipmentContainer : ConnectivityNodeContainer
    {
        private List<long> equipments = new List<long>();
        public EquipmentContainer(long globalId) : base(globalId)
        {
            
        }

        public List<long> Equipments { get => equipments; set => equipments = value; }

        public override bool Equals(object obj)
        {
            return obj is EquipmentContainer container &&
                   base.Equals(obj) &&
                   EqualityComparer<List<long>>.Default.Equals(equipments, container.equipments);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.EQUIPMENT_CONTAINER_EQUIPMENTS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.EQUIPMENT_CONTAINER_EQUIPMENTS:
                    property.SetValue(this.equipments);
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
    }
}
