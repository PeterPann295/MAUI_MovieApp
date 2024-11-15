using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.Views;
namespace MovieApp.ViewModels
{   

    public class MoviesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Movie> HotMovies { get; set; }
        public ObservableCollection<Movie> NewMovies { get; set; }

        public ICommand MovieTappedCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MoviesViewModel()
        {
            HotMovies = new ObservableCollection<Movie>();
            NewMovies = new ObservableCollection<Movie>();
            _ = LoadMoviesFromApi();  // Gọi API khi khởi tạo ViewModel
            MovieTappedCommand = new Command<Movie>(OnMovieTapped);
        }


        // Gọi API để lấy danh sách phim
        public async Task LoadMoviesFromApi()
        {
            try
            {

                using (var client = new HttpClient())

                {
                    // Gọi API
                    string apiUrl = "http://10.0.2.2:80/api/v1/movie/list?pageSize=10";

                    client.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc dữ liệu JSON từ API
                        string jsonData = await response.Content.ReadAsStringAsync();

                        // Parse JSON thành danh sách các Movie
                        // Deserialize JSON thành đối tượng MovieApiResponse
                        MovieApiResponse apiResponse = JsonConvert.DeserializeObject<MovieApiResponse>(jsonData);

                        // Bây giờ bạn có thể truy cập danh sách phim
                        List<Movie> movies = apiResponse.Data.Items;
                        int count = 0;
                        // Thêm phim vào danh sách HotMovies
                        foreach (var movie in movies)
                        {
                            if(count<5)
                            {
                                count++;
                                HotMovies.Add(movie);
                            }
                            else
                            {
                                NewMovies.Add(movie);
                            }
                          
                         

                        }
                    }
                    else
                    {
                        // Xử lý khi API không thành công
                        await App.Current.MainPage.DisplayAlert("Error", "Cannot load movies", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khi gọi API
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                Console.WriteLine(ex.ToString());
            }
        }

        // Hàm giúp cập nhật giao diện khi có thay đổi
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private async void OnMovieTapped(Movie selectedMovie)
        {
            if (selectedMovie == null)
                return;

            // Điều hướng sang trang chi tiết phim và truyền đối tượng phim
            await Application.Current.MainPage.Navigation.PushAsync(new MovieDetailsPage(selectedMovie));
        }
    }
}
