using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using webdemo.Models;
using System.Web;
namespace webdemo.Controllers;
public class ActionController : Controller{
    [HttpPost]
    public JsonResult CreateAlbum(string name, string url, string image, string  publish, int id){
        
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
            command.CommandText = "select count(id) from router where (url='"+url+"')";
            Int64 check_canonical = (Int64) command.ExecuteScalar();
            if(check_canonical != 0){
                return Json(new {
                    message_1 = "Đường dẫn đã tồn tại",
                    message_2 = "Xin vui lòng thử lại",
                    code = "error"
                });
            }

            command.CommandText = "INSERT INTO `music_data`.`album` (`name`, `url`,`image`, `publish`, `memberid`) VALUES ('"+name+"', '"+url+"', '"+HttpUtility.UrlEncode("\\upload\\images\\"+id+"\\"+image)+"', '"+publish+"', '"+id+"');";
            var flag = command.ExecuteNonQuery();
            if(flag > 0){
                command.CommandText = "select id from album order by id desc limit 1";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        command.CommandText = "INSERT INTO `music_data`.`router` (`objectid`, `module`, `url`) VALUES ('"+String.Format("{0}",reader["id"])+"', 'album', '"+url+"');";
                        connection.Close();
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            connection.Close();

            if(flag > 0){
                return Json(new {
                    message_1 = "Tạo Album thành công",
                    message_2 = "Thành công",
                    code = "success"
                });
            }else{
                return Json(new {
                    message_1 = "Có lỗi xảy ra",
                    message_2 = "Xin vui lòng thử lại",
                    code = "error"
                });
            }
        }

         return Json(new {
            message_1 = "Tạo Album thành công",
            message_2 = "Thành công",
            code = "success"
        });
    }

    [HttpPost]
    public JsonResult CreateMusic(string name, string url, string image,string music, string  publish, int id, string artist, int album, string theloai){
        
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
            command.CommandText = "select count(id) from router where (url='"+url+"')";
            Int64 check_canonical = (Int64) command.ExecuteScalar();
            if(check_canonical != 0){
                return Json(new {
                    message_1 = "Đường dẫn đã tồn tại",
                    message_2 = "Xin vui lòng thử lại",
                    code = "error"
                });
            }

            command.CommandText = "INSERT INTO `music_data`.`music` (`name`, `url`,`image`, `publish`,`artist`, `album`, `theloai`, `music`, `memberid`) VALUES ('"+name+"', '"+url+"', '"+HttpUtility.UrlEncode("\\upload\\images\\"+id+"\\"+image)+"', '"+publish+"', '"+artist+"', '"+album+"', '"+theloai+"', '"+HttpUtility.UrlEncode("\\upload\\music\\"+id+"\\"+music)+"',  '"+id+"');";
            var flag = command.ExecuteNonQuery();
            if(flag > 0){
                command.CommandText = "select id from music order by id desc limit 1";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        command.CommandText = "INSERT INTO `music_data`.`router` (`objectid`, `module`, `url`) VALUES ('"+String.Format("{0}",reader["id"])+"', 'music', '"+url+"');";
                        connection.Close();
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            connection.Close();

            if(flag > 0){
                return Json(new {
                    message_1 = "Tạo Album thành công",
                    message_2 = "Thành công",
                    code = "success"
                });
            }else{
                return Json(new {
                    message_1 = "Có lỗi xảy ra",
                    message_2 = "Xin vui lòng thử lại",
                    code = "error"
                });
            }
        }

         return Json(new {
            message_1 = "Tạo Album thành công",
            message_2 = "Thành công",
            code = "success"
        });
    }
}