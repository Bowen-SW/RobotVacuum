using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]

public class RunReport
{
	public string floorPlanId;
	public string duration;
	public string dateStamp;
	public string algorithmType;
	public int coverageValue;

	public RunReport(string dateStampS, string durationS, string algorithmTypeS, int coveragePercentage)
	{
		this.duration = durationS;
		this.dateStamp = dateStampS;
		this.algorithmType = algorithmTypeS;
		this.coverageValue = coveragePercentage;
	}

}