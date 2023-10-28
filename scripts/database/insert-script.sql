/* 
    INFORMATION : The following inserts are required, your application won't be able to work if you don't execute them 
*/
-- Default Roles
INSERT INTO Roles VALUES ('Admin',0);
INSERT INTO Roles VALUES ('Member',0);

-- Default User 
INSERT INTO Users VALUES ('root','$2a$11$gYV7LxSylL7ZaDnalPo3y.oNQtW2.m4d5pB5j3iVm50..bbZ4Akgm', 1); -- name : root / pass : root
