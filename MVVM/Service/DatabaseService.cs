using RestaurantPOS.Models;
using SQLite;

namespace RestaurantPOS.Data
{
    // DatabaseService handles interactions with the SQLite database for the Restaurant POS system.

    public class DatabaseService : IAsyncDisposable
    {
        private readonly SQLiteAsyncConnection _connection;

        // Constructor for DatabaseService which initializes the SQLite connection to the local database.

        public DatabaseService()
           
           // set the sqlite connection
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\RestaurantPOS.db3");

            _connection = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }


        // Initializes the database by creating required tables and inserting seed data if the database is empty.

        public async Task InitializeDatabase()

        {

            // Creating tables for different models in the SQLite database
            await _connection.CreateTableAsync<MenuCategory>();
            await _connection.CreateTableAsync<MenuItem>();
            await _connection.CreateTableAsync<MenuItemCategoryMapping>();
            await _connection.CreateTableAsync<Order>();
            await _connection.CreateTableAsync<OrderItem>();

            await SeedDataAsync();
            // Seeds data if the database is empty

        }

        // Seed initial data into the tables if the database is empty.

        private async Task SeedDataAsync()
        {
            var firstCategory = await _connection.Table<MenuCategory>().FirstOrDefaultAsync();

            if (firstCategory != null)
            {
                return; // DB has been seeded
            }


            // Retrieve seed data for categories, menu items, and mappings.

            var categories = SeedData.GetMenuCategories();
            var menuItems = SeedData.GetMenuItems();
            var mappings = SeedData.GetMenuItemCategoryMappings();


            // Insert seed data into respective tables.

            await _connection.InsertAllAsync(categories);
            await _connection.InsertAllAsync(menuItems);
            await _connection.InsertAllAsync(mappings);
        }

        public async ValueTask DisposeAsync()
            
        {
            if (_connection != null)
            {
                await _connection.CloseAsync();
            }
        }


        // Retrieves all menu categories from the database asynchronously.


        public async Task<MenuCategory[]> GetMenuCategoriesAsync() => await _connection.Table<MenuCategory>().ToArrayAsync();

        public async Task<MenuItem[]> GetMenuItemsByCategoryIdAsync(int categoryId)
        {
            // get the menu items by category 
            var query = @"
                            SELECT mi.*
                            FROM MenuItem AS mi
                                INNER JOIN MenuItemCategoryMapping AS mcm
                                    ON mi.Id = mcm.MenuItemId
                            WHERE mcm.CategoryId = ?
                        ";
            var menuItems = await _connection.QueryAsync<MenuItem>(query, categoryId);

            return [.. menuItems];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>String with error message if any. Null if no error.</returns>
        public async Task<string?> PlaceOrderAsync(OrderModel model)
        {
            var newOrder = new Order
            {
                OrderDate = model.OrderDate,
                PaymentMode = model.PaymentMode,
                TotalAmountPaid = model.TotalAmountPaid,
                TotalItemsCount = model.TotalItemsCount
            };

            if (await _connection.InsertAsync(newOrder) > 0)
            {
                foreach (var item in model.Items)
                {
                    item.OrderId = newOrder.Id;
                }
                if (await _connection.InsertAllAsync(model.Items) == 0)
                {
                    await _connection.DeleteAsync(newOrder);
                    return "Error inserting order items";
                }
            }
            else
            {
                return "Error inserting order";
                // Return error if the order itself couldn't be inserted.

            }
            model.Id = newOrder.Id;

            // Assign the newly created order ID to the model.

            return null;
        }

        // Retrieves order items associated with a specific order ID.

        public async Task<Order[]> GetOrdersAsync() => await _connection.Table<Order>().OrderByDescending(o => o.OrderDate).ToArrayAsync();

        public async Task<OrderItem[]> GetOrderItemsByOrderIdAsync(int orderId) => await _connection
            .Table<OrderItem>()
            .Where(oi => oi.OrderId == orderId)
            .ToArrayAsync();

        public async Task<MenuCategory[]> GetCategoriesByMenuItemIdAsync(int menuItemId)
        {
            //  join the menu itmes category mapping
            var query = @"
                        SELECT cat.* 
                        FROM MenuCategory cat
                        INNER JOIN MenuItemCategoryMapping mcm
                        ON cat.Id = mcm.CategoryId
                        WHERE mcm.MenuItemId = ?
                    ";
            var categories = await _connection.QueryAsync<MenuCategory>(query, menuItemId);
            return [.. categories];

            // Return categories associated with the specified menu item.

        }

        // Saves a menu item into the database (either inserting a new item or updating an existing one).

        public async Task<string?> SaveMenuItemAsync(MenuItemModel model)
        {
            if (model.Id == 0)
            {
                // Inserting a new menu item if the model has no ID.

                MenuItem menuItem = new()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Icon = model.Icon,
                    Description = model.Description,
                    Price = model.Price
                };

                if (await _connection.InsertAsync(menuItem) > 0)
                {
                    var categoryMapping = model.SelectedCategories
                                                .Select(c => new MenuItemCategoryMapping
                                                {
                                                    Id = c.Id,
                                                    CategoryId = c.Id,
                                                    MenuItemId = menuItem.Id
                                                });
                    if (await _connection.InsertAllAsync(categoryMapping) > 0)
                    {
                        model.Id = menuItem.Id;
                        return null;
                        // Return null if the menu item was saved successfully.

                    }
                    else
                    {
                        await _connection.DeleteAsync(menuItem);

                        // Rollback insertion if category mapping failed.

                    }
                }
                return "Error saving menu item";
            }
            else
            {
                string? errorMessage = null;

                await _connection.RunInTransactionAsync(db =>
                {
                    var menuItem = db.Find<MenuItem>(model.Id);

                    menuItem.Name = model.Name;
                    menuItem.Icon = model.Icon;
                    menuItem.Description = model.Description;
                    menuItem.Price = model.Price;

                    if (db.Update(menuItem) == 0)
                    {
                        errorMessage = "Error updating menu item";
                        throw new Exception();
                        // Throw exception if update fails.

                    }

                    var deleteQuery = @"
                        DELETE FROM MenuItemCategoryMapping 
                        WHERE MenuItemId = ?";
                    db.Execute(deleteQuery, menuItem.Id);

                    var categoryMapping = model.SelectedCategories
                            .Select(c => new MenuItemCategoryMapping
                            {
                                Id = c.Id,
                                CategoryId = c.Id,
                                MenuItemId = menuItem.Id
                            });
                    if (db.InsertAll(categoryMapping) == 0)
                    {
                        errorMessage = "Error updating menu item categories";
                        throw new Exception();
                        // Throw exception if category mapping update fails.

                    }
                });

                return errorMessage;
            }
        }

        // Placeholder method for getting a database connection asynchronously (currently not implemented).

        internal async Task GetConnectionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
