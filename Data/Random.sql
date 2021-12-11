﻿DROP DATABASE ProductManager

DROP TABLE ProductsCategories

DROP TABLE Categories 

INSERT INTO Categories ( Name, Description, ImageURL, IDParent)
VALUES
( 'Bottom', 'Lorem ipsum', 'www.placeholder38.com',NULL),
( 'Top', 'Lorem ipsum dolor', 'www.placeholder638.com', NULL),
( 'Accessories', 'hats, sunglasses, belt, jewelry', 'www.placeholder368.com', NULL),
( 'Outwear', 'warm clothing during winter, worn for formal or casual occasions', 'www.placeholder3r4.com', NULL),
( 'Shoes', 'leather shoes for any season and occasion', 'www.placeholder998.com', NULL),
( 'Running', 'Nike Air','http://images/viewu8.png', '5'),
( 'T-shirt','Polo', 'http://images/viewuhu88.png', '2'),
( 'Tights', 'Adidas', 'http://images/viewuuw38.png', '1')

DROP TABLE Products 

INSERT INTO Products (ArticleNumber, Name, Description, ImageURL, Price)
VALUES
('B72354', 'Leather pants', 'Black colour, skinny', 'http://placeholder233.com', 1500),
('N13754', 'Jeans', 'boot-cut', 'http://placeholder2e3.com', 1200),
('B34245', 'Baseball','Blue colour, 100% cotton', 'http://placeholder132.com', 700),
('G34534', 'Suit', 'black clour', 'http://placeholder22.com', 2000)

INSERT INTO Users (Name, Password)
VALUES 
('Tina', 'strategi'),
('Alex', 'skydd')



  INSERT INTO ProductsCategories (IDProduct, IDCategory)
  VALUES
  (1,1),
  (2,1),
  (3,3),
  (4,2)
  