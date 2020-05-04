/*
CREATE TABLE User (
	System_ID INT NOT NULL PRIMARY KEY,
    Email VARCHAR(200),
    User_Password VARCHAR(2000),
	First_Name VARCHAR(200),
    Last_Name VARCHAR(200),
    Account_Status BOOL,
    Account_Type VARCHAR(200)
);
*/
CREATE TABLE `bracket_info` (
  `bracketID` int(11) NOT NULL,
  `bracket_name` varchar(75) DEFAULT NULL,
  `bracketTypeID` int(11) DEFAULT NULL,
  `number_player` int(11) DEFAULT NULL,
  `game_played` varchar(75) DEFAULT NULL,
  `gaming_platform` varchar(75) DEFAULT NULL,
  `rules` varchar(500) DEFAULT NULL,
  `start_date` datetime DEFAULT NULL,
  `end_date` datetime DEFAULT NULL,
  `status_code` tinyint DEFAULT NULL,
  PRIMARY KEY (`bracketID`)
);

CREATE TABLE `bracket_player_info` (
  `bracketID` int(11) NOT NULL,
  `hashedUserID` int(11) NOT NULL,
  `roleID` int(11) DEFAULT NULL,
  `placement` int(11) DEFAULT NULL,
  `score` int(11) DEFAULT NULL,
  PRIMARY KEY (`bracketID`,`hashedUserID`)
) ;

CREATE TABLE `bracket_type` (
  `bracketTypeID` int(11) NOT NULL,
  `bracket_type` varchar(75) DEFAULT NULL,
  PRIMARY KEY (`bracketTypeID`)
);

CREATE TABLE `event_braket_list` (
  `eventID` int(11) NOT NULL,
  `braketID` int(11) NOT NULL,
  PRIMARY KEY (`eventID`,`braketID`)
) ;

CREATE TABLE `event_info` (
  `eventID` int(11) NOT NULL,
  `event_name` varchar(75) DEFAULT NULL,
  `address` varchar(75) DEFAULT NULL,
  `event_description` varchar(500) DEFAULT NULL,
  `start_date` datetime DEFAULT NULL,
  `end_date` datetime DEFAULT NULL,
  PRIMARY KEY (`eventID`)
) ;

CREATE TABLE `gamer_info` (
  `hashedUserID` int(11) NOT NULL,
  `gamerTag` varchar(20) DEFAULT NULL,
  `gamerTagID` int(11) DEFAULT NULL,
  `teamID` int(11) DEFAULT NULL,
  PRIMARY KEY (`hashedUserID`)
) ;

CREATE TABLE `login_info` (
  `email` int(11) NOT NULL,
  `hashed_password` varchar(45) DEFAULT NULL,
  `salt` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`email`)
) ;

CREATE TABLE `role_type` (
  `roleID` int(11) NOT NULL,
  `role_type` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`roleID`)
);

CREATE TABLE `team_info` (
  `teamID` int(11) NOT NULL,
  `team_name` varchar(75) DEFAULT NULL,
  PRIMARY KEY (`teamID`)
);

CREATE TABLE `team_list` (
  `teamID` int(11) NOT NULL,
  `hashedUserID` int(11) NOT NULL,
  PRIMARY KEY (`teamID`,`hashedUserID`)
) ;

CREATE TABLE `user_information` (
  	`userID` int(11) NOT NULL,
  	`email` varchar(45) DEFAULT NULL,
	`hashed_password` varchar(45) DEFAULT NULL,
	`salt` varchar(45) DEFAULT NULL,
  	`fname` varchar(45) DEFAULT NULL,
  	`lname` varchar(45) DEFAULT NULL,
	`account_type` varchar(20) NOT NULL,
	`account_status` bool DEFAULT NULL,
  	PRIMARY KEY (`userID`)
);

CREATE TABLE `userid` (
  `userID` int(11) NOT NULL,
  `hashed_userID` varchar(45) NOT NULL,
  PRIMARY KEY (`userID`,`hashed_userID`)
) ;
