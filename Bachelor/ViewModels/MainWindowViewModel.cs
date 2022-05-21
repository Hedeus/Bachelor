using Bachelor.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace Bachelor.ViewModels
{
    [MarkupExtensionReturnType(typeof(MainWindowViewModel))]
    internal class MainWindowViewModel : ViewModel
    {
        /*---------------------------------------------Properies------------------------------------------------*/
        #region Свойства

        #region Title - Заголовок вікна
        private string _Title = "Бакалаврська робота";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        } 
        #endregion


        #endregion

    }
}
