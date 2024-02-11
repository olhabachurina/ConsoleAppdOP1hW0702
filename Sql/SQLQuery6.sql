CREATE TABLE Reviews (
    Id INT PRIMARY KEY IDENTITY,
    ProductId INT,
    ReviewText NVARCHAR(MAX),
    CONSTRAINT FK_Reviews_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
);