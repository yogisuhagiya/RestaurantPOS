using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using MvvmHelpers;
using RestaurantPosMAUI.MVVM.Models;
using RestaurantPosMAUI.MVVM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace RestaurantPosMAUI.MVVM.ViewModels
{
    //  SignUpViewModel is responsible for handling the sign-up logic and user interactions in the SignUpPage.
    public partial class SignUpViewModel: CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        public SignUpViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            // Constructor
            _firebaseAuthClient = firebaseAuthClient;
        }

        [ObservableProperty]
        private SignUpModel _signUpModel = new SignUpModel();

        [ObservableProperty]
        private string _errorMessage;


        [RelayCommand]
        private async Task SignUp()
        {
            try
            {
                var result = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(_signUpModel.Email, _signUpModel.Username, _signUpModel.Password);

                if (!string.IsNullOrWhiteSpace(result?.User?.Info?.Email))
                {
                    await Shell.Current.GoToAsync($"{nameof(SignInPage)}", true);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }  
        }
        [RelayCommand]
        private async Task NavigateSignIn()
        {
            await Shell.Current.GoToAsync($"{nameof(SignInPage)}");
        }

    }
}
