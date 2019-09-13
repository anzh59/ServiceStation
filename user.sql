CREATE USER 'admin'@'localhost' IDENTIFIED BY 'Admin123@';

GRANT ALL PRIVILEGES ON servicestation. * TO 'admin'@'localhost';

FLUSH PRIVILEGES;