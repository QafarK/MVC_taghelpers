using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MVC_taghelpers.Entities;
using MVC_taghelpers.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
namespace MVC_taghelpers.Controllers
{
	public class UserController : Controller
	{
		public string Index()
		{
			return "UserController\n--------------\nUser/Add\nUser/Details/id";
		}

		[HttpGet]
		public IActionResult Add()
		{

			var vm = new UserAddViewModel()
			{
				User = new User()
			};
			return View("Add", vm);
		}

		[HttpPost]
		public IActionResult Add(UserAddViewModel userVM)
		{
			if (ModelState.IsValid)
			{
				var jsonPath = "Data/Users.json";
				List<UserAddViewModel> users = null;
				string jsonString = string.Empty;

				if (System.IO.File.Exists(jsonPath))
				{
					string jsonReadFile = System.IO.File.ReadAllText(jsonPath);

					var usersViewModelJ = JsonConvert.DeserializeObject<dynamic>(jsonReadFile); // dynamic is JArray or JObject

					if (usersViewModelJ is JArray)
					{
						users = usersViewModelJ.ToObject<List<UserAddViewModel>>();
						users.Add(userVM);
					}
					else //usersViewModelJ is JObject
					{
						users = new()
						{
							usersViewModelJ.ToObject<UserAddViewModel>(),
							userVM
						};
					}

					jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);
				}
				else
				{
					jsonString = JsonConvert.SerializeObject(userVM, Formatting.Indented);
					users = new() { userVM }; // user as List
				}
				System.IO.File.WriteAllText(jsonPath, jsonString);

				View("Details", users);
				return RedirectToAction("Details");
			}
			return View("Add");
		}

		public IActionResult Details(int id = -1)
		{
			if (id == -1)
				return View("Details", Data.GetDatas());
			else
				return View("Details", Data.GetDatas().Where(u => u.User.Id == id));
		}
	}
}
