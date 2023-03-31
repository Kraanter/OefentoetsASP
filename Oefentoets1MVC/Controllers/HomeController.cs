using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oefentoets1MVC.Data;
using Oefentoets1MVC.Data.Entities;
using Oefentoets1MVC.Models;
using Oefentoets1MVC.Utils;

namespace Oefentoets1MVC.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly CijferRegistratieDBContext _dbContext;

	public HomeController(ILogger<HomeController> logger, CijferRegistratieDBContext dbContext)
	{
		_logger = logger;
		_dbContext = dbContext;
	}

	public IActionResult Index()
	{
		var vakken = _dbContext.Vakken.OrderByDescending(e => e.EC).Include(v => v.Pogingen).ToList();
		var messages = HttpContext.Session.GetLog();
		return View(new IndexViewModel()
		{
			Log = messages,
			Vakken = vakken
		});
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}