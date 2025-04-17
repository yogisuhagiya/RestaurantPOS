----- Project Name -----
Online Restaurant Management System (RestaurantPOS System)

----- Project Overview -----
This cross-platform mobile app is designed to simplify and digitize the daily operations of a restaurant. It enables staff to manage menus, take and track customer orders, process payments (including online payment integration), and generate bills. The app also supports login for staff and managers, real-time order updates, and SQLite database integration for offline functionality.

---- Objective ----
The main goal of the Restaurant POS System is to simplify restaurant operations, reduce human error, speed up order processing, and improve overall customer satisfaction. By integrating order management, billing, inventory, and customer interactions into one unified system, the app aims to optimize restaurant workflows, reduce wait times, and increase revenue.


----- Tools Used -----
XAML: Used for designing the user interface of the application, providing a rich and interactive experience for users.

.NET MAUI: Used to create the cross-platform mobile app for both Android and iOS.

SQLite: For local offline data storage, allowing the app to function without a constant internet connection.

Firebase: For user authentication and real-time database synchronization.

Microsoft Azure: Used for cloud storage and hosting backend services.

---- Git Usage ---- 
Git was a crucial tool for organizing and managing the development of the Restaurant POS System. It helped keep the project on track and made it easy to collaborate. Here's how I used Git during the project:

1. Branching:
I created separate branches for different features, like feature/sqlite-integration for adding SQLite support. This allowed me to work on new features without affecting the main app. The master branch always had the stable, working version of the app.

2. Merging:
Once I finished a feature or fix, I would merge it back into the master branch after testing. This made sure everything worked properly before it went live. I also used pull requests (PRs) to review and make sure the code was good before merging.

3. Commits:
I made regular commits with short, clear messages like "Add SQLite database connection to Menu Items.""
Each commit captured a specific change, making it easy to track progress and undo changes if something went wrong.

4. Conflict Resolution & Versioning:
Sometimes, changes from different branches conflicted. Git helped me fix these conflicts quickly, and if needed, I could go back to earlier versions of the code to fix issues.

Using Git made the development process much smoother and allowed me to work on new features without breaking anything important. It helped keep the project organized and made it easy to track changes and collaborate.


----- Reflection -----
Using Git while building my restaurant ordering app was a lifesaver! It really helped me keep everything organized and made the work go smoother.

I basically had two versions of my project going: the main one (master) that always worked, and a separate one (sqlite-integration) where I built the new database feature. This way, I could mess around with the database stuff without worrying about breaking the main app.

When I finally got the database part working perfectly, Git made it super easy to add it back into the main version without any drama.

I also got into the habit of saving my progress often with little notes explaining what I changed. This was great because I could always look back and see what I did and why.

Even though I was working alone, I tried to use Git properly – like using those separate versions (branches) and testing things before adding them to the main app. It just kept things cleaner and less confusing. Plus, having my project on GitHub meant it was backed up online and I could work on it from anywhere.

Overall, Git just made me feel more in control, helped me avoid mistakes, and made the whole process less stressful. I learned a ton, and I know this will be really useful for future projects, especially if I'm working with a team.



----- Folder Structure ------

/RestaurantPOS 
│
├── Properties
├── Controls
├── Services
├── Models
├── Pages
├── Platforms
├── Resources
├── ViewModels
│
├── .gitattributes
├── .gitignore
├── App.xaml
├── AppShell.xaml
├── MauiProgram.cs
├── README.md

