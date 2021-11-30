﻿CREATE DATABASE ProductManager
GO

USE ProductManager

CREATE TABLE ProductCategories (
ID INT IDENTITY,
Name NVARCHAR (13) NOT NULL,
Description NVARCHAR (100),
ImageURL NVARCHAR (50),
PRIMARY KEY (ID)
)

CREATE TABLE Products (
ID INT IDENTITY,
ArticleNumber NVARCHAR(13) NOT NULL ,
Name NVARCHAR (50) NOT NULL,
Description NVARCHAR(50),
ImageURL NVARCHAR(50),
Price DECIMAL NOT NULL,
IDCategory int FOREIGN KEY REFERENCES ProductCategories(ID)
CONSTRAINT AN_ArticleNumber UNIQUE(ArticleNumber),
PRIMARY KEY (ID),
 )




