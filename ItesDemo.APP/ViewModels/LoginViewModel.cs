using ItesDemo.APP.Services;
using ItesDemo.APP.Views;
using System.Windows.Input;

namespace ItesDemo.APP.ViewModels;

public class LoginViewModel : BaseViewModel
{
    #region VARIABLES
    private string usuario;
    private string password;
    #endregion

    #region CONSTRUCTOR
    public LoginViewModel()
    {
#if DEBUG
        Usuario = "luis";
        Password = "1234";
#endif

        // LoginCommand = new Command(async () => await Login());

        LoginCommand = new(async () => await LoginAsync(),
            () => !String.IsNullOrWhiteSpace(Usuario) && !String.IsNullOrWhiteSpace(Password));
    }
    #endregion

    #region OBJETOS
    public string Usuario
    {
        get { return usuario; }
        set { SetProperty(ref usuario, value); }
    }

    public string Password
    {
        get { return password; }
        set { SetProperty(ref password, value); }
    }
    #endregion

    #region METODOS

    private async Task LoginAsync()
    {
        if (!IsBusy)
        {
            IsBusy = true;

            // asignamos objeto con datos del usuario-establecimiento logueados
            if (usuario != string.Empty && password != string.Empty)
            {

                var apiClient = new ApiClient();

                var response = await apiClient.ValidarLogin(usuario, password);

                if (response != null)
                {
                    // guardo en memoria token para usar en consultas protegidas
                    Configuration.AppConfiguration.Token = response.token;

                    Application.Current.MainPage = new NavigationPage(new InicioPage());

                    Usuario = string.Empty;
                    Password = string.Empty;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Las credenciales ingresadas no son válidas", "Aceptar");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Las credenciales son Obligatorias. Verifique!", "Aceptar");
            }

            IsBusy = false;
        }
    }
    #endregion

    #region COMANDOS
    public Command LoginCommand { get; set; }

    #endregion
}


