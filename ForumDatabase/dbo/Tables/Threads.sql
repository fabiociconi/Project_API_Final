CREATE TABLE [dbo].[Threads] (
    [thread_id] UNIQUEIDENTIFIER NOT NULL,
    [user_id]   UNIQUEIDENTIFIER NOT NULL,
    [subject]   VARCHAR (100)    NOT NULL,
    [posted_on] DATETIME         NOT NULL,
    CONSTRAINT [PK_Table_2] PRIMARY KEY CLUSTERED ([thread_id] ASC),
    CONSTRAINT [FK_Threads_Users] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([user_id])
);

