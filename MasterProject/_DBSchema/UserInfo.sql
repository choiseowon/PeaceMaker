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
-- 테이블 구조 `UserInfo`
--

CREATE TABLE `UserInfo` (
  `UserNo` int NOT NULL COMMENT '유저 고유 넘버',
  `UserId` varchar(15) CHARACTER SET euckr COLLATE euckr_korean_ci NOT NULL COMMENT '유저 아이디',
  `UserPw` varchar(15) CHARACTER SET euckr COLLATE euckr_korean_ci NOT NULL COMMENT '유저 비밀번호',
  `UserNick` varchar(20) CHARACTER SET euckr COLLATE euckr_korean_ci NOT NULL COMMENT '유저 닉네임',
  `UserGold` int DEFAULT '0' COMMENT '유저 소지 골드',
  `UserWin` int DEFAULT '0' COMMENT '유저 승리수',
  `UserDefeat` int DEFAULT '0' COMMENT '유저 패배수'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- 테이블의 덤프 데이터 `UserInfo`
--

INSERT INTO `UserInfo` (`UserNo`, `UserId`, `UserPw`, `UserNick`, `UserGold`, `UserWin`, `UserDefeat`) VALUES
(1, 'bms', '12345', 'bms', 2650, 17, 8),
(3, 'TestChang', '1111', '국기스텐', 1100, 4, 8),
(6, 'zzz', '11111', 'ZZZ', 2300, 11, 7),
(7, 'TestTower', '1234567', 'TowerSetDone', 2550, 14, 10),
(8, 'NewTest0001', '1234567', '늑내약해요', 1100, 3, 4),
(9, '1111', '1111', '1111', 2150, 4, 2),
(10, 'test1234', '1234567', 'test1234', 250, 6, 5),
(11, 'asdf', '1234', 'asdf', 1100, 2, 0),
(12, 'test123', '1234', 'test123', 50, 24, 1),
(13, '12344', '12344', '12344', 700, 3, 13),
(14, 'akflsk', 'akflsk', '마리나', 0, 2, 3),
(15, 'test112', '1234567', 'test112', 150, 2, 17),
(16, 'akflfh', 'akflfh', '마리로', 150, 1, 5),
(17, 'rnghdk', 'rngkdk', '구하아', 400, 2, 2),
(18, 'skalfl', 'skalfl', '나리미', 600, 0, 6),
(19, 'rnakdbtl', 'rnakdbtl', '구마유시', 0, 1, 6),
(20, 'qodrnfm', 'qodrmfn', '뱅글', 1500, 1, 3),
(21, 'qodrmf', 'qodrmf', 'qodrmf', 0, 2, 2),
(22, 'ccc', '1111', '씨', 37500, 4, 4),
(23, 'ssmart123', '123456', '신승만', 900, 2, 1),
(24, 'Unity111', '111111', '야옹이', 1700, 12, 0),
(25, 'tte', 'tetet', 'tetetet', 250, 0, 3),
(26, 't', 't', 'r', 1900, 0, 2);

--
-- 덤프된 테이블의 인덱스
--

--
-- 테이블의 인덱스 `UserInfo`
--
ALTER TABLE `UserInfo`
  ADD PRIMARY KEY (`UserNo`);

--
-- 덤프된 테이블의 AUTO_INCREMENT
--

--
-- 테이블의 AUTO_INCREMENT `UserInfo`
--
ALTER TABLE `UserInfo`
  MODIFY `UserNo` int NOT NULL AUTO_INCREMENT COMMENT '유저 고유 넘버', AUTO_INCREMENT=27;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
