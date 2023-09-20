using CommunityToolkit.Mvvm.ComponentModel;
using ItesDemo.APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItesDemo.APP.ViewModels
{

    public partial class ProductoDetalleViewModel : ObservableObject
    {
        private ProductoModel productoModel;

        public ProductoModel ProductoModel
        {
            get { return productoModel; }
            set { SetProperty(ref productoModel, value); }
        }
    }
    }
