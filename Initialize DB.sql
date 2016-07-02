Create	Database	WPDB	-- Weekly Program Database
Go
Use WPDB
Go
Create	Table	Users(
UserId	Int	Identity not null primary key,
Email	nvarchar(100)	not null unique,
Password	nvarchar(100)	not null,
Active	Bit	 not null	default(0),
Blocked Bit	 not null	default(0),
CreatedAt	DateTime	Default(GetDate()))
Go
Create	Table	AYears( --Academic Year
AYearId	Int Identity not null primary key,
UserId		Int not null References Users(UserId),
Title	nvarchar(100)		Not Null,
DefaultRingCount Int	not null,
CreatedAt	DateTime	Default(GetDate()))
Go
Create	Table	Lessons(
LessonId	Int	Identity	not null	primary key,
UsersId		Int not null References Users(UserId),
AYearId		Int	not null References AYears(AYearId),
Name		nvarchar(100)	Not null unique)
Go
Create	Table	Teachers(
TeacherId	Int	Identity	not null	primary key,
UserId		Int not null References Users(UserId),
AYearId		Int not null References AYears(AYearId),
Name		nvarchar(50)	not null,
Job			nvarchar(50),
CreatedAt	DateTime	Default(GetDate()),
Constraint UK_Teacher Unique(Name, AYearId))
Go
Create Table	CanTeaches(
CanTeachId	Int	Identity	not null	primary key,
UserId		Int not null References Users(UserId),
--AYearId		Int not null References AYears(AYearId),	-- because it use teacher table
TeacherId	Int not null References Teachers(TeacherId),
LessonId	Int not null	References Lessons(LessonId),
CreatedAt	DateTime Default(GetDate()))
Go
Create	Table	Classes(
ClassId	Int	Identity not null primary key,
UserId	Int not null References Users(UserId),
AYearId	Int not null References AYears(AYearId),
Title	nvarchar(100)	not null unique,
CreatedAt	DateTime	Default(GetDate()))
Go
Create	Table	RHours(--Required Hours ClassLesson
RHourId	Int identity not null primary key,
UserId	Int not null	references Users(UserId),
--AYearId	Int	not null	references AYears(AYearId), -- because it use class table
ClassId	Int	not null	references Classes(ClassId),
LessonId Int not null	references Lessons(LessonId),
[Hours]	Int	not null	Default(0),
[HHours]	Int not null	Default(0), -- Half Hours
Constraint UK_ClassLesson Unique(ClassId, LessonId))
Go
Create	Table	TFT(		--Teachers Free Times 
TFTId	Int Identity not null primary key,
UserId	Int not null References Users(UserId),
AYearId	Int not null References AYears(AYearId),
TeacherId	Int not null	References Teachers(TeacherId),
DaysOfWeek	Int	not null,	--0,1,2,3,4,5,6
RingNumber	Int	not null, -- 1,2,3,4,....Z
FHIsFree		bit	not null, 
SHIsFree		bit	not null,
CreatedAt	DateTime	Default(GetDate()))
Go
Create	Table	WP(		-- Weekly Program
WPId	Int	Identity	not null	primary key,
UsersId		Int not null References Users(UserId),
AYearId	Int not null References AYears(AYearId),
ClassId	Int not null References Class(ClassId),
DaysOfWeek	Int	not null,	--0,1,2,3,4,5,6
RingNumber	Int	not null, -- 1,2,3,4,....
HalfRingNumber Int not null, --1,2
Teacher	Int	References Teachers(TeacherId),		--check that this teacher can teach this lesson
Lesson	Int	References Lessons(LessonId),
CreatedAt	DateTime	Default(GetDate()))
Go