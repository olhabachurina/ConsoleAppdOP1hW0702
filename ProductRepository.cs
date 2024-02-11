using ConsoleAppdOP1hW0702.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppdOP1hW0702
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddProduct(Product product)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (Name, Price, Quantity, CategoryId) VALUES (@Name, @Price, @Quantity, @CategoryId)";
                db.Execute(query, product);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity, CategoryId = @CategoryId WHERE Id = @Id";
                db.Execute(query, product);
            }
        }

        public void DeleteProduct(int productId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE Id = @Id";
                db.Execute(query, new { Id = productId });
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products";
                return db.Query<Product>(query);
            }
        }
        public void AddToCart(int productId, int quantity)
        {
            var cartItemToAdd = new CartItem
            {
                ProductId = productId,
                Quantity = quantity
            };

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO CartItems (product_id, quantity) VALUES (@ProductId, @Quantity)";
                db.Execute(query, cartItemToAdd);
            }
        }

        //public void ViewCart()
        //{

        //    int currentUserId = GetCurrentUserId();

        //    using (IDbConnection db = new SqlConnection(connectionString))
        //    {
        //        string query = @"
        //        SELECT c.productid AS ProductId, c.quantity AS Quantity
        //        FROM CartItems c
        //        WHERE c.productid = @ProductId";
        //        var cartItems = db.Query<CartItem>(query, new { ProductId = currentUserId });

        //        Console.WriteLine("Содержимое корзины:");
        //        foreach (var item in cartItems)
        //        {
        //            Product product = GetProductById(item.ProductId);
        //            Console.WriteLine($"Продукт: {product.Name}, Количество: {item.Quantity}");
        //        }
        //    }
        //}

            private int GetCurrentUserId()
        {
          
            return 123; 
        }

        private Product GetProductById(int productId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products WHERE id = @ProductId";
                return db.QueryFirstOrDefault<Product>(query, new { ProductId = productId });
            }
        }

        public void Checkout(Order order)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        // Добавление заказа в таблицу Orders
                        string insertOrderQuery = "INSERT INTO Orders (UserId, OrderDate) VALUES (@UserId, @OrderDate); SELECT SCOPE_IDENTITY()";
                        int orderId = db.QuerySingle<int>(insertOrderQuery, new { UserId = order.UserId, OrderDate = DateTime.Now }, transaction);

                        // Добавление элементов заказа в таблицу OrderItems
                        foreach (var item in order.Items)
                        {
                            string insertOrderItemQuery = "INSERT INTO OrderItems (OrderId, ProductId, Quantity) VALUES (@OrderId, @ProductId, @Quantity)";
                            db.Execute(insertOrderItemQuery, new { OrderId = orderId, ProductId = item.ProductId, Quantity = item.Quantity }, transaction);

                            // Обновление количества продукта на складе
                            string updateQuery = "UPDATE Products SET Quantity = Quantity - @Quantity WHERE Id = @ProductId";
                            db.Execute(updateQuery, new { Quantity = item.Quantity, ProductId = item.ProductId }, transaction);
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
            public IEnumerable<Product> GetBestSellingProducts()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT TOP 10 p.Id, p.Name, MAX(p.Price) AS Price, p.Quantity
        FROM Products p
        JOIN OrderItems oi ON p.Id = oi.ProductId
        JOIN Orders o ON oi.OrderId = o.Id
        WHERE o.OrderDate >= DATEADD(month, -1, GETDATE())
        GROUP BY p.Id, p.Name, p.Quantity
        ORDER BY COUNT(oi.Id) DESC";

                return db.Query<Product>(query);
            }
        
        }

        public decimal GetTotalRevenue()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT SUM(oi.Quantity * p.Price) AS TotalRevenue
            FROM OrderItems oi
            JOIN Products p ON oi.ProductId = p.Id
            JOIN Orders o ON oi.OrderId = o.Id";

                return db.QueryFirstOrDefault<decimal>(query);
            }
        }

        public void AddReview(int productId, string review)
        {
            var reviewToAdd = new Review
            {
                ProductId = productId,
                ReviewText = review,
                ReviewDate = DateTime.Now
            };

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Reviews (Productid, Reviewtext, Reviewdate) VALUES (@ProductId, @ReviewText, @ReviewDate)";
                db.Execute(query, reviewToAdd);
            }
        }

        public IEnumerable<string> GetReviews(int productId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT Review
            FROM ProductReviews
            WHERE ProductId = @ProductId";

                return db.Query<string>(query, new { ProductId = productId });
            }
        }
    }
}

