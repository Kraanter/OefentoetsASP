namespace Oefentoets1MVC.Models;

public class WhatHappened
{
	public List<string> things { get; set; } = new List<string>();

	public void Add(string str)
	{
		things.Add(str);
	}

	public List<string> Export()
	{
		var retList = new List<string>(things);
		retList.Reverse();
		return retList;
	}

}