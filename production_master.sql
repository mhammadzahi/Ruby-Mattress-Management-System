-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 08, 2023 at 02:02 PM
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
-- Database: `productionmaster`
--

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
(20, 'customer1', '0531459745');

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
(68, 4, 'item12', 'C', '6754', 77, '799', 4, 7);

-- --------------------------------------------------------

--
-- Table structure for table `items_combo`
--

CREATE TABLE `items_combo` (
  `id_` int(11) NOT NULL,
  `name_` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `items_combo`
--

INSERT INTO `items_combo` (`id_`, `name_`) VALUES
(13, 'item12');

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
  `item` varchar(255) DEFAULT NULL,
  `drawing` longblob DEFAULT NULL,
  `lift_size_len` int(11) DEFAULT NULL,
  `lift_size_width` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `job_card`
--

INSERT INTO `job_card` (`id_job`, `order_date`, `delive_date`, `location`, `area`, `type`, `saleman`, `customer`, `item`, `drawing`, `lift_size_len`, `lift_size_width`) VALUES
(52, '2023-02-07 16:37:09', '2023-02-23 16:37:09', 'Ajman', 'ki', 'BDG', 'Sardar', 'customer1', 'item12', NULL, 4, 4);

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
(11, 'Sardar2', '05456325');

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
-- Indexes for table `items_combo`
--
ALTER TABLE `items_combo`
  ADD PRIMARY KEY (`id_`);

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
  MODIFY `id_customer` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `item`
--
ALTER TABLE `item`
  MODIFY `id_item` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=69;

--
-- AUTO_INCREMENT for table `items_combo`
--
ALTER TABLE `items_combo`
  MODIFY `id_` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `job_card`
--
ALTER TABLE `job_card`
  MODIFY `id_job` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=53;

--
-- AUTO_INCREMENT for table `saleman`
--
ALTER TABLE `saleman`
  MODIFY `id_saleman` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
