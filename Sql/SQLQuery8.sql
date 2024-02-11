CREATE TABLE ProductReviews (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT,
    Review NVARCHAR(MAX),
    CONSTRAINT FK_ProductReviews_ProductId FOREIGN KEY (ProductId) REFERENCES Products(Id)
);