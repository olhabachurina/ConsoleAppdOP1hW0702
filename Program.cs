using ConsoleAppdOP1hW0702.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ConsoleAppdOP1hW0702;

 class Program
{
    static void Main()
    {
        string connectionString = GetConnectionString();

        using (IDbConnection connection = new SqlConnection(connectionString))

        {
            var productRepository = new ProductRepository(connectionString);
            //// Добавляем новый продукт
            //Product newProduct = new Product
            //{
            //    Name = "Laptop",
            //    Price = 12295.99m,
            //    Quantity = 100,
            //    CategoryId = 2
            //};
            //productRepository.AddProduct(newProduct);
            // Получаем список всех продуктов и выводим их
            //IEnumerable<Product> allProducts = productRepository.GetAllProducts();
            //Console.WriteLine("Список всех продуктов:");
            //foreach (var product in allProducts)
            //{
            //    Console.WriteLine($"ID: {product.Id}, Название: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
            //}
            // Добавляем продукт в корзину
            //productRepository.AddToCart(1, 5);
            // Просматриваем содержимое корзины
            //productRepository.ViewCart();
            // Создаем заказ и оформляем его
            //Order newOrder = new Order
            //{
            //    UserId = 123, // Замените на реальный идентификатор пользователя
            //    OrderDate = DateTime.Now,
            //    Items = new List<CartItem>
            //{
            //    new CartItem { ProductId = 1, Quantity = 5 } // Замените на реальный идентификатор продукта и количество
            //}
            //};
            //productRepository.Checkout(newOrder);
            // Получаем список самых продаваемых продуктов и выводим их
            //IEnumerable<Product> bestSellingProducts = productRepository.GetBestSellingProducts();
            //Console.WriteLine("Самые продаваемые продукты:");
            //foreach (var product in bestSellingProducts)
            //{
            // Console.WriteLine($"ID: {product.Id}, Название: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
            //}
            // Получаем общую выручку и выводим ее
            //decimal totalRevenue = productRepository.GetTotalRevenue();
            //Console.WriteLine($"Общая выручка: {totalRevenue}");

            // Добавляем отзыв о продукте
            //    productRepository.AddReview(1, "Отличный продукт, очень доволен!");

            //    //// Получаем отзывы о продукте и выводим их
            //    IEnumerable<string> reviews = productRepository.GetReviews(1);
            //    Console.WriteLine("Отзывы о продукте:");
            //    foreach (var review in reviews)
            //    {
            //        Console.WriteLine(review);
            //    }
            //}

        }
    }
    private static string GetConnectionString()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        return config.GetConnectionString("DefaultConnection");

    }
}

