CREATE TABLE [dbo].[Users] (
    [user_id]      UNIQUEIDENTIFIER NOT NULL,
    [first_name]   NVARCHAR (50)    NOT NULL,
    [last_name]    NVARCHAR (50)    NOT NULL,
    [date_created] DATETIME         NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([user_id] ASC)
);

