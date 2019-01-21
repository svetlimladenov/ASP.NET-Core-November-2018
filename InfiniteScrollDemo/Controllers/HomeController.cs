using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InfiniteScrollDemo.Models;

namespace InfiniteScrollDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var posts = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                posts.Add($"New post {i}");
            }

            var viewModel = new AllPostsViewModel()
            {
                Posts = posts
            };

            
            return View(viewModel);
        }

        public void GetPosts(int pageNumber, int pageSize)
        {
            return;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
