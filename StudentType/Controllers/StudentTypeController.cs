using Microsoft.AspNetCore.Mvc;

namespace StudentType.Controllers;

[ApiController]
[Route("/[controller]")]
public class StudentTypeController: ControllerBase
{
	private static readonly string[] Types = new[]
	{
		"Master", "Chipmunk", "Snatcher"
	};
	
	[HttpGet(Name = "get")]
	public string Get()
	{
		int randNum = new Random().Next(3);
		return Types[randNum];
	}
}