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
-- 테이블 구조 `AttItem`
--

CREATE TABLE `AttItem` (
  `No` int NOT NULL COMMENT '아이템 키',
  `UnitName` varchar(20) CHARACTER SET euckr COLLATE euckr_korean_ci NOT NULL COMMENT '유닛 이름',
  `UnitAttack` int NOT NULL COMMENT '유닛 공격력',
  `UnitDefence` int NOT NULL COMMENT '유닛 방어력',
  `UnitHP` int NOT NULL COMMENT '유닛 HP',
  `UnitAttSpeed` float NOT NULL COMMENT '유닛 공격속도',
  `UnitMoveSpeed` float NOT NULL COMMENT '유닛 이동속도',
  `UnitPrice` int NOT NULL COMMENT '유닛 가격',
  `UnitUpPrice` int NOT NULL COMMENT '유닛 업그레이드 가격',
  `UnitUseable` int NOT NULL COMMENT '유닛 사용가능횟수',
  `UnitKind` int NOT NULL COMMENT '유닛 공격 / 수비 확인',
  `UnitRange` int DEFAULT '0' COMMENT '유닛 공격 범위',
  `UnitSkill` float NOT NULL COMMENT '유닛 스킬 쿨타임'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- 테이블의 덤프 데이터 `AttItem`
--

INSERT INTO `AttItem` (`No`, `UnitName`, `UnitAttack`, `UnitDefence`, `UnitHP`, `UnitAttSpeed`, `UnitMoveSpeed`, `UnitPrice`, `UnitUpPrice`, `UnitUseable`, `UnitKind`, `UnitRange`, `UnitSkill`) VALUES
(1, 'NormalTank', 10, 10, 100, 3, 5, 1000, 500, 5, 1, 15, 0),
(2, 'SpeedTank', 2, 2, 70, 1.5, 8, 1000, 500, 5, 1, 7, 0),
(3, 'Repair', 5, 2, 200, 3, 5, 1000, 500, 5, 1, 15, 3),
(4, 'SolidTank', 5, 10, 200, 4, 3, 1000, 500, 5, 1, 15, 3),
(5, 'CannonTank', 15, 5, 150, 8, 4, 1000, 500, 5, 1, 30, 0);

--
-- 덤프된 테이블의 인덱스
--

--
-- 테이블의 인덱스 `AttItem`
--
ALTER TABLE `AttItem`
  ADD PRIMARY KEY (`No`);

--
-- 덤프된 테이블의 AUTO_INCREMENT
--

--
-- 테이블의 AUTO_INCREMENT `AttItem`
--
ALTER TABLE `AttItem`
  MODIFY `No` int NOT NULL AUTO_INCREMENT COMMENT '아이템 키', AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
