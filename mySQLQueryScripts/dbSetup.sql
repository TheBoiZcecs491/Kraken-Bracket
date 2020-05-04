drop database kraken_bracket;
create database kraken_bracket;
use kraken_bracket;
CREATE TABLE `user_information` (
  `userID` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(45) DEFAULT NULL,
`hashed_password` varchar(45) DEFAULT NULL,
`salt` varchar(45) DEFAULT NULL,
  `fname` varchar(45) DEFAULT NULL,
  `lname` varchar(45) DEFAULT NULL,
`account_type` varchar(20) NOT NULL,
`account_status` bool DEFAULT 1,
  PRIMARY KEY (`userID`)
);
CREATE TABLE `bracket_info` (
  `id` int(11) NOT NULL,
  `bracket_name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
);
CREATE TABLE `userid` (
  `userID` int(11) NOT NULL,
  `hasheduserID` varchar(45) NOT NULL,
    PRIMARY KEY (`userID`)
);
