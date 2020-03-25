using System.Collections;
using System.Collections.Generic;
using System;
	
//for testing purposes					
// public class Program
// {
// 	public static void Main()
// 	{
// 		Console.WriteLine("Hello World");
// 		RunReport run1 = new RunReport();
// 		run1.setStart();
// 		run1.setTimeStamp();
// 		run1.setAlgorithm("Snaking");
// 		run1.setID("Some_floor_Plan");
// 		run1.setCoveragePercentage(34.1);
// 		Console.WriteLine(run1.gettimeStamp());
// 		Console.WriteLine(run1.getDuration());
// 		Console.WriteLine(run1.getID());
// 		Console.WriteLine(run1.getAlgorithm());
// 		Console.WriteLine(run1.getCoveragePercentage());
// 	}
// }

[System.Serializable]
public class RunReport
{
	public static string floorPlanId;
	public static int duration;
	public static DateTime startTime;
	public static DateTime timeStamp;
	public static string algorithmType;
	public static double coverage;

	public RunReport()
	{
		floorPlanId = "";
		duration = 0;
		startTime = DateTime.Now;
		timeStamp = DateTime.Now;
		algorithmType ="";
		coverage=0;
	}
	// get and set floor plan ID
	public string getID()
	{
		return floorPlanId;
    }
    public void setID(string ID)
    {
    	floorPlanId = ID;
    }

    // set the start time
	public void setStart()
	{
		startTime = DateTime.Now;
	}

	//get and set the duration of the simulation
	public int getDuration()
	{
		return duration;
	}
	public void setDuration(double timeRunning) 
	{
		duration = (int)timeRunning;
	}

	// get and set the time stamp
	public DateTime gettimeStamp()
	{
		return timeStamp;
	}

	public void setTimeStamp()
	{
		timeStamp = DateTime.Now;
	}

	// get and set the algorithm that is being used for this run
	public string getAlgorithm()
	{
		return algorithmType;
	}

	public void setAlgorithm(string AlgorithmName)
	{
		algorithmType = AlgorithmName;
	}

	//get and set functions for the room coverage
	public double getCoveragePercentage()
	{
		return coverage;
	}
	public void setCoveragePercentage(double cleanlinessValue)
	{
		coverage = cleanlinessValue;
	}
}