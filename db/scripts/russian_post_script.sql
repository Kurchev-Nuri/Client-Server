--
CREATE DATABASE RussianPost;

--
USE RussianPost;
	CREATE USER RussianPostMigrator
	FROM LOGIN RussianPostMigrator;
GO
--
USE RussianPost;
	CREATE USER RussianPostManager
	FROM LOGIN RussianPostManager;
GO

-- Roles for RussianPostMigrator
ALTER ROLE db_datareader ADD MEMBER RussianPostMigrator;
GO
ALTER ROLE db_datawriter ADD MEMBER RussianPostMigrator;
GO
ALTER ROLE db_ddladmin ADD MEMBER RussianPostMigrator;
GO

-- Roles for RussianPostManager
CREATE ROLE db_executor
GRANT EXECUTE TO db_executor

ALTER ROLE db_datareader ADD MEMBER RussianPostManager;
GO
ALTER ROLE db_datawriter ADD MEMBER RussianPostManager;
GO
ALTER ROLE db_executor ADD MEMBER RussianPostManager;
GO