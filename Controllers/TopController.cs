using Microsoft.AspNetCore.Mvc;

namespace webdemo.Controllers;
public class TopController : Controller{
    public IActionResult Index(){
        return View();
    }
}