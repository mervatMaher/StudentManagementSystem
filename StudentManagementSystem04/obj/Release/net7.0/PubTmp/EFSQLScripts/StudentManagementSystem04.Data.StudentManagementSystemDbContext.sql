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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [ProfileImageUrl] nvarchar(max) NULL,
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240325203737_Init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240325203737_Init', N'7.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Email');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Email];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Levels] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Levels] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Notifications] (
        [Id] int NOT NULL IDENTITY,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [ApplicationUserLevel] (
        [ApplicationUsersId] nvarchar(450) NOT NULL,
        [LevelsId] int NOT NULL,
        CONSTRAINT [PK_ApplicationUserLevel] PRIMARY KEY ([ApplicationUsersId], [LevelsId]),
        CONSTRAINT [FK_ApplicationUserLevel_AspNetUsers_ApplicationUsersId] FOREIGN KEY ([ApplicationUsersId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ApplicationUserLevel_Levels_LevelsId] FOREIGN KEY ([LevelsId]) REFERENCES [Levels] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Subjects] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [LevelId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Subjects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Subjects_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Subjects_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Lectures] (
        [Id] int NOT NULL IDENTITY,
        [DateTime] datetime2 NOT NULL,
        [StartTime] time NULL,
        [EndTime] time NULL,
        [AttendanceNumber] int NULL,
        [AttendanceState] bit NOT NULL,
        [SubjectId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Lectures] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Lectures_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Lectures_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Tasks] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [ColorCode] nvarchar(max) NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [CategoryId] int NOT NULL,
        [SubjectId] int NOT NULL,
        [LevelId] int NULL,
        CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tasks_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Tasks_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Tasks_Levels_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Levels] ([Id]),
        CONSTRAINT [FK_Tasks_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [UniProjects] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Link] nvarchar(max) NOT NULL,
        [MainImage] nvarchar(max) NOT NULL,
        [AppName] nvarchar(max) NOT NULL,
        [Rate] int NULL,
        [Feedback] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [SubjectId] int NOT NULL,
        [TaskId] int NOT NULL,
        CONSTRAINT [PK_UniProjects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UniProjects_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UniProjects_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UniProjects_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UniProjects_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Students] (
        [Id] int NOT NULL IDENTITY,
        [BornDate] datetime2 NOT NULL,
        [Gender] int NOT NULL,
        [Career] nvarchar(max) NOT NULL,
        [StudentAttendaceState] bit NOT NULL,
        [CoverPhoto] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        [levelId] int NOT NULL,
        [TaskId] int NULL,
        [UniProjectId] int NULL,
        CONSTRAINT [PK_Students] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Students_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Students_Levels_levelId] FOREIGN KEY ([levelId]) REFERENCES [Levels] ([Id]),
        CONSTRAINT [FK_Students_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id]),
        CONSTRAINT [FK_Students_UniProjects_UniProjectId] FOREIGN KEY ([UniProjectId]) REFERENCES [UniProjects] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [PersonalProjects] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Link] nvarchar(max) NOT NULL,
        [MainImage] nvarchar(max) NOT NULL,
        [AppName] nvarchar(max) NOT NULL,
        [CategoryId] int NOT NULL,
        [StudentId] int NOT NULL,
        CONSTRAINT [PK_PersonalProjects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PersonalProjects_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PersonalProjects_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [StudentLectures] (
        [StudentId] int NOT NULL,
        [LectureId] int NOT NULL,
        [AttdentaceNumberState] bit NOT NULL,
        CONSTRAINT [PK_StudentLectures] PRIMARY KEY ([LectureId], [StudentId]),
        CONSTRAINT [FK_StudentLectures_Lectures_LectureId] FOREIGN KEY ([LectureId]) REFERENCES [Lectures] ([Id]),
        CONSTRAINT [FK_StudentLectures_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [StudentSubject] (
        [StudentsId] int NOT NULL,
        [SubjectsId] int NOT NULL,
        CONSTRAINT [PK_StudentSubject] PRIMARY KEY ([StudentsId], [SubjectsId]),
        CONSTRAINT [FK_StudentSubject_Students_StudentsId] FOREIGN KEY ([StudentsId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_StudentSubject_Subjects_SubjectsId] FOREIGN KEY ([SubjectsId]) REFERENCES [Subjects] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [StudentUniProjects] (
        [StudentId] int NOT NULL,
        [UniProjectId] int NOT NULL,
        CONSTRAINT [PK_StudentUniProjects] PRIMARY KEY ([StudentId], [UniProjectId]),
        CONSTRAINT [FK_StudentUniProjects_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_StudentUniProjects_UniProjects_UniProjectId] FOREIGN KEY ([UniProjectId]) REFERENCES [UniProjects] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [AttachedImages] (
        [Id] int NOT NULL IDENTITY,
        [Image] nvarchar(max) NOT NULL,
        [UniProjectId] int NOT NULL,
        [PersonalProjectId] int NULL,
        CONSTRAINT [PK_AttachedImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AttachedImages_PersonalProjects_PersonalProjectId] FOREIGN KEY ([PersonalProjectId]) REFERENCES [PersonalProjects] ([Id]),
        CONSTRAINT [FK_AttachedImages_UniProjects_UniProjectId] FOREIGN KEY ([UniProjectId]) REFERENCES [UniProjects] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [CommentContent] nvarchar(max) NOT NULL,
        [CommentDate] datetime2 NOT NULL,
        [PersonalProjectId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [UniProjectId] int NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Comments_PersonalProjects_PersonalProjectId] FOREIGN KEY ([PersonalProjectId]) REFERENCES [PersonalProjects] ([Id]),
        CONSTRAINT [FK_Comments_UniProjects_UniProjectId] FOREIGN KEY ([UniProjectId]) REFERENCES [UniProjects] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE TABLE [Likes] (
        [Id] int NOT NULL IDENTITY,
        [likeDate] datetime2 NOT NULL,
        [likeCount] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [UniProjectId] int NOT NULL,
        [PersonalProjectId] int NOT NULL,
        CONSTRAINT [PK_Likes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Likes_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Likes_PersonalProjects_PersonalProjectId] FOREIGN KEY ([PersonalProjectId]) REFERENCES [PersonalProjects] ([Id]),
        CONSTRAINT [FK_Likes_UniProjects_UniProjectId] FOREIGN KEY ([UniProjectId]) REFERENCES [UniProjects] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_ApplicationUserLevel_LevelsId] ON [ApplicationUserLevel] ([LevelsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_AttachedImages_PersonalProjectId] ON [AttachedImages] ([PersonalProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_AttachedImages_UniProjectId] ON [AttachedImages] ([UniProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Comments_PersonalProjectId] ON [Comments] ([PersonalProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Comments_UniProjectId] ON [Comments] ([UniProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Lectures_SubjectId] ON [Lectures] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Lectures_UserId] ON [Lectures] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Likes_PersonalProjectId] ON [Likes] ([PersonalProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Likes_UniProjectId] ON [Likes] ([UniProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Likes_UserId] ON [Likes] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_PersonalProjects_CategoryId] ON [PersonalProjects] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_PersonalProjects_StudentId] ON [PersonalProjects] ([StudentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_StudentLectures_StudentId] ON [StudentLectures] ([StudentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Students_levelId] ON [Students] ([levelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Students_TaskId] ON [Students] ([TaskId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Students_UniProjectId] ON [Students] ([UniProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE UNIQUE INDEX [IX_Students_UserId] ON [Students] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_StudentSubject_SubjectsId] ON [StudentSubject] ([SubjectsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_StudentUniProjects_UniProjectId] ON [StudentUniProjects] ([UniProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Subjects_LevelId] ON [Subjects] ([LevelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Subjects_UserId] ON [Subjects] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Tasks_CategoryId] ON [Tasks] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Tasks_LevelId] ON [Tasks] ([LevelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Tasks_SubjectId] ON [Tasks] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_Tasks_UserId] ON [Tasks] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_UniProjects_CategoryId] ON [UniProjects] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_UniProjects_SubjectId] ON [UniProjects] ([SubjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_UniProjects_TaskId] ON [UniProjects] ([TaskId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    CREATE INDEX [IX_UniProjects_UserId] ON [UniProjects] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240429201508_AddingModels')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240429201508_AddingModels', N'7.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240502131642_EditAttachedImage')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AttachedImages]') AND [c].[name] = N'UniProjectId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AttachedImages] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AttachedImages] ALTER COLUMN [UniProjectId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240502131642_EditAttachedImage')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240502131642_EditAttachedImage', N'7.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240517113705_EditLecture')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lectures]') AND [c].[name] = N'EndTime');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Lectures] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Lectures] DROP COLUMN [EndTime];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240517113705_EditLecture')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lectures]') AND [c].[name] = N'StartTime');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Lectures] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Lectures] DROP COLUMN [StartTime];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240517113705_EditLecture')
BEGIN
    EXEC sp_rename N'[Lectures].[DateTime]', N'StartDate', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240517113705_EditLecture')
BEGIN
    ALTER TABLE [Lectures] ADD [EndDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240517113705_EditLecture')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240517113705_EditLecture', N'7.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240518184158_EditUniProject')
BEGIN
    ALTER TABLE [UniProjects] ADD [UploadTime] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240518184158_EditUniProject')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240518184158_EditUniProject', N'7.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240531233117_AppNameDelete')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UniProjects]') AND [c].[name] = N'AppName');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [UniProjects] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [UniProjects] DROP COLUMN [AppName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240531233117_AppNameDelete')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonalProjects]') AND [c].[name] = N'AppName');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [PersonalProjects] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [PersonalProjects] DROP COLUMN [AppName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240531233117_AppNameDelete')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240531233117_AppNameDelete', N'7.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240603161911_TaskUploadTime')
BEGIN
    ALTER TABLE [Tasks] ADD [UploadTime] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240603161911_TaskUploadTime')
BEGIN
    ALTER TABLE [PersonalProjects] ADD [UploadTime] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240603161911_TaskUploadTime')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240603161911_TaskUploadTime', N'7.0.17');
END;
GO

COMMIT;
GO

