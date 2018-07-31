-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: sys_employee
-- ------------------------------------------------------
-- Server version	8.0.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `authority`
--

DROP TABLE IF EXISTS `authority`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `authority` (
  `authorityId` varchar(45) NOT NULL,
  `authorityName` varchar(45) DEFAULT NULL,
  `description` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`authorityId`),
  UNIQUE KEY `authorityId_UNIQUE` (`authorityId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `authority`
--

LOCK TABLES `authority` WRITE;
/*!40000 ALTER TABLE `authority` DISABLE KEYS */;
INSERT INTO `authority` VALUES ('ad001','admin','管理者'),('mg001','manager','マネージャー'),('us001','employee','社員');
/*!40000 ALTER TABLE `authority` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `customer` (
  `customerId` varchar(45) NOT NULL COMMENT '顧客ID',
  `name` varchar(45) DEFAULT NULL COMMENT '顧客名',
  `address` varchar(45) DEFAULT NULL COMMENT '職場先',
  `description` varchar(45) DEFAULT NULL COMMENT '参考',
  PRIMARY KEY (`customerId`),
  UNIQUE KEY `customerId_UNIQUE` (`customerId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
INSERT INTO `customer` VALUES ('cus001','liefnet','〒102-0083 東京都千代田区麹町２丁目１４−２ 麹町NKビル5階','liefnet'),('cus002','deadnet','〒102-0083 東京都千代田区麹町２丁目１４−２ 麹町NKビル5階','deadnet');
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `employee` (
  `employeeId` varchar(45) NOT NULL,
  `managerId` varchar(45) DEFAULT NULL,
  `userName` varchar(45) NOT NULL,
  `passWord` varchar(255) NOT NULL,
  `authorityId` varchar(45) NOT NULL,
  `dateOfBirth` datetime DEFAULT NULL,
  `address` varchar(45) DEFAULT NULL,
  `personalNumber` varchar(45) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `kataName` varchar(100) DEFAULT NULL,
  `telephoneNumber` varchar(45) DEFAULT NULL,
  `personalDocumentPath` varchar(45) DEFAULT NULL,
  `mailAddress` varchar(45) DEFAULT NULL,
  `customerId` varchar(45) DEFAULT NULL,
  `accountBankInfo` varchar(255) DEFAULT NULL,
  `avatarFilePath` varchar(255) DEFAULT NULL,
  `depentdentFamily` int(11) DEFAULT '0',
  `entryDate` varchar(15) DEFAULT NULL,
  `leavingDate` varchar(15) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`employeeId`),
  UNIQUE KEY `idEmployee_UNIQUE` (`employeeId`),
  UNIQUE KEY `userName_UNIQUE` (`userName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='会社員情報';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES ('vis001','vis002','dongkx93','5d63445648f54e603da84d0d983423f8ecbe71cb77ab1c6faaff85623cf7ee4e','ad001','2018-07-26 00:00:00','東京足立区足立４丁目２−9','CDA12345678','Kieu Xuan Dong','キエウスアンドン','07039694501','Content\\EmployeeInformation\\vis001\\vis001','kd191093@gmial.com','cus002','ゆうちょう銀行','\\Content\\EmployeeInformation\\vis001\\vis001.png',0,'2018/07/24','2018/07/25','ノーコメント'),('vis002',NULL,'kimduyen20','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','mg001','1996-10-19 00:00:00','東京足立区足立４丁目２−9','CDA12345678','Nguyen Thi Kim Duyen','グエンティキムヅエン','07039694501','Content\\EmployeeInformation\\vis002\\vis002','kd191096@gmail.com','cus001','ゆうちょう銀行','\\Content\\EmployeeInformation\\vis002\\vis002.png',2,'2018-02-14',NULL,'ノーコメント'),('vis003','vis002','mien91','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','ad001','1991-10-19 00:00:00','東京足立区足立４丁目２−9','CDA12345678','Tran Van Mien',NULL,'07039694501',NULL,'mien91@gmail.com','cus001','ゆうちょう銀行',NULL,1,'2018/07/24','2018/07/31','帰国'),('vis004',NULL,'mk1993','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','us001','2018-07-26 00:00:00','東伊興2-4-9 ゴールデンゲイン竹の塚',NULL,'Kieu Xuan Manh',NULL,NULL,'Content\\EmployeeInformation\\vis004\\vis004','km191093@gmail.com',NULL,NULL,'',0,'2018/07/24','2018/07/31',NULL),('vis005',NULL,'mk1992','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','us001','2018-07-19 00:00:00','東伊興2-4-9 ゴールデンゲイン竹の塚',NULL,'Kieu Xuan Duc',NULL,'7039694501','Content\\EmployeeInformation\\vis005\\vis005','km191093@gmail.com',NULL,NULL,'',0,'2018/07/25',NULL,NULL),('vis008','vis002','kimlong22','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','ad001','2018-07-18 00:00:00','池上',NULL,'Kieu Xuan Long',NULL,'7039694501',NULL,'km191093@gmail.com','cus001',NULL,'',1,'2018/07/19',NULL,NULL);
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `paidholiday`
--

DROP TABLE IF EXISTS `paidholiday`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `paidholiday` (
  `paidHolidayId` varchar(45) NOT NULL,
  `emplyeeId` varchar(45) DEFAULT NULL,
  `startDate` varchar(30) NOT NULL,
  `endDate` varchar(30) NOT NULL,
  `reason` varchar(150) DEFAULT NULL,
  `description` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`paidHolidayId`),
  UNIQUE KEY `paidHolidayId_UNIQUE` (`paidHolidayId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='有給習得情報';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paidholiday`
--

LOCK TABLES `paidholiday` WRITE;
/*!40000 ALTER TABLE `paidholiday` DISABLE KEYS */;
/*!40000 ALTER TABLE `paidholiday` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `salary`
--

DROP TABLE IF EXISTS `salary`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `salary` (
  `salaryId` varchar(45) NOT NULL COMMENT '給料支払いId',
  `employeeId` varchar(45) NOT NULL,
  `paymentDate` varchar(30) DEFAULT NULL,
  `salaryDetailFilePath` varchar(100) DEFAULT NULL,
  `totalPaidAmount` double DEFAULT NULL,
  `deductionAmount` double DEFAULT NULL,
  `paidAmount` double DEFAULT NULL,
  `transportationExpenses` double DEFAULT NULL,
  `housingRental` double DEFAULT NULL,
  `overTimePayment` double DEFAULT NULL,
  `lateNightOverTimePayment` double DEFAULT NULL,
  `lateWorkDeduction` double DEFAULT NULL,
  `absentDeduction` double DEFAULT NULL,
  `description` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`salaryId`),
  UNIQUE KEY `idSalary_UNIQUE` (`salaryId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='給料明細情報';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `salary`
--

LOCK TABLES `salary` WRITE;
/*!40000 ALTER TABLE `salary` DISABLE KEYS */;
INSERT INTO `salary` VALUES ('vis001201801','vis001','2018/01/15','\\Content\\Salary\\201801\\001 KIEU XUAN DONG.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis001201807','vis001','2018/07/15','\\Content\\Salary\\201807\\001 KIEU XUAN DONG.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Dongkx'),('vis001201808','vis001','2018/08/15','\\Content\\Salary\\201808\\001 KIEU XUAN DONG.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis001201809','vis001','2018/09/15','\\Content\\Salary\\201809\\001 KIEU XUAN DONG.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis001201810','vis001','2018/10/15','\\Content\\Salary\\201810\\001 KIEU XUAN DONG.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis001201811','vis001','2018/11/15','\\Content\\Salary\\201811\\001 KIEU XUAN DONG.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis002201801','vis002','2018/01/15','\\Content\\Salary\\201801\\002 NGUYEN THI KIM DUYEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis002201807','vis002','2018/07/15','\\Content\\Salary\\201807\\002 NGUYEN THI KIM DUYEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),('vis002201808','vis002','2018/08/15','\\Content\\Salary\\201808\\002 NGUYEN THI KIM DUYEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis002201809','vis002','2018/09/15','\\Content\\Salary\\201809\\002 NGUYEN THI KIM DUYEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis002201810','vis002','2018/10/15','\\Content\\Salary\\201810\\002 NGUYEN THI KIM DUYEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis002201811','vis002','2018/11/15','\\Content\\Salary\\201811\\002 NGUYEN THI KIM DUYEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis003201801','vis003','2018/01/15','\\Content\\Salary\\201801\\003 TRAN VAN MIEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis003201807','vis003','2018/07/15','\\Content\\Salary\\201807\\003 TRAN VAN MIEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),('vis003201808','vis003','2018/08/15','\\Content\\Salary\\201808\\003 TRAN VAN MIEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis003201809','vis003','2018/09/15','\\Content\\Salary\\201809\\003 TRAN VAN MIEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis003201810','vis003','2018/10/15','\\Content\\Salary\\201810\\003 TRAN VAN MIEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis003201811','vis003','2018/11/15','\\Content\\Salary\\201811\\003 TRAN VAN MIEN.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis004201801','vis004','2018/01/15','\\Content\\Salary\\201801\\004 KIEU XUAN MANH.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis004201807','vis004','2018/07/15','\\Content\\Salary\\201807\\004 KIEU XUAN MANH.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),('vis004201808','vis004','2018/08/15','\\Content\\Salary\\201808\\004 KIEU XUAN MANH.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis004201809','vis004','2018/09/15','\\Content\\Salary\\201809\\004 KIEU XUAN MANH.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis004201810','vis004','2018/10/15','\\Content\\Salary\\201810\\004 KIEU XUAN MANH.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis004201811','vis004','2018/11/15','\\Content\\Salary\\201811\\004 KIEU XUAN MANH.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis005201801','vis005','2018/01/15','\\Content\\Salary\\201801\\005 KIEU XUAN DUC.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis005201807','vis005','2018/07/15','\\Content\\Salary\\201807\\005 KIEU XUAN DUC.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),('vis005201808','vis005','2018/08/15','\\Content\\Salary\\201808\\005 KIEU XUAN DUC.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis005201809','vis005','2018/09/15','\\Content\\Salary\\201809\\005 KIEU XUAN DUC.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis005201810','vis005','2018/10/15','\\Content\\Salary\\201810\\005 KIEU XUAN DUC.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),('vis005201811','vis005','2018/11/15','\\Content\\Salary\\201811\\005 KIEU XUAN DUC.pdf',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'');
/*!40000 ALTER TABLE `salary` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-07-31 16:20:48
