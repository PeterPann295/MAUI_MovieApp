using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CallApi();
        }

        private static async Task CallApi()
        {
            using (HttpClient client = new HttpClient())
            {
                // Đặt thời gian chờ cho HttpClient
                client.Timeout = TimeSpan.FromSeconds(30);

                // Địa chỉ API của bạn
                string apiUrl = "http://192.168.1.5:80/api/v1/movie/list";

                try
                {
                    // Gọi API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Kiểm tra xem yêu cầu có thành công không
                    response.EnsureSuccessStatusCode();

                    // Đọc nội dung phản hồi
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Xử lý phản hồi (Ví dụ: in ra console)
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    // Xử lý lỗi nếu có
                    Console.WriteLine($"Lỗi khi gọi API: {e.Message}");
                }
            }
        }
    }
}
