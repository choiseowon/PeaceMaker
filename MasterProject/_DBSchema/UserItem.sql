-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- 생성 시간: 21-11-01 17:52
-- 서버 버전: 8.0.26
-- PHP 버전: 7.4.24

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 데이터베이스: `pmaker`
--

-- --------------------------------------------------------

--
-- 테이블 구조 `UserItem`
--

CREATE TABLE `UserItem` (
  `ItemNo` int NOT NULL COMMENT '아이템 ID',
  `ItemName` varchar(20) CHARACTER SET euckr COLLATE euckr_korean_ci NOT NULL COMMENT '아이템 이름',
  `Level` int DEFAULT '1' COMMENT '아이템 레벨(업그레이드 시 증가)',
  `isBuy` tinyint(1) DEFAULT '0' COMMENT '아이템 구입여부',
  `KindOfItem` int NOT NULL COMMENT '아이템 종류',
  `ItemUsable` int DEFAULT '1' COMMENT '아이템 사용 가능 수량',
  `isAttack` tinyint(1) DEFAULT '1' COMMENT '공격용 / 수비용인지',
  `UserId` int NOT NULL COMMENT '유저 소유 id'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- 테이블의 덤프 데이터 `UserItem`
--

INSERT INTO `UserItem` (`ItemNo`, `ItemName`, `Level`, `isBuy`, `KindOfItem`, `ItemUsable`, `isAttack`, `UserId`) VALUES
(1, 'NormalTank', 3, 1, 1, 10, 1, 1),
(2, 'MachineGunTower', 1, 1, 1, 0, 0, 1),
(3, 'NormalTank', 1, 1, 1, 10, 1, 3),
(4, 'MachineGunTower', 1, 1, 1, 0, 0, 3),
(9, 'NormalTank', 1, 1, 1, 10, 1, 6),
(10, 'MachineGunTower', 1, 1, 1, 0, 0, 6),
(11, 'NormalTank', 1, 1, 1, 10, 1, 7),
(12, 'MachineGunTower', 1, 1, 1, 0, 0, 7),
(13, 'EmpTower', 1, 1, 3, 0, 0, 7),
(14, 'EmpTower', 1, 1, 2, 0, 0, 7),
(15, 'SuperMachineGunTower', 1, 1, 4, 0, 0, 7),
(16, 'NormalTank', 3, 1, 1, 10, 1, 8),
(17, 'MachineGunTower', 2, 1, 1, 0, 0, 8),
(19, 'SuperMachineGunTower', 2, 1, 4, 0, 0, 8),
(20, 'NormalTank', 1, 1, 1, 10, 1, 9),
(21, 'MachineGunTower', 1, 1, 1, 0, 0, 9),
(22, 'NormalTank', 1, 1, 1, 10, 1, 10),
(23, 'MachineGunTower', 1, 1, 1, 0, 0, 10),
(24, 'SpeedTank', 1, 1, 2, 5, 1, 1),
(25, 'NormalTank', 20, 1, 1, 0, 1, 11),
(26, 'SpeedTank', 1, 1, 2, 0, 1, 11),
(27, 'Repair', 1, 1, 3, 0, 1, 11),
(28, 'NormalTank', 1, 1, 1, 10, 1, 13),
(29, 'MachineGunTower', 1, 1, 1, 0, 0, 13),
(39, 'NormalTank', 1, 1, 1, 10, 1, 14),
(40, 'MachineGunTower', 1, 1, 1, 0, 0, 14),
(41, 'NormalTank', 1, 1, 1, 10, 1, 15),
(42, 'MachineGunTower', 1, 1, 1, 0, 0, 15),
(43, 'NormalTank', 3, 1, 1, 0, 1, 12),
(44, 'NormalTank', 1, 1, 1, 10, 1, 16),
(45, 'MachineGunTower', 1, 1, 1, 0, 0, 16),
(46, 'SpeedTank', 1, 1, 2, 5, 1, 12),
(47, 'NormalTank', 1, 1, 1, 10, 1, 17),
(48, 'MachineGunTower', 1, 1, 1, 0, 0, 17),
(49, 'NormalTank', 1, 1, 1, 10, 1, 18),
(50, 'MachineGunTower', 1, 1, 1, 0, 0, 18),
(51, 'NormalTank', 1, 1, 1, 10, 1, 19),
(52, 'MachineGunTower', 1, 1, 1, 0, 0, 19),
(53, 'NormalTank', 1, 1, 1, 10, 1, 20),
(54, 'MachineGunTower', 1, 1, 1, 0, 0, 20),
(55, 'NormalTank', 1, 1, 1, 10, 1, 21),
(56, 'MachineGunTower', 1, 1, 1, 0, 0, 21),
(57, 'NormalTank', 6, 1, 1, 10, 1, 22),
(58, 'MachineGunTower', 8, 1, 1, 0, 0, 22),
(59, 'Repair', 1, 1, 3, 5, 1, 12),
(60, 'SolidTank', 1, 1, 4, 5, 1, 12),
(62, 'NormalTank', 1, 1, 1, 10, 1, 23),
(63, 'MachineGunTower', 1, 1, 1, 0, 0, 23),
(65, 'Repair', 1, 1, 3, 5, 1, 1),
(67, 'SolidTank', 1, 1, 4, 5, 1, 1),
(68, 'SpeedTank', 4, 1, 2, 5, 1, 10),
(69, 'NormalTank', 1, 1, 1, 10, 1, 24),
(70, 'MachineGunTower', 1, 1, 1, 0, 0, 24),
(71, 'MissileTower', 1, 1, 2, 0, 0, 1),
(72, 'Repair', 1, 1, 3, 5, 1, 10),
(73, 'SolidTank', 1, 1, 4, 5, 1, 10),
(74, 'SpeedTank', 1, 1, 2, 5, 1, 24),
(75, 'NormalTank', 1, 1, 1, 10, 1, 25),
(76, 'MachineGunTower', 1, 1, 1, 0, 0, 25),
(77, 'Repair', 1, 1, 3, 5, 1, 24),
(78, 'NormalTank', 1, 1, 1, 10, 1, 26),
(79, 'MachineGunTower', 1, 1, 1, 0, 0, 26),
(81, 'MissileTower', 1, 1, 2, 0, 0, 22),
(82, 'SpeedTank', 2, 1, 2, 5, 1, 22),
(83, 'MachineGunTower', 1, 1, 1, 0, 0, 12),
(84, 'MissileTower', 1, 1, 2, 0, 0, 12),
(85, 'EmpTower', 1, 1, 3, 0, 0, 12);

--
-- 덤프된 테이블의 인덱스
--

--
-- 테이블의 인덱스 `UserItem`
--
ALTER TABLE `UserItem`
  ADD PRIMARY KEY (`ItemNo`);

--
-- 덤프된 테이블의 AUTO_INCREMENT
--

--
-- 테이블의 AUTO_INCREMENT `UserItem`
--
ALTER TABLE `UserItem`
  MODIFY `ItemNo` int NOT NULL AUTO_INCREMENT COMMENT '아이템 ID', AUTO_INCREMENT=86;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
