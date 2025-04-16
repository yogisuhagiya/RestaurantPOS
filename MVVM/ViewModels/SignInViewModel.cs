using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using MvvmHelpers;
using RestaurantPOS.Pages;
using RestaurantPosMAUI.MVVM.Models;
using RestaurantPosMAUI.MVVM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantPosMAUI.MVVM.ViewModels
{
    //SignInViewModel is responsible for handling the sign-in logic and user interactions in the SignInPage.
    public partial class SignInViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

        [ObservableProperty]
        private SignInModel _signInModel = new SignInModel();


        [ObservableProperty]
        private string _errorMessage;

        public SignInViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }

        [RelayCommand]
        private async Task SignIn()
        {
            try
            {
                var result = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(_signInModel.Email, _signInModel.Password);
                if (!string.IsNullOrWhiteSpace(result?.User?.Info?.Email))
                {
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}", true);
                }
            }
            catch (Exception ex)
            {
                // Handle sign-in error
                ErrorMessage = ex.Message;
            }
        }
        [RelayCommand]
        private async Task NavigateSignUp()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }

    }
}
