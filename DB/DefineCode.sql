BEGIN TRANSACTION;
DROP TABLE IF EXISTS `DefineCode`;
CREATE TABLE IF NOT EXISTS `DefineCode` (
	`CodeGroup`	TEXT NOT NULL,
	`CodeType`	INTEGER NOT NULL,
	`CodeName`	TEXT NOT NULL
);
INSERT INTO `DefineCode` VALUES ('UserType',0,'教授');
INSERT INTO `DefineCode` VALUES ('UserType',1,'助教');
INSERT INTO `DefineCode` VALUES ('UserType',2,'學生');
COMMIT;
