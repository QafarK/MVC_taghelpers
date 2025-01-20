using System.ComponentModel.DataAnnotations;

namespace MVC_taghelpers.Entities
{
	public class User
	{
		[Range(1,20)]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Surname { get; set; }

		public string Image { get; set; } = string.Empty;

		[Range(18, 60)]
		public int Age { get; set; }
	}
}
