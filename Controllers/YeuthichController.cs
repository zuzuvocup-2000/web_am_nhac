using Microsoft.AspNetCore.Mvc;

namespace webdemo.Controllers;
public class YeuthichController : Controller{
    public IActionResult Index(){
        return View();
    }
}