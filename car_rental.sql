-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 08, 2024 at 04:04 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `car_rental`
--

-- --------------------------------------------------------

--
-- Table structure for table `cars`
--

CREATE TABLE `cars` (
  `id` int(11) NOT NULL,
  `car_name` varchar(50) NOT NULL,
  `car_model` varchar(50) NOT NULL,
  `price` double NOT NULL,
  `plate_no` varchar(10) NOT NULL,
  `color` varchar(10) NOT NULL,
  `status` int(11) NOT NULL DEFAULT 0 COMMENT '0 = Available | 1 = Rented',
  `added_at` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `cars`
--

INSERT INTO `cars` (`id`, `car_name`, `car_model`, `price`, `plate_no`, `color`, `status`, `added_at`) VALUES
(1, 'Camry', 'Metro', 82.39, 'NP09-PIO-3', 'Aquamarine', 0, '2017-10-24'),
(2, 'Civic', 'Celica', 403.06, 'JE15-OFP-8', 'Yellow', 1, '2020-06-17'),
(3, 'Camry', 'DB9', 774.96, 'JM91-RFB-9', 'Blue', 0, '2011-11-29'),
(4, 'Accord', 'Esperante', 830.71, 'BN54-FEK-7', 'Khaki', 1, '2015-05-17'),
(5, 'Civic', 'Park Avenue', 514.57, 'QO07-CPV-4', 'Teal', 0, '2017-06-28'),
(6, 'Corolla', 'E-Class', 188.84, 'YQ65-SSL-8', 'Red', 0, '2018-07-29'),
(7, 'Civic', 'GLK-Class', 716.75, 'PJ42-PHP-4', 'Fuscia', 1, '2017-08-31'),
(8, 'Accord', 'RX Hybrid', 552.38, 'PW05-ILQ-9', 'Purple', 0, '2013-02-01'),
(9, 'Mustang', 'Express 2500', 866.79, 'PL34-KVX-3', 'Turquoise', 1, '2012-09-16'),
(10, 'Accord', 'Sundance', 399.28, 'XH58-MQA-5', 'Orange', 0, '2015-09-08'),
(11, 'Corolla', 'Azure', 644.7, 'MB11-DDS-5', 'Turquoise', 0, '2018-12-07'),
(12, 'Civic', 'Fleetwood', 784.03, 'TP82-XHR-9', 'Yellow', 0, '2020-06-25'),
(13, 'Civic', 'Bronco II', 239.39, 'IE60-HPL-1', 'Crimson', 0, '2016-05-15'),
(14, 'Civic', 'Festiva', 348.04, 'ZA33-CNO-6', 'Goldenrod', 0, '2017-04-20'),
(15, 'Corolla', 'ZX2', 177.42, 'CL53-RUT-7', 'Purple', 1, '2015-09-29'),
(16, 'Mustang', 'M-Class', 694.54, 'KE39-QDK-7', 'Pink', 1, '2014-06-16'),
(17, 'Camry', 'Sienna', 678.1, 'GG55-SAJ-2', 'Teal', 1, '2013-02-06'),
(18, 'Camry', '6000', 388.82, 'TY73-PUN-0', 'Red', 0, '2013-08-18'),
(19, 'Corolla', '3000GT', 510.59, 'FZ06-KCJ-2', 'Puce', 1, '2019-01-17'),
(20, 'Camry', 'Yukon', 878.25, 'MV69-WND-3', 'Mauv', 1, '2011-10-25'),
(21, 'Accord', 'M', 67.6, 'HP87-XKX-1', 'Pink', 0, '2011-05-06'),
(22, 'Corolla', 'Xtra', 589.43, 'DC36-QNB-8', 'Pink', 0, '2016-05-28'),
(23, 'Camry', 'Aerostar', 341.74, 'ID42-HMN-4', 'Yellow', 1, '2021-06-29'),
(24, 'Accord', 'JX', 65.23, 'KZ34-ZUL-6', 'Indigo', 0, '2019-06-05'),
(25, 'Civic', 'Familia', 763.26, 'GK67-USE-6', 'Maroon', 1, '2017-09-19'),
(26, 'Accord', 'Mazda3', 777.41, 'CT72-IPW-4', 'Orange', 1, '2010-10-18'),
(27, 'Mustang', 'Avalon', 673.3, 'PM37-VOV-8', 'Turquoise', 1, '2020-06-21'),
(28, 'Mustang', 'Caprice', 253.93, 'MK99-KII-8', 'Mauv', 0, '2014-12-15'),
(29, 'Accord', 'Wrangler', 560.22, 'QT45-EYW-6', 'Yellow', 1, '2013-04-06'),
(30, 'Civic', 'H3', 159.37, 'VZ37-FMD-1', 'Teal', 0, '2010-10-22'),
(31, 'Accord', 'Safari', 227.93, 'SR97-DJD-2', 'Mauv', 1, '2019-01-10'),
(32, 'Mustang', 'Prelude', 617.3, 'BN53-FVN-7', 'Teal', 0, '2011-08-12'),
(33, 'Corolla', 'Galant', 195.71, 'WM42-FZI-3', 'Aquamarine', 1, '2014-08-14'),
(34, 'Accord', 'Camry Hybrid', 613.04, 'BH20-REL-5', 'Violet', 1, '2020-02-26'),
(35, 'Civic', 'Avalanche', 886.4, 'JQ96-BKQ-2', 'Indigo', 0, '2013-10-28'),
(36, 'Mustang', 'Ram 3500', 235.49, 'JH24-OOI-1', 'Violet', 1, '2012-10-18'),
(37, 'Accord', 'Sunfire', 766.91, 'WS83-LFO-3', 'Green', 1, '2016-05-24'),
(38, 'Camry', 'W201', 903.92, 'WQ72-BJP-2', 'Khaki', 0, '2019-08-29'),
(39, 'Corolla', '600SL', 951.39, 'EA54-RTN-6', 'Teal', 1, '2018-11-10'),
(40, 'Accord', 'Liberty', 211.47, 'RG01-MJB-4', 'Turquoise', 1, '2019-09-30'),
(41, 'Accord', 'Colorado', 591.66, 'HY80-JIU-1', 'Violet', 0, '2016-05-28'),
(42, 'Civic', 'Intrepid', 102.54, 'ES88-DMY-1', 'Maroon', 1, '2020-11-23'),
(43, 'Accord', 'D250', 469.51, 'NZ78-GPK-2', 'Goldenrod', 1, '2020-04-12'),
(44, 'Civic', 'Taurus', 259.04, 'AY15-SSS-5', 'Red', 1, '2013-07-19'),
(45, 'Mustang', 'Prizm', 879.6, 'HV16-LVR-6', 'Indigo', 0, '2018-06-28'),
(46, 'Camry', 'Yukon', 255.48, 'LB39-PZM-7', 'Violet', 0, '2019-01-07'),
(47, 'Camry', 'F250', 90.89, 'OI88-VOE-2', 'Pink', 0, '2017-11-11'),
(48, 'Civic', 'Tempest', 413.14, 'HK92-MKQ-5', 'Orange', 0, '2014-07-18'),
(49, 'Mustang', 'F350', 583.69, 'TJ88-UEN-6', 'Yellow', 0, '2010-11-03'),
(50, 'Corolla', 'Camaro', 5.24, 'KU38-MHR-9', 'Red', 1, '2010-08-15'),
(51, 'Accord', 'S-Type', 296.7, 'RX35-LFV-5', 'Aquamarine', 0, '2012-11-30'),
(52, 'Accord', 'Tacoma', 407.8, 'FQ84-FDE-8', 'Red', 1, '2014-06-30'),
(53, 'Camry', 'Catera', 606.64, 'BE46-FDQ-1', 'Turquoise', 0, '2020-11-28'),
(54, 'Camry', 'Excel', 815.48, 'IA38-LLB-8', 'Violet', 0, '2017-09-17'),
(55, 'Civic', 'rio', 545.89, 'KV49-VQE-1', 'Pink', 0, '2010-03-26'),
(56, 'Civic', 'Mirage', 874.2, 'JQ29-KLC-4', 'Fuscia', 0, '2021-07-22'),
(57, 'Mustang', 'Impreza', 847.03, 'AM28-HIG-6', 'Maroon', 1, '2014-01-06'),
(58, 'Corolla', 'RX', 203.35, 'HQ69-REZ-3', 'Blue', 1, '2013-07-14'),
(59, 'Accord', '911', 784.35, 'TM99-ZLH-5', 'Pink', 0, '2021-02-25'),
(60, 'Accord', 'New Beetle', 324.69, 'VD79-GQS-8', 'Purple', 1, '2014-03-06'),
(61, 'Civic', 'Silhouette', 163.43, 'DG99-HEM-3', 'Teal', 1, '2012-03-04'),
(62, 'Camry', 'Protege5', 865.21, 'LV07-IPO-5', 'Green', 1, '2018-11-05'),
(63, 'Corolla', 'Rodeo', 420.11, 'LW09-RMN-9', 'Aquamarine', 0, '2010-09-08'),
(64, 'Camry', 'Ridgeline', 317.92, 'ER68-GZQ-1', 'Yellow', 0, '2014-03-23'),
(65, 'Mustang', 'Compass', 41.51, 'JL28-WCI-6', 'Red', 1, '2017-10-27'),
(66, 'Corolla', 'G6', 476.86, 'YX22-ZOD-6', 'Purple', 0, '2021-06-15'),
(67, 'Camry', 'Golf', 490.5, 'MS59-FRY-4', 'Indigo', 0, '2010-01-25'),
(68, 'Mustang', 'Corvette', 103.22, 'YD37-DOX-5', 'Orange', 1, '2011-06-14'),
(69, 'Corolla', 'XJ', 821.84, 'HT17-JEH-8', 'Violet', 0, '2018-10-13'),
(70, 'Mustang', 'Swift', 30.78, 'LV17-NYY-2', 'Green', 1, '2011-04-06'),
(71, 'Civic', 'Impreza', 668.26, 'JW77-ZSO-8', 'Crimson', 1, '2012-02-19'),
(72, 'Accord', 'Bronco II', 651.86, 'XZ38-UBW-1', 'Maroon', 0, '2018-12-08'),
(73, 'Civic', 'Grand Am', 289.95, 'JM16-WRR-4', 'Goldenrod', 0, '2020-12-25'),
(74, 'Corolla', 'Camaro', 632.44, 'HN59-PXN-5', 'Khaki', 0, '2020-07-16'),
(75, 'Camry', 'B-Series', 835.57, 'UN19-LQL-1', 'Violet', 0, '2016-05-28'),
(76, 'Mustang', 'Touareg', 996.35, 'OE06-OUH-9', 'Yellow', 1, '2017-12-31'),
(77, 'Camry', '300', 77.56, 'EM91-MMK-8', 'Turquoise', 0, '2017-10-18'),
(78, 'Corolla', 'Reatta', 940.26, 'MN31-HIG-8', 'Purple', 0, '2011-09-27'),
(79, 'Corolla', 'Spirit', 183.73, 'WH43-LZI-3', 'Teal', 0, '2019-10-02'),
(80, 'Corolla', 'GS', 263.77, 'MM95-UBQ-6', 'Turquoise', 1, '2010-08-14'),
(81, 'Corolla', 'Econoline E350', 335.88, 'CK48-BJQ-5', 'Red', 1, '2017-05-15'),
(82, 'Camry', 'Forester', 901.05, 'WV29-YLT-0', 'Mauv', 1, '2017-07-08'),
(83, 'Camry', 'Outback', 993.86, 'HD40-ULN-3', 'Blue', 0, '2017-05-10'),
(84, 'Camry', 'Forte', 58.09, 'VM50-QWT-8', 'Aquamarine', 0, '2020-09-24'),
(85, 'Civic', 'XLR-V', 908.14, 'IT82-IJH-3', 'Yellow', 1, '2019-12-30'),
(86, 'Civic', 'Metro', 240.01, 'TP33-CQH-5', 'Indigo', 0, '2013-06-26'),
(87, 'Civic', 'Range Rover', 473.66, 'BC14-IEM-0', 'Puce', 1, '2011-08-07'),
(88, 'Corolla', 'Lumina APV', 250.6, 'KD84-YQP-6', 'Khaki', 0, '2015-05-25'),
(89, 'Mustang', 'Grand Marquis', 13.64, 'PH95-JGK-6', 'Red', 0, '2017-04-22'),
(90, 'Corolla', '911', 620.78, 'DO93-SKX-5', 'Turquoise', 0, '2011-03-17'),
(91, 'Camry', 'Q', 154.63, 'WI80-LCY-2', 'Goldenrod', 0, '2020-11-22'),
(92, 'Mustang', 'Land Cruiser', 982.22, 'VH66-NFJ-6', 'Fuscia', 1, '2015-05-09'),
(93, 'Civic', 'Century', 525.88, 'TP25-UTE-6', 'Blue', 1, '2017-12-17'),
(94, 'Civic', 'SC', 348.57, 'AM69-SDL-4', 'Maroon', 0, '2018-02-19'),
(95, 'Accord', 'Celica', 612.6, 'RY07-KSH-2', 'Turquoise', 0, '2017-07-01'),
(96, 'Mustang', 'Escalade EXT', 401.54, 'RJ41-GWB-1', 'Khaki', 1, '2017-06-24'),
(97, 'Mustang', 'Sable', 76.65, 'NL48-YGB-8', 'Pink', 0, '2020-02-01'),
(98, 'Accord', 'Spyder', 267.44, 'LJ91-NHR-6', 'Puce', 1, '2010-06-03'),
(99, 'Mustang', 'Tiguan', 182.42, 'MU03-IYL-6', 'Teal', 0, '2017-10-28'),
(100, 'Civic', 'MPV', 196.35, 'CK98-MTH-6', 'Puce', 1, '2011-10-21'),
(101, 'MUSTANG ', 'HONDA', 135.3, '133QCB', 'RED', 0, '2024-05-07'),
(102, 'GT 1000 ', 'HONDA', 135.4, '133QES', 'SILVER', 0, '2024-05-08');

-- --------------------------------------------------------

--
-- Table structure for table `rent_status`
--

CREATE TABLE `rent_status` (
  `id` int(11) NOT NULL,
  `status_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rent_status`
--

INSERT INTO `rent_status` (`id`, `status_name`) VALUES
(1, 'ON GOING'),
(2, 'FINISHED'),
(3, 'CANCELED');

-- --------------------------------------------------------

--
-- Table structure for table `transaction_table`
--

CREATE TABLE `transaction_table` (
  `id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  `car_id` int(11) NOT NULL,
  `return_at` date NOT NULL,
  `added_at` date NOT NULL,
  `invoice_no` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cars`
--
ALTER TABLE `cars`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `rent_status`
--
ALTER TABLE `rent_status`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `transaction_table`
--
ALTER TABLE `transaction_table`
  ADD PRIMARY KEY (`id`),
  ADD KEY `client_id` (`client_id`),
  ADD KEY `car_id` (`car_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cars`
--
ALTER TABLE `cars`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=103;

--
-- AUTO_INCREMENT for table `rent_status`
--
ALTER TABLE `rent_status`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `transaction_table`
--
ALTER TABLE `transaction_table`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
