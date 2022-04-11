using Microsoft.AspNetCore.Mvc;

namespace webdemo.Controllers;
public class AlbumController : Controller{
    public IActionResult Index(){
        return View();
    }
}