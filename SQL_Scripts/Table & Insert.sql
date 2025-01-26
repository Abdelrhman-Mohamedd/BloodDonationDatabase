-- Table: Admins
CREATE TABLE Admins (
    Admin_ID INT PRIMARY KEY,
    First_name VARCHAR(50),
    Last_name VARCHAR(50),
    Address VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Gender CHAR(1)
);

-- Table: Hospital
CREATE TABLE Hospital (
    Hospital_ID INT PRIMARY KEY,
    Name VARCHAR(100),
    Address VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Admin_ID INT,
    FOREIGN KEY (Admin_ID) REFERENCES Admins(Admin_ID)
);

-- Table: Donor
CREATE TABLE Donor (
    Donor_ID INT PRIMARY KEY,
    First_name VARCHAR(50),
    Last_name VARCHAR(50),
    Address VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Gender CHAR(1),
    Blood_Type VARCHAR(3),
    Admin_ID INT,
    FOREIGN KEY (Admin_ID) REFERENCES Admins(Admin_ID)
);

-- Table: Donations
CREATE TABLE Donations (
    Donations_ID INT PRIMARY KEY,
    Blood_Type VARCHAR(3),
    Date_of_Donation DATE,
    Donor_ID INT,
    Admin_ID INT,
    FOREIGN KEY (Admin_ID) REFERENCES Admins(Admin_ID),
    FOREIGN KEY (Donor_ID) REFERENCES Donor(Donor_ID)
);

-- Table: Employee
CREATE TABLE Employee (
    Employee_ID INT PRIMARY KEY,
    First_name VARCHAR(50),
    Last_name VARCHAR(50),
    Address VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Gender CHAR(1),
    Admin_ID INT,
    Hospital_ID INT,
    FOREIGN KEY (Admin_ID) REFERENCES Admins(Admin_ID),
    FOREIGN KEY (Hospital_ID) REFERENCES Hospital(Hospital_ID)
);


-- Table: Doctor
CREATE TABLE Doctor (
    Doctor_ID INT PRIMARY KEY,
    First_name VARCHAR(50),
    Last_name VARCHAR(50),
    Address VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Gender CHAR(1),
    Admin_ID INT,
    Hospital_ID INT,
    FOREIGN KEY (Admin_ID) REFERENCES Admins(Admin_ID),
    FOREIGN KEY (Hospital_ID) REFERENCES Hospital(Hospital_ID)
);

-- Table: Sees
CREATE TABLE Sees (
    Doctor_ID INT,
    Donations_ID INT,
    PRIMARY KEY (Doctor_ID, Donations_ID),
    FOREIGN KEY (Doctor_ID) REFERENCES Doctor(Doctor_ID),
    FOREIGN KEY (Donations_ID) REFERENCES Donations(Donations_ID)
);

-- Table: Manage_Donors
CREATE TABLE Manage_Donors (
    Employee_ID INT,
    Donor_ID INT,
    PRIMARY KEY (Employee_ID, Donor_ID),
    FOREIGN KEY (Employee_ID) REFERENCES Employee(Employee_ID),
    FOREIGN KEY (Donor_ID) REFERENCES Donor(Donor_ID)
);



