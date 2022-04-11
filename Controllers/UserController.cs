using System.Data.Common;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using MySql.Data.MySqlClient;
using webdemo.Models;

namespace webdemo.Controllers;
public class UserController : Controller{
    public IActionResult Index(){
        var stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder["Server"] = "127.0.0.1";
            stringBuilder["Database"] = "music_data";
            stringBuilder["User Id"] = "root";
            stringBuilder["Password"] = "root";
            stringBuilder["Port"] = "3307";
        String sqlConnectionString = stringBuilder.ToString();
        var connection = new MySqlConnection(sqlConnectionString);

        connection.Open();

        using (DbCommand command = connection.CreateCommand())
        {
            var userId = HttpContext.Request.Cookies["login"];
            if (String.IsNullOrEmpty(userId)){
                return Redirect("/home/index");
            }
            using JsonDocument doc = JsonDocument.Parse(userId);
                JsonElement root = doc.RootElement;
            var email = root.GetProperty("email");
            var name = root.GetProperty("fullname");
            var id = root.GetProperty("id");
            //  query
            command.CommandText = "select id, name, email from user where (id='"+id+"')";
            var data  = command.ExecuteReader();
            Dictionary<string, string> print = new Dictionary<string, string>();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    var id_user = data.GetString(0);
                    var fullname_user = data.GetString(1);
                    var email_check = data.GetString(2);

                    ViewBag.id = id_user;
                    ViewBag.fullname = fullname_user;
                    ViewBag.email = email_check;
                }
            }else{
                return Redirect("/home/index");
            }
            connection.Close();
        }
        return View();
    }

    public IActionResult Pay(){

        var stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder["Server"] = "127.0.0.1";
            stringBuilder["Database"] = "music_data";
            stringBuilder["User Id"] = "root";
            stringBuilder["Password"] = "root";
            stringBuilder["Port"] = "3307";
        String sqlConnectionString = stringBuilder.ToString();
        var connection = new MySqlConnection(sqlConnectionString);

        connection.Open();

        using (DbCommand command = connection.CreateCommand())
        {
            var userId = HttpContext.Request.Cookies["login"];
            if (String.IsNullOrEmpty(userId)){
                return Redirect("/home/index");
            }
            using JsonDocument doc = JsonDocument.Parse(userId);
                JsonElement root = doc.RootElement;

            var email = root.GetProperty("email");
            var name = root.GetProperty("fullname");
            var id = root.GetProperty("id");
            //  query
            command.CommandText = "select id, name, email from user where (id='"+id+"')";
            var data  = command.ExecuteReader();
            Dictionary<string, string> print = new Dictionary<string, string>();
            if (data.HasRows)
            {
                return View();
            }else{
                return Redirect("/home/index");
            }
            connection.Close();
        }
    }
    public IActionResult Upload(){
        using (var db = new music_dataContext()){
            var userId = HttpContext.Request.Cookies["login"];
            UserAuth deptObj = JsonSerializer.Deserialize<UserAuth>(userId);
            var categories = db.Albums.Where(a => a.Memberid == int.Parse(deptObj.id)).ToList();
            var indexViewModel = new IndexViewModel();
            indexViewModel.Albums = categories;
            return View(indexViewModel);
        }
        return View();
    }

    public class UserAuth
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class IndexViewModel{
        public List<Album> Albums {get;set;}
    }

    [HttpPost]
    public IActionResult AlbumCreate(IFormFile image){
        var userId = HttpContext.Request.Cookies["login"];
        using JsonDocument doc = JsonDocument.Parse(userId);
            JsonElement root = doc.RootElement;
        var id = root.GetProperty("id");
        FileInfo fileInfo = new FileInfo("/upload/images/"+id+"/");
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\images\\"+id , image.FileName);
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\images\\"+id , "");
        if (!Directory.Exists(folder)) {
            Directory.CreateDirectory(folder);
        }
        using (var file  = new FileStream(fullPath, FileMode.Create)){
            image.CopyTo(file);
        }

        return Redirect("/user/upload");
    }


    [HttpPost]
    public IActionResult MusicCreate(IFormFile image, IFormFile music){
        var userId = HttpContext.Request.Cookies["login"];
        using JsonDocument doc = JsonDocument.Parse(userId);
            JsonElement root = doc.RootElement;
        var id = root.GetProperty("id");

        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\images\\"+id , image.FileName);
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\images\\"+id , "");
        if (!Directory.Exists(folder)) {
            Directory.CreateDirectory(folder);
        }
        using (var file  = new FileStream(fullPath, FileMode.Create)){
            image.CopyTo(file);
        }

        string fullPath_music = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\music\\"+id , music.FileName);
        string folder_music = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\music\\"+id , "");
        if (!Directory.Exists(folder_music)) {
            Directory.CreateDirectory(folder_music);
        }
        using (var file_music  = new FileStream(fullPath_music, FileMode.Create)){
            music.CopyTo(file_music);
        }

        return Redirect("/user/upload");
    }
}
