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
-- 테이블 구조 `UserDEC`
--

CREATE TABLE `UserDEC` (
  `UserDecN` int NOT NULL COMMENT 'DEC의 고유키',
  `UserDec1` int NOT NULL COMMENT '아이템넘버',
  `UserDec2` int NOT NULL COMMENT '아이템넘버',
  `UserDec3` int NOT NULL COMMENT '아이템넘버',
  `UserDec4` int NOT NULL COMMENT '아이템넘버',
  `UserDec5` int NOT NULL COMMENT '아이템넘버',
  `UserN` int NOT NULL COMMENT '유저넘버',
  `UserDec1_Num` int NOT NULL COMMENT '1번탱크의 댓수',
  `UserDec2_Num` int NOT NULL COMMENT '2번탱크의 댓수',
  `UserDec3_Num` int NOT NULL COMMENT '3번탱크의 댓수',
  `UserDec4_Num` int NOT NULL COMMENT '4번탱크의 댓수',
  `UserDec5_Num` int NOT NULL COMMENT '5번 탱크의 댓수',
  `UserDecCount` int NOT NULL COMMENT '유저의덱카운트'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- 테이블의 덤프 데이터 `UserDEC`
--

INSERT INTO `UserDEC` (`UserDecN`, `UserDec1`, `UserDec2`, `UserDec3`, `UserDec4`, `UserDec5`, `UserN`, `UserDec1_Num`, `UserDec2_Num`, `UserDec3_Num`, `UserDec4_Num`, `UserDec5_Num`, `UserDecCount`) VALUES
(2, 1, 2, 3, 4, -1, 1, 10, 5, 5, 5, -1, 0),
(5, 1, -1, -1, -1, -1, 8, 10, -1, -1, -1, -1, 0),
(9, 4, 2, -1, -1, -1, 6, 10, 20, -1, -1, -1, 0),
(10, 1, -1, 4, 2, 3, 10, 10, -1, 5, 5, 5, 0),
(11, 1, -1, -1, -1, -1, 13, 10, -1, -1, -1, -1, 0),
(12, 1, -1, -1, -1, -1, 13, 10, -1, -1, -1, -1, 1),
(13, 1, -1, -1, -1, -1, 14, 10, -1, -1, -1, -1, 0),
(14, 1, 2, 3, 4, -1, 12, 0, 5, 5, 5, -1, 0),
(15, 1, -1, -1, -1, -1, 15, 10, -1, -1, -1, -1, 0),
(16, 1, -1, -1, -1, -1, 16, 10, -1, -1, -1, -1, 0),
(17, 1, -1, -1, -1, -1, 18, 10, -1, -1, -1, -1, 0),
(18, 1, -1, -1, -1, -1, 19, 10, -1, -1, -1, -1, 0),
(19, 1, -1, -1, -1, -1, 9, 10, -1, -1, -1, -1, 0),
(20, 1, -1, -1, -1, -1, 21, 10, -1, -1, -1, -1, 0),
(21, 1, -1, -1, -1, -1, 23, 10, -1, -1, -1, -1, 0),
(22, -1, -1, -1, -1, -1, 1, -1, -1, -1, -1, -1, 1),
(23, 1, 2, 4, 3, -1, 10, 10, 5, 5, 5, -1, 1),
(24, 1, 2, 3, -1, -1, 24, 10, 5, 5, -1, -1, 0),
(25, 1, -1, -1, -1, -1, 22, 10, -1, -1, -1, -1, 0),
(26, -1, -1, -1, -1, -1, 24, -1, -1, -1, -1, -1, 1),
(27, 1, 2, 3, 4, -1, 12, 0, 5, 5, 5, -1, 1);

--
-- 덤프된 테이블의 인덱스
--

--
-- 테이블의 인덱스 `UserDEC`
--
ALTER TABLE `UserDEC`
  ADD PRIMARY KEY (`UserDecN`);

--
-- 덤프된 테이블의 AUTO_INCREMENT
--

--
-- 테이블의 AUTO_INCREMENT `UserDEC`
--
ALTER TABLE `UserDEC`
  MODIFY `UserDecN` int NOT NULL AUTO_INCREMENT COMMENT 'DEC의 고유키', AUTO_INCREMENT=28;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
