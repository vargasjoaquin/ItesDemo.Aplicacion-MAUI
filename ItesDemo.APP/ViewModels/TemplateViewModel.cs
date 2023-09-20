using System.Windows.Input;

namespace ItesDemo.APP.ViewModels;

public class PlantillaViewModel : BaseViewModel
{
    #region VARIABLES
    private string myVar;
    #endregion

    #region CONSTRUCTOR
    public PlantillaViewModel()
    {

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
    private void SimpleMetodo()
    {

    }

    private async Task ProcesoAsync()
    {

    }

    #endregion

    #region COMANDOS
    public ICommand ProcesoAsyncComando => new Command(async () => await ProcesoAsync());
    public ICommand ProcesoSimpleComando => new Command(SimpleMetodo);

    #endregion
}
