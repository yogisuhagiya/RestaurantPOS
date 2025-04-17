using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPosMAUI.MVVM.Models
{
    // Represents the user credentials required for signing in
    public class SignInModel
    {
        // User's email address used for login
        public string Email { get; set; }

        // User's password used for login
        public string Password { get; set; }
    }
}
