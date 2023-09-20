using ItesDemo.APP.Views;
using System.Windows.Input;

namespace ItesDemo.APP.ViewModels;

public class InicioViewModel : BaseViewModel
{
    #region VARIABLES
    private string myVar;
    #endregion

    #region CONSTRUCTOR
    public InicioViewModel()
    {
        Title = "Principal";

        GoToListaProductoComando = new Command(() => GoToListaProducto());
        GoToAcercaComando = new Command(() => GoToAcerca());
        GoToExitCommand = new Command(() => Application.Current.MainPage = new LoginPage());
    }
    #endregion

    #region OBJETOS
    public string MyProperty
    {
        get { return myVar; }
        set { SetProperty(ref myVar, value); }
    }
    #endregion

    #region METODOS
    // private void SimpleMetodo() { }
    // private async Task ProcesoAsync() { }

    private async Task GoToListaProducto()
    {
        if (!IsBusy)
        {
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoListaPage());
            IsBusy = false;
        }
    }

    private async Task GoToAcerca()
    {
        if (!IsBusy)
        {
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PushAsync(new AcercaPage());
            IsBusy = false;
        }
    }
    #endregion

    #region COMANDOS
    // public ICommand ProcesoAsyncComando => new Command(async () => await ProcesoAsync());

    // public ICommand ProcesoSimpleComando => new Command(SimpleMetodo);
    public ICommand GoToListaProductoComando { get; set; }
    public ICommand GoToAcercaComando { get; set; }
    public ICommand GoToExitCommand { get; set; }

    #endregion
}
