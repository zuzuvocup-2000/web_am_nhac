using Microsoft.AspNetCore.Mvc;

namespace webdemo.Controllers;
public class ArtistController : Controller{
    public IActionResult Index(){
        return View();
    }
}