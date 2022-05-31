﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TravellerAppPart1.ViewModel
{
    public class HomeVM
    {

        public NewTravelCommand NewTravelCommand { get; set; }

        public HomeVM()
        {
            NewTravelCommand = new NewTravelCommand(this);
        }
        internal void NewTravelNavigation()
        {
            App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }

    public class NewTravelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HomeVM vM;

        public NewTravelCommand(HomeVM vm)
        {
            vM= vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vM.NewTravelNavigation();
        }
    }
}
