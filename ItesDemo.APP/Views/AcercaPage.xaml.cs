namespace ItesDemo.APP.Views;

public partial class AcercaPage : ContentPage
{
	public AcercaPage()
	{
		InitializeComponent();
	}

    
    private async void AbrirLinkedln(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://www.linkedin.com/in/joaqu%C3%ADn-vargas-140019236/"));
    }

    private async void AbrirGitHub(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://github.com/vargasjoaquin"));
    }

    


}

