using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]

public class RunReport
{
	public int reportID;
	public string dateStamp;
	public string duration;
	public string algorithmType;
	public int coverageValue;

	public RunReport(int reportIdI, string dateStampS, string durationS, string algorithmTypeS, int coveragePercentage)
	{
		this.reportID = reportIdI;
		this.dateStamp = dateStampS;
		this.duration = durationS;
		this.algorithmType = algorithmTypeS;
		this.coverageValue = coveragePercentage;
	}

	public int getID()
	{
		return reportID;
	}

	public string getDate()
	{
		return dateStamp;
	}

	public string getDuration()
	{
		return duration;
	}

	public string getAlgorithmType()
	{
		return algorithmType;
	}

	public int getCoverage()
	{
		return coverageValue;
	}


}