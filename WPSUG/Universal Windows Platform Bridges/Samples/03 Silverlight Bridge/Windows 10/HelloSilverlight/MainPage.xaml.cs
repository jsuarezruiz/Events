using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HelloSilverlight.Resources;

namespace HelloSilverlight
{

   public partial class MainPage
      : Windows.UI.Xaml.Controls.Page
   {

      // Constructor
      public MainPage()
      {
         InitializeComponent();
      // Código de ejemplo para traducir ApplicationBar
      //BuildLocalizedApplicationBar();
      }
   // Código de ejemplo para compilar una ApplicationBar traducida
   //private void BuildLocalizedApplicationBar()
   //{
   //    // Establecer ApplicationBar de la página en una nueva instancia de ApplicationBar.
   //    ApplicationBar = new ApplicationBar();
   //    // Crear un nuevo botón y establecer el valor de texto en la cadena traducida de AppResources.
   //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
   //    appBarButton.Text = AppResources.AppBarButtonText;
   //    ApplicationBar.Buttons.Add(appBarButton);
   //    // Crear un nuevo elemento de menú con la cadena traducida de AppResources.
   //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
   //    ApplicationBar.MenuItems.Add(appBarMenuItem);
   //}
   }

}