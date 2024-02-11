CREATE TABLE OrderItems (
    id INT IDENTITY (1, 1) NOT NULL,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    PRIMARY KEY CLUSTERED (id ASC),
    FOREIGN KEY (OrderId) REFERENCES Orders(id),
    FOREIGN KEY (ProductId) REFERENCES Products(id)
);