﻿--
-- Script was generated by Devart dbForge Studio 2020 for MySQL, Version 9.0.338.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 8/4/2023 2:30:02 PM
-- Server version: 8.0.33
-- Client version: 4.1
--

-- 
-- Disable foreign keys
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Set SQL mode
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE misa_web05_qlts;

--
-- Drop procedure `Proc_GetFixedAssetPaging`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAssetPaging;

--
-- Drop procedure `Proc_GetFixedAssetTotalFilter`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAssetTotalFilter;

--
-- Drop procedure `Proc_CreateDepartment`
--
DROP PROCEDURE IF EXISTS Proc_CreateDepartment;

--
-- Drop procedure `Proc_DeleteDepartment`
--
DROP PROCEDURE IF EXISTS Proc_DeleteDepartment;

--
-- Drop procedure `Proc_GetDepartment`
--
DROP PROCEDURE IF EXISTS Proc_GetDepartment;

--
-- Drop procedure `Proc_GetDepartmentByCode`
--
DROP PROCEDURE IF EXISTS Proc_GetDepartmentByCode;

--
-- Drop procedure `Proc_GetDepartments`
--
DROP PROCEDURE IF EXISTS Proc_GetDepartments;

--
-- Drop procedure `Proc_UpdateDepartment`
--
DROP PROCEDURE IF EXISTS Proc_UpdateDepartment;

--
-- Drop table `department`
--
DROP TABLE IF EXISTS department;

--
-- Drop procedure `Proc_CreateFixedAsset`
--
DROP PROCEDURE IF EXISTS Proc_CreateFixedAsset;

--
-- Drop procedure `Proc_DeleteFixedAsset`
--
DROP PROCEDURE IF EXISTS Proc_DeleteFixedAsset;

--
-- Drop procedure `Proc_GetFixedAsset`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAsset;

--
-- Drop procedure `Proc_GetFixedAssetByCode`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAssetByCode;

--
-- Drop procedure `Proc_GetFixedAssets`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAssets;

--
-- Drop procedure `Proc_UpdateFixedAsset`
--
DROP PROCEDURE IF EXISTS Proc_UpdateFixedAsset;

--
-- Drop procedure `Proc_GetFixedAssetCodeNew`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAssetCodeNew;

--
-- Drop function `Func_GetFixedAssetCode`
--
DROP FUNCTION IF EXISTS Func_GetFixedAssetCode;

--
-- Drop table `fixed_asset`
--
DROP TABLE IF EXISTS fixed_asset;

--
-- Drop procedure `Proc_CreateFixedAssetCategory`
--
DROP PROCEDURE IF EXISTS Proc_CreateFixedAssetCategory;

--
-- Drop procedure `Proc_DeleteFixedAssetCategory`
--
DROP PROCEDURE IF EXISTS Proc_DeleteFixedAssetCategory;

--
-- Drop procedure `Proc_GetFixedAssetCategories`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAssetCategories;

--
-- Drop procedure `Proc_GetFixedAssetCategory`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAssetCategory;

--
-- Drop procedure `Proc_GetFixedAssetCategoryByCode`
--
DROP PROCEDURE IF EXISTS Proc_GetFixedAssetCategoryByCode;

--
-- Drop procedure `Proc_UpdateFixedAssetCategory`
--
DROP PROCEDURE IF EXISTS Proc_UpdateFixedAssetCategory;

--
-- Drop table `fixed_asset_category`
--
DROP TABLE IF EXISTS fixed_asset_category;

--
-- Set default database
--
USE misa_web05_qlts;

