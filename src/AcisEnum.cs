using System.Collections.Generic;

namespace NetSAT
{
	public enum AcisEntityType
	{
		ENM_ACIS_STRAIGHT_CRV = 1,
		ENM_ACIS_ELLIPSE_CRV = 2,
		ENM_ACIS_INTCURVE_CRV = 3,
		ENM_ACIS_PLANE_SURF = 11,
		ENM_ACIS_CONE_SURF = 12,
		ENM_ACIS_SPHERE_SURF = 13,
		ENM_ACIS_TORUS_SURF = 14,
		ENM_ACIS_SPLINE_SURF = 15
	}

	public enum AcisSense
	{
		UNKNOWN = -1,
		FORWARD = 0,
		REVERSED = 1
	}

	public enum AcisBsplineType
	{
		OPEN = 0,
		PERIODIC = 1
	}
}