-- Ordering Donations by Date
SELECT *
FROM Donations 
JOIN Donor ON Donations.Donor_ID = Donor.Donor_ID 
JOIN Admins ON Donations.Admin_ID = Admins.Admin_ID
ORDER BY Donations.Date_of_Donation DESC;

-- Ordering Doctors, Employees, and Admins by Name
SELECT * 
FROM Doctor
ORDER BY First_name;

SELECT * 
FROM Employee
ORDER BY First_name;

SELECT * 
FROM Admins
ORDER BY First_name;

-- Ordering Hospitals by Name and Address
SELECT * 
FROM hospital
ORDER BY name ASC, address;

-- Ordering Donors by Blood Type
SELECT * 
FROM Donor
ORDER BY Blood_Type;

-- Filtering Donations by Blood Types
SELECT * 
FROM Donations
WHERE blood_type = 'A+';

SELECT * 
FROM Donations
WHERE blood_type = 'A-';

SELECT * 
FROM Donations
WHERE blood_type = 'AB+';

SELECT * 
FROM Donations
WHERE blood_type = 'AB-';

SELECT * 
FROM Donations
WHERE blood_type = 'O+';

SELECT * 
FROM Donations
WHERE blood_type = 'O-';

-- Filtering Hospitals by Address
SELECT * 
FROM hospital
WHERE address = 'Cairo, Egypt';

-- Filtering Doctors, Employees, and Admins by Gender
SELECT * 
FROM Doctor
WHERE gender = 'F';

SELECT * 
FROM Employee
WHERE gender = 'M';

SELECT * 
FROM Admins
WHERE gender = 'M';

-- Filtering Donors by Gender
SELECT * 
FROM Donor
WHERE gender = 'F';

-- String Pattern Matching Queries
SELECT * 
FROM Doctor
WHERE First_name LIKE 'A%' OR Last_name LIKE 'F%';

SELECT * 
FROM Donor
WHERE First_name LIKE 'A%' OR Last_name LIKE 'A%';

SELECT * 
FROM Admins
WHERE First_name LIKE 'A%' OR Last_name LIKE 'A%';

SELECT * 
FROM Employee
WHERE First_name LIKE 'A%' OR Last_name LIKE 'A%';

SELECT * 
FROM Donations
WHERE Blood_Type LIKE '%+';

SELECT * 
FROM Donations
WHERE Blood_Type LIKE '%-';

SELECT * 
FROM hospital
WHERE name LIKE 'A%' OR address LIKE 'C%';

-- Join Queries
-- INNER JOIN
SELECT Donor.Donor_ID, Donor.First_name, Donor.Last_name, Donor.Blood_Type, Employee.First_name AS Employee_Name
FROM Donor
INNER JOIN Employee ON Donor.Admin_ID = Employee.Admin_ID;

-- LEFT JOIN
SELECT Doctor.Doctor_ID, Doctor.First_name, Doctor.Last_name, Hospital.Name AS Hospital_Name, Hospital.Address AS Hospital_Address
FROM Doctor
LEFT JOIN Hospital ON Doctor.Hospital_ID = Hospital.Hospital_ID;

-- RIGHT JOIN
SELECT Donations.Donations_ID, Donations.Blood_Type, Donations.Date_of_Donation, Donor.Donor_ID, Donor.First_name, Donor.Last_name 
FROM Donations
RIGHT JOIN Donor ON Donations.Donor_ID = Donor.Donor_ID;

-- FULL OUTER JOIN
SELECT Employee.Employee_ID, Employee.First_name, Employee.Last_name, Hospital.Phone, Hospital.Name AS Hospital_Name, Hospital.Address AS Hospital_Address
FROM Employee
FULL OUTER JOIN Hospital ON Employee.Hospital_ID = Hospital.Hospital_ID;

-- Aggregate Functions
-- COUNT
SELECT COUNT(*) AS Total_Donations
FROM Donations;

SELECT COUNT(*) AS Total_Donors
FROM Donor;

-- AVG
SELECT AVG(Donations_Count) AS Average_Donations_Per_Donor
FROM (
    SELECT Donations.Donations_ID, COUNT(Donor.Donor_ID) AS Donations_Count
    FROM Donations
    LEFT JOIN Donor ON Donations.Donor_ID = Donor.Donor_ID
    GROUP BY Donations.Donations_ID
) AS Donation_Counts;

-- SUM
SELECT SUM(Hospital_ID) AS Total_Hospital_ID
FROM Hospital;

-- MIN: Blood Type with Minimum Donations
SELECT TOP 1 Blood_Type
FROM Donations
GROUP BY Blood_Type
HAVING COUNT(*) = (
    SELECT MIN(Donation_Count)
    FROM (
        SELECT COUNT(*) AS Donation_Count
        FROM Donations
        GROUP BY Blood_Type
    ) AS DonationCounts
);

-- MAX: Blood Type with Maximum Donations
SELECT TOP 1 Blood_Type
FROM Donations
GROUP BY Blood_Type
HAVING COUNT(*) = (
    SELECT MAX(Donation_Count)
    FROM (
        SELECT COUNT(*) AS Donation_Count
        FROM Donations
        GROUP BY Blood_Type
    ) AS DonationCounts
);
