BEGIN TRANSACTION;
DROP TABLE IF EXISTS `Assignment`;
CREATE TABLE IF NOT EXISTS `Assignment` (
	`AssignmentNo`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`CourseNo`	INTEGER NOT NULL,
	`AssignOrder`	INTEGER NOT NULL,
	`AssignmentTitle`	TEXT NOT NULL,
	`AssignmentDescription`	TEXT,
	`StartDate`	DateTime NOT NULL,
	`EndDate`	DateTime NOT NULL
);
COMMIT;
