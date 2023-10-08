IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreateAt] datetime2 NULL,
        [ModifiedAt] datetime2 NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [Roles] (
        [Id] int NOT NULL IDENTITY,
        [RoleName] nvarchar(max) NOT NULL,
        [CreateAt] datetime2 NULL,
        [DeleteAt] datetime2 NULL,
        [IsDelete] bit NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [Vouchers] (
        [Id] int NOT NULL IDENTITY,
        [DiscountPercent] decimal(18,2) NULL,
        [VoucherCode] nvarchar(450) NOT NULL,
        [ExpirationDate] datetime2 NOT NULL,
        [StartDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Vouchers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [CreatedAt] datetime2 NULL,
        [ModifieldAt] datetime2 NULL,
        [DeletedAt] datetime2 NULL,
        [isDelete] bit NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [SKU] nvarchar(max) NOT NULL,
        [QuantityInStock] int NOT NULL,
        [EditedBy] nvarchar(max) NULL,
        [PercentDiscount] decimal(18,2) NULL,
        [PriceAfterDiscount] decimal(18,2) NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [Email] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PasswordHash] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NULL,
        [LastName] nvarchar(max) NOT NULL,
        [DoB] datetime2 NOT NULL,
        [AvatarUrl] nvarchar(max) NULL,
        [CreatedAt] datetime2 NULL,
        [ModifiedAt] datetime2 NULL,
        [DeletedAt] datetime2 NULL,
        [Gender] nvarchar(max) NULL,
        [RoleId] int NOT NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [ProductFeatures] (
        [Id] int NOT NULL IDENTITY,
        [FeatureName] nvarchar(max) NOT NULL,
        [FeatureValue] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NULL,
        [ModiedAt] datetime2 NULL,
        [Price] decimal(18,2) NULL,
        [ProductId] int NULL,
        CONSTRAINT [PK_ProductFeatures] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductFeatures_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [ProductImages] (
        [Id] int NOT NULL IDENTITY,
        [ImageUrl] nvarchar(max) NULL,
        [CreatedAt] datetime2 NULL,
        [ModifiedAt] datetime2 NULL,
        [ProductId] int NULL,
        [IsMainImage] bit NOT NULL,
        CONSTRAINT [PK_ProductImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [ProductSpecifications] (
        [Id] int NOT NULL IDENTITY,
        [SpecificationName] nvarchar(max) NOT NULL,
        [SpecificationValue] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NULL,
        [ModiedAt] datetime2 NULL,
        [Price] decimal(18,2) NULL,
        [ProductId] int NULL,
        CONSTRAINT [PK_ProductSpecifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductSpecifications_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [BankAccounts] (
        [Id] int NOT NULL IDENTITY,
        [Bank] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [AccountNo] nvarchar(max) NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_BankAccounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BankAccounts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NULL,
        [OrderDate] datetime2 NULL,
        [TotalPrice] decimal(18,2) NULL,
        [ShippingDate] datetime2 NULL,
        [OrderStatus] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [StreetAddress] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [PaymentStatus] nvarchar(max) NULL,
        [PaymentDate] datetime2 NULL,
        [ModifiedAt] datetime2 NULL,
        [VoucherId] int NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]),
        CONSTRAINT [FK_Orders_Vouchers_VoucherId] FOREIGN KEY ([VoucherId]) REFERENCES [Vouchers] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [ProductReviews] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [UserId] int NOT NULL,
        [Rating] int NULL,
        [ReviewText] nvarchar(max) NULL,
        [ReviewDate] datetime2 NULL,
        [DeletedAt] nvarchar(max) NULL,
        [CreateAt] nvarchar(max) NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_ProductReviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductReviews_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductReviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [Wishlists] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [ModifiedAt] datetime2 NULL,
        CONSTRAINT [PK_Wishlists] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Wishlists_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [OrderDetails] (
        [Id] int NOT NULL IDENTITY,
        [OrderId] int NULL,
        [ProductId] int NULL,
        [Quantity] int NULL,
        [Price] decimal(18,2) NULL,
        CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]),
        CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE TABLE [WishlistItems] (
        [Id] int NOT NULL IDENTITY,
        [WishListId] int NOT NULL,
        [ProductId] int NULL,
        CONSTRAINT [PK_WishlistItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WishlistItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]),
        CONSTRAINT [FK_WishlistItems_Wishlists_WishListId] FOREIGN KEY ([WishListId]) REFERENCES [Wishlists] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_BankAccounts_UserId] ON [BankAccounts] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_OrderDetails_OrderId] ON [OrderDetails] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_OrderDetails_ProductId] ON [OrderDetails] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_Orders_VoucherId] ON [Orders] ([VoucherId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_ProductFeatures_ProductId] ON [ProductFeatures] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_ProductReviews_ProductId] ON [ProductReviews] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_ProductReviews_UserId] ON [ProductReviews] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_ProductSpecifications_ProductId] ON [ProductSpecifications] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE UNIQUE INDEX [IX_Vouchers_VoucherCode] ON [Vouchers] ([VoucherCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_WishlistItems_ProductId] ON [WishlistItems] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE INDEX [IX_WishlistItems_WishListId] ON [WishlistItems] ([WishListId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    CREATE UNIQUE INDEX [IX_Wishlists_UserId] ON [Wishlists] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231007160539_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231007160539_init', N'6.0.22');
END;
GO

COMMIT;
GO

