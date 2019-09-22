--
USE RussianPost
	CREATE USER RussianPostHangfireManager
	FROM LOGIN RussianPostHangfireManager;
GO

--
CREATE SCHEMA HangFire AUTHORIZATION RussianPostHangfireManager;
GO

--
GRANT ALTER,
	  SELECT,
      UPDATE,
	  DELETE,
	  INSERT,
	  EXECUTE,
	  REFERENCES,
	  VIEW DEFINITION 
  ON SCHEMA::HangFire
  TO RussianPostHangfireManager;

--
GRANT CREATE TABLE TO RussianPostHangfireManager;
GO