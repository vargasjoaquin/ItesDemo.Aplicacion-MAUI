using ItesDemo.APP.Models;
using ItesDemo.APP.Services;
using ItesDemo.APP.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ItesDemo.APP.ViewModels
{
    public class ProductoListaViewModel : BaseViewModel
    {
        #region VARIABLES
        private bool isRefreshing;
        private ProductoModel SeleccionProducto;
        private ObservableCollection<ProductoModel> productos = new ObservableCollection<ProductoModel>();
        private ObservableCollection<ProductoModel> listaProductos = new ObservableCollection<ProductoModel>();

        private string searchText = "";//Variable para buscar los producto mediante su nombre/texto.
        private string selectedTipo = "--Seleccione--";// "      "      "    "     "       "       "    tipo de producto especifico.

        #endregion

        #region CONSTRUCTOR
        public ProductoListaViewModel()
        {
            Title = "Lista Productos";

            Task.Run(async () => { await ConsultarApi(); }).Wait();

            RefreshCommand = new Command(async () =>
            {
                if (IsBusy)
                {
                    return;
                }

                await ConsultarApi();
            });
        }
        #endregion

        #region PROPIEDADES
        public ObservableCollection<ProductoModel> Productos
        {
            get { return productos; }//Devuelve la colección actual de productos.
            set { SetProperty(ref productos, value);}//Actualizamos la colección de productos.
        }

        public ProductoModel SeleccionDeProducto
        {
            get { return SeleccionProducto; }//Devuelve el producto seleccionado.
            set
            {
                if (SetProperty(ref SeleccionProducto, value))//Actualizamos la colección de productos.
                {
                    GoToDetail();//Llamamos al metodo de detalles.
                }
            }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public string SearchText
        {
            get { return searchText; }  // Devuelve el valor actual de la propiedad SearchText.
            
            set
            {
                if (searchText != value)
                {
                    searchText = value;// Asignamos el nuevo valor a la propiedad.
                    OnPropertyChanged(nameof(SearchText));// Lanzamos un evento para notificar que la propiedad ha cambiado.
                    FilterProducts();//Llamamos al método FilterProducts() para filtrar los productos basados en el nuevo valor de SearchText.
                }
            }
        }

        public string SelectedTipo //Misma metodologia del metodo SearchText pero en este caso es para filtra el tipo de producto.
        {
            get { return selectedTipo; }//Devuelve el valor actual de la propiedad SelectedTipo.
            set
            {
                if (SetProperty(ref selectedTipo, value))//Establecemos el valor de SelectedTipo y verificamos si ha cambiado.
                {
                    FilterProducts();//Llamos al método FilterProducts para aplicar el filtro según el tipo de producto seleccionado.
                }
            }
        }

        #endregion

        #region COMANDOS
        public ICommand GoToCancelarCommand => new Command(() => GoToCancelar());

        public ICommand RefreshCommand { get; set; }
        #endregion

        #region METOODOS

        private async Task ConsultarApi()
        {
            IsBusy = IsRefreshing = true;

            Productos.Clear();

            var apiClient = new ApiClient();

            var lista = await apiClient.ObtenerProductos();

            listaProductos = new ObservableCollection<ProductoModel>(lista);

            FilterProducts();

            IsBusy = IsRefreshing = false;
        }

        private void GoToCancelar()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void GoToDetail()
        {
            if (SeleccionDeProducto != null)
            {
                var ProductoDetalleViewModel = new ProductoDetalleViewModel();// Creamos una nueva instancia del ProductoDetalleViewModel para la página de detalles del producto.
                ProductoDetalleViewModel.ProductoModel = SeleccionDeProducto; // Asignamos el producto seleccionado al ProductoDetalleViewModel.
                var ProductoDetallePage = new ProductoDetallePage { BindingContext = ProductoDetalleViewModel }; // Creamos una nueva instancia de la página de detalles del producto.
                await Application.Current.MainPage.Navigation.PushAsync(ProductoDetallePage);//Nos dijimos a la página de detalles de producto de forma asíncrona.
            }
        }

        private void FilterProducts()
        {
            var filteredProducts = listaProductos.ToList();//Copiamos la lista original de productos en una nueva lista llamada 'filteredProducts'

            if (!string.IsNullOrWhiteSpace(SearchText))//Verificamos si se ha ingresado texto de búsqueda de productos
            {
                
                filteredProducts = filteredProducts // Filtramos la para encontrar productos cuyos nombres contengan un texto 
                    .Where(p => p.nombre.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (SelectedTipo != "--Seleccione--") //Filtramos si no se selecciona "--Seleccione--"
            {
                filteredProducts = filteredProducts
                    .Where(p => p.tipo.Equals(SelectedTipo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            Productos = new ObservableCollection<ProductoModel>(filteredProducts);// Tuki, actualizamos la colección de productos con los resultados filtrados
        }
        #endregion
    }
}
