-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Jan 12. 17:17
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `autok`
--
CREATE DATABASE IF NOT EXISTS `autok` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `autok`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `auto`
--

CREATE TABLE `auto` (
  `rendszam` varchar(7) NOT NULL COMMENT 'AAA-111 formátumban',
  `marka` varchar(50) NOT NULL,
  `modell` varchar(50) NOT NULL,
  `gyartasiev` year(4) NOT NULL,
  `forgalmiErvenyesseg` date NOT NULL,
  `vetelar` int(11) NOT NULL,
  `kmallas` int(11) NOT NULL,
  `hengerűrtartalom` int(11) NOT NULL COMMENT 'köbcentiméterben',
  `tomeg` int(11) NOT NULL COMMENT 'Az autó saját tömege utasok és terhelés nélkül',
  `teljesitmeny` int(11) NOT NULL COMMENT 'teljesítmény LE mértékegységben'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `auto`
--

INSERT INTO `auto` (`rendszam`, `marka`, `modell`, `gyartasiev`, `forgalmiErvenyesseg`, `vetelar`, `kmallas`, `hengerűrtartalom`, `tomeg`, `teljesitmeny`) VALUES
('ABC-123', 'Toyota', 'Corolla', '2018', '2028-12-31', 15000, 50000, 1600, 1200, 120),
('DEF-456', 'Ford', 'Focus', '2019', '2024-10-15', 18000, 60000, 2000, 1300, 140),
('GHI-789', 'Honda', 'Civic', '2020', '2028-09-20', 20000, 75000, 1800, 1100, 110),
('JKL-012', 'Volkswagen', 'Golf', '2017', '2027-11-05', 16000, 55000, 1700, 1250, 130),
('MNO-345', 'Hyundai', 'Elantra', '2021', '2025-05-18', 22000, 80000, 1900, 1400, 150),
('PQR-678', 'Mercedes-Benz', 'A-Class', '2016', '2027-08-25', 22000, 70000, 1900, 1400, 160),
('STU-901', 'BMW', '3 Series', '2017', '2028-04-12', 25000, 80000, 2000, 1500, 180),
('VWX-234', 'Audi', 'A4', '2018', '2024-01-30', 28000, 90000, 2200, 1600, 200),
('YZA-567', 'Chevrolet', 'Cruze', '2019', '2027-12-15', 18000, 60000, 1800, 1300, 150),
('BCD-890', 'Nissan', 'Altima', '2020', '2025-09-10', 21000, 75000, 2000, 1400, 170),
('EFG-123', 'Kia', 'Optima', '2016', '2028-11-08', 19000, 65000, 1700, 1200, 140),
('HIJ-456', 'Subaru', 'Impreza', '2017', '2027-10-02', 20000, 70000, 1800, 1300, 160),
('KLM-789', 'Mazda', 'Mazda3', '2018', '2024-05-22', 23000, 85000, 2000, 1400, 180),
('NOP-012', 'Volvo', 'S60', '2019', '2028-08-17', 26000, 90000, 2200, 1600, 200),
('QRS-345', 'Lexus', 'IS', '2020', '2025-02-03', 29000, 95000, 2400, 1800, 220),
('TUV-678', 'Toyota', 'Camry', '2016', '2027-07-10', 20000, 70000, 1900, 1400, 160),
('WXY-901', 'Ford', 'Fusion', '2017', '2028-03-28', 23000, 80000, 2000, 1500, 180),
('ZAB-234', 'Honda', 'Accord', '2018', '2024-02-15', 26000, 90000, 2200, 1600, 200),
('CDE-567', 'Volkswagen', 'Passat', '2019', '2027-10-30', 19000, 65000, 1800, 1300, 150),
('FGH-890', 'Hyundai', 'Sonata', '2020', '2025-07-25', 22000, 75000, 2000, 1400, 170),
('IJK-123', 'Mercedes-Benz', 'C-Class', '2016', '2028-12-18', 25000, 80000, 1900, 1400, 160),
('LMN-456', 'BMW', '5 Series', '2017', '2027-09-05', 28000, 90000, 2000, 1500, 180),
('OPQ-789', 'Audi', 'A6', '2018', '2024-04-20', 31000, 95000, 2200, 1600, 200),
('RST-012', 'Chevrolet', 'Malibu', '2019', '2028-07-15', 24000, 85000, 1800, 1300, 150),
('UVW-345', 'Nissan', 'Maxima', '2020', '2025-01-30', 27000, 90000, 2000, 1400, 170),
('XYZ-678', 'Kia', 'Cadenza', '2016', '2027-06-22', 20000, 70000, 1900, 1400, 160),
('ABC-901', 'Subaru', 'Legacy', '2017', '2028-02-08', 23000, 80000, 2000, 1500, 180),
('DEF-234', 'Mazda', 'Mazda6', '2018', '2024-01-25', 26000, 90000, 2200, 1600, 200),
('GHI-567', 'Volvo', 'S90', '2019', '2027-11-10', 29000, 95000, 2400, 1800, 220),
('JKL-890', 'Lexus', 'ES', '2020', '2025-06-05', 32000, 100000, 2600, 2000, 240),
('MNO-123', 'Toyota', 'Avalon', '2016', '2027-05-12', 21000, 75000, 1900, 1400, 160),
('PQR-456', 'Ford', 'Taurus', '2017', '2028-01-28', 24000, 85000, 2000, 1500, 180),
('STU-789', 'Honda', 'Crosstour', '2018', '2024-08-15', 27000, 90000, 2200, 1600, 200),
('VWX-012', 'Audi', 'A7', '2019', '2028-10-30', 30000, 95000, 2400, 1800, 220),
('YZA-345', 'Chevrolet', 'Impala', '2020', '2025-05-25', 33000, 100000, 2600, 2000, 240),
('BCD-678', 'Nissan', 'Altima', '2016', '2027-04-22', 22000, 75000, 1900, 1400, 160),
('EFG-901', 'Kia', 'Optima', '2017', '2028-12-08', 25000, 80000, 2000, 1500, 180),
('HIJ-234', 'Subaru', 'Legacy', '2018', '2024-07-20', 28000, 90000, 2200, 1600, 200),
('KLM-567', 'Mazda', 'Mazda6', '2019', '2027-10-15', 31000, 95000, 2400, 1800, 220),
('NOP-890', 'Volvo', 'S90', '2020', '2025-04-30', 34000, 100000, 2600, 2000, 240),
('QRS-123', 'Lexus', 'ES', '2016', '2027-03-18', 23000, 80000, 1900, 1400, 160),
('TUV-456', 'Toyota', 'Avalon', '2017', '2028-11-02', 26000, 90000, 2000, 1500, 180),
('WXY-789', 'Ford', 'Taurus', '2018', '2024-06-15', 29000, 95000, 2200, 1600, 200),
('ZAB-012', 'Honda', 'Crosstour', '2019', '2028-09-30', 32000, 100000, 2400, 1800, 220),
('CDE-345', 'Volkswagen', 'Arteon', '2020', '2025-06-25', 35000, 105000, 2600, 2000, 240),
('FGH-678', 'Hyundai', 'Azera', '2016', '2027-02-12', 24000, 85000, 1900, 1400, 160),
('IJK-901', 'Mercedes-Benz', 'E-Class', '2017', '2028-10-18', 27000, 90000, 2000, 1500, 180),
('LMN-234', 'BMW', '7 Series', '2018', '2024-05-02', 30000, 95000, 2200, 1600, 200),
('OPQ-567', 'Audi', 'A8', '2019', '2028-07-30', 33000, 100000, 2400, 1800, 220),
('RST-890', 'Chevrolet', 'Impala', '2020', '2025-03-15', 36000, 105000, 2600, 2000, 240);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
