using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using UdaanChalange.Models;

namespace UdaanChalange.Controllers
{
    public class FindsStringController : Controller
    {
        // GET: FindsString
        public ActionResult Index()
        {
            IEnumerable<findString> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://newsapi.org/v2/top-headlines?apiKey=cd1db87dec794c2288c915ba6abeee94&country=in");
                //HTTP GET
                var responseTask = client.GetAsync("String2");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<findString>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<findString>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View();
        }
    }
}