INSERT INTO Hospital (Hospital_ID, Name, Address, Email, Phone, Admin_ID) VALUES
(1, 'Cairo General Hospital', 'Cairo, Egypt', 'cgh@example.com', '01056789123', 1),
(2, 'Alexandria Health Center', 'Alexandria, Egypt', 'ahc@example.com', '01167891234', 2),
(3, 'Giza Medical Facility', 'Giza, Egypt', 'gmf@example.com', '01278912345', 3),
(4, 'Luxor Regional Hospital', 'Luxor, Egypt', 'lrh@example.com', '01098765432', 1),
(5, 'Aswan Medical Center', 'Aswan, Egypt', 'amc@example.com', '01122334455', 2),
(6, 'Port Said Hospital', 'Port Said, Egypt', 'psh@example.com', '01233445566', 3),
(7, 'Hurghada Health Facility', 'Hurghada, Egypt', 'hhf@example.com', '01144556677', 1),
(8, 'Sharm El-Sheikh Clinic', 'Sharm El-Sheikh, Egypt', 'ssc@example.com', '01055667788', 2),
(9, 'Mansoura Medical Institute', 'Mansoura, Egypt', 'mmi@example.com', '01266778899', 3),
(10, 'Tanta General Hospital', 'Tanta, Egypt', 'tgh@example.com', '01177889900', 1),
(11, 'Fayoum Health Center', 'Fayoum, Egypt', 'fhc@example.com', '01088990011', 2),
(12, 'Ismailia Medical Facility', 'Ismailia, Egypt', 'imf@example.com', '01299001122', 3),
(13, 'Suez Regional Hospital', 'Suez, Egypt', 'srh@example.com', '01110111223', 1),
(14, 'Assiut Health Complex', 'Assiut, Egypt', 'ahc@example.com', '01011223344', 2),
(15, 'Sohag Medical Center', 'Sohag, Egypt', 'smc@example.com', '01222334455', 3),
(16, 'Damanhour Clinic', 'Damanhour, Egypt', 'dc@example.com', '01133445566', 1),
(17, 'Qena Regional Hospital', 'Qena, Egypt', 'qrh@example.com', '01044556677', 2),
(18, 'Asyut Specialized Hospital', 'Asyut, Egypt', 'ash@example.com', '01255667788', 3),
(19, 'Gharbia Health Institute', 'Gharbia, Egypt', 'ghi@example.com', '01166778899', 1),
(20, 'Minya Medical Facility', 'Minya, Egypt', 'mmf@example.com', '01077889900', 2);

INSERT INTO Doctor (Doctor_ID, First_name, Last_name, Address, Email, Phone, Gender, Admin_ID, Hospital_ID) VALUES
(1, 'Ahmed', 'Shazly', 'Cairo, Egypt', 'ahmed.shazly@example.com', '01012345678', 'M', 1, 1),
(2, 'Mona', 'El-Sayed', 'Alexandria, Egypt', 'mona.elsayed@example.com', '01098765432', 'F', 2, 2),
(3, 'Khaled', 'Ibrahim', 'Giza, Egypt', 'khaled.ibrahim@example.com', '01112345678', 'M', 3, 3),
(4, 'Samy', 'Farid', 'Luxor, Egypt', 'samy.farid@example.com', '01234567890', 'M', 1, 1),
(5, 'Leila', 'Mahmoud', 'Aswan, Egypt', 'leila.mahmoud@example.com', '01267890123', 'F', 2, 2),
(6, 'Hassan', 'Omar', 'Hurghada, Egypt', 'hassan.omar@example.com', '01123456789', 'M', 3, 3),
(7, 'Nour', 'Hossam', 'Sharm El-Sheikh, Egypt', 'nour.hossam@example.com', '01056789234', 'F', 1, 1),
(8, 'Yara', 'Hassan', 'Cairo, Egypt', 'yara.hassan@example.com', '01087654321', 'F', 2, 2),
(9, 'Ali', 'Zaki', 'Alexandria, Egypt', 'ali.zaki@example.com', '01234567891', 'M', 3, 3),
(10, 'Omar', 'Fathy', 'Giza, Egypt', 'omar.fathy@example.com', '01012398765', 'M', 1, 1),
(11, 'Mariam', 'Kamel', 'Luxor, Egypt', 'mariam.kamel@example.com', '01111222333', 'F', 2, 2),
(12, 'Rania', 'Saad', 'Aswan, Egypt', 'rania.saad@example.com', '01233445566', 'F', 3, 3),
(13, 'Hisham', 'Tarek', 'Hurghada, Egypt', 'hisham.tarek@example.com', '01099887766', 'M', 1, 1),
(14, 'Laila', 'Sami', 'Sharm El-Sheikh, Egypt', 'laila.sami@example.com', '01122334455', 'F', 2, 2),
(15, 'Tamer', 'Ghanem', 'Cairo, Egypt', 'tamer.ghanem@example.com', '01066778899', 'M', 3, 3),
(16, 'Fady', 'Youssef', 'Alexandria, Egypt', 'fady.youssef@example.com', '01277889900', 'M', 1, 1),
(17, 'Nada', 'Shafik', 'Giza, Egypt', 'nada.shafik@example.com', '01188990011', 'F', 2, 2),
(18, 'Shady', 'Hisham', 'Luxor, Egypt', 'shady.hisham@example.com', '01099887788', 'M', 3, 3),
(19, 'Karim', 'Osman', 'Aswan, Egypt', 'karim.osman@example.com', '01122334455', 'M', 1, 1),
(20, 'Eman', 'Salah', 'Hurghada, Egypt', 'eman.salah@example.com', '01233445599', 'F', 2, 2);

