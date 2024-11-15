using MovieApp.Models;

namespace MovieApp.Views;

public partial class MovieDetailsPage : ContentPage
{
	public MovieDetailsPage(Movie movie)
	{
		InitializeComponent();
        BindingContext = movie;
    }
}