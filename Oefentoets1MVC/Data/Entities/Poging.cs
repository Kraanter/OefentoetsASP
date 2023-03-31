using System.ComponentModel.DataAnnotations;

namespace Oefentoets1MVC.Data.Entities;

public class Poging
{
	public int Id { get; set; }
	
	[Required]
	public int Jaar { get; set; }
	
	[Required]
	public int Resultaat { get; set; }
	
	public string? StudentType { get; set; }
	
	public string VakNaam { get; set; }
	
	// DEEZ moet ? hebben omdat hij anders niet door de modelvalid komt moet dit anders?
	public Vak? Vak { get; set; }
}