using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    public class Importer
    {
		private static Importer ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static Importer Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new Importer();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			ImportSubGeographicalRegions();
			ImportLines();
			ImportSeriesCompensators();
			ImportDCLineSegments();
			ImportPerLengthSequenceImpedances();
			ImportACLineSegments();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import

		//SubGeographicalRegion
		private void ImportSubGeographicalRegions()
		{
			SortedDictionary<string, object> cimSubGegoraphicalRegions = concreteModel.GetAllObjectsOfType("FTN.SubGeographicalRegion");
			if (cimSubGegoraphicalRegions != null)
			{
				foreach (KeyValuePair<string, object> cimSubGeographicalRegionPair in cimSubGegoraphicalRegions)
				{
					FTN.SubGeographicalRegion cimSubGeographicalRegion = cimSubGeographicalRegionPair.Value as FTN.SubGeographicalRegion;

					ResourceDescription rd = CreateSubGeographicalRegionResourceDescription(cimSubGeographicalRegion);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("SubGeographicalRegion ID = ").Append(cimSubGeographicalRegion.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("SubGeographicalRegion ID = ").Append(cimSubGeographicalRegion.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}

		}

		private ResourceDescription CreateSubGeographicalRegionResourceDescription(FTN.SubGeographicalRegion cimSubGeographicalRegion)
		{
			ResourceDescription rd = null;
			if (cimSubGeographicalRegion != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SUBGEOGRAPHICALREGION, importHelper.CheckOutIndexForDMSType(DMSType.SUBGEOGRAPHICALREGION));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSubGeographicalRegion.ID, gid);

				////populate ResourceDescription
				Converter.PopulateSubGeographicalRegionProperties(cimSubGeographicalRegion, rd);
			}
			return rd;
		}

		// Line
		private void ImportLines()
		{
			SortedDictionary<string, object> cimLines = concreteModel.GetAllObjectsOfType("FTN.Line");
			if (cimLines != null)
			{
				foreach (KeyValuePair<string, object> cimLinePair in cimLines)
				{
					FTN.Line cimLine = cimLinePair.Value as FTN.Line;

					ResourceDescription rd = CreateLineResourceDescription(cimLine);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Line ID = ").Append(cimLine.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Line ID = ").Append(cimLine.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}

		}

		private ResourceDescription CreateLineResourceDescription(FTN.Line cimLine)
		{
			ResourceDescription rd = null;
			if (cimLine != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.LINE, importHelper.CheckOutIndexForDMSType(DMSType.LINE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimLine.ID, gid);

				////populate ResourceDescription
				Converter.PopulateLineProperties(cimLine, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportSeriesCompensators()
		{
			SortedDictionary<string, object> cimSeriesCompensators = concreteModel.GetAllObjectsOfType("FTN.SeriesCompensator");
			if (cimSeriesCompensators != null)
			{
				foreach (KeyValuePair<string, object> cimSeriesCompensatorPair in cimSeriesCompensators)
				{
					FTN.SeriesCompensator cimSeriesCompensator = cimSeriesCompensatorPair.Value as FTN.SeriesCompensator;

					ResourceDescription rd = CreateSeriesCompensatorResourceDescription(cimSeriesCompensator);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}

		}

		private ResourceDescription CreateSeriesCompensatorResourceDescription(FTN.SeriesCompensator cimSeriesCompensator)
		{
			ResourceDescription rd = null;
			if (cimSeriesCompensator != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SERIESCOMPENSATOR, importHelper.CheckOutIndexForDMSType(DMSType.SERIESCOMPENSATOR));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSeriesCompensator.ID, gid);

				////populate ResourceDescription
				Converter.PopulateSeriesCompensatorProperties(cimSeriesCompensator, rd, importHelper, report);
			}
			return rd;
		}

		//DCLineSegment
		private void ImportDCLineSegments()
		{
			SortedDictionary<string, object> cimDCLineSegments = concreteModel.GetAllObjectsOfType("FTN.DCLineSegment");
			if (cimDCLineSegments != null)
			{
				foreach (KeyValuePair<string, object> cimDCLineSegmentPair in cimDCLineSegments)
				{
					FTN.DCLineSegment cimDCLineSegment = cimDCLineSegmentPair.Value as FTN.DCLineSegment;

					ResourceDescription rd = CreateDCLineSegmentResourceDescription(cimDCLineSegment);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateDCLineSegmentResourceDescription(FTN.DCLineSegment cimDCLineSegment)
		{
			ResourceDescription rd = null;
			if (cimDCLineSegment != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DCLINESEGMENT, importHelper.CheckOutIndexForDMSType(DMSType.DCLINESEGMENT));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimDCLineSegment.ID, gid);

				////populate ResourceDescription
				Converter.PopulateDCLineSegmentProperties(cimDCLineSegment, rd, importHelper, report);
			}
			return rd;
		}

		//ACLineSegment

		private void ImportACLineSegments()
		{
			SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.ACLineSegment");
			if (cimACLineSegments != null)
			{
				foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)
				{
					FTN.ACLineSegment cimACLineSegment = cimACLineSegmentPair.Value as FTN.ACLineSegment;

					ResourceDescription rd = CreateACLineSegmentResourceDescription(cimACLineSegment);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateACLineSegmentResourceDescription(FTN.ACLineSegment cimACLineSegment)
		{
			ResourceDescription rd = null;
			if (cimACLineSegment != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ACLINESEGMENT, importHelper.CheckOutIndexForDMSType(DMSType.ACLINESEGMENT));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimACLineSegment.ID, gid);

				////populate ResourceDescription
				Converter.PopulateACLineSegmentProperties(cimACLineSegment, rd, importHelper, report);
			}
			return rd;
		}
		private void ImportPerLengthSequenceImpedances()
		{
			SortedDictionary<string, object> cimPerLengthSequenceImpedances = concreteModel.GetAllObjectsOfType("FTN.PerLengthSequenceImpedance");
			if (cimPerLengthSequenceImpedances != null)
			{
				
				foreach (KeyValuePair<string, object> cimPerLengthSequenceImpedancePair in cimPerLengthSequenceImpedances)
				{
					FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance = cimPerLengthSequenceImpedancePair.Value as FTN.PerLengthSequenceImpedance;

					ResourceDescription rd = CreatePerLengthSequenceImpedanceResourceDescription(cimPerLengthSequenceImpedance);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("PerLengthSequenceImpedance ID = ").Append(cimPerLengthSequenceImpedance.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("PerLengthSequenceImpedance ID = ").Append(cimPerLengthSequenceImpedance.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreatePerLengthSequenceImpedanceResourceDescription(FTN.PerLengthSequenceImpedance cimPerLengthSequenceImpedance)
		{
			ResourceDescription rd = null;
			if (cimPerLengthSequenceImpedance != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PERLENGTHSEQUENCEIMPEDANCE, importHelper.CheckOutIndexForDMSType(DMSType.PERLENGTHSEQUENCEIMPEDANCE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimPerLengthSequenceImpedance.ID, gid);

				////populate ResourceDescription
				Converter.PopulatePerLengthSequenceImpedanceProperties(cimPerLengthSequenceImpedance, rd, importHelper, report);
			}
			return rd;
		}

		#endregion
	}
}
