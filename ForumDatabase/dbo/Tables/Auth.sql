CREATE TABLE [dbo].[Auth] (
    [email]    NVARCHAR (50)    NOT NULL,
    [password] NVARCHAR (50)    NOT NULL,
    [user_id]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Auth] PRIMARY KEY CLUSTERED ([email] ASC),
    CONSTRAINT [FK_Auth_Users] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([user_id])
);

