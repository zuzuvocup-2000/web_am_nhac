using Microsoft.AspNetCore.Mvc;

namespace webdemo.Controllers;
public class MusicController : Controller{
    public IActionResult Index(){
        return View();
    }
}