--
-- Create table `fixed_asset_category`
--
CREATE TABLE fixed_asset_category (
  FixedAssetCategoryId char(36) NOT NULL COMMENT 'Id loại tài sản',
  FixedAssetCategoryCode varchar(20) NOT NULL DEFAULT '' COMMENT 'Mã loại tài sản',
  FixedAssetCategoryName varchar(100) NOT NULL DEFAULT '' COMMENT 'Tên loại tài sản',
  OrganizationId char(36) DEFAULT NULL COMMENT 'Id của đơn vị',
  DepreciationRate float UNSIGNED NOT NULL COMMENT 'Tỷ lệ hao mòn (%)',
  LifeTime int UNSIGNED NOT NULL COMMENT 'Số năm sử dụng',
  Description varchar(255) DEFAULT NULL COMMENT 'Ghi chú',
  CreatedBy varchar(50) DEFAULT NULL COMMENT 'Người tạo',
  CreatedDate timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày tạo',
  ModifiedBy varchar(50) DEFAULT NULL COMMENT 'Người sửa',
  ModifiedDate timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày sửa',
  PRIMARY KEY (FixedAssetCategoryId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 2730,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
COMMENT = 'Loại tài sản';

--
-- Create index `FixedAssetCategoryCode` on table `fixed_asset_category`
--
ALTER TABLE fixed_asset_category
ADD UNIQUE INDEX FixedAssetCategoryCode (FixedAssetCategoryCode);

DELIMITER $$

--
-- Create procedure `Proc_UpdateFixedAssetCategory`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_UpdateFixedAssetCategory (IN FixedAssetCategoryId char(36), IN FixedAssetCategoryCode varchar(20),
IN FixedAssetCategoryName varchar(100), IN DepreciationRate float, IN LifeTime int)
COMMENT 'Cập nhập một loại tài sản'
BEGIN
  UPDATE fixed_asset_category fac
  SET fac.FixedAssetCategoryCode = FixedAssetCategoryCode,
      fac.FixedAssetCategoryName = FixedAssetCategoryName,
      fac.DepreciationRate = DepreciationRate,
      fac.LifeTime = LifeTime,
      fac.ModifiedDate = NOW(),
      fac.ModifiedBy = 'BHTuyen'
  WHERE fac.FixedAssetCategoryId = FixedAssetCategoryId;
END
$$

--
-- Create procedure `Proc_GetFixedAssetCategoryByCode`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAssetCategoryByCode (IN FixedAssetCategoryCode varchar(20))
COMMENT 'Lấy ra một loại tài sản theo mã loại tài sản'
BEGIN
  SELECT
    *
  FROM fixed_asset_category fac
  WHERE fac.FixedAssetCategoryCode = FixedAssetCategoryCode;
END
$$

--
-- Create procedure `Proc_GetFixedAssetCategory`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAssetCategory (IN FixedAssetCategoryId char(36))
COMMENT 'Lấy ra một loại tài sản theo Id'
BEGIN
  SELECT
    *
  FROM fixed_asset_category fac
  WHERE fac.FixedAssetCategoryId = FixedAssetCategoryId;
END
$$

--
-- Create procedure `Proc_GetFixedAssetCategories`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAssetCategories ()
COMMENT 'Lấy ra tất cả loại tài sản'
BEGIN
  SELECT
    *
  FROM fixed_asset_category fac
  ORDER BY fac.ModifiedDate DESC;
END
$$

--
-- Create procedure `Proc_DeleteFixedAssetCategory`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_DeleteFixedAssetCategory (IN FixedAssetCategoryId char(36))
COMMENT 'Xóa một loại tài sản theo Id'
BEGIN
  DELETE
    FROM fixed_asset_category
  WHERE fixed_asset_category.FixedAssetCategoryId = FixedAssetCategoryId;
END
$$

--
-- Create procedure `Proc_CreateFixedAssetCategory`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_CreateFixedAssetCategory (IN FixedAssetCategoryId char(36), IN FixedAssetCategoryCode varchar(20),
IN FixedAssetCategoryName varchar(100), IN OrganizationId char(36), IN DepreciationRate float, IN LifeTime int, IN Description varchar(255),
IN CreatedBy varchar(50), IN CreatedDate timestamp, IN ModifiedBy varchar(50), IN ModifiedDate timestamp)
COMMENT 'Tạo mới một loại tài sản'
BEGIN
  INSERT INTO fixed_asset_category (FixedAssetCategoryId, FixedAssetCategoryCode, FixedAssetCategoryName, OrganizationId, DepreciationRate,
  LifeTime, Description, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
    VALUES (FixedAssetCategoryId, FixedAssetCategoryCode, FixedAssetCategoryName, OrganizationId, DepreciationRate, LifeTime, Description, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate);
END
$$

DELIMITER ;

--
-- Create table `fixed_asset`
--
CREATE TABLE fixed_asset (
  FixedAssetId char(36) NOT NULL COMMENT 'Id tài sản',
  FixedAssetCode varchar(20) NOT NULL DEFAULT '' COMMENT 'Mã tài sản',
  FixedAssetName varchar(100) NOT NULL DEFAULT '' COMMENT 'Tên tài sản',
  OrganizationId char(36) DEFAULT NULL COMMENT 'Id của đơn vị',
  OrganizationCode varchar(20) DEFAULT NULL COMMENT 'Mã đơn vị',
  OrganizationName varchar(100) DEFAULT NULL COMMENT 'Tên của đơn vị',
  DepartmentId char(36) NOT NULL DEFAULT '' COMMENT 'Id phòng ban',
  DepartmentCode varchar(20) NOT NULL DEFAULT '' COMMENT 'Mã phòng ban',
  DepartmentName varchar(100) NOT NULL DEFAULT '' COMMENT 'Tên phòng ban',
  FixedAssetCategoryId char(36) NOT NULL DEFAULT '' COMMENT 'Id loại tài sản',
  FixedAssetCategoryCode varchar(20) NOT NULL DEFAULT '' COMMENT 'Mã loại tài sản',
  FixedAssetCategoryName varchar(100) NOT NULL DEFAULT '' COMMENT 'Tên loại tài sản',
  PurchaseDate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày mua',
  UsingStartedDate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày bắt đầu sử dụng',
  Cost decimal(19, 4) UNSIGNED NOT NULL COMMENT 'Nguyên giá',
  Quantity int UNSIGNED NOT NULL DEFAULT 0 COMMENT 'Số lượng',
  DepreciationRate float UNSIGNED NOT NULL COMMENT 'Tỷ lệ hao mòn (%)',
  TrackedYear int UNSIGNED NOT NULL COMMENT 'Năm bắt đầu theo dõi tài sản trên phần mềm',
  LifeTime int UNSIGNED NOT NULL COMMENT 'Số năm sử dụng',
  ProductionYear int UNSIGNED DEFAULT NULL COMMENT 'Năm sản xuất',
  Active bit(1) DEFAULT b'0' COMMENT 'Sử dụng',
  CreatedBy varchar(50) DEFAULT NULL COMMENT 'Người tạo',
  CreatedDate timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày tạo',
  ModifiedBy varchar(50) DEFAULT NULL COMMENT 'Người sửa',
  ModifiedDate timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày sửa',
  PRIMARY KEY (FixedAssetId)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
COMMENT = 'Tài sản';

--
-- Create index `FixedAssetCode` on table `fixed_asset`
--
ALTER TABLE fixed_asset
ADD UNIQUE INDEX FixedAssetCode (FixedAssetCode);

--
-- Create foreign key
--
ALTER TABLE fixed_asset
ADD CONSTRAINT FK_fixed_asset_FixedAssetCategoryId FOREIGN KEY (FixedAssetCategoryId)
REFERENCES fixed_asset_category (FixedAssetCategoryId);

DELIMITER $$

--
-- Create function `Func_GetFixedAssetCode`
--
CREATE DEFINER = 'root'@'localhost'
FUNCTION Func_GetFixedAssetCode ()
RETURNS varchar(20) CHARSET utf8mb4
READS SQL DATA
COMMENT 'Sinh mã tài sản gợi ý'
BEGIN
  DECLARE FixedAssetCodeOld varchar(20);
  -- Lấy ra mã tài sản được sửa đổi gần nhất
  SET FixedAssetCodeOld = (SELECT
      fa.FixedAssetCode
    FROM fixed_asset fa
    ORDER BY fa.ModifiedDate DESC LIMIT 1);

  -- Nếu không tồn lại thì sinh mặc định là 01
  IF (NOT EXISTS (SELECT
        *
      FROM fixed_asset fa)) THEN
    RETURN '01';
  END IF;

  -- Kiểm tra đến khi mã tài sản không còn bị trùng 
  WHILE
    (FixedAssetCodeOld IN (SELECT
        fa.FixedAssetCode
      FROM fixed_asset fa)) DO
    IF REGEXP_SUBSTR(FixedAssetCodeOld, '(?!0)([0-9]*$)') = '' THEN
      SET FixedAssetCodeOld = REGEXP_REPLACE(FixedAssetCodeOld, '(?!0)([0-9]*$)', CONVERT(CONVERT(REGEXP_SUBSTR(FixedAssetCodeOld, '(?!0)([0-9]*)'), binary) + 1, char(20)));
    ELSE
      SET FixedAssetCodeOld = REGEXP_REPLACE(FixedAssetCodeOld, '(?!0)([0-9]+$)', CONVERT(CONVERT(REGEXP_SUBSTR(FixedAssetCodeOld, '(?!0)([0-9]+$)'), binary) + 1, char(20)));
    END IF;
  END WHILE;

  RETURN FixedAssetCodeOld;

END
$$

--
-- Create procedure `Proc_GetFixedAssetCodeNew`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAssetCodeNew ()
COMMENT 'Sinh mã tài sản mới gợi ý'
BEGIN
  SELECT
    Func_GetFixedAssetCode();
END
$$

--
-- Create procedure `Proc_UpdateFixedAsset`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_UpdateFixedAsset (IN FixedAssetId char(36), IN FixedAssetCode varchar(20), IN FixedAssetName varchar(100),
IN OrganizationId char(36), IN OrganizationCode varchar(20), IN OrganizationName varchar(100),
IN DepartmentId char(36), IN DepartmentCode varchar(20), IN DepartmentName varchar(100), IN FixedAssetCategoryId char(36),
IN FixedAssetCategoryCode varchar(20), IN FixedAssetCategoryName varchar(100), IN PurchaseDate timestamp, IN UsingStartedDate timestamp,
IN Cost decimal(19, 4), IN Quantity int, IN DepreciationRate float, IN TrackedYear int, IN LifeTime int, IN ProductionYear int, IN Active bit(1),
IN ModifiedBy varchar(50), IN ModifiedDate timestamp)
COMMENT 'Cập nhật một tài sản'
BEGIN
  UPDATE fixed_asset fa
  SET fa.FixedAssetCode = FixedAssetCode,
      fa.FixedAssetName = FixedAssetName,
      fa.OrganizationId = OrganizationId,
      fa.OrganizationCode = OrganizationName,
      fa.OrganizationName = OrganizationName,
      fa.DepartmentId = DepartmentId,
      fa.DepartmentCode = DepartmentCode,
      fa.DepartmentName = DepartmentName,
      fa.FixedAssetCategoryId = FixedAssetCategoryId,
      fa.FixedAssetCategoryCode = FixedAssetCategoryCode,
      fa.FixedAssetCategoryName = FixedAssetCategoryName,
      fa.PurChaseDate = PurChaseDate,
      fa.UsingStartedDate = UsingStartedDate,
      fa.Cost = Cost,
      fa.Quantity = Quantity,
      fa.DepreciationRate = DepreciationRate,
      fa.TrackedYear = TrackedYear,
      fa.LifeTime = LifeTime,
      fa.ProductionYear = ProductionYear,
      fa.Active = Active,
      fa.ModifiedDate = ModifiedDate,
      fa.ModifiedBy = ModifiedBy
  WHERE fa.FixedAssetId = FixedAssetId;
END
$$

--
-- Create procedure `Proc_GetFixedAssets`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAssets ()
COMMENT 'Lấy ra tấy cả tài sản'
BEGIN
  SELECT
    *
  FROM fixed_asset fa
  ORDER BY fa.ModifiedDate DESC;
END
$$

--
-- Create procedure `Proc_GetFixedAssetByCode`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAssetByCode (IN FixedAssetCode varchar(20))
COMMENT 'Lấy ra một tài sản theo mã tài sản'
BEGIN
  SELECT
    *
  FROM fixed_asset fa
  WHERE fa.FixedAssetCode = FixedAssetCode;
END
$$

--
-- Create procedure `Proc_GetFixedAsset`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAsset (IN FixedAssetId char(36))
COMMENT 'Lấy ra một tài sản theo Id của tài sản'
BEGIN
  SELECT
    *
  FROM fixed_asset fa
  WHERE fa.FixedAssetId = FixedAssetId;
END
$$

--
-- Create procedure `Proc_DeleteFixedAsset`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_DeleteFixedAsset (IN FixedAssetId char(36))
COMMENT 'Xóa một tài sản theo Id'
BEGIN
  DELETE
    FROM fixed_asset
  WHERE fixed_asset.FixedAssetId = FixedAssetId;
END
$$

--
-- Create procedure `Proc_CreateFixedAsset`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_CreateFixedAsset (IN FixedAssetId char(36), IN FixedAssetCode varchar(20), IN FixedAssetName varchar(100),
IN OrganizationId char(36), IN OrganizationCode varchar(20), IN OrganizationName varchar(100),
IN DepartmentId char(36), IN DepartmentCode varchar(20), IN DepartmentName varchar(100), IN FixedAssetCategoryId char(36),
IN FixedAssetCategoryCode varchar(20), IN FixedAssetCategoryName varchar(100), IN PurchaseDate timestamp, IN UsingStartedDate timestamp,
IN Cost decimal(19, 4), IN Quantity int, IN DepreciationRate float, IN TrackedYear int, IN LifeTime int, IN ProductionYear int, IN Active bit(1),
IN CreatedBy varchar(50), IN CreatedDate timestamp, IN ModifiedBy varchar(50), IN ModifiedDate timestamp)
COMMENT 'Tạo mới một tài sản'
BEGIN
  INSERT INTO fixed_asset (FixedAssetId, FixedAssetCode, FixedAssetName, OrganizationId, OrganizationCode, OrganizationName, DepartmentId,
  DepartmentCode, DepartmentName, FixedAssetCategoryId, FixedAssetCategoryCode, FixedAssetCategoryName, PurchaseDate, UsingStartedDate, Cost,
  Quantity, DepreciationRate, TrackedYear, LifeTime, ProductionYear, Active, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
    VALUES (FixedAssetId, FixedAssetCode, FixedAssetName, OrganizationId, OrganizationCode, OrganizationName, DepartmentId, DepartmentCode, DepartmentName, FixedAssetCategoryId, FixedAssetCategoryCode, FixedAssetCategoryName, PurchaseDate, UsingStartedDate, Cost, Quantity, DepreciationRate, TrackedYear, LifeTime, ProductionYear, Active, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate);
END
$$

DELIMITER ;

--
-- Create table `department`
--
CREATE TABLE department (
  DepartmentId char(36) NOT NULL COMMENT 'Id của phòng ban',
  DepartmentCode varchar(20) NOT NULL DEFAULT '' COMMENT 'Mã của phòng ban',
  DepartmentName varchar(100) NOT NULL DEFAULT '' COMMENT 'Tên phòng ban',
  Description varchar(255) DEFAULT NULL COMMENT 'Ghi chú',
  IsParent bit(1) DEFAULT NULL COMMENT 'Có phải là cha không',
  ParentId char(36) DEFAULT NULL COMMENT 'Id phòng ban cha',
  OrganizationId char(36) DEFAULT NULL COMMENT 'Id của đơn vị',
  CreatedBy varchar(50) DEFAULT NULL COMMENT 'Người tạo',
  CreatedDate timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày tạo',
  ModifiedBy varchar(50) DEFAULT NULL COMMENT 'Người sửa',
  ModifiedDate timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày sửa',
  PRIMARY KEY (DepartmentId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 1820,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
COMMENT = 'Danh mục phòng ban';

--
-- Create index `DepartmentCode` on table `department`
--
ALTER TABLE department
ADD UNIQUE INDEX DepartmentCode (DepartmentCode);

DELIMITER $$

--
-- Create procedure `Proc_UpdateDepartment`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_UpdateDepartment (IN DepartmentId char(36), IN DepartmentCode varchar(20), IN DepartmentName varchar(100),
IN Description varchar(255), IN IsParent bit(1), IN ParentId char(36), IN OrganizationId char(36), IN ModifiedBy varchar(50), IN ModifiedDate timestamp)
COMMENT 'Cập nhật dữ liệu một phòng ban'
BEGIN
  UPDATE department d
  SET d.DepartmentCode = DepartmentCode,
      d.DepartmentName = DepartmentName,
      d.Description = Description,
      d.IsParent = IsParent,
      d.ParentId = ParentId,
      d.OrganizationId = OrganizationId,
      d.ModifiedBy = ModifiedBy,
      d.ModifiedDate = ModifiedDate
  WHERE d.DepartmentId = DepartmentId;
END
$$

--
-- Create procedure `Proc_GetDepartments`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetDepartments ()
COMMENT 'Lấy ra tất cả phòng ban'
BEGIN
  SELECT
    *
  FROM department d
  ORDER BY d.ModifiedDate DESC;
END
$$

--
-- Create procedure `Proc_GetDepartmentByCode`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetDepartmentByCode (IN DepartmentCode varchar(20))
COMMENT 'Lấy ra một phòng ban theo mã phòng ban'
BEGIN
  SELECT
    *
  FROM department d
  WHERE d.DepartmentCode = DepartmentCode;
END
$$

--
-- Create procedure `Proc_GetDepartment`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetDepartment (IN DepartmentId char(36))
COMMENT 'Lấy ra một phòng ban theo id của phòng ban'
BEGIN

  SELECT
    *
  FROM department d
  WHERE d.DepartmentId = DepartmentId;
END
$$

--
-- Create procedure `Proc_DeleteDepartment`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_DeleteDepartment (IN DepartmentId char(36))
COMMENT 'Xóa một phòng ban'
BEGIN
  DELETE
    FROM department
  WHERE department.DepartmentId = DepartmentId;
END
$$

--
-- Create procedure `Proc_CreateDepartment`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_CreateDepartment (IN DepartmentId char(36), IN DepartmentCode varchar(20), IN DepartmentName varchar(100),
IN Description varchar(255), IN IsParent bit(1), IN ParentId char(36), IN OrganizationId char(36),
IN CreatedBy varchar(50), IN CreatedDate timestamp, IN ModifiedBy varchar(50), IN ModifiedDate timestamp)
COMMENT 'Tạo mới một phòng ban'
BEGIN
  INSERT INTO department (DepartmentId, DepartmentCode, DepartmentName, Description, IsParent, ParentId, OrganizationId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
    VALUES (DepartmentId, DepartmentCode, DepartmentName, Description, IsParent, ParentId, OrganizationId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate);

END
$$

--
-- Create procedure `Proc_GetFixedAssetTotalFilter`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAssetTotalFilter (IN FixedAssetCodeOrName varchar(100), IN DepartmentName varchar(100),
IN FixedAssetCategoryName varchar(100))
COMMENT 'Lấy tổng số tài sản'
BEGIN
  -- Khởi tạo biến lưu câu truy vấn
  SET @WhereQuery = 'SELECT COUNT(*) FROM fixed_asset fa ';

  -- Cắt bỏ phần khoảng trắng đầu và cuối
  SET FixedAssetCodeOrName = TRIM(FixedAssetCodeOrName);
  SET DepartmentName = TRIM(DepartmentName);
  SET FixedAssetCategoryName = TRIM(FixedAssetCategoryName);

  -- Nếu FixedAssetCodeOrName rỗng
  IF (ISNULL(FixedAssetCodeOrName)) THEN
    SET FixedAssetCodeOrName = '';
  END IF;

  -- Nếu DepartmentName rỗng
  IF (ISNULL(DepartmentName)) THEN
    SET DepartmentName = '';
  END IF;

  -- Nếu FixedAssetCategoryName rỗng
  IF (ISNULL(FixedAssetCategoryName)) THEN
    SET FixedAssetCategoryName = '';
  END IF;

  -- Chỉ search theo FixedAssetCodeOrName
  IF (FixedAssetCodeOrName <> ''
    AND DepartmentName = ''
    AND FixedAssetCategoryName = '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE fa.FixedAssetCode = ', QUOTE(FixedAssetCodeOrName), ' OR fa.FixedAssetName = ',
    QUOTE(FixedAssetCodeOrName));

  -- Chỉ search theo FixedAssetCodeOrName và DepartmentName
  ELSEIF (FixedAssetCodeOrName <> ''
    AND DepartmentName <> ''
    AND FixedAssetCategoryName = '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE (fa.FixedAssetCode = ', QUOTE(FixedAssetCodeOrName), ' OR fa.FixedAssetName = ',
    QUOTE(FixedAssetCodeOrName), ') AND fa.DepartmentName = ', QUOTE(DepartmentName));

  -- Chỉ search theo FixedAssetCodeOrName và FixedAssetCategoryName
  ELSEIF (FixedAssetCodeOrName <> ''
    AND DepartmentName = ''
    AND FixedAssetCategoryName <> '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE (fa.FixedAssetCode = ', QUOTE(FixedAssetCodeOrName), ' OR fa.FixedAssetName = ',
    QUOTE(FixedAssetCodeOrName), ') AND fa.FixedAssetCategoryName = ', QUOTE(FixedAssetCategoryName));

  -- Chỉ search theo DepartmentName và FixedAssetCategoryName
  ELSEIF (FixedAssetCodeOrName = ''
    AND DepartmentName <> ''
    AND FixedAssetCategoryName <> '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE fa.DepartmentName = ', QUOTE(DepartmentName),
    ' AND fa.FixedAssetCategoryName = ', QUOTE(FixedAssetCategoryName));

  -- Chỉ search theo DepartmentName
  ELSEIF (FixedAssetCodeOrName = ''
    AND DepartmentName <> ''
    AND FixedAssetCategoryName = '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE fa.DepartmentName = ', QUOTE(DepartmentName));

  -- Chỉ search theo FixedAssetCategoryName
  ELSEIF (FixedAssetCodeOrName = ''
    AND DepartmentName = ''
    AND FixedAssetCategoryName <> '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE fa.FixedAssetCategoryName = ', QUOTE(FixedAssetCategoryName));

  -- Search theo FixedAssetCodeOrName và DepartmentName và FixedAssetCategoryName
  ELSEIF (FixedAssetCodeOrName <> ''
    AND DepartmentName <> ''
    AND FixedAssetCategoryName <> '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE (fa.FixedAssetCode = ', QUOTE(FixedAssetCodeOrName), ' OR fa.FixedAssetName = ',
    QUOTE(FixedAssetCodeOrName), ') AND fa.DepartmentName = ', QUOTE(DepartmentName),
    ' AND fa.FixedAssetCategoryName = ', QUOTE(FixedAssetCategoryName));
  END IF;

  -- Thực thi câu truy vấn
  PREPARE query FROM @WhereQuery;
  EXECUTE query;
  DEALLOCATE PREPARE query;
END
$$

--
-- Create procedure `Proc_GetFixedAssetPaging`
--
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Proc_GetFixedAssetPaging (IN FixedAssetCodeOrName varchar(100), IN DepartmentName varchar(100),
IN FixedAssetCategoryName varchar(100), IN PageLimit int, IN PageNumber int)
COMMENT 'Phân trang tài sản'
BEGIN
  -- Khởi tạo biến lưu câu truy vấn
  SET @RowIndex = 0;
  SET @WhereQuery = 'SELECT (@RowIndex:=@RowIndex+1) AS RowIndex, fa.* FROM fixed_asset fa ';

  -- Cắt bỏ phần khoảng trắng đầu và cuối
  SET FixedAssetCodeOrName = TRIM(FixedAssetCodeOrName);
  SET DepartmentName = TRIM(DepartmentName);
  SET FixedAssetCategoryName = TRIM(FixedAssetCategoryName);

  -- Nếu FixedAssetCodeOrName rỗng
  IF (ISNULL(FixedAssetCodeOrName)) THEN
    SET FixedAssetCodeOrName = '';
  END IF;

  -- Nếu DepartmentName rỗng
  IF (ISNULL(DepartmentName)) THEN
    SET DepartmentName = '';
  END IF;

  -- Nếu FixedAssetCategoryName rỗng
  IF (ISNULL(FixedAssetCategoryName)) THEN
    SET FixedAssetCategoryName = '';
  END IF;

  -- Chỉ search theo FixedAssetCodeOrName
  IF (FixedAssetCodeOrName <> ''
    AND DepartmentName = ''
    AND FixedAssetCategoryName = '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE fa.FixedAssetCode = ', QUOTE(FixedAssetCodeOrName), ' OR fa.FixedAssetName = ',
    QUOTE(FixedAssetCodeOrName));

  -- Chỉ search theo FixedAssetCodeOrName và DepartmentName
  ELSEIF (FixedAssetCodeOrName <> ''
    AND DepartmentName <> ''
    AND FixedAssetCategoryName = '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE (fa.FixedAssetCode = ', QUOTE(FixedAssetCodeOrName), ' OR fa.FixedAssetName = ',
    QUOTE(FixedAssetCodeOrName), ') AND fa.DepartmentName = ', QUOTE(DepartmentName));

  -- Chỉ search theo FixedAssetCodeOrName và FixedAssetCategoryName
  ELSEIF (FixedAssetCodeOrName <> ''
    AND DepartmentName = ''
    AND FixedAssetCategoryName <> '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE (fa.FixedAssetCode = ', QUOTE(FixedAssetCodeOrName), ' OR fa.FixedAssetName = ',
    QUOTE(FixedAssetCodeOrName), ') AND fa.FixedAssetCategoryName = ', QUOTE(FixedAssetCategoryName));

  -- Chỉ search theo DepartmentName và FixedAssetCategoryName
  ELSEIF (FixedAssetCodeOrName = ''
    AND DepartmentName <> ''
    AND FixedAssetCategoryName <> '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE fa.DepartmentName = ', QUOTE(DepartmentName),
    ' AND fa.FixedAssetCategoryName = ', QUOTE(FixedAssetCategoryName));

  -- Chỉ search theo DepartmentName
  ELSEIF (FixedAssetCodeOrName = ''
    AND DepartmentName <> ''
    AND FixedAssetCategoryName = '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE fa.DepartmentName = ', QUOTE(DepartmentName));

  -- Chỉ search theo FixedAssetCategoryName
  ELSEIF (FixedAssetCodeOrName = ''
    AND DepartmentName = ''
    AND FixedAssetCategoryName <> '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE fa.FixedAssetCategoryName = ', QUOTE(FixedAssetCategoryName));

  -- Search theo FixedAssetCodeOrName và DepartmentName và FixedAssetCategoryName
  ELSEIF (FixedAssetCodeOrName <> ''
    AND DepartmentName <> ''
    AND FixedAssetCategoryName <> '') THEN
    SET @WhereQuery = CONCAT(@WhereQuery, 'WHERE (fa.FixedAssetCode = ', QUOTE(FixedAssetCodeOrName), ' OR fa.FixedAssetName = ',
    QUOTE(FixedAssetCodeOrName), ') AND fa.DepartmentName = ', QUOTE(DepartmentName),
    ' AND fa.FixedAssetCategoryName = ', QUOTE(FixedAssetCategoryName));
  END IF;

  -- Lấy theo ngày sửa đổi gần nhất
  SET @WhereQuery = CONCAT(@WhereQuery, ' ORDER BY fa.ModifiedDate DESC');


  -- Lấy theo số trang và số bản ghi trong một trang
  SET @WhereQuery = CONCAT('SELECT * FROM (', @WhereQuery, ') AS Asset WHERE Asset.RowIndex >= ', (PageNumber - 1) * PageLimit + 1,
  ' AND Asset.RowIndex <= ', PageNumber * PageLimit);

  -- Thực thi câu truy vấn
  PREPARE query FROM @WhereQuery;
  EXECUTE query;
  DEALLOCATE PREPARE query;
END
$$

DELIMITER ;

-- 
-- Dumping data for table fixed_asset_category
--
INSERT INTO fixed_asset_category VALUES
('22732062-2efb-11ee-b538-5414f3b68d06', 'TSCA', 'Tài sản cá nhân', '', 5, 20, '', 'BHTuyen', '2023-07-31 00:04:24', 'BHTuyen', '2023-07-31 00:04:24'),
('31a41923-2efb-11ee-b538-5414f3b68d06', 'TSCC', 'Tài sản công cộng', '', 10, 10, '', 'BHTuyen', '2023-07-31 00:04:49', 'BHTuyen', '2023-07-31 00:04:49'),
('4c742964-2efb-11ee-b538-5414f3b68d06', 'TSDN', 'Tài sản doanh nghiệp', '', 6.67, 15, '', 'BHTuyen', '2023-07-31 00:05:34', 'BHTuyen', '2023-07-31 00:05:34'),
('5e23b7bd-2efb-11ee-b538-5414f3b68d06', 'TSNN', 'Tài sản nhà nước', '', 6.25, 16, '', 'BHTuyen', '2023-07-31 00:06:04', 'BHTuyen', '2023-07-31 00:06:04'),
('75c21c62-2efb-11ee-b538-5414f3b68d06', 'TSNK', 'Tài sản nhập khẩu', '', 3.33, 30, '', 'BHTuyen', '2023-07-31 00:06:44', 'BHTuyen', '2023-07-31 00:06:44'),
('8289fc4d-2efb-11ee-b538-5414f3b68d06', 'TSXK', 'Tài sản xuất khẩu', '', 4, 25, '', 'BHTuyen', '2023-07-31 00:07:05', 'BHTuyen', '2023-07-31 00:07:05');

-- 
-- Dumping data for table fixed_asset
--
INSERT INTO fixed_asset VALUES
('1219a8e4-cd81-4128-997e-f684bfe26266', 'VN000013', 'Máy pha cà phê', NULL, NULL, NULL, 'd700fb6c-2efa-11ee-b538-5414f3b68d06', 'PMAK', 'Phòng Marketing', '4c742964-2efb-11ee-b538-5414f3b68d06', 'TSDN', 'Tài sản doanh nghiệp', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 50000000.0000, 10, 10, 2023, 10, NULL, NULL, 'BHTuyen', '2023-08-04 13:59:42', 'BHTuyen', '2023-08-04 13:59:42'),
('2aab65ab-e650-4174-998e-a4e0f45f2ede', 'VN000015', 'Ô che mưa cỡ lớn', NULL, NULL, NULL, 'bed61b77-2efa-11ee-b538-5414f3b68d06', 'PCSKH', 'Phòng chăm sóc khách hàng', '75c21c62-2efb-11ee-b538-5414f3b68d06', 'TSNK', 'Tài sản nhập khẩu', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 600000.0000, 1406, 4, 2023, 25, NULL, NULL, 'BHTuyen', '2023-08-04 14:00:45', 'BHTuyen', '2023-08-04 14:00:45'),
('33e79fc9-9492-4740-abc3-1deb2a890698', 'VN00009', 'Máy pha cà phê', NULL, NULL, NULL, 'd700fb6c-2efa-11ee-b538-5414f3b68d06', 'PMAK', 'Phòng Marketing', '4c742964-2efb-11ee-b538-5414f3b68d06', 'TSDN', 'Tài sản doanh nghiệp', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 50000000.0000, 10, 10, 2023, 10, NULL, NULL, 'BHTuyen', '2023-08-04 13:59:30', 'BHTuyen', '2023-08-04 13:59:30'),
('36aa7efb-7542-46d7-a7b3-cb69d57e3a6d', 'VN000018', 'Máy pha cà phê', NULL, NULL, NULL, '8b7c6a5a-2efa-11ee-b538-5414f3b68d06', 'PKT', 'Phòng kế toán', '31a41923-2efb-11ee-b538-5414f3b68d06', 'TSCC', 'Tài sản công cộng', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 50000000.0000, 10, 10, 2023, 10, NULL, NULL, 'BHTuyen', '2023-08-04 14:01:17', 'BHTuyen', '2023-08-04 14:01:17'),
('389860db-dd1f-45aa-a2a4-92d1d1242788', 'VN000012', 'Ô che mưa cỡ lớn', NULL, NULL, NULL, 'c9ce56f4-2efa-11ee-b538-5414f3b68d06', 'PCNTT', 'Phòng công nghệ thông tin', '75c21c62-2efb-11ee-b538-5414f3b68d06', 'TSNK', 'Tài sản nhập khẩu', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 600000.0000, 1406, 3.33, 2023, 30, NULL, NULL, 'BHTuyen', '2023-08-04 13:59:39', 'BHTuyen', '2023-08-04 13:59:39'),
('3d3d2346-2c03-4427-8ca1-bee4cc1ee7c4', 'VN000011', 'Máy pha cà phê', NULL, NULL, NULL, 'd700fb6c-2efa-11ee-b538-5414f3b68d06', 'PMAK', 'Phòng Marketing', '4c742964-2efb-11ee-b538-5414f3b68d06', 'TSDN', 'Tài sản doanh nghiệp', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 50000000.0000, 10, 10, 2023, 10, NULL, NULL, 'BHTuyen', '2023-08-04 13:59:36', 'BHTuyen', '2023-08-04 13:59:36'),
('732be299-5452-4a3c-93f7-9e0b8d3216d9', 'VN000014', 'Máy pha cà phê', NULL, NULL, NULL, 'd700fb6c-2efa-11ee-b538-5414f3b68d06', 'PMAK', 'Phòng Marketing', '4c742964-2efb-11ee-b538-5414f3b68d06', 'TSDN', 'Tài sản doanh nghiệp', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 500000000.0000, 10, 5, 2023, 20, NULL, NULL, 'BHTuyen', '2023-08-04 14:00:35', 'BHTuyen', '2023-08-04 14:00:35'),
('8bcc244d-24ce-4f53-9258-8610358da161', 'VN000017', 'Ô che mưa cỡ lớn', NULL, NULL, NULL, 'c9ce56f4-2efa-11ee-b538-5414f3b68d06', 'PCNTT', 'Phòng công nghệ thông tin', '75c21c62-2efb-11ee-b538-5414f3b68d06', 'TSNK', 'Tài sản nhập khẩu', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 600000.0000, 500, 3.33, 2023, 30, NULL, NULL, 'BHTuyen', '2023-08-04 14:01:06', 'BHTuyen', '2023-08-04 14:01:06'),
('8d0059cb-26d2-46ff-a835-b64d7707d5ce', 'VN000016', 'Máy pha cà phê', NULL, NULL, NULL, 'd700fb6c-2efa-11ee-b538-5414f3b68d06', 'PMAK', 'Phòng Marketing', '75c21c62-2efb-11ee-b538-5414f3b68d06', 'TSNK', 'Tài sản nhập khẩu', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 50000000.0000, 29, 3.33, 2023, 30, NULL, NULL, 'BHTuyen', '2023-08-04 14:01:00', 'BHTuyen', '2023-08-04 14:01:00'),
('abe226bd-a00c-42ee-bd88-e1195fc0c793', 'VN00003', 'Máy pha cà phê', NULL, NULL, NULL, 'd700fb6c-2efa-11ee-b538-5414f3b68d06', 'PMAK', 'Phòng Marketing', '4c742964-2efb-11ee-b538-5414f3b68d06', 'TSDN', 'Tài sản doanh nghiệp', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 50000000.0000, 10, 10, 2023, 10, NULL, NULL, 'BHTuyen', '2023-08-04 05:33:38', 'BHTuyen', '2023-08-04 13:55:42'),
('b97d89e8-b001-40c2-9b28-e70f7ac3170a', 'VN00004', 'Ô che mưa cỡ lớn', NULL, NULL, NULL, 'c9ce56f4-2efa-11ee-b538-5414f3b68d06', 'PCNTT', 'Phòng công nghệ thông tin', '75c21c62-2efb-11ee-b538-5414f3b68d06', 'TSNK', 'Tài sản nhập khẩu', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 600000.0000, 1406, 3.33, 2023, 30, NULL, NULL, 'BHTuyen', '2023-08-04 13:59:19', 'BHTuyen', '2023-08-04 13:59:19'),
('c4035371-ccc2-4d0e-8300-bf41e64528e6', 'VN00008', 'Ô che mưa cỡ lớn', NULL, NULL, NULL, 'c9ce56f4-2efa-11ee-b538-5414f3b68d06', 'PCNTT', 'Phòng công nghệ thông tin', '75c21c62-2efb-11ee-b538-5414f3b68d06', 'TSNK', 'Tài sản nhập khẩu', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 600000.0000, 1406, 3.33, 2023, 30, NULL, NULL, 'BHTuyen', '2023-08-04 13:59:27', 'BHTuyen', '2023-08-04 13:59:27'),
('d92d9088-fed6-4619-9bbb-b1784109016e', 'VN00005', 'Máy pha cà phê', NULL, NULL, NULL, 'd700fb6c-2efa-11ee-b538-5414f3b68d06', 'PMAK', 'Phòng Marketing', '4c742964-2efb-11ee-b538-5414f3b68d06', 'TSDN', 'Tài sản doanh nghiệp', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 50000000.0000, 10, 10, 2023, 10, NULL, NULL, 'BHTuyen', '2023-08-04 13:59:23', 'BHTuyen', '2023-08-04 13:59:23'),
('ecb692d4-eebc-418d-ad0e-95693fa17839', 'VN000010', 'Ô che mưa cỡ lớn', NULL, NULL, NULL, 'c9ce56f4-2efa-11ee-b538-5414f3b68d06', 'PCNTT', 'Phòng công nghệ thông tin', '75c21c62-2efb-11ee-b538-5414f3b68d06', 'TSNK', 'Tài sản nhập khẩu', '2023-08-02 00:00:00', '2023-08-04 00:00:00', 600000.0000, 1406, 3.33, 2023, 30, NULL, NULL, 'BHTuyen', '2023-08-04 13:59:33', 'BHTuyen', '2023-08-04 13:59:33');

-- 
-- Dumping data for table department
--
INSERT INTO department VALUES
('8b7c6a5a-2efa-11ee-b538-5414f3b68d06', 'PKT', 'Phòng kế toán', '', False, '', '', 'BHTuyen', '2023-07-31 00:00:11', 'BHTuyen', '2023-07-31 00:00:11'),
('98692360-2efa-11ee-b538-5414f3b68d06', 'PNS', 'Phòng nhân sự', '', False, '', '', 'BHTuyen', '2023-07-31 00:00:32', 'BHTuyen', '2023-07-31 00:00:32'),
('9dddfd6a-2efa-11ee-b538-5414f3b68d06', 'PHC', 'Phòng hành chính', '', False, '', '', 'BHTuyen', '2023-07-31 00:00:42', 'BHTuyen', '2023-07-31 00:00:42'),
('bed61b77-2efa-11ee-b538-5414f3b68d06', 'PCSKH', 'Phòng chăm sóc khách hàng', '', False, '', '', 'BHTuyen', '2023-07-31 00:01:37', 'BHTuyen', '2023-07-31 00:01:37'),
('c9ce56f4-2efa-11ee-b538-5414f3b68d06', 'PCNTT', 'Phòng công nghệ thông tin', '', False, '', '', 'BHTuyen', '2023-07-31 00:01:55', 'BHTuyen', '2023-07-31 00:01:55'),
('d700fb6c-2efa-11ee-b538-5414f3b68d06', 'PMAK', 'Phòng Marketing', '', False, '', '', 'BHTuyen', '2023-07-31 00:02:17', 'BHTuyen', '2023-07-31 00:02:17'),
('e00b6dd9-2efa-11ee-b538-5414f3b68d06', 'PKD', 'Phòng kinh doanh', '', False, '', '', 'BHTuyen', '2023-07-31 00:02:33', 'BHTuyen', '2023-07-31 00:02:33');

-- 
-- Restore previous SQL mode
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Enable foreign keys
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;