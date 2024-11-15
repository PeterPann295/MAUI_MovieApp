using MovieApp.ViewModels;

namespace MovieApp.Views;

public partial class MovieMain : ContentPage
{
	public MovieMain()
	{
		InitializeComponent();
        BindingContext = new MoviesViewModel();
    }
}