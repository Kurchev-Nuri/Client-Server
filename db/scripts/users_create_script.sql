-- Default Migrator
USE MASTER;
	CREATE LOGIN RussianPostMigrator
	WITH PASSWORD = 'post2002#';
GO

-- Default reader/writer
USE MASTER;
	CREATE LOGIN RussianPostManager
	WITH PASSWORD = 'post2002';
GO

-- Default Hangfire-User
USE MASTER
	CREATE LOGIN RussianPostHangfireManager 
	WITH PASSWORD = 'post2002HF';
GO