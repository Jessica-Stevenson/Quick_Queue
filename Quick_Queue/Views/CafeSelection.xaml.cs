namespace Quick_Queue.Views;

public partial class CafeSelection : Window
{
	public CafeSelection()
	{
		//Tegan
		//This for cs of CafeSelection
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