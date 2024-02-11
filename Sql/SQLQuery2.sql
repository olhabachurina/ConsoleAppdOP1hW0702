CREATE TABLE Reviews (
    id INT PRIMARY KEY IDENTITY(1,1),
    product_id INT,
    review_text TEXT,
    review_date DATETIME,
    FOREIGN KEY (product_id) REFERENCES Products(id)
);