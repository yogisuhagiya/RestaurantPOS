using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPosMAUI.MVVM.Models
{

    // Represents the user information required for account registration

    public class SignUpModel
    {
        // User's email address for account creation
        public string Email { get; set; }

        // Desired username for the new account
        public string Username { get; set; }

        // Password for securing the account
        public string Password { get; set; }
        
    }
}
