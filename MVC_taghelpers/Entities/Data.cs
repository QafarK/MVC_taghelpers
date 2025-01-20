using MVC_taghelpers.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MVC_taghelpers.Entities
{
	public static class Data
	{
		public static List<UserAddViewModel> GetDatas()
		{
			var jsonPath = "Data/Users.json";
			List<UserAddViewModel> users = null;
			string jsonString = string.Empty;

			if (System.IO.File.Exists(jsonPath))
			{
				string jsonReadFile = System.IO.File.ReadAllText(jsonPath);

				var usersViewModelJ = JsonConvert.DeserializeObject<dynamic>(jsonReadFile); // dynamic is JArray or JObject

				if (usersViewModelJ is JArray)
					users = usersViewModelJ.ToObject<List<UserAddViewModel>>();
				else //usersViewModelJ is JObject
				{
					users = new()
					{
						usersViewModelJ.ToObject<UserAddViewModel>()
					};
				}

				return users;
			}
			else
				return null;
		}
	}
}
