using Microsoft.Build.Framework;
using Oefentoets1MVC.Data.Entities;

namespace Oefentoets1MVC.Models;

public class CreateSpecialViewModel
{
	public int VakId { get; set; }

	[Required]
	public string VakNaam { get; set; }
	
	[Required]
	public int Jaar { get; set; }
	
	[Required]
	public int? Resultaat { get; set; }
}