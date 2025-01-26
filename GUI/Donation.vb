Imports System.Data.SqlClient

Public Class Donation
    Private dgvDonation As DataGridView

    Private Sub Donation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up Donation form properties
        Me.Text = "Donation Management"
        Me.Size = New Size(800, 600)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set the background image
        Me.BackgroundImage = Image.FromFile("F:\gallery\download\blod.jpg") ' Update the path to your image
        Me.BackgroundImageLayout = ImageLayout.Stretch ' Adjust as needed (Tile, Center, Zoom, None, or Stretch)


        ' Add input controls for the Donations table columns
        Dim labels() As String = {"Donation ID", "Blood Type", "Date of Donation", "Donor ID", "Admin ID"}
        Dim yPosition As Integer = 30

        For Each label As String In labels
            Dim lbl As New Label With {
                .Text = label & ":",
                .Location = New Point(20, yPosition),
                .AutoSize = True,
                .Font = New Font("Arial", 10, FontStyle.Bold)
            }
            Dim txt As New TextBox With {
                .Name = "txt" & label.Replace(" ", ""),
                .Location = New Point(150, yPosition),
                .Width = 300,
                .Font = New Font("Arial", 10)
            }
            Me.Controls.Add(lbl)
            Me.Controls.Add(txt)
            yPosition += 40
        Next

        ' Add buttons
        Dim btnSave As New Button With {
            .Text = "Save",
            .Location = New Point(70, yPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkBlue,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnSave)

        Dim btnUpdate As New Button With {
            .Text = "Update",
            .Location = New Point(190, yPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkGreen,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnUpdate)

        Dim btnDelete As New Button With {
            .Text = "Delete",
            .Location = New Point(310, yPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkRed,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnDelete)

        Dim btnSearch As New Button With {
            .Text = "Search",
            .Location = New Point(430, yPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.Orange,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnSearch)

        ' Add DataGridView
        dgvDonation = New DataGridView With {
            .Location = New Point(20, yPosition + 60),
            .Size = New Size(750, 300),
            .ReadOnly = True,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }
        Me.Controls.Add(dgvDonation)


        ' Add Dashboard button
        Dim btnDashboard As New Button With {
            .Text = "Back to Dashboard",
            .Location = New Point(800, yPosition + 30),
            .Size = New Size(150, 40),
            .BackColor = Color.Gray,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnDashboard)

        ' Add Refresh button
        Dim btnRefresh As New Button With {
    .Text = "Refresh",
    .Location = New Point(540, yPosition),
    .Size = New Size(100, 40),
    .BackColor = Color.DarkCyan,
    .ForeColor = Color.White,
    .Font = New Font("Arial", 10, FontStyle.Bold)
}
        Me.Controls.Add(btnRefresh) ' This line was missing


        ' Assign button click event handlers
        AddHandler btnSave.Click, AddressOf SaveData
        AddHandler btnUpdate.Click, AddressOf UpdateData
        AddHandler btnDelete.Click, AddressOf DeleteData
        AddHandler btnSearch.Click, AddressOf SearchData
        AddHandler btnDashboard.Click, AddressOf DashboardButton_Click
        AddHandler btnRefresh.Click, AddressOf RefreshData



        ' Load data into DataGridView
        LoadDonationData()
    End Sub

    Private Sub LoadDonationData()
        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "SELECT * FROM Donations"

        Try
            Using connection As New SqlConnection(connectionString)
                Using adapter As New SqlDataAdapter(query, connection)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    dgvDonation.DataSource = table
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveData(sender As Object, e As EventArgs)
        Dim donationID As String = Me.Controls("txtDonationID").Text
        Dim bloodType As String = Me.Controls("txtBloodType").Text
        Dim dateOfDonation As String = Me.Controls("txtDateOfDonation").Text
        Dim donorID As String = Me.Controls("txtDonorID").Text
        Dim adminID As String = Me.Controls("txtAdminID").Text

        If String.IsNullOrWhiteSpace(donationID) OrElse String.IsNullOrWhiteSpace(bloodType) OrElse
           String.IsNullOrWhiteSpace(dateOfDonation) OrElse String.IsNullOrWhiteSpace(donorID) OrElse
           String.IsNullOrWhiteSpace(adminID) Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "INSERT INTO Donations (Donations_ID, Blood_Type, Date_of_Donation, Donor_ID, Admin_ID) " &
                              "VALUES (@DonationsID, @BloodType, @DateOfDonation, @DonorID, @AdminID)"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DonationsID", Integer.Parse(donationID))
                    command.Parameters.AddWithValue("@BloodType", bloodType)
                    command.Parameters.AddWithValue("@DateOfDonation", DateTime.Parse(dateOfDonation))
                    command.Parameters.AddWithValue("@DonorID", Integer.Parse(donorID))
                    command.Parameters.AddWithValue("@AdminID", Integer.Parse(adminID))

                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Donation saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadDonationData()
                    Else
                        MessageBox.Show("Failed to save donation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateData(sender As Object, e As EventArgs)
        Dim donationID As String = Me.Controls("txtDonationID").Text
        Dim bloodType As String = Me.Controls("txtBloodType").Text
        Dim dateOfDonation As String = Me.Controls("txtDateOfDonation").Text
        Dim donorID As String = Me.Controls("txtDonorID").Text
        Dim adminID As String = Me.Controls("txtAdminID").Text

        If String.IsNullOrWhiteSpace(donationID) Then
            MessageBox.Show("Please provide the Donation ID to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "UPDATE Donations SET Blood_Type = @BloodType, Date_of_Donation = @DateOfDonation, " &
                          "Donor_ID = @DonorID, Admin_ID = @AdminID WHERE Donations_ID = @DonationsID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DonationsID", Integer.Parse(donationID))
                    command.Parameters.AddWithValue("@BloodType", bloodType)
                    command.Parameters.AddWithValue("@DateOfDonation", DateTime.Parse(dateOfDonation))
                    command.Parameters.AddWithValue("@DonorID", Integer.Parse(donorID))
                    command.Parameters.AddWithValue("@AdminID", Integer.Parse(adminID))

                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Donation updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadDonationData()
                    Else
                        MessageBox.Show("Failed to update donation. Please check the Donation ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteData(sender As Object, e As EventArgs)
        Dim donationID As String = Me.Controls("txtDonationID").Text

        If String.IsNullOrWhiteSpace(donationID) Then
            MessageBox.Show("Please provide the Donation ID to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "DELETE FROM Donations WHERE Donations_ID = @DonationsID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DonationsID", Integer.Parse(donationID))

                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Donation deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadDonationData()
                    Else
                        MessageBox.Show("Failed to delete donation. Please check the Donation ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SearchData(sender As Object, e As EventArgs)
        Dim donationID As String = Me.Controls("txtDonationID").Text

        If String.IsNullOrWhiteSpace(donationID) Then
            MessageBox.Show("Please provide the Donation ID to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "SELECT * FROM Donations WHERE Donations_ID = @DonationsID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DonationsID", Integer.Parse(donationID))

                    connection.Open()
                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            Me.Controls("txtBloodType").Text = reader("Blood_Type").ToString()
                            Me.Controls("txtDateOfDonation").Text = reader("Date_of_Donation").ToString()
                            Me.Controls("txtDonorID").Text = reader("Donor_ID").ToString()
                            Me.Controls("txtAdminID").Text = reader("Admin_ID").ToString()
                            MessageBox.Show("Donation found.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("No donation found with the provided ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DashboardButton_Click(sender As Object, e As EventArgs)
        Dim dashboardForm As New Dashboard()
        dashboardForm.Show()
        Me.Hide()
    End Sub

    Private Sub RefreshData(sender As Object, e As EventArgs)
        ' Clear all textboxes
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Clear()
            End If
        Next

        ' Reload data into DataGridView
        LoadDonationData()
    End Sub


End Class