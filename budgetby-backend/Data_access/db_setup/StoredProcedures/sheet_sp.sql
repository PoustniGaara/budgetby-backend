#Sheet stored procedures#

DELIMITER //

CREATE PROCEDURE spSheet_Create(IN _userID  INT UNSIGNED, IN _dateID INT UNSIGNED)
BEGIN
    INSERT INTO sheet (userID, dateID)
    VALUES (_userID, _dateID);
END//

DELIMITER ;

DELIMITER //

CREATE PROCEDURE spSheet_Read(IN _id INT unsigned)
BEGIN
    SELECT id, userID, dateID
    FROM sheet
    WHERE id = _id;
END//

DELIMITER ;

DELIMITER //

CREATE PROCEDURE spSheet_ReadAll()
BEGIN
    SELECT *
    FROM sheet;
END//

DELIMITER ;

DELIMITER //

CREATE PROCEDURE spSheet_Update(IN id INT, IN _userID INT unsigned, IN _dateID INT unsigned)
BEGIN
    UPDATE sheet
    SET userID = _userID, dateID = _dateID
    WHERE id = id;
END//

DELIMITER ;

DELIMITER //

CREATE PROCEDURE spSheet_Delete(IN _id INT)
BEGIN
    DELETE FROM sheet
    WHERE id = _id;
END//

DELIMITER ;