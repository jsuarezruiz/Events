using System;
using Microsoft.Phone.Controls;
using Recetario.Model;
using Recetario.ViewModel;

namespace Recetario.View
{
    public partial class RecipeDetailPage : PhoneApplicationPage
    {
        public RecipeDetailPage()
        {
            InitializeComponent();
        }

        public RecipeDataItem RecipeItem
        {
            set { (DataContext as RecipeDetailViewModel).RecipeItem = value; }
        }

        private void btnStartCooking_Click(object sender, EventArgs e)
        {
            (DataContext as RecipeDetailViewModel).StartCookingCommand.Execute(null);
        }

        private void btnShareShareTask_Click(object sender, EventArgs e)
        {
            (DataContext as RecipeDetailViewModel).ShareCommand.Execute(null);
        }

        private void btnPinToStart_Click(object sender, EventArgs e)
        {
            (DataContext as RecipeDetailViewModel).PinToStartCommand.Execute(null);
        }

        /// <summary>
        ///     Leer la receta. Text to Speech.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recipe_Click(object sender, EventArgs e)
        {
            (DataContext as RecipeDetailViewModel).SpeechRecipeCommand.Execute(null);
        }

        /// <summary>
        ///     Leer los ingredientes. Text to Speech.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ingredients_Click(object sender, EventArgs e)
        {
            (DataContext as RecipeDetailViewModel).SpeechIngredientsCommand.Execute(null);
        }
    }
}