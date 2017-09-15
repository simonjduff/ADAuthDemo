using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdWeb.Models;
using Novell.Directory.Ldap;

namespace AdWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Login(AuthModel model)
        {
            LdapConnection connection = null;
            try
            {
                connection = new LdapConnection();
                connection.Connect("host", 80); // Change these
                connection.Bind(model.Username, model.Password);
                model.Message = "Succeeded";
            }
            catch (Exception e)
            {
                model.Message = $"Failed: {e.ToString()}";
            }
            finally
            {
                connection?.Disconnect();
                connection?.Dispose();
            }

            return View("Index", model);
        }
    }
}
