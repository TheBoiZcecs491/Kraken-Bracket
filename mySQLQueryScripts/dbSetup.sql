drop database kraken_bracket;
create database kraken_bracket;
use kraken_bracket;
CREATE TABLE `user_information` (
  `userID` int NOT NULL AUTO_INCREMENT,
  `email` varchar(45) DEFAULT NULL,
  `fname` varchar(45) DEFAULT NULL,
  `lname` varchar(45) DEFAULT NULL,
  `hashed_password` varchar(45) DEFAULT NULL,
  `salt` varchar(45) DEFAULT NULL,
  `account_type` varchar(45) DEFAULT "",
  `account_status` int DEFAULT 0,
  PRIMARY KEY (`userID`)
);

CREATE TABLE `userid` (
  `userID` int NOT NULL,
  `hashedUserID` varchar(45) NOT NULL,
  PRIMARY KEY (`hashedUserID`),
  CONSTRAINT `userID` FOREIGN KEY (`userID`) REFERENCES `user_information` (`userID`)
);

CREATE TABLE `gamer_info` (
  `hashedUserID` varchar(45) NOT NULL,
  `gamerTag` varchar(21) DEFAULT NULL,
  PRIMARY KEY (`hashedUserID`),
  CONSTRAINT `fk--gamer_info--userid` FOREIGN KEY (`hashedUserID`) REFERENCES `userid` (`hashedUserID`)
);

CREATE TABLE `bracket_info` (
  `bracketID` int NOT NULL AUTO_INCREMENT,
  `bracket_name` varchar(75) DEFAULT NULL,
  `bracketTypeID` int DEFAULT 0,
  `number_player` int DEFAULT NULL,
  `game_played` varchar(75) DEFAULT "",
  `gaming_platform` varchar(75) DEFAULT "",
  `rules` varchar(500) DEFAULT "",
  `start_date` datetime DEFAULT NULL,
  `end_date` datetime DEFAULT NULL,
  `status_code` int DEFAULT 0,
  `reason` varchar(100) DEFAULT "",
  PRIMARY KEY (`bracketID`)
);

CREATE TABLE `bracket_player_info` (
  `bracketID` int NOT NULL,
  `hashedUserID` varchar(45) NOT NULL,
  `roleID` int DEFAULT 0,
  `placement` int DEFAULT 0,
  `score` int DEFAULT 0,
  `claim` varchar(45) DEFAULT "",
  `status_code` int DEFAULT 0,
  `reason` varchar(100) DEFAULT "",
  CONSTRAINT `fk--bracket_player_info--bracket_info` FOREIGN KEY (`bracketID`) REFERENCES `bracket_info` (`bracketID`),
  CONSTRAINT `fk--bracket_player_info--userid` FOREIGN KEY (`hashedUserID`) REFERENCES `userid` (`hashedUserID`),
  PRIMARY KEY (`bracketID`,`hashedUserID`)
);

CREATE TABLE `event_info` (
  `eventID` int NOT NULL AUTO_INCREMENT,
  `event_name` varchar(45) DEFAULT NULL,
  `address` varchar(75) DEFAULT "",
  `event_description` varchar(500) DEFAULT "",
  `start_date` datetime DEFAULT NULL,
  `end_date` datetime DEFAULT NULL,
  `status_code` int DEFAULT 0,
  `reason` varchar(100) DEFAULT "",
  PRIMARY KEY (`eventID`)
);

CREATE TABLE `event_player_info` (
  `eventID` int NOT NULL,
  `hashedUserID` VARCHAR(45) NOT NULL,
  `roleID` int DEFAULT 0,
  `claim` varchar(45) DEFAULT "",
  `status_code` int DEFAULT 0,
  `reason` varchar(100) DEFAULT "",
  CONSTRAINT `fk--event_player_info--event_info` FOREIGN KEY (`eventID`) REFERENCES `event_info` (`eventID`),
  CONSTRAINT `fk--event_player_info--userid` FOREIGN KEY (`hashedUserID`) REFERENCES `userid` (`hashedUserID`),
  PRIMARY KEY (`eventID`, `hashedUserID`)
 );

CREATE TABLE `event_bracket_list` (
  `eventID` int NOT NULL,
  `bracketID` int NOT NULL,
  CONSTRAINT `fk--event_bracket_list--event_info` FOREIGN KEY (`eventID`) REFERENCES `event_info` (`eventID`),
  CONSTRAINT `fk--event_bracket_list--bracket_info` FOREIGN KEY (`bracketID`) REFERENCES `bracket_info` (`bracketID`),
  PRIMARY KEY (`eventID`,`bracketID`)
);
