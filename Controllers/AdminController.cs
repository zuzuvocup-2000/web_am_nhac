using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using webdemo.Models;

namespace webdemo.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }
    public IActionResult Login(){
        return View();
    }
    public IActionResult Dashboard(){
        return View();
    }
    
    public IActionResult User(){
        using (var db = new music_dataContext()){
            var listUser = db.Users.ToList();
            var countUser = db.Users.Count();
            var viewModel = new ListUserViewModel();
            viewModel.User = listUser;
            viewModel.countUser = countUser;
             return View(viewModel);
        }
    }

    public IActionResult NewUser()
    {
        return View();
    }

    public RedirectResult ActionNewUser(CreateUserFromModel newForm)
    {
        using( var db= new music_dataContext())
        {
            db.Users.Add(new User{
                Name = newForm.Name,
                Email = newForm.Email,
                Password = newForm.Password,
                Type = newForm.Type,
                Coin = newForm.Coin,
            });
            db.SaveChanges();
            return new RedirectResult(url: "/Admin/User");
        }
    }
    public IActionResult EditUser(int id)
    {
        using( var db= new music_dataContext())
        {
             var targetObj = db.Users.Where(e => e.Id == id).FirstOrDefault();
             if(targetObj != null) {
                var userViewModel = new UserViewModel();
                userViewModel.User = targetObj;
                return View(userViewModel);
             }
            return new RedirectResult(url: "/Admin/User");
        }
    }
    public RedirectResult ActionEditUser(int id, CreateUserFromModel editForm)
    {
        using( var db= new music_dataContext())
        {
             var targetObj = db.Users.Where(e => e.Id == id).FirstOrDefault();
             if(targetObj != null) {
                var userViewModel = new UserViewModel();
                userViewModel.User = targetObj;
                targetObj.Name = editForm.Name;
                targetObj.Email = editForm.Email;
                targetObj.Password = editForm.Password;
                targetObj.Type = editForm.Type;
                targetObj.Coin = editForm.Coin;
                db.SaveChanges();
             }
            return new RedirectResult(url: "/Admin/User");
        }
    }
    public RedirectResult DeleteUser(int id)
    {
        using( var db= new music_dataContext())
        {
            var targetObj = db.Users.Where(e => e.Id == id).FirstOrDefault();
            if(targetObj != null) {
                db.Users.Remove(targetObj);
                db.SaveChanges();
            }
            return new RedirectResult(url: "/Admin/User");
        }
    }

    public IActionResult NewAlbum()
    {
        return View();
    }
    
    public IActionResult Album(){
        using (var db = new music_dataContext()){
            var listAlbum = db.Albums.ToList();
            var viewModel = new ListAlbumViewModel();
            viewModel.Album = listAlbum;
             return View(viewModel);
        }
    }
    public IActionResult EditAlbum(int id)
    {
        using( var db= new music_dataContext())
        {
             var targetObj = db.Albums.Where(e => e.Id == id).FirstOrDefault();
             if(targetObj != null) {
                var albumViewModel = new AlbumViewModel();
                albumViewModel.Album = targetObj;
                return View(albumViewModel);
             }
            return new RedirectResult(url: "/Admin/Album");
        }
    }
    public RedirectResult ActionNewAlbum(CreateAlbumFromModel newForm, IFormFile image)
    {
        using( var db= new music_dataContext())
        {
            var userId = HttpContext.Request.Cookies["login"];
            using JsonDocument doc = JsonDocument.Parse(userId);
                JsonElement root = doc.RootElement;
            var id_member = root.GetProperty("id");
            FileInfo fileInfo = new FileInfo("/upload/images/"+id_member+"/");
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\images\\"+id_member , image.FileName);
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\images\\"+id_member , "");
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
            var file  = new FileStream(fullPath, FileMode.Create);
            using (file){
                image.CopyTo(file);
            }
            db.Albums.Add(new Album{
                Name = newForm.Name,
                Url = newForm.Url,
                Publish = newForm.Publish,
                Image = HttpUtility.UrlEncode("\\upload\\images\\"+id_member+"\\"+image.FileName),
            });
            db.SaveChanges();
            return new RedirectResult(url: "/Admin/Album");
        }
    }
    public RedirectResult ActionEditAlbum(int id, CreateAlbumFromModel editForm, IFormFile image)
    {
        using( var db= new music_dataContext())
        {
            var targetObj = db.Albums.Where(e => e.Id == id).FirstOrDefault();
            var userId = HttpContext.Request.Cookies["login"];
            using JsonDocument doc = JsonDocument.Parse(userId);
                JsonElement root = doc.RootElement;
            var id_member = root.GetProperty("id");
            FileInfo fileInfo = new FileInfo("/upload/images/"+id_member+"/");
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\images\\"+id_member , image.FileName);
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload\\images\\"+id_member , "");
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
            var file  = new FileStream(fullPath, FileMode.Create);
            using (file){
                image.CopyTo(file);
            }
            if(targetObj != null) {
                var AlbumViewModel = new AlbumViewModel();
                AlbumViewModel.Album = targetObj;
                targetObj.Name = editForm.Name;
                targetObj.Url = editForm.Url;
                targetObj.Publish = editForm.Publish;
                if(image.FileName != ""){
                    targetObj.Image = HttpUtility.UrlEncode("\\upload\\images\\"+id_member+"\\"+image.FileName);
                }
                db.SaveChanges();
            }
            
            return new RedirectResult(url: "/Admin/Album");
        }
    }
    public RedirectResult DeleteAlbum(int id)
    {
        using( var db= new music_dataContext())
        {
            var targetObj = db.Albums.Where(e => e.Id == id).FirstOrDefault();
            if(targetObj != null) {
                db.Albums.Remove(targetObj);
                db.SaveChanges();
            }
            return new RedirectResult(url: "/Admin/Album");
        }
    }

    public IActionResult Music(){
        using (var db = new music_dataContext()){
            var listMusic = db.Musics.ToList();
            var viewModel = new ListMusicViewModel();
            viewModel.Music = listMusic;
             return View(viewModel);
        }
    }
    public IActionResult NewMusic()
    {
        return View();
    }
    public RedirectResult ActionNewMusic(CreateMusicFromModel newForm)
    {
        using( var db= new music_dataContext())
        {
            db.Musics.Add(new Music{
                Album = newForm.Album,
                Name = newForm.Name,
                Artist = newForm.Artist,
                Theloai = newForm.Theloai,
                Publish = newForm.Publish,
                Url = newForm.Url,
                Music1 = newForm.Music1,
                Image = newForm.Image,
                Memberid = newForm.Memberid,
                Viewed = newForm.Viewed,                       
            });
            db.SaveChanges();
            return new RedirectResult(url: "/Admin/Music");
        }
    }
    
    public IActionResult EditMusic(int id)
    {
        using( var db= new music_dataContext())
        {
             var targetObj = db.Musics.Where(e => e.Id == id).FirstOrDefault();
             if(targetObj != null) {
                var musicViewModel = new MusicViewModel();
                musicViewModel.Music = targetObj;
                return View(musicViewModel);
             }
            return new RedirectResult(url: "/Admin/Music");
        }
    }
    public RedirectResult ActionEditMusic(int id, CreateMusicFromModel editForm)
    {
        using( var db= new music_dataContext())
        {
             var targetObj = db.Musics.Where(e => e.Id == id).FirstOrDefault();
             if(targetObj != null) {
                var MusicViewModel = new MusicViewModel();
                MusicViewModel.Music = targetObj;
                targetObj.Album = editForm.Album;
                targetObj.Name = editForm.Name;
                targetObj.Artist = editForm.Artist;
                targetObj.Theloai = editForm.Theloai;
                targetObj.Publish = editForm.Publish;
                targetObj.Url = editForm.Url;
                targetObj.Music1 = editForm.Music1;
                targetObj.Image = editForm.Image;
                targetObj.Memberid = editForm.Memberid;
                targetObj.Viewed = editForm.Viewed;
                db.SaveChanges();
             }
            return new RedirectResult(url: "/Admin/Music");
        }
    }
    public RedirectResult DeleteMusic(int id){
        using( var db= new music_dataContext())
        {
            var targetObj = db.Musics.Where(e => e.Id == id).FirstOrDefault();
            if(targetObj != null) {
                db.Musics.Remove(targetObj);
                db.SaveChanges();
            }
            return new RedirectResult(url: "/Admin/Music");
        }
    }
}

public partial class ListUserViewModel{
    public int countUser{get;set;}
    public List<User> User{get;set;}
}

public partial class UserViewModel
{
    public User User {get; set;}
}

public partial class CreateUserFromModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public sbyte Type { get; set; }
    public int Coin { get; set; }
}

public partial class ListAlbumViewModel{
    public List<Album> Album{get;set;}
}
public partial class AlbumViewModel
{
    public Album Album {get; set;}
}

public partial class CreateAlbumFromModel
{
    public string Name { get; set; }
    public string Url { get; set; }
    public sbyte Publish { get; set; }
    public string Image { get; set; }
    public int Memberid { get; set; }
}

public partial class ListMusicViewModel{
    public List<Music> Music{get;set;}
}
public partial class MusicViewModel
{
    public Music Music {get; set;}
}

public partial class CreateMusicFromModel
{
    public int Album { get; set; }
    public string Name { get; set; } 
    public string Artist { get; set; } 
    public string Theloai { get; set; } 
    public sbyte Publish { get; set; } 
    public string Url { get; set; }
    public string Music1 { get; set; }
    public string Image { get; set; }
    public string Memberid { get; set; }
    public int Viewed { get; set; }
}