using System.ComponentModel.DataAnnotations;

namespace Oefentoets1MVC.Data.Entities;

public class Vak
{
	public int Id { get; set; }
	
	[Required]
	[MinLength(6)]
	public string Naam { get; set; }

	[Required]
	public int EC { get; set; }

	public ICollection<Poging?> Pogingen { get; set; }
}