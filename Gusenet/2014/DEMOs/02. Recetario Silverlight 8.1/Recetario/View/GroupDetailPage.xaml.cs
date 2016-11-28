using Microsoft.Phone.Controls;
using Recetario.Model;
using Recetario.ViewModel;

namespace Recetario.View
{
    public partial class GroupDetailPage : PhoneApplicationPage
    {
        public GroupDetailPage()
        {
            InitializeComponent();
        }

        public RecipeDataGroup RecipeGroup
        {
            set { (DataContext as GroupDetailViewModel).RecipeGroup = value; }
        }
    }
}