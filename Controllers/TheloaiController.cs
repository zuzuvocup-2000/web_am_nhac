using Microsoft.AspNetCore.Mvc;

namespace webdemo.Controllers;
public class TheloaiController : Controller{
    public IActionResult Index(){
        return View();
    }
}