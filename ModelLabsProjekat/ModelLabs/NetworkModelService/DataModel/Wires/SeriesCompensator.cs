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
    }
}
