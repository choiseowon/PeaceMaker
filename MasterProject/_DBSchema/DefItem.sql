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
-- 테이블 구조 `DefItem`
--

CREATE TABLE `DefItem` (
  `No` int NOT NULL COMMENT '아이템 키',
  `UnitName` varchar(20) CHARACTER SET euckr COLLATE euckr_korean_ci NOT NULL COMMENT '유닛 이름',
  `UnitAttack` int NOT NULL COMMENT '유닛 공격력',
  `UnitDefence` int NOT NULL COMMENT '유닛 방어력',
  `UnitHP` int NOT NULL COMMENT '유닛 HP',
  `UnitAttSpeed` float NOT NULL COMMENT '유닛 공격속도',
  `UnitPrice` int NOT NULL COMMENT '유닛 가격',
  `UnitUpPrice` int NOT NULL COMMENT '유닛 업그레이드 가격',
  `UnitKind` int NOT NULL COMMENT '유닛 공격 / 수비 확인',
  `UnitRange` int DEFAULT '0' COMMENT '유닛 공격 범위'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- 테이블의 덤프 데이터 `DefItem`
--

INSERT INTO `DefItem` (`No`, `UnitName`, `UnitAttack`, `UnitDefence`, `UnitHP`, `UnitAttSpeed`, `UnitPrice`, `UnitUpPrice`, `UnitKind`, `UnitRange`) VALUES
(1, 'MachineGunTower', 10, 10, 200, 1, 1000, 500, 0, 15),
(2, 'MissileTower', 15, 50, 200, 1.5, 1000, 500, 0, 15),
(3, 'EmpTower', 0, 20, 700, 2, 1000, 500, 0, 7),
(4, 'SuperMachineGunTower', 10, 10, 100, 0.5, 1000, 500, 0, 15);

--
-- 덤프된 테이블의 인덱스
--

--
-- 테이블의 인덱스 `DefItem`
--
ALTER TABLE `DefItem`
  ADD PRIMARY KEY (`No`);

--
-- 덤프된 테이블의 AUTO_INCREMENT
--

--
-- 테이블의 AUTO_INCREMENT `DefItem`
--
ALTER TABLE `DefItem`
  MODIFY `No` int NOT NULL AUTO_INCREMENT COMMENT '아이템 키', AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
