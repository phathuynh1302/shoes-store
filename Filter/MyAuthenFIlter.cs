using Microsoft.AspNetCore.Http;
using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PRN211_ShoesStore.Repository;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Service;

namespace PRN211_ShoesStore.Filter

{
	public class MyAuthenFIlter : ActionFilterAttribute
	{
		private readonly string _role;

		public MyAuthenFIlter(string role)
		{
			_role = role;
		}
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			// Check if the user is authenticated and has the required session data
			//if (!context.HttpContext.User.Identity.IsAuthenticated)
			//{
			//	context.Result = new UnauthorizedResult();
			//}
			//else
			//{
			int? id = context.HttpContext.Session.GetInt32("UserId");
			var role = context.HttpContext.Session.GetString("Role");
			var status = context.HttpContext.Session.GetString("Status");

			int userId;

			if (id == null)
			{
				userId = 0;
			}
			else
			{
				userId = id.Value;
			}

			if (_role == "User")
			{
				if (userId == 0 || !role.Equals("User") || status.Equals("false")) {
					context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
				}
				

			}

            if (_role == "Admin")
            {
                if (userId == 0 || !role.Equals("Admin") || status.Equals("false"))
                {
                    context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
                }


            }


        }







		//}
	}
}

