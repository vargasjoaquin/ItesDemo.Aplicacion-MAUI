using CommunityToolkit.Mvvm.ComponentModel;
using ItesDemo.APP.Models;

namespace ItesDemo.APP.ViewModels
{

    public partial class ProductoDetalleViewModel : ObservableObject
    {
        private ProductoModel productoModel;//Variable para almacenar el producto seleccionado que se esta viendo.

        public ProductoModel ProductoModel//Propiedad para acceder al detalle del producto.
        {
            get { return productoModel; }//Devolvemos el producto que nosotros queremos ver.
            set { SetProperty(ref productoModel, value);}//Actualizamos el producto.
        }        
    }
}
