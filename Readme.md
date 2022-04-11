var stringBuilder = new MySqlConnectionStringBuilder();
    stringBuilder["Server"] = "127.0.0.1";
    stringBuilder["Database"] = "music_data";
    stringBuilder["User Id"] = "root";
    stringBuilder["Password"] = "root";
    stringBuilder["Port"] = "3307";
String sqlConnectionString = stringBuilder.ToString();

var connection = new MySqlConnection(sqlConnectionString);

// mở kết nối
connection.Open();

using (DbCommand command = connection.CreateCommand())
{
    // Câu truy vấn SQL
    command.CommandText = "select * from user Limit 5";
    var reader = command.ExecuteReader();
    // Đọc kết quả truy vấn
    Console.WriteLine("\r\nCÁC SẢN PHẨM:");
    Console.WriteLine($"{"id ",10} {"name "}");
    while (reader.Read())
    {
    Console.WriteLine($"{reader["id"],10} {reader["name"]}");
    }
}

<!-- Viet Cookie -->

contex.Response.Cookies.Append("ten","value");