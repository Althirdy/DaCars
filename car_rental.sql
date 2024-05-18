  -- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 18, 2024 at 07:21 AM
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
  `car_status` int(11) NOT NULL DEFAULT 1,
  `added_at` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `cars`
--

INSERT INTO `cars` (`id`, `car_name`, `car_model`, `price`, `plate_no`, `color`, `status`, `car_status`, `added_at`) VALUES
(3, 'Toyota Corolla', 'toyota', 1500, '3xs-eqw', 'silver', 0, 1, '2024-05-15'),
(4, 'Honda Civic', 'Honda', 2000, 'dsa-eq3', 'silver', 0, 1, '2024-05-15'),
(5, 'GT 10000', 'Mustang', 1500, '32s-wsd', 'White', 0, 1, '2024-05-15'),
(6, 'Toyota Camry', '2020', 2500, ' DEF-5678', 'Silver', 0, 1, '2024-05-15'),
(9, 'Mustang', 'Ford', 3500, 'JKL-1121', 'Green', 0, 1, '2024-05-15'),
(10, 'BMW x5', '2018', 3000, 'GHI-9101', 'White', 0, 1, '2024-05-15'),
(11, 'Mercedes-Benz E-Class', '2022', 5500, 'MNO-3141', 'White', 0, 1, '2024-05-15'),
(12, 'Nissan Altima', '2020', 2000, 'STU-7181', 'Silver', 0, 1, '2024-05-15'),
(13, ' Kia Optima', '2018', 1000, ' BCD-3231', 'silver', 0, 1, '2024-05-15'),
(14, 'HIACE', 'toyota', 2500, '132-SGT', 'black', 0, 1, '2024-05-15'),
(15, 'Grandia', 'Toyota', 3000, 'WE3-dsa', 'black', 0, 1, '2024-05-15'),
(16, 'Tric', 'Honda', 2500, 'qwe-sat', 'silver', 0, 1, '2024-05-15'),
(17, 'HIACE GRANDIA', '2022', 2500, 'pst-eqw', 'silver', 0, 2, '2024-05-15');

-- --------------------------------------------------------

--
-- Table structure for table `car_status`
--

CREATE TABLE `car_status` (
  `id` int(11) NOT NULL,
  `car_status` varchar(20) NOT NULL,
  `added_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `car_status`
--

INSERT INTO `car_status` (`id`, `car_status`, `added_at`) VALUES
(1, 'IN GARAGE', '2024-05-16 02:56:19'),
(2, 'NOT IN GARAGE', '2024-05-16 02:56:19');

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `id` int(11) NOT NULL,
  `first_name` varchar(59) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `contact_no` varchar(20) NOT NULL,
  `email` text NOT NULL,
  `driver_license_no` varchar(50) NOT NULL,
  `rent_count` int(11) NOT NULL,
  `added_at` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`id`, `first_name`, `last_name`, `contact_no`, `email`, `driver_license_no`, `rent_count`, `added_at`) VALUES
(1, 'Alfredo', 'Sanger', '09301420649', 'althirdysanger@gmail.com', 'DL-1234567890', 0, '2024-05-15'),
(2, 'Sherby', 'Sanger', '09763866387', 'sherbycabangon@gmail.com', 'DL-9876543', 0, '2024-05-15'),
(4, 'John', 'Doe', '09324151231', 'john@gmail.com', 'DL-323123123', 0, '2024-05-15');

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
  `invoice_no` varchar(70) NOT NULL,
  `total_amount` double NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transaction_table`
--

INSERT INTO `transaction_table` (`id`, `client_id`, `car_id`, `return_at`, `added_at`, `invoice_no`, `total_amount`, `status`) VALUES
(7, 2, 3, '2024-05-16', '2024-05-15', 'INV-20240515155146-cfe0-2', 1500, 2),
(8, 2, 4, '2024-05-16', '2024-05-15', 'INV-20240515155456-28a3-2', 2000, 2),
(9, 4, 14, '2024-05-17', '2024-05-15', 'INV-20240515161907-9da5-4', 2500, 2),
(10, 2, 5, '2024-05-16', '2024-05-15', 'INV-20240515162727-684a-2', 1500, 2),
(11, 2, 11, '2024-05-16', '2024-05-15', 'INV-20240515163300-57c3-2', 5500, 2),
(12, 2, 12, '2024-05-16', '2024-05-15', 'INV-20240515163658-bfde-2', 2000, 2),
(13, 2, 17, '2024-05-17', '2024-05-15', 'INV-20240515163930-4813-2', 2000, 2),
(14, 2, 9, '2024-05-24', '2024-05-16', 'INV-20240515164200-e6a0-2', 28000, 3),
(15, 1, 10, '2024-05-20', '2024-05-16', 'INV-20240515190821-9105-1', 12000, 3),
(16, 2, 17, '2024-05-19', '2024-05-16', 'INV-20240516083716-a617-2', 3000, 2),
(17, 1, 17, '2024-05-21', '2024-05-19', 'INV-20240517135437-6468-1', 5000, 3),
(18, 4, 17, '2024-05-18', '2024-05-17', 'INV-20240517140032-9de4-4', 2500, 2),
(19, 1, 10, '2024-05-20', '2024-05-17', 'INV-20240517140200-f8e3-1', 9000, 2);

-- --------------------------------------------------------

--
-- Table structure for table `trans_status`
--

CREATE TABLE `trans_status` (
  `id` int(11) NOT NULL,
  `status` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `trans_status`
--

INSERT INTO `trans_status` (`id`, `status`) VALUES
(1, 'ON GOING'),
(2, 'FINISHED'),
(3, 'CANCELED'),
(4, 'RESERVED');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` text NOT NULL,
  `added_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `username`, `password`, `added_at`) VALUES
(1, 'admin', 'e3274be5c857fb42ab72d786e281b4b8', '2024-05-17 07:53:25');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cars`
--
ALTER TABLE `cars`
  ADD PRIMARY KEY (`id`),
  ADD KEY `car_status` (`status`) USING BTREE,
  ADD KEY `car_status_id` (`car_status`);

--
-- Indexes for table `car_status`
--
ALTER TABLE `car_status`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`id`),
  ADD KEY `driver_license` (`driver_license_no`);

--
-- Indexes for table `transaction_table`
--
ALTER TABLE `transaction_table`
  ADD PRIMARY KEY (`id`),
  ADD KEY `client_id` (`client_id`),
  ADD KEY `car_id` (`car_id`),
  ADD KEY `trans_status_id` (`status`);

--
-- Indexes for table `trans_status`
--
ALTER TABLE `trans_status`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cars`
--
ALTER TABLE `cars`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `car_status`
--
ALTER TABLE `car_status`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `transaction_table`
--
ALTER TABLE `transaction_table`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `trans_status`
--
ALTER TABLE `trans_status`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cars`
--
ALTER TABLE `cars`
  ADD CONSTRAINT `car_status_id` FOREIGN KEY (`car_status`) REFERENCES `car_status` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `transaction_table`
--
ALTER TABLE `transaction_table`
  ADD CONSTRAINT `Transact_id` FOREIGN KEY (`status`) REFERENCES `trans_status` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `car_id` FOREIGN KEY (`car_id`) REFERENCES `cars` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `customer_id` FOREIGN KEY (`client_id`) REFERENCES `customer` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
