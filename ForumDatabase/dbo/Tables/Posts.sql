CREATE TABLE [dbo].[Posts] (
    [post_id]   UNIQUEIDENTIFIER NOT NULL,
    [thread_id] UNIQUEIDENTIFIER NOT NULL,
    [user_id]   UNIQUEIDENTIFIER NOT NULL,
    [message]   NVARCHAR (MAX)   NOT NULL,
    [posted_on] DATETIME         NOT NULL,
    CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED ([post_id] ASC),
    CONSTRAINT [FK_Posts_Threads] FOREIGN KEY ([thread_id]) REFERENCES [dbo].[Threads] ([thread_id]),
    CONSTRAINT [FK_Posts_Users] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([user_id])
);

