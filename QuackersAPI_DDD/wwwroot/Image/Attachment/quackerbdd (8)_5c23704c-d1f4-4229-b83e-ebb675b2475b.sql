-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Apr 16, 2024 at 07:10 AM
-- Server version: 5.7.24
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `quackerbdd`
--

-- --------------------------------------------------------

--
-- Table structure for table `attachment`
--

CREATE TABLE `attachment` (
  `Attachment_Id` int(11) NOT NULL,
  `Attachment_Name` varchar(255) NOT NULL,
  `Attachment_Attachment` varchar(255) DEFAULT NULL,
  `Message_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `channel`
--

CREATE TABLE `channel` (
  `Channel_ID` int(11) NOT NULL,
  `Channel_Name` varchar(50) NOT NULL,
  `Channel_ImagePath` varchar(50) DEFAULT NULL,
  `ChannelType_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `channel`
--

INSERT INTO `channel` (`Channel_ID`, `Channel_Name`, `Channel_ImagePath`, `ChannelType_Id`) VALUES
(1, 'string', 'string', 6),
(8, 'stridng', 'string', 6),
(10, 'strifsdng', 'string', 6),
(11, 'strdfing', 'string', 6),
(13, 'stridfsfng', 'string', 6);

-- --------------------------------------------------------

--
-- Table structure for table `channelpersonrole`
--

CREATE TABLE `channelpersonrole` (
  `ChannelPersonRole_Id` int(11) NOT NULL,
  `ChannelPersonRole_Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `channelpersonrolexpersonxchannel`
--

CREATE TABLE `channelpersonrolexpersonxchannel` (
  `Person_Id` int(11) NOT NULL,
  `Channel_ID` int(11) NOT NULL,
  `ChannelPersonRoleXPersonXChannel_AffectDate` datetime DEFAULT NULL,
  `ChannelPersonRole_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `channeltype`
--

CREATE TABLE `channeltype` (
  `ChannelType_Id` int(11) NOT NULL,
  `ChannelType_Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `channeltype`
--

INSERT INTO `channeltype` (`ChannelType_Id`, `ChannelType_Name`) VALUES
(1, 'strihg'),
(6, 'string');

-- --------------------------------------------------------

--
-- Table structure for table `message`
--

CREATE TABLE `message` (
  `Message_ID` int(11) NOT NULL,
  `Message_Text` text,
  `Message_Date` datetime DEFAULT NULL,
  `Message_IsNotArchived` tinyint(1) NOT NULL DEFAULT '0',
  `Channel_ID` int(11) NOT NULL,
  `Person_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `messagexreactionxperson`
--

CREATE TABLE `messagexreactionxperson` (
  `Person_Id` int(11) NOT NULL,
  `Message_ID` int(11) NOT NULL,
  `Reaction_ID` int(11) NOT NULL,
  `MessageXReactionXPerson_ReactionDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `notification`
--

CREATE TABLE `notification` (
  `Notification_Id` int(11) NOT NULL,
  `Notification_Name` varchar(50) DEFAULT NULL,
  `Notification_Text` varchar(255) DEFAULT NULL,
  `Notification_DatePost` date NOT NULL,
  `Notification_Type_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `notification_type`
--

CREATE TABLE `notification_type` (
  `Notification_Type_ID` int(11) NOT NULL,
  `NotificationType_Name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `person`
--

CREATE TABLE `person` (
  `Person_Id` int(11) NOT NULL,
  `Person_Password` varchar(255) NOT NULL,
  `Person_Email` varchar(100) NOT NULL,
  `Person_PhoneNumber` varchar(16) DEFAULT NULL,
  `Person_FirstName` varchar(50) NOT NULL,
  `Person_LastName` varchar(50) NOT NULL,
  `Person_CreatedTimePerson` datetime DEFAULT NULL,
  `Person_ProfilPicturePath` varchar(255) DEFAULT NULL,
  `Person_Description` varchar(255) DEFAULT NULL,
  `Person_TokenResetPassword` varchar(255) DEFAULT NULL,
  `Person_IsTemporaryPassword` tinyint(1) NOT NULL,
  `Person_LoggedInToken` varchar(255) DEFAULT NULL,
  `Person_LoggedInTokenExpirationDate` datetime DEFAULT NULL,
  `PersonJobTitle_Id` int(11) NOT NULL,
  `PersonStatut_Id` int(11) NOT NULL,
  `PersonRole_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `person`
--

INSERT INTO `person` (`Person_Id`, `Person_Password`, `Person_Email`, `Person_PhoneNumber`, `Person_FirstName`, `Person_LastName`, `Person_CreatedTimePerson`, `Person_ProfilPicturePath`, `Person_Description`, `Person_TokenResetPassword`, `Person_IsTemporaryPassword`, `Person_LoggedInToken`, `Person_LoggedInTokenExpirationDate`, `PersonJobTitle_Id`, `PersonStatut_Id`, `PersonRole_Id`) VALUES
(1, '$FjlZkD90QcZ', 'user@example.com', '0617769262', 'string', 'string', '2024-04-16 09:05:30', 'Path/To/Default/Image', 'Je suis string string nouveau de Quacker!', '9Hv0TH884ywi+BkMF/IzzduOW7I8YEmR4twjXb7f/CA=', 1, NULL, NULL, 1, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `personjobtitle`
--

CREATE TABLE `personjobtitle` (
  `PersonJobTitle_Id` int(11) NOT NULL,
  `PersonJobTitle_Name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `personjobtitle`
--

INSERT INTO `personjobtitle` (`PersonJobTitle_Id`, `PersonJobTitle_Name`) VALUES
(1, 'RH');

-- --------------------------------------------------------

--
-- Table structure for table `personrole`
--

CREATE TABLE `personrole` (
  `PersonRole_Id` int(11) NOT NULL,
  `PersonRole_Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `personrole`
--

INSERT INTO `personrole` (`PersonRole_Id`, `PersonRole_Name`) VALUES
(1, 'Utilisateur');

-- --------------------------------------------------------

--
-- Table structure for table `personstatut`
--

CREATE TABLE `personstatut` (
  `PersonStatut_Id` int(11) NOT NULL,
  `PersonStatut_Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `personstatut`
--

INSERT INTO `personstatut` (`PersonStatut_Id`, `PersonStatut_Name`) VALUES
(1, 'En Ligne');

-- --------------------------------------------------------

--
-- Table structure for table `personxchannel`
--

CREATE TABLE `personxchannel` (
  `Person_Id` int(11) NOT NULL,
  `Channel_ID` int(11) NOT NULL,
  `PersonXChannel_SignInDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `personxchannel`
--

INSERT INTO `personxchannel` (`Person_Id`, `Channel_ID`, `PersonXChannel_SignInDate`) VALUES
(1, 1, '2024-04-16 09:06:08');

-- --------------------------------------------------------

--
-- Table structure for table `personxmessage`
--

CREATE TABLE `personxmessage` (
  `Person_Id` int(11) NOT NULL,
  `Message_ID` int(11) NOT NULL,
  `PersonXMessage_ReadDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `personxnotification`
--

CREATE TABLE `personxnotification` (
  `Person_Id` int(11) NOT NULL,
  `Notification_Id` int(11) NOT NULL,
  `PersonXNotification_ReadDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `reaction`
--

CREATE TABLE `reaction` (
  `Reaction_ID` int(11) NOT NULL,
  `Reaction_Name` varchar(50) NOT NULL,
  `Reaction_PicturePath` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attachment`
--
ALTER TABLE `attachment`
  ADD PRIMARY KEY (`Attachment_Id`),
  ADD UNIQUE KEY `Attachment_Name` (`Attachment_Name`),
  ADD KEY `Message_ID` (`Message_ID`);

--
-- Indexes for table `channel`
--
ALTER TABLE `channel`
  ADD PRIMARY KEY (`Channel_ID`),
  ADD UNIQUE KEY `Channel_Name` (`Channel_Name`),
  ADD KEY `ChannelType_Id` (`ChannelType_Id`);

--
-- Indexes for table `channelpersonrole`
--
ALTER TABLE `channelpersonrole`
  ADD PRIMARY KEY (`ChannelPersonRole_Id`),
  ADD UNIQUE KEY `ChannelPersonRole_Name` (`ChannelPersonRole_Name`);

--
-- Indexes for table `channelpersonrolexpersonxchannel`
--
ALTER TABLE `channelpersonrolexpersonxchannel`
  ADD PRIMARY KEY (`Person_Id`,`Channel_ID`),
  ADD KEY `Channel_ID` (`Channel_ID`),
  ADD KEY `ChannelPersonRole_Id` (`ChannelPersonRole_Id`);

--
-- Indexes for table `channeltype`
--
ALTER TABLE `channeltype`
  ADD PRIMARY KEY (`ChannelType_Id`),
  ADD UNIQUE KEY `ChannelType_Name` (`ChannelType_Name`);

--
-- Indexes for table `message`
--
ALTER TABLE `message`
  ADD PRIMARY KEY (`Message_ID`),
  ADD KEY `Channel_ID` (`Channel_ID`),
  ADD KEY `Person_Id` (`Person_Id`);

--
-- Indexes for table `messagexreactionxperson`
--
ALTER TABLE `messagexreactionxperson`
  ADD PRIMARY KEY (`Person_Id`,`Message_ID`,`Reaction_ID`),
  ADD KEY `Message_ID` (`Message_ID`),
  ADD KEY `Reaction_ID` (`Reaction_ID`);

--
-- Indexes for table `notification`
--
ALTER TABLE `notification`
  ADD PRIMARY KEY (`Notification_Id`),
  ADD KEY `Notification_Type_ID` (`Notification_Type_ID`);

--
-- Indexes for table `notification_type`
--
ALTER TABLE `notification_type`
  ADD PRIMARY KEY (`Notification_Type_ID`);

--
-- Indexes for table `person`
--
ALTER TABLE `person`
  ADD PRIMARY KEY (`Person_Id`),
  ADD UNIQUE KEY `Person_Email` (`Person_Email`),
  ADD UNIQUE KEY `Person_PhoneNumber` (`Person_PhoneNumber`),
  ADD KEY `PersonJobTitle_Id` (`PersonJobTitle_Id`),
  ADD KEY `PersonStatut_Id` (`PersonStatut_Id`),
  ADD KEY `PersonRole_Id` (`PersonRole_Id`);

--
-- Indexes for table `personjobtitle`
--
ALTER TABLE `personjobtitle`
  ADD PRIMARY KEY (`PersonJobTitle_Id`);

--
-- Indexes for table `personrole`
--
ALTER TABLE `personrole`
  ADD PRIMARY KEY (`PersonRole_Id`),
  ADD UNIQUE KEY `PersonRole_Name` (`PersonRole_Name`);

--
-- Indexes for table `personstatut`
--
ALTER TABLE `personstatut`
  ADD PRIMARY KEY (`PersonStatut_Id`),
  ADD UNIQUE KEY `PersonStatut_Name` (`PersonStatut_Name`);

--
-- Indexes for table `personxchannel`
--
ALTER TABLE `personxchannel`
  ADD PRIMARY KEY (`Person_Id`,`Channel_ID`),
  ADD KEY `Channel_ID` (`Channel_ID`);

--
-- Indexes for table `personxmessage`
--
ALTER TABLE `personxmessage`
  ADD PRIMARY KEY (`Person_Id`,`Message_ID`),
  ADD KEY `Message_ID` (`Message_ID`);

--
-- Indexes for table `personxnotification`
--
ALTER TABLE `personxnotification`
  ADD PRIMARY KEY (`Person_Id`,`Notification_Id`),
  ADD KEY `Notification_Id` (`Notification_Id`);

--
-- Indexes for table `reaction`
--
ALTER TABLE `reaction`
  ADD PRIMARY KEY (`Reaction_ID`),
  ADD UNIQUE KEY `Reaction_Name` (`Reaction_Name`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attachment`
--
ALTER TABLE `attachment`
  MODIFY `Attachment_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `channel`
--
ALTER TABLE `channel`
  MODIFY `Channel_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

ALTER TABLE `channelpersonrole` 
  MODIFY `ChannelPersonRole_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `channeltype`
--
ALTER TABLE `channeltype`
  MODIFY `ChannelType_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `message`
--
ALTER TABLE `message`
  MODIFY `Message_ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `notification`
--
ALTER TABLE `notification`
  MODIFY `Notification_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `notification_type`
--
ALTER TABLE `notification_type`
  MODIFY `Notification_Type_ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `person`
--
ALTER TABLE `person`
  MODIFY `Person_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `personjobtitle`
--
ALTER TABLE `personjobtitle`
  MODIFY `PersonJobTitle_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `personrole`
--
ALTER TABLE `personrole`
  MODIFY `PersonRole_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `personstatut`
--
ALTER TABLE `personstatut`
  MODIFY `PersonStatut_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `reaction`
--
ALTER TABLE `reaction`
  MODIFY `Reaction_ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `attachment`
--
ALTER TABLE `attachment`
  ADD CONSTRAINT `attachment_ibfk_1` FOREIGN KEY (`Message_ID`) REFERENCES `message` (`Message_ID`);

--
-- Constraints for table `channel`
--
ALTER TABLE `channel`
  ADD CONSTRAINT `channel_ibfk_1` FOREIGN KEY (`ChannelType_Id`) REFERENCES `channeltype` (`ChannelType_Id`);

--
-- Constraints for table `channelpersonrolexpersonxchannel`
--
ALTER TABLE `channelpersonrolexpersonxchannel`
  ADD CONSTRAINT `channelpersonrolexpersonxchannel_ibfk_1` FOREIGN KEY (`Person_Id`) REFERENCES `person` (`Person_Id`),
  ADD CONSTRAINT `channelpersonrolexpersonxchannel_ibfk_2` FOREIGN KEY (`Channel_ID`) REFERENCES `channel` (`Channel_ID`),
  ADD CONSTRAINT `channelpersonrolexpersonxchannel_ibfk_3` FOREIGN KEY (`ChannelPersonRole_Id`) REFERENCES `channelpersonrole` (`ChannelPersonRole_Id`);

--
-- Constraints for table `message`
--
ALTER TABLE `message`
  ADD CONSTRAINT `message_ibfk_1` FOREIGN KEY (`Channel_ID`) REFERENCES `channel` (`Channel_ID`),
  ADD CONSTRAINT `message_ibfk_2` FOREIGN KEY (`Person_Id`) REFERENCES `person` (`Person_Id`);

--
-- Constraints for table `messagexreactionxperson`
--
ALTER TABLE `messagexreactionxperson`
  ADD CONSTRAINT `messagexreactionxperson_ibfk_1` FOREIGN KEY (`Person_Id`) REFERENCES `person` (`Person_Id`),
  ADD CONSTRAINT `messagexreactionxperson_ibfk_2` FOREIGN KEY (`Message_ID`) REFERENCES `message` (`Message_ID`),
  ADD CONSTRAINT `messagexreactionxperson_ibfk_3` FOREIGN KEY (`Reaction_ID`) REFERENCES `reaction` (`Reaction_ID`);

--
-- Constraints for table `notification`
--
ALTER TABLE `notification`
  ADD CONSTRAINT `notification_ibfk_1` FOREIGN KEY (`Notification_Type_ID`) REFERENCES `notification_type` (`Notification_Type_ID`);

--
-- Constraints for table `person`
--
ALTER TABLE `person`
  ADD CONSTRAINT `person_ibfk_1` FOREIGN KEY (`PersonJobTitle_Id`) REFERENCES `personjobtitle` (`PersonJobTitle_Id`),
  ADD CONSTRAINT `person_ibfk_2` FOREIGN KEY (`PersonStatut_Id`) REFERENCES `personstatut` (`PersonStatut_Id`),
  ADD CONSTRAINT `person_ibfk_3` FOREIGN KEY (`PersonRole_Id`) REFERENCES `personrole` (`PersonRole_Id`);

--
-- Constraints for table `personxchannel`
--
ALTER TABLE `personxchannel`
  ADD CONSTRAINT `personxchannel_ibfk_1` FOREIGN KEY (`Person_Id`) REFERENCES `person` (`Person_Id`),
  ADD CONSTRAINT `personxchannel_ibfk_2` FOREIGN KEY (`Channel_ID`) REFERENCES `channel` (`Channel_ID`);

--
-- Constraints for table `personxmessage`
--
ALTER TABLE `personxmessage`
  ADD CONSTRAINT `personxmessage_ibfk_1` FOREIGN KEY (`Person_Id`) REFERENCES `person` (`Person_Id`),
  ADD CONSTRAINT `personxmessage_ibfk_2` FOREIGN KEY (`Message_ID`) REFERENCES `message` (`Message_ID`);

--
-- Constraints for table `personxnotification`
--
ALTER TABLE `personxnotification`
  ADD CONSTRAINT `personxnotification_ibfk_1` FOREIGN KEY (`Person_Id`) REFERENCES `person` (`Person_Id`),
  ADD CONSTRAINT `personxnotification_ibfk_2` FOREIGN KEY (`Notification_Id`) REFERENCES `notification` (`Notification_Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
