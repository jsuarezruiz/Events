using System.Collections.ObjectModel;
using Montaditos.Models;
using Montaditos.ViewModels.Base;

namespace Montaditos.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Montadito> Montaditos { get; set; }

        public MainViewModel()
        {
            Montaditos = new ObservableCollection<Montadito>
            {
                new Montadito
                {
                    Name = "Jamón Ibérico",
                    Image =
                        "../Assets/Jamon.jpg"
                },
                new Montadito
                {
                    Name = "Pate con salsa de mostaza",
                    Image =
                        "../Assets/PateSalsaMostaza.jpg"
                },
                new Montadito
                {
                    Name = "Queso Brie con pimiento",
                    Image =
                        "../Assets/QuesoBriePimiento.jpg"
                },
                new Montadito
                {
                    Name = "Queso de cabra",
                    Image =
                        "../Assets/QuesoCabra.jpg"
                },
                new Montadito
                {
                    Name = "Queso con pimienta",
                    Image =
                        "../Assets/QuesoIbericoPimienta.jpg"
                }
            };
        }
    }
}
