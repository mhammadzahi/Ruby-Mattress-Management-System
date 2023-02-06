-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 06, 2023 at 06:34 AM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
create database  `productionmaster`;
use `productionmaster`;

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `id_customer` int(11) NOT NULL,
  `name_customer` varchar(255) DEFAULT NULL,
  `number_customer` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`id_customer`, `name_customer`, `number_customer`) VALUES
(1, 'Customer 1', '0546932587'),
(14, 'customer 3', 'customer 3'),
(15, 'SS', 'SS');

-- --------------------------------------------------------

--
-- Table structure for table `item`
--

CREATE TABLE `item` (
  `id_item` int(11) NOT NULL,
  `s_width` int(11) DEFAULT NULL,
  `name_item` varchar(255) DEFAULT NULL,
  `category_item` varchar(7) DEFAULT NULL,
  `description_item` varchar(8000) DEFAULT NULL,
  `quantity_item` int(11) DEFAULT NULL,
  `remark_item` varchar(8000) DEFAULT NULL,
  `s_length` int(11) DEFAULT NULL,
  `s_height` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `item`
--

INSERT INTO `item` (`id_item`, `s_width`, `name_item`, `category_item`, `description_item`, `quantity_item`, `remark_item`, `s_length`, `s_height`) VALUES
(53, 4, '4', 'B', '44', 4, '4', 4, 4),
(55, 4, '4', 'A', '4', 4, '4', 4, 4),
(59, 4, '4', 'C', '  fv f', 44, 'f', 4, 4),
(60, 7, '4', 'D', 'g', 9, 'y', 8, 5),
(61, 7, 'fff', 'B', 'ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8ahdss sagyusa asyuas aso8[ wdywq wqiwqb 8', 4, '44', 5, 5),
(62, 1, '4', 'B', 'jssc', 33, 'uuy', 2, 3),
(63, 4, 'item', 'B', 'ddddd', 2, 'rdhdr', 5, 6);

-- --------------------------------------------------------

--
-- Table structure for table `job_card`
--

CREATE TABLE `job_card` (
  `id_job` int(11) NOT NULL,
  `order_date` datetime DEFAULT NULL,
  `delive_date` datetime DEFAULT NULL,
  `location` varchar(255) DEFAULT NULL,
  `area` varchar(255) DEFAULT NULL,
  `type` varchar(55) DEFAULT NULL,
  `saleman` varchar(255) DEFAULT NULL,
  `customer` varchar(255) DEFAULT NULL,
  `item` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `saleman`
--

CREATE TABLE `saleman` (
  `id_saleman` int(11) NOT NULL,
  `name_saleman` varchar(255) DEFAULT NULL,
  `phone_number_saleman` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `saleman`
--

INSERT INTO `saleman` (`id_saleman`, `name_saleman`, `phone_number_saleman`) VALUES
(1, 'person1', '0564463214'),
(2, 'person2', '0564822031'),
(3, 'person3', '0533697412'),
(8, 'fff', '8888888888888888888888'),
(9, '7474', '111111111111');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`id_customer`);

--
-- Indexes for table `item`
--
ALTER TABLE `item`
  ADD PRIMARY KEY (`id_item`);

--
-- Indexes for table `job_card`
--
ALTER TABLE `job_card`
  ADD PRIMARY KEY (`id_job`);

--
-- Indexes for table `saleman`
--
ALTER TABLE `saleman`
  ADD PRIMARY KEY (`id_saleman`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `id_customer` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `item`
--
ALTER TABLE `item`
  MODIFY `id_item` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=64;

--
-- AUTO_INCREMENT for table `job_card`
--
ALTER TABLE `job_card`
  MODIFY `id_job` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `saleman`
--
ALTER TABLE `saleman`
  MODIFY `id_saleman` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
