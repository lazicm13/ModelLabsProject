namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    using FTN.Common;
    public static class Converter
    {
		#region Populate ResourceDescription

		// finished
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_DESCRIPTION, cimIdentifiedObject.AliasName));
				}
			}
		}

		// finished
		public static void PopulateSubGeographicalRegionProperties(FTN.SubGeographicalRegion cimSubGeographicalRegion, ResourceDescription rd)
		{
			if ((cimSubGeographicalRegion != null) && (rd != null))
			{
				Converter.PopulateIdentifiedObjectProperties(cimSubGeographicalRegion, rd);
			}
		}
		// finished
		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				Converter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}

		// finished
		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				Converter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);
			}

			if (cimEquipment.AggregateHasValue)
			{
				rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
			}
			if (cimEquipment.NormallyInServiceHasValue)
			{
				rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORM_IN_SERV, cimEquipment.NormallyInService));
			}
			if (cimEquipment.EquipmentContainerHasValue)
			{
				long gid = importHelper.GetMappedGID(cimEquipment.EquipmentContainer.ID);
				if (gid < 0)
				{
					report.Report.Append("WARNING: Convert ").Append(cimEquipment.GetType().ToString()).Append(" rdfID = \"").Append(cimEquipment.ID);
					report.Report.Append("\" - Failed to set reference to EquipmentContainer: rdfID \"").Append(cimEquipment.EquipmentContainer.ID).AppendLine(" \" is not mapped to GID!");
				}
				rd.AddProperty(new Property(ModelCode.EQUIPMENT_EQUIPCONT, gid));
			}
		}

		// finished
		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				Converter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);
			}
		}

		// finished
		public static void PopulateConductorProperties(FTN.Conductor cimConductor, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductor != null) && (rd != null))
			{
				Converter.PopulateConductingEquipmentProperties(cimConductor, rd, importHelper, report);
			}

			if (cimConductor.LengthHasValue)
			{
				rd.AddProperty(new Property(ModelCode.CONDUCTOR_LENGTH, cimConductor.Length));
			}
		}

		// finished
		public static void PopulateSeriesCompensatorProperties(FTN.SeriesCompensator cimSeriesCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimSeriesCompensator != null) && (rd != null))
			{
				Converter.PopulateConductingEquipmentProperties(cimSeriesCompensator, rd, importHelper, report);
			}

			if (cimSeriesCompensator.RHasValue)
			{
				rd.AddProperty(new Property(ModelCode.SERIES_COMPENSATOR_R, cimSeriesCompensator.R));
			}
			if (cimSeriesCompensator.R0HasValue)
			{
				rd.AddProperty(new Property(ModelCode.SERIES_COMPENSATOR_R0, cimSeriesCompensator.R0));
			}
			if (cimSeriesCompensator.XHasValue)
			{
				rd.AddProperty(new Property(ModelCode.SERIES_COMPENSATOR_X, cimSeriesCompensator.X));
			}
			if (cimSeriesCompensator.X0HasValue)
			{
				rd.AddProperty(new Property(ModelCode.SERIES_COMPENSATOR_X0, cimSeriesCompensator.X0));
			}
		}

		// finished
		public static void PopulateDCLineSegmentProperties(FTN.DCLineSegment cimDCLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimDCLineSegment != null) && (rd != null))
			{
				Converter.PopulateConductorProperties(cimDCLineSegment, rd, importHelper, report);
			}
			if (cimDCLineSegment.DcSegmentInductanceHasValue)
			{
				rd.AddProperty(new Property(ModelCode.DC_LINE_SEGMENT_INDUCTANCE, cimDCLineSegment.DcSegmentInductance));
			}
			if (cimDCLineSegment.DcSegmentResistanceHasValue)
			{
				rd.AddProperty(new Property(ModelCode.DC_LINE_SEGMENT_RESISTANCE, cimDCLineSegment.DcSegmentResistance));
			}
		}

		// finished
		public static void PopulateACLineSegmentProperties(FTN.ACLineSegment cimACLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimACLineSegment != null) && (rd != null))
			{
				Converter.PopulateConductorProperties(cimACLineSegment, rd, importHelper, report);

				if (cimACLineSegment.B0chHasValue)
				{
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_B0CH, cimACLineSegment.B0ch));
				}
				if (cimACLineSegment.BchHasValue)
				{
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_BCH, cimACLineSegment.Bch));
				}
				if (cimACLineSegment.G0chHasValue)
				{
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_G0CH, cimACLineSegment.G0ch));
				}
				if (cimACLineSegment.GchHasValue)
				{
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_GCH, cimACLineSegment.Gch));
				}
				if (cimACLineSegment.RHasValue)
				{
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_R, cimACLineSegment.R));
				}
				if (cimACLineSegment.R0HasValue)
				{
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_R0, cimACLineSegment.R0));
				}
				if (cimACLineSegment.XHasValue)
				{
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_X, cimACLineSegment.X));
				}
				if (cimACLineSegment.X0HasValue)
				{
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_X0, cimACLineSegment.X0));
				}

				if (cimACLineSegment.PerLengthImpedanceHasValue)
				{
					long gid = importHelper.GetMappedGID(cimACLineSegment.PerLengthImpedance.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimACLineSegment.GetType().ToString()).Append(" rdfID = \"").Append(cimACLineSegment.ID);
						report.Report.Append("\" - Failed to set reference to PerLengthImpedance: rdfID \"").Append(cimACLineSegment.PerLengthImpedance.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.AC_LINE_SEGMENT_PERLENGTHIMP, gid));
				}
			}
		}

		// finished
		public static void PopulateConnectivityNodeContainerProperties(FTN.ConnectivityNodeContainer cimConnectivityNodeContainer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConnectivityNodeContainer != null) && (rd != null))
			{
				Converter.PopulatePowerSystemResourceProperties(cimConnectivityNodeContainer, rd, importHelper, report);
			}
		}

		// finished
		public static void PopulateEquipmentContainerProperties(FTN.EquipmentContainer cimEquipmentContainer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipmentContainer != null) && (rd != null))
			{
				Converter.PopulateConnectivityNodeContainerProperties(cimEquipmentContainer, rd, importHelper, report);
			}
		}

		public static void PopulateLineProperties(FTN.Line cimLine, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimLine != null) && (rd != null))
			{
				Converter.PopulateEquipmentContainerProperties(cimLine, rd, importHelper, report);
			}

			if (cimLine.RegionHasValue)
			{
				long gid = importHelper.GetMappedGID(cimLine.Region.ID);
				if (gid < 0)
				{
					report.Report.Append("WARNING: Convert ").Append(cimLine.GetType().ToString()).Append(" rdfID = \"").Append(cimLine.ID);
					report.Report.Append("\" - Failed to set reference to Region: rdfID \"").Append(cimLine.Region.ID).AppendLine(" \" is not mapped to GID!");
				}
				rd.AddProperty(new Property(ModelCode.LINE_REGION, gid));
			}
		}

		// finished
		public static void PopulatePerLengthImpedanceProperties(FTN.PerLengthImpedance cimPerLengthImpedance, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPerLengthImpedance != null) && (rd != null))
			{
				Converter.PopulateIdentifiedObjectProperties(cimPerLengthImpedance, rd);
			}

		}
		
		// finished
		public static void PopulatePerLengthSequenceImpedanceProperties(FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPerLengthSequenceImpedance != null) && (rd != null))
			{
				Converter.PopulatePerLengthImpedanceProperties(cimPerLengthSequenceImpedance, rd, importHelper, report);

				if (cimPerLengthSequenceImpedance.B0chHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PER_LENGTH_SEQ_IMPEDANCE_B0CH, cimPerLengthSequenceImpedance.B0ch));
				}
				if (cimPerLengthSequenceImpedance.BchHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PER_LENGTH_SEQ_IMPEDANCE_BCH, cimPerLengthSequenceImpedance.Bch));
				}
				if (cimPerLengthSequenceImpedance.G0chHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PER_LENGTH_SEQ_IMPEDANCE_G0CH, cimPerLengthSequenceImpedance.G0ch));
				}
				if (cimPerLengthSequenceImpedance.GchHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PER_LENGTH_SEQ_IMPEDANCE_GCH, cimPerLengthSequenceImpedance.Gch));
				}
				if (cimPerLengthSequenceImpedance.RHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PER_LENGTH_SEQ_IMPEDANCE_R, cimPerLengthSequenceImpedance.R));
				}
				if (cimPerLengthSequenceImpedance.R0HasValue)
				{
					rd.AddProperty(new Property(ModelCode.PER_LENGTH_SEQ_IMPEDANCE_R0, cimPerLengthSequenceImpedance.R0));
				}
				if (cimPerLengthSequenceImpedance.XHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PER_LENGTH_SEQ_IMPEDANCE_X, cimPerLengthSequenceImpedance.X));
				}
				if (cimPerLengthSequenceImpedance.X0HasValue)
				{
					rd.AddProperty(new Property(ModelCode.PER_LENGTH_SEQ_IMPEDANCE_X0, cimPerLengthSequenceImpedance.X0));
				}
			}
		}

		#endregion
	}
}
