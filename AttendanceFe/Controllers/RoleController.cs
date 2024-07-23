using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AttendanceFe.Models;

namespace AttendanceFe.Controllers
{
    public class RoleController : BaseController
    {
        private string rootApiUrl;
        private IConfiguration _configuration;

        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;
            rootApiUrl = _configuration.GetSection("ApiUrls")["MyApi"];
        }

        public IActionResult RoleDetail()
        {
            string roleResourcePath = rootApiUrl + "Role";
            List<Role>? roles = GetData<List<Role>?>(roleResourcePath).Result;
            if (roles == null)
            {
                roles = new List<Role>();
            }
            return View(roles);
        }


    }
}
