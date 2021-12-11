﻿DROP DATABASE ProductManager

DROP TABLE Products

DROP TABLE Categories

INSERT INTO Categories ( Name, Description, ImageURL)
VALUES
( 'Bottom', 'Lorem ipsum', 'http://images/view2.png'),
( 'Top', 'Lorem ipsum dolor', 'http://images/viewu8.png'),
( 'Accessories', 'hats, sunglasses, belt, jewelry', 'http://images/view9.png'),
( 'Outwear', 'warm clothing during winter, worn for formal or casual occasions', 'http://images/view67.png'),
( 'Shoes', 'leather shoes for any season and occasion', 'http://images/vie678tw.png')



INSERT INTO Products (ArticleNumber, Name, Description, ImageURL, Price)
VALUES
('B72354' ,'Leather pants' , 'Black colour, skinny', 'http://placeholder233.com', 1500),
('N13754' , 'Jeans', 'boot-cut', 'http://placeholder2e3.com', 1200),
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
  