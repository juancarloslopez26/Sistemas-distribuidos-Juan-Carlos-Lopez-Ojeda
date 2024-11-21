USE DistributedSystems;
    
create table dbo.Books
(
    Id        uniqueidentifier not null
        constraint Books_pk
            primary key,
    Title varchar(255) collate SQL_Latin1_General_CP1_CI_AS,
    Author  varchar(255) collate SQL_Latin1_General_CP1_CI_AS,
    Publisher     varchar(255) collate SQL_Latin1_General_CP1_CI_AS,
    PublishedDate  datetime2
)
    go
INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), 'The Great Gatsby', 'F. Scott Fitzgerald', 'Scribner', '1925-04-10');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), 'To Kill a Mockingbird', 'Harper Lee', 'J.B. Lippincott & Co.', '1960-07-11');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), '1984', 'George Orwell', 'Secker & Warburg', '1949-06-08');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), 'Pride and Prejudice', 'Jane Austen', 'T. Egerton', '1813-01-28');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), 'Moby-Dick', 'Herman Melville', 'Harper & Brothers', '1851-10-18');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), 'The Great Adventure', 'John Doe', 'Adventure Press', '2020-05-15');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), 'To Save a Mockingbird', 'Jane Smith', 'Lippincott & Co.', '2018-03-22');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), '1985', 'George Orwell', 'Penguin Books', '2020-09-12');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), 'Pride and Passion', 'Emily Bronte', 'Egerton Publishing', '2017-11-19');

INSERT INTO dbo.Books (Id, Title, Author, Publisher, PublishedDate)
VALUES (NEWID(), 'Moby-Duck', 'Donovan Hohn', 'Viking', '2011-03-03');
SELECT * from DISTRIBUTEDsystems.dbo.Books;