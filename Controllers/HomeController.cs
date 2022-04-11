using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using webdemo.Models;

namespace webdemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        using (var db = new music_dataContext()){
            var newsOrder = db.Musics.Where(a => a.Publish == 1).OrderByDescending(o=>o.Id).Take(15).ToList();
            var categories = db.Albums.Where(a => a.Publish == 1).ToList();
            var indexViewModel = new IndexViewModel();
            indexViewModel.Albums = categories;
            indexViewModel.newsOrder = newsOrder;

            return View(indexViewModel);
        }

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

    public class IndexViewModel{
        public List<Album> Albums {get;set;}
        public List<Music> newsOrder {get;set;}
    }
}
