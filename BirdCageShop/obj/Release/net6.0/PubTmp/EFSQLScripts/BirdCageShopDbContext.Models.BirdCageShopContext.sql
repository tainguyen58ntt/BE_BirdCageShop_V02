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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        [DoB] datetime2 NULL,
        [AvatarUrl] nvarchar(max) NULL,
        [CreatedAt] datetime2 NULL,
        [ModifiedAt] datetime2 NULL,
        [DeletedAt] datetime2 NULL,
        [Gender] nvarchar(max) NULL,
        [IsDelete] bit NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [BirdCageTypes] (
        [Id] int NOT NULL IDENTITY,
        [TypeName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreateAt] datetime2 NULL,
        [ModifiedAt] datetime2 NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_BirdCageTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [Feature] (
        [Id] int NOT NULL IDENTITY,
        [FeatureName] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NULL,
        [ModiedAt] datetime2 NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_Feature] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [Specification] (
        [Id] int NOT NULL IDENTITY,
        [SpecificationName] nvarchar(max) NOT NULL,
        [SpecificationValue] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NULL,
        [ModiedAt] datetime2 NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_Specification] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [Statuses] (
        [Id] int NOT NULL IDENTITY,
        [StatusState] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Statuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [Order] (
        [Id] int NOT NULL IDENTITY,
        [ApplicationUserId] nvarchar(450) NOT NULL,
        [OrderDate] datetime2 NULL,
        [TotalPrice] decimal(18,2) NULL,
        [TotalPriceBeforeVoucher] decimal(18,2) NULL,
        [ShipCost] decimal(18,2) NULL,
        [ShippingDate] datetime2 NULL,
        [OrderStatus] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [NameRecieved] nvarchar(max) NULL,
        [StreetAddress] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [PaymentStatus] nvarchar(max) NULL,
        [SessionId] nvarchar(max) NULL,
        [PaymentIntentId] nvarchar(max) NULL,
        [PaymentDate] datetime2 NULL,
        [ModifiedAt] datetime2 NULL,
        [VoucherCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Order_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [Vouchers] (
        [Id] int NOT NULL IDENTITY,
        [ApplicationUserId] nvarchar(450) NOT NULL,
        [DiscountPercent] decimal(18,2) NULL,
        [VoucherCode] nvarchar(450) NOT NULL,
        [ExpirationDate] datetime2 NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [IsDelete] bit NOT NULL,
        [IsValid] bit NOT NULL,
        CONSTRAINT [PK_Vouchers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Vouchers_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [Formulas] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(max) NOT NULL,
        [MinWidth] int NOT NULL,
        [MaxWidth] int NOT NULL,
        [MinHeight] int NOT NULL,
        [MaxHeight] int NOT NULL,
        [Material] nvarchar(max) NOT NULL,
        [MinBars] int NOT NULL,
        [MaxBars] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [BirdCageTypeId] int NULL,
        [ConstructionTime] int NULL,
        CONSTRAINT [PK_Formulas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Formulas_BirdCageTypes_BirdCageTypeId] FOREIGN KEY ([BirdCageTypeId]) REFERENCES [BirdCageTypes] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [BirdCageTypeId] int NULL,
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
        CONSTRAINT [FK_Products_BirdCageTypes_BirdCageTypeId] FOREIGN KEY ([BirdCageTypeId]) REFERENCES [BirdCageTypes] ([Id]),
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [OrderDetail] (
        [Id] int NOT NULL IDENTITY,
        [OrderId] int NULL,
        [ProductId] int NULL,
        [Quantity] int NULL,
        [Price] decimal(18,2) NULL,
        [Model] nvarchar(max) NULL,
        [Width] int NULL,
        [Height] int NULL,
        [Material] nvarchar(max) NULL,
        [Bars] int NULL,
        CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderDetail_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([Id]),
        CONSTRAINT [FK_OrderDetail_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [ProductFeature] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [FeatureId] int NOT NULL,
        CONSTRAINT [PK_ProductFeature] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductFeature_Feature_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [Feature] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductFeature_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [ProductReviews] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [ApplicationUserId] nvarchar(450) NOT NULL,
        [Rating] int NULL,
        [ReviewText] nvarchar(max) NULL,
        [ReviewDate] datetime2 NULL,
        [DeletedAt] nvarchar(max) NULL,
        [CreateAt] nvarchar(max) NULL,
        [IsDelete] bit NOT NULL,
        CONSTRAINT [PK_ProductReviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductReviews_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductReviews_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [ProductSpecifications] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [SpecificationId] int NOT NULL,
        CONSTRAINT [PK_ProductSpecifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductSpecifications_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductSpecifications_Specification_SpecificationId] FOREIGN KEY ([SpecificationId]) REFERENCES [Specification] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [ShoppingCarts] (
        [Id] int NOT NULL IDENTITY,
        [Count] int NOT NULL,
        [CreatedAt] datetime2 NULL,
        [ModifiedAt] datetime2 NULL,
        [ProductId] int NOT NULL,
        [ApplicationUserId] nvarchar(450) NOT NULL,
        [PriceDesign] decimal(18,2) NULL,
        [Model] nvarchar(max) NULL,
        [Width] int NULL,
        [Height] int NULL,
        [Material] nvarchar(max) NULL,
        [Bars] int NULL,
        CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ShoppingCarts_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ShoppingCarts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE TABLE [Wishlists] (
        [Id] int NOT NULL IDENTITY,
        [ApplicationUserId] nvarchar(450) NOT NULL,
        [ProductId] int NOT NULL,
        CONSTRAINT [PK_Wishlists] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Wishlists_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Wishlists_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''116ef264-5733-45c6-a1a0-0226b7d1b613'', N''2'', N''Manager'', N''Manager''),
    (N''27b0b46c-c395-479d-acf2-1745c69d8687'', N''2'', N''Customer'', N''Customer''),
    (N''57febbcf-47cb-44c9-8395-cde1afc1fb3b'', N''3'', N''Staff'', N''Staff''),
    (N''86c4d3d4-8d79-4cf9-a2d5-83cc64b6d125'', N''1'', N''Admin'', N''Admin'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'StatusState') AND [object_id] = OBJECT_ID(N'[Statuses]'))
        SET IDENTITY_INSERT [Statuses] ON;
    EXEC(N'INSERT INTO [Statuses] ([Id], [StatusState])
    VALUES (1, N''Pending''),
    (2, N''Approved''),
    (3, N''Canceled''),
    (4, N''Shipped''),
    (5, N''Refund''),
    (6, N''COD''),
    (7, N''PAYONLINE'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'StatusState') AND [object_id] = OBJECT_ID(N'[Statuses]'))
        SET IDENTITY_INSERT [Statuses] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_Formulas_BirdCageTypeId] ON [Formulas] ([BirdCageTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_Order_ApplicationUserId] ON [Order] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_OrderDetail_OrderId] ON [OrderDetail] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_OrderDetail_ProductId] ON [OrderDetail] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ProductFeature_FeatureId] ON [ProductFeature] ([FeatureId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ProductFeature_ProductId] ON [ProductFeature] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ProductReviews_ApplicationUserId] ON [ProductReviews] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ProductReviews_ProductId] ON [ProductReviews] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_Products_BirdCageTypeId] ON [Products] ([BirdCageTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ProductSpecifications_ProductId] ON [ProductSpecifications] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ProductSpecifications_SpecificationId] ON [ProductSpecifications] ([SpecificationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ShoppingCarts_ApplicationUserId] ON [ShoppingCarts] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_ShoppingCarts_ProductId] ON [ShoppingCarts] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_Vouchers_ApplicationUserId] ON [Vouchers] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE UNIQUE INDEX [IX_Vouchers_VoucherCode] ON [Vouchers] ([VoucherCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_Wishlists_ApplicationUserId] ON [Wishlists] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    CREATE INDEX [IX_Wishlists_ProductId] ON [Wishlists] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231103041721_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231103041721_init', N'6.0.23');
END;
GO

COMMIT;
GO

