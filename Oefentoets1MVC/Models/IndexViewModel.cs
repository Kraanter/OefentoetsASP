using Oefentoets1MVC.Data.Entities;

namespace Oefentoets1MVC.Models;

public class IndexViewModel
{
	public IEnumerable<Vak> Vakken { get; set; }

	public List<string> Log { get; set; }
}