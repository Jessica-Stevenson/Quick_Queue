namespace Quick_Queue.Views;

public partial class HomePage : Window
{
	public HomePage()
	{
		//This is the CS for the home page
		InitializeComponent();
		Page = new ContentPage()
		{
			Content = new VerticalStackLayout
			{
				Children = {
					new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
					}
				}
			}
		};
	}
}