INSERT INTO Donor (Donor_ID, First_name, Last_name, Address, Email, Phone, Gender, Blood_Type, Admin_ID) VALUES
(1, 'Youssef', 'Sayed', 'Cairo, Egypt', 'youssef.sayed@example.com', '01022334455', 'M', 'A+', 1),
(2, 'Rana', 'Mahmoud', 'Alexandria, Egypt', 'rana.mahmoud@example.com', '01234567890', 'F', 'O-', 2),
(3, 'Khaled', 'Ibrahim', 'Giza, Egypt', 'khaled.ibrahim@example.com', '01054321876', 'M', 'B+', 3),
(4, 'Salma', 'Ali', 'Sharm El-Sheikh, Egypt', 'salma.ali@example.com', '01123456789', 'F', 'AB-', 1),
(5, 'Ahmed', 'Mohamed', 'Luxor, Egypt', 'ahmed.mohamed@example.com', '01098765432', 'M', 'O+', 2),
(6, 'Dina', 'Mansour', 'Aswan, Egypt', 'dina.mansour@example.com', '01021314567', 'F', 'A-', 3),
(7, 'Omar', 'El-Sharkawy', 'Port Said, Egypt', 'omar.elsharkawy@example.com', '01133456789', 'M', 'B-', 1),
(8, 'Huda', 'Fathi', 'Alexandria, Egypt', 'huda.fathi@example.com', '01245678901', 'F', 'AB+', 2),
(9, 'Mustafa', 'Hassan', 'Giza, Egypt', 'mustafa.hassan@example.com', '01067890123', 'M', 'O-', 3),
(10, 'Layla', 'Salem', 'Sharm El-Sheikh, Egypt', 'layla.salem@example.com', '01178901234', 'F', 'A+', 1),
(11, 'Said', 'Zaki', 'Mansoura, Egypt', 'said.zaki@example.com', '01012356789', 'M', 'B+', 2),
(12, 'Rania', 'Yousef', 'Port Said, Egypt', 'rania.yousef@example.com', '01234567891', 'F', 'O+', 3),
(13, 'Nabil', 'Khalil', 'Suez, Egypt', 'nabil.khalil@example.com', '01098765432', 'M', 'AB-', 1),
(14, 'Nadia', 'Ahmed', 'Hurghada, Egypt', 'nadia.ahmed@example.com', '01122334456', 'F', 'O-', 2),
(15, 'Zeinab', 'Ali', 'Luxor, Egypt', 'zeinab.ali@example.com', '01012345678', 'F', 'A-', 3),
(16, 'Ali', 'Salah', 'Cairo, Egypt', 'ali.salah@example.com', '01123456789', 'M', 'B+', 1),
(17, 'Hussein', 'Ibrahim', 'Tanta, Egypt', 'hussein.ibrahim@example.com', '01012121212', 'M', 'A+', 2),
(18, 'Sara', 'Kamal', 'Ismailia, Egypt', 'sara.kamal@example.com', '01122223333', 'F', 'B-', 3),
(19, 'Nour', 'Hassan', 'Sohag, Egypt', 'nour.hassan@example.com', '01233445566', 'F', 'O+', 1),
(20, 'Omar', 'Zaki', 'Minya, Egypt', 'omar.zaki@example.com', '01044556677', 'M', 'AB-', 2);

INSERT INTO Donations (Donations_ID, Blood_Type, Date_of_Donation, Donor_ID, Admin_ID) VALUES
(1, 'A+', '2023-01-10', 1, 1),
(2, 'O-', '2023-02-15', 2, 2),
(3, 'B+', '2023-03-20', 3, 3),
(4, 'AB-', '2023-04-25', 4, 1),
(5, 'O+', '2023-05-10', 5, 2),
(6, 'A-', '2023-06-18', 6, 3),
(7, 'B-', '2023-07-22', 7, 1),
(8, 'AB+', '2023-08-05', 8, 2),
(9, 'O-', '2023-09-12', 9, 3),
(10, 'A+', '2023-10-01', 10, 1),
(11, 'B+', '2023-11-15', 11, 2),
(12, 'O+', '2023-12-20', 12, 3),
(13, 'AB-', '2024-01-07', 13, 1),
(14, 'O-', '2024-02-14', 14, 2),
(15, 'A-', '2024-03-01', 15, 3),
(16, 'B+', '2024-04-10', 16, 1),
(17, 'A+', '2024-05-18', 17, 2),
(18, 'B-', '2024-06-25', 18, 3),
(19, 'O+', '2024-07-30', 19, 1),
(20, 'AB-', '2024-08-15', 20, 2);

INSERT INTO Employee (Employee_ID, First_name, Last_name, Address, Email, Phone, Gender, Admin_ID, Hospital_ID) VALUES
(1, 'Bakr', 'Elsayed', 'Cairo, Egypt', 'bakr.elsayed@example.com', '01023456789', 'M', 1, 1),
(2, 'Maha', 'Mohamed', 'Alexandria, Egypt', 'maha.mohamed@example.com', '01234567890', 'F', 2, 2),
(3, 'Sami', 'El-Sayed', 'Giza, Egypt', 'sami.elsayed@example.com', '01056789234', 'M', 3, 3),
(4, 'Nour', 'Mokhtar', 'Hurghada, Egypt', 'nour.mokhtar@example.com', '01123456789', 'F', 1, 1),
(5, 'Ali', 'Ghanem', 'Sharm El-Sheikh, Egypt', 'ali.ghanem@example.com', '01098765432', 'M', 2, 2),
(6, 'Yara', 'Fathy', 'Cairo, Egypt', 'yara.fathy@example.com', '01034567890', 'F', 3, 3),
(7, 'Moustafa', 'Hassan', 'Aswan, Egypt', 'moustafa.hassan@example.com', '01267890123', 'M', 1, 1),
(8, 'Rania', 'Shafik', 'Alexandria, Egypt', 'rania.shafik@example.com', '01087654321', 'F', 2, 2),
(9, 'Tariq', 'Salem', 'Giza, Egypt', 'tariq.salem@example.com', '01012312312', 'M', 3, 3),
(10, 'Mona', 'Ghanem', 'Hurghada, Egypt', 'mona.ghanem@example.com', '01111122233', 'F', 1, 1),
(11, 'Fadi', 'El-Sayed', 'Cairo, Egypt', 'fadi.elsayed@example.com', '01022233344', 'M', 2, 2),
(12, 'Hassan', 'Mahmoud', 'Luxor, Egypt', 'hassan.mahmoud@example.com', '01144455566', 'M', 3, 3),
(13, 'Amira', 'Fathy', 'Sharm El-Sheikh, Egypt', 'amira.fathy@example.com', '01067890123', 'F', 1, 1),
(14, 'Samiha', 'Salem', 'Giza, Egypt', 'samiha.salem@example.com', '01098765432', 'F', 2, 2),
(15, 'Bassem', 'Shafik', 'Mansoura, Egypt', 'bassem.shafik@example.com', '01122334455', 'M', 3, 3),
(16, 'Eman', 'Hassan', 'Cairo, Egypt', 'eman.hassan@example.com', '01011122334', 'F', 1, 1),
(17, 'Hanan', 'Yousef', 'Gharbia, Egypt', 'hanan.yousef@example.com', '01066778899', 'F', 2, 2),
(18, 'Maged', 'Fathy', 'Suez, Egypt', 'maged.fathy@example.com', '01277889900', 'M', 3, 3),
(19, 'Alaa', 'Ibrahim', 'Port Said, Egypt', 'alaa.ibrahim@example.com', '01188990011', 'F', 1, 1),
(20, 'Mona', 'Ziad', 'Cairo, Egypt', 'mona.ziad@example.com', '01099887766', 'F', 2, 2);

INSERT INTO Manage_Donors (Employee_ID, Donor_ID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20);

INSERT INTO Sees (Doctor_ID, Donations_ID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20);