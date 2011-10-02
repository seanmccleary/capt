SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';


-- -----------------------------------------------------
-- Table `EventType`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `EventType` (
  `ID` INT NOT NULL AUTO_INCREMENT ,
  `Description` VARCHAR(45) NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Event`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Event` (
  `Id` INT NOT NULL AUTO_INCREMENT ,
  `IPv4Address` INT NOT NULL ,
  `Host` VARCHAR(100) NOT NULL ,
  `Datetime` DATETIME NOT NULL ,
  `EventTypeId` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Event_EventType1` (`EventTypeId` ASC) ,
  CONSTRAINT `fk_Event_EventType1`
    FOREIGN KEY (`EventTypeId` )
    REFERENCES `EventType` (`ID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `User`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `User` (
  `Id` INT NOT NULL AUTO_INCREMENT ,
  `IsLocked` TINYINT(1)  NOT NULL ,
  `Name` VARCHAR(45) NULL ,
  `IsAdmin` TINYINT(1)  NOT NULL ,
  `AdminNote` VARCHAR(140) NULL ,
  `EmailAddress` VARCHAR(100) NULL ,
  `IsEmailAddressVerified` TINYINT(1)  NOT NULL ,
  `Guid` CHAR(36) CHARACTER SET 'latin1' NULL ,
  `CreateEventId` INT NOT NULL ,
  `LastLogin` DATETIME NOT NULL ,
  PRIMARY KEY (`Id`) ,
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) ,
  INDEX `Name_INDEX` (`Name` ASC) ,
  UNIQUE INDEX `EmailAddress_UNIQUE` (`EmailAddress` ASC) ,
  UNIQUE INDEX `Guid_UNIQUE` (`Guid` ASC) ,
  INDEX `Guid_INDEX` (`Guid` ASC) ,
  INDEX `fk_User_Event1` (`CreateEventId` ASC) ,
  CONSTRAINT `fk_User_Event1`
    FOREIGN KEY (`CreateEventId` )
    REFERENCES `Event` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `License`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `License` (
  `Id` INT NOT NULL ,
  `Description` VARCHAR(100) NOT NULL ,
  `InfoUrl` VARCHAR(100) NULL ,
  `ImageUrl` VARCHAR(100) NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Picture`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Picture` (
  `Id` INT NOT NULL AUTO_INCREMENT ,
  `Url` VARCHAR(100) NOT NULL ,
  `IsVisible` TINYINT(1)  NOT NULL ,
  `IsNSFW` TINYINT(1)  NOT NULL ,
  `UserId` INT NULL ,
  `AdminNote` VARCHAR(140) NULL ,
  `Attribution` VARCHAR(1024) NULL ,
  `Guid` CHAR(36) CHARACTER SET 'latin1' NOT NULL ,
  `IsPrivate` TINYINT(1)  NOT NULL ,
  `Activates` DATETIME NOT NULL ,
  `LicenseId` INT NOT NULL ,
  `CreateEventId` INT NOT NULL ,
  `AttributionUrl` VARCHAR(100) NULL ,
  `Rank` DOUBLE NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Picture_User1` (`UserId` ASC) ,
  UNIQUE INDEX `Guid_UNIQUE` (`Guid` ASC) ,
  INDEX `Guid_INDEX` (`Guid` ASC) ,
  INDEX `Url_INDEX` (`Url` ASC) ,
  INDEX `fk_Picture_License1` (`LicenseId` ASC) ,
  INDEX `fk_Picture_Event1` (`CreateEventId` ASC) ,
  CONSTRAINT `fk_Picture_User1`
    FOREIGN KEY (`UserId` )
    REFERENCES `User` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Picture_License1`
    FOREIGN KEY (`LicenseId` )
    REFERENCES `License` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Picture_Event1`
    FOREIGN KEY (`CreateEventId` )
    REFERENCES `Event` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Caption`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Caption` (
  `Id` INT NOT NULL AUTO_INCREMENT ,
  `PictureId` INT NOT NULL ,
  `UserId` INT NOT NULL ,
  `IsVisible` TINYINT(1)  NOT NULL ,
  `IsAnonymous` TINYINT(1)  NOT NULL ,
  `AdminNote` VARCHAR(140) NULL ,
  `Text` VARCHAR(140) NOT NULL ,
  `CreateEventId` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Caption_Picture1` (`PictureId` ASC) ,
  INDEX `fk_Caption_User1` (`UserId` ASC) ,
  INDEX `fk_Caption_Event1` (`CreateEventId` ASC) ,
  CONSTRAINT `fk_Caption_Picture1`
    FOREIGN KEY (`PictureId` )
    REFERENCES `Picture` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Caption_User1`
    FOREIGN KEY (`UserId` )
    REFERENCES `User` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Caption_Event1`
    FOREIGN KEY (`CreateEventId` )
    REFERENCES `Event` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Vote`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Vote` (
  `Id` INT NOT NULL AUTO_INCREMENT ,
  `AdminNote` VARCHAR(140) NULL ,
  `Weight` INT NOT NULL ,
  `UserId` INT NOT NULL ,
  `CreateEventId` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Vote_User1` (`UserId` ASC) ,
  INDEX `fk_Vote_Event1` (`CreateEventId` ASC) ,
  CONSTRAINT `fk_Vote_User1`
    FOREIGN KEY (`UserId` )
    REFERENCES `User` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vote_Event1`
    FOREIGN KEY (`CreateEventId` )
    REFERENCES `Event` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ExternalLoginProvider`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `ExternalLoginProvider` (
  `Id` INT NOT NULL ,
  `Name` VARCHAR(45) NOT NULL ,
  PRIMARY KEY (`Id`) ,
  UNIQUE INDEX `Name_UNIQUE` (`Name` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `OAuthToken`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `OAuthToken` (
  `Id` INT NOT NULL AUTO_INCREMENT ,
  `Token` VARCHAR(1024) CHARACTER SET 'latin1' NOT NULL ,
  `Secret` VARCHAR(1024) NOT NULL ,
  `ExternalLoginProviderId` INT NOT NULL ,
  `UserId` INT NOT NULL ,
  `Expires` DATETIME NULL ,
  `CreateEventId` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_OAuthTokens_OAuthProvider1` (`ExternalLoginProviderId` ASC) ,
  INDEX `fk_OAuthTokens_User1` (`UserId` ASC) ,
  INDEX `Token_INDEX` (`Token` ASC) ,
  INDEX `fk_OAuthToken_Event1` (`CreateEventId` ASC) ,
  CONSTRAINT `fk_OauthTokens_ExternalLoginProvider1`
    FOREIGN KEY (`ExternalLoginProviderId` )
    REFERENCES `ExternalLoginProvider` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_OauthTokens_User1`
    FOREIGN KEY (`UserId` )
    REFERENCES `User` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_OAuthToken_Event1`
    FOREIGN KEY (`CreateEventId` )
    REFERENCES `Event` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ExternalLogin`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `ExternalLogin` (
  `Id` INT NOT NULL AUTO_INCREMENT ,
  `ExternalLoginProviderId` INT NOT NULL ,
  `Identifier` VARCHAR(767) CHARACTER SET 'latin1' NOT NULL ,
  `UserId` INT NOT NULL ,
  `CreateEventId` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_ExternalLogin_User1` (`UserId` ASC) ,
  INDEX `Identifier_INDEX` (`Identifier` ASC) ,
  INDEX `fk_ExternalLogin_ExternalLoginProvider1` (`ExternalLoginProviderId` ASC) ,
  UNIQUE INDEX `Identifier_ExternalLoginProviderID_UNIQUE` (`ExternalLoginProviderId` ASC, `Identifier` ASC) ,
  INDEX `fk_ExternalLogin_Event1` (`CreateEventId` ASC) ,
  CONSTRAINT `fk_ExternalLogin_User1`
    FOREIGN KEY (`UserId` )
    REFERENCES `User` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ExternalLogin_ExternalLoginProvider1`
    FOREIGN KEY (`ExternalLoginProviderId` )
    REFERENCES `ExternalLoginProvider` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ExternalLogin_Event1`
    FOREIGN KEY (`CreateEventId` )
    REFERENCES `Event` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CaptionVote`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `CaptionVote` (
  `VoteId` INT NOT NULL ,
  `CaptionId` INT NOT NULL ,
  PRIMARY KEY (`VoteId`, `CaptionId`) ,
  INDEX `fk_Vote_has_Caption_Caption1` (`CaptionId` ASC) ,
  INDEX `fk_Vote_has_Caption_Vote1` (`VoteId` ASC) ,
  CONSTRAINT `fk_Vote_has_Caption_Vote1`
    FOREIGN KEY (`VoteId` )
    REFERENCES `Vote` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vote_has_Caption_Caption1`
    FOREIGN KEY (`CaptionId` )
    REFERENCES `Caption` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PictureVote`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `PictureVote` (
  `PictureId` INT NOT NULL ,
  `VoteId` INT NOT NULL ,
  PRIMARY KEY (`PictureId`, `VoteId`) ,
  INDEX `fk_Picture_has_Vote_Vote1` (`VoteId` ASC) ,
  INDEX `fk_Picture_has_Vote_Picture1` (`PictureId` ASC) ,
  CONSTRAINT `fk_Picture_has_Vote_Picture1`
    FOREIGN KEY (`PictureId` )
    REFERENCES `Picture` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Picture_has_Vote_Vote1`
    FOREIGN KEY (`VoteId` )
    REFERENCES `Vote` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Comment`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Comment` (
  `Id` INT NOT NULL AUTO_INCREMENT ,
  `Text` VARCHAR(140) NOT NULL ,
  `IsVisible` TINYINT(1)  NULL ,
  `ParentCommentId` INT NULL ,
  `UserId` INT NOT NULL ,
  `AdminNote` VARCHAR(140) NULL ,
  `CreateEventId` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `FK_ParentCommentId` (`ParentCommentId` ASC) ,
  INDEX `fk_Comment_User1` (`UserId` ASC) ,
  INDEX `fk_Comment_Event1` (`CreateEventId` ASC) ,
  CONSTRAINT `FK_ParentCommentId`
    FOREIGN KEY (`ParentCommentId` )
    REFERENCES `Comment` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Comment_User1`
    FOREIGN KEY (`UserId` )
    REFERENCES `User` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Comment_Event1`
    FOREIGN KEY (`CreateEventId` )
    REFERENCES `Event` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PictureComment`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `PictureComment` (
  `PictureId` INT NOT NULL ,
  `CommentId` INT NOT NULL ,
  PRIMARY KEY (`PictureId`, `CommentId`) ,
  INDEX `fk_Picture_has_Comment_Comment1` (`CommentId` ASC) ,
  INDEX `fk_Picture_has_Comment_Picture1` (`PictureId` ASC) ,
  CONSTRAINT `fk_Picture_has_Comment_Picture1`
    FOREIGN KEY (`PictureId` )
    REFERENCES `Picture` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Picture_has_Comment_Comment1`
    FOREIGN KEY (`CommentId` )
    REFERENCES `Comment` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `CaptionComment`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `CaptionComment` (
  `CaptionId` INT NOT NULL ,
  `CommentId` INT NOT NULL ,
  PRIMARY KEY (`CaptionId`, `CommentId`) ,
  INDEX `fk_Caption_has_Comment_Comment1` (`CommentId` ASC) ,
  INDEX `fk_Caption_has_Comment_Caption1` (`CaptionId` ASC) ,
  CONSTRAINT `fk_Caption_has_Comment_Caption1`
    FOREIGN KEY (`CaptionId` )
    REFERENCES `Caption` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Caption_has_Comment_Comment1`
    FOREIGN KEY (`CommentId` )
    REFERENCES `Comment` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- function PICTURE_RANK
-- -----------------------------------------------------
DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `PICTURE_RANK`(PictureId INT, Activates DATETIME) RETURNS double
    READS SQL DATA
BEGIN
	
    DECLARE Score INT; 
    DECLARE Age INT; 
    DECLARE Rank DOUBLE; 
    DECLARE Gravity DOUBLE DEFAULT 1.5;
    DECLARE IncreasedGravityDenominator INT DEFAULT 200000;
    
    IF Activates IS NULL THEN
        SELECT
            UNIX_TIMESTAMP(Picture.Activates)
        FROM
            Picture
        WHERE
            Picture.Id = PictureId
        INTO 
            Age;    
    ELSE
        SET Age = UNIX_TIMESTAMP(Activates);
    END IF;

    SELECT
        SUM(Weight) 
    FROM
        Vote, PictureVote
    WHERE
        Vote.Id = PictureVote.VoteId
        AND PictureVote.PictureId = PictureId
    INTO
        Score;
        

    IF Score < 0 THEN
        SET Gravity = Gravity - ((1 - (1 / (ABS(Score) + 1))) / IncreasedGravityDenominator);
    END IF;
        


    RETURN 1 / POW(Age, Gravity);

END
$$

$$
DELIMITER ;


DELIMITER $$
CREATE TRIGGER Picture_BEFORE_INSERT BEFORE INSERT ON Picture
FOR EACH ROW BEGIN
    SET NEW.Rank = PICTURE_RANK(NEW.Id, NEW.Activates);
END;

$$


DELIMITER ;

DELIMITER $$


CREATE TRIGGER PictureVote_AFTER_DELETE AFTER DELETE ON PictureVote
FOR EACH ROW BEGIN
    UPDATE Picture SET Rank = PICTURE_RANK(OLD.PictureId, NULL) WHERE Picture.Id = OLD.PictureId;
END;

$$

CREATE TRIGGER PictureVote_AFTER_INSERT AFTER INSERT ON PictureVote
FOR EACH ROW BEGIN
    UPDATE Picture SET Rank = PICTURE_RANK(NEW.PictureId, NULL) WHERE Picture.Id = NEW.PictureId;
END;

$$


DELIMITER ;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `EventType`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO EventType (`ID`, `Description`) VALUES (1, 'User created');
INSERT INTO EventType (`ID`, `Description`) VALUES (2, 'Picture created');
INSERT INTO EventType (`ID`, `Description`) VALUES (3, 'Caption created');
INSERT INTO EventType (`ID`, `Description`) VALUES (4, 'Vote created');
INSERT INTO EventType (`ID`, `Description`) VALUES (5, 'External login created');
INSERT INTO EventType (`ID`, `Description`) VALUES (6, 'OAuth token created');
INSERT INTO EventType (`ID`, `Description`) VALUES (7, 'Comment created');

COMMIT;

-- -----------------------------------------------------
-- Data for table `License`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO License (`Id`, `Description`, `InfoUrl`, `ImageUrl`) VALUES (1, 'Creative Commons Attribution 2.0 Generic (CC BY 2.0) ', 'http://creativecommons.org/licenses/by/2.0/', 'http://i.creativecommons.org/l/by/2.0/80x15.png');
INSERT INTO License (`Id`, `Description`, `InfoUrl`, `ImageUrl`) VALUES (2, 'Creative Commons Attribution-ShareAlike 2.0 Generic (CC BY-SA 2.0) ', 'http://creativecommons.org/licenses/by-sa/2.0/', 'http://i.creativecommons.org/l/by-sa/2.0/80x15.png');
INSERT INTO License (`Id`, `Description`, `InfoUrl`, `ImageUrl`) VALUES (3, 'No Known Copyright', 'http://creativecommons.org/about/pdm', 'http://i.creativecommons.org/p/mark/1.0/80x15.png');

COMMIT;

-- -----------------------------------------------------
-- Data for table `ExternalLoginProvider`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO ExternalLoginProvider (`Id`, `Name`) VALUES (1, 'Generic OpenID');
INSERT INTO ExternalLoginProvider (`Id`, `Name`) VALUES (2, 'Facebook');
INSERT INTO ExternalLoginProvider (`Id`, `Name`) VALUES (3, 'Twitter');

COMMIT;
