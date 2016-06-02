# BookService
.NET Core RC2 MVC Web 2 Api with EF Core to SQL Server

This is a demonstrator Book Service built using .NET Core RC2.

Featuring:

1. MVC Routes
2. Web 2 Api CRUD
3. Identity Authentication for POST/PUT
4. Entity Framework Core to SQL Server
5. Angular Views embedded in Razor Partials.

Angular Detail:

1. Uses ngResource to load data from .NET Web API
2. Uses ui.router via $stateProvider & $stateParams to load a partial view into an MVC Route
3. Entry to demo form is at ```/Views/Api/index.cshtml``` inside this block ```<div data-ui-view></div>```
4. The ```ui-view``` directive tells $state where to place the templates located in the ```/Views/Api/*.cshtml``` folder.
5. The ```ui-sref``` binds the contained link to the state, e.g. ```ui-sref="viewBook({id:book.Id})"```.

Schema Reference:

 ```sql
CREATE TABLE [dbo].[Authors] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [BirthPlace] NVARCHAR (MAX) NULL,
    [Name]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Books] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [AuthorId] INT             NOT NULL,
    [Genre]    NVARCHAR (MAX)  NULL,
    [Price]    DECIMAL (18, 2) NOT NULL,
    [Title]    NVARCHAR (MAX)  NOT NULL,
    [Year]     INT             NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([Id]) ON DELETE CASCADE
);
 ```
