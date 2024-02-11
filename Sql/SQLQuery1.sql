CREATE TABLE CartItems (
    id INT PRIMARY KEY IDENTITY(1,1),
    product_id INT,
    quantity INT,
    FOREIGN KEY (product_id) REFERENCES Products(id)
);

CREATE TABLE Orders (
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT, 
    order_date DATETIME
);
INSERT INTO CartItems (product_id, quantity) VALUES
(1, 2),
(3, 1);

INSERT INTO Orders (user_id, order_date) VALUES
(1, '2024-02-09 10:00:00'), 
(2, '2024-02-10 12:00:00');