using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Web;
using System.Text.Json;
using DotNet.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using webdemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Drawing;

namespace webdemo.Controllers;

public class UploadController : Controller
{
    [HttpPost]
    public IActionResult Music(string image, string music){
        var userId = HttpContext.Request.Cookies["login"];
        using JsonDocument doc = JsonDocument.Parse(userId);
            JsonElement root = doc.RootElement;
        var id = root.GetProperty("id");
        FileInfo fileInfo = new FileInfo("/upload/music/"+id+"/");
        Console.WriteLine(fileInfo);

        return Redirect("/user/upload");
    }
}
