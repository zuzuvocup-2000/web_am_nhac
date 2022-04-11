using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using webdemo.Models;

namespace webdemo.Controllers;

public class AuthController : Controller
{
    [HttpPost]
    public JsonResult Register(string fullname, string email, string password){
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
            command.CommandText = "select count(id) from user where (email='"+email+"')";
            Int64 check_email = (Int64) command.ExecuteScalar();
            if(check_email != 0){
                return Json(new {
                    message_1 = "Email đã tồn tại",
                    message_2 = "Xin vui lòng thử lại",
                    code = "error"
                });
            }

            command.CommandText = "INSERT INTO `music_data`.`user` (`name`, `email`, `password`) VALUES ('"+fullname+"', '"+email+"', '"+password+"');";
            var flag = command.ExecuteNonQuery();
            connection.Close();

            if(flag > 0){
                return Json(new {
                    message_1 = "Đăng ký tài khoản thành công",
                    message_2 = "Xin vui lòng tiến hành đăng nhập",
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
    }

    [HttpPost]
    public JsonResult Login( string email, string password){
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
            command.CommandText = "select count(id) from user where (email='"+email+"' and password ='"+password+"')";
            Int64 check_email = (Int64) command.ExecuteScalar();
            if(check_email != 0){
                command.CommandText = "select id, name, email from user where (email='"+email+"' and password ='"+password+"')";
                var data  = command.ExecuteReader();
                Dictionary<string, string> print = new Dictionary<string, string>();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        var id = data.GetString(0);
                        var fullname = data.GetString(1);
                        var email_check = data.GetString(2);
                        print = new Dictionary<string, string> {
                            { "id", id },
                            { "fullname", fullname },
                            { "email", email_check }
                        };
                    }
                }
                
                return Json(new {
                    message_1 = "Đăng nhập thành công",
                    message_2 = "Thành công",
                    code = "success",
                    data = print
                });
            }
            connection.Close();
            return Json(new {
                message_1 = "Tài khoản hoặc mật khẩu không đúng!",
                message_2 = "Xin vui lòng thử lại",
                code = "error"
            });
        }
    }

    [HttpPost]
    public JsonResult Update(string id, string email, string fullname, string old){
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
            
            command.CommandText = "select id, name, email from user where (id='"+id+"' and password ='"+old+"')";
            var data  = command.ExecuteReader();
            Dictionary<string, string> print = new Dictionary<string, string>();
            if (data.HasRows)
            {
                connection.Close();
                connection.Open();
                command.CommandText = "UPDATE user SET name='"+fullname+"', email='"+email+"' WHERE id='"+id+"';";
                var flag = command.ExecuteNonQuery();
                return Json(new {
                    message_1 = "Cập nhật tài khoản thành công",
                    message_2 = "Thành công",
                    code = "success"
                });
            }else{
                connection.Close();
                return Json(new {
                    message_1 = "Mật khẩu không đúng!",
                    message_2 = "Xin vui lòng thử lại",
                    code = "error"
                });
            }
        }
    }
    
    [HttpPost]
    public JsonResult ChangePassword(string id, string old, string email, string password){
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
            command.CommandText = "select id, name, email from user where (id='"+id+"' and password ='"+old+"')";
            var data  = command.ExecuteReader();
            Dictionary<string, string> print = new Dictionary<string, string>();
            if (data.HasRows)
            {
                connection.Close();
                connection.Open();
                command.CommandText = "UPDATE user SET password='"+password+"' WHERE id='"+id+"';";
                var flag = command.ExecuteNonQuery();
                connection.Close();

                return Json(new {
                    message_1 = "Đổi mật khẩu thành công",
                    message_2 = "Thành công",
                    code = "success"
                });
            }else{
            connection.Close();

                return Json(new {
                    message_1 = "Mật khẩu không đúng!",
                    message_2 = "Xin vui lòng thử lại",
                    code = "error"
                });
            }
        }
    }
}
