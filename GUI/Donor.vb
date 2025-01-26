Imports System.Data.SqlClient

Public Class Donor
    Inherits Form

    Private dataGridViewDonor As DataGridView

    Private Sub Donor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the properties of the form such as title, size, and screen position.
        Me.Text = "Donor Registration"
        Me.Size = New Size(800, 600)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set the background image
        Me.BackgroundImage = Image.FromFile("F:\gallery\download\blod.jpg") ' Update the path to your image
        Me.BackgroundImageLayout = ImageLayout.Stretch ' Adjust as needed (Tile, Center, Zoom, None, or Stretch)



        ' Define the labels for the donor information fields.
        Dim fieldLabels() As String = {"Donor ID", "First Name", "Last Name", "Address", "Email", "Phone", "Gender", "Blood Type", "Admin ID"}
        Dim currentYPosition As Integer = 30

        ' Dynamically create labels and text boxes for each field.
        For Each fieldLabel As String In fieldLabels
            Dim label As New Label With {
                .Text = fieldLabel & ":",
                .Location = New Point(20, currentYPosition),
                .AutoSize = True,
                .Font = New Font("Arial", 10, FontStyle.Bold)
            }
            Dim textBox As New TextBox With {
                .Name = "textBox" & fieldLabel.Replace(" ", ""),
                .Location = New Point(150, currentYPosition),
                .Width = 300,
                .Font = New Font("Arial", 10)
            }
            Me.Controls.Add(label)
            Me.Controls.Add(textBox)
            currentYPosition += 40
        Next

        ' Add buttons for Save, Update, Delete, and Search.
        Dim saveButton As New Button With {
            .Text = "Save",
            .Location = New Point(70, currentYPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkBlue,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(saveButton)

        Dim updateButton As New Button With {
            .Text = "Update",
            .Location = New Point(190, currentYPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkGreen,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(updateButton)

        Dim deleteButton As New Button With {
            .Text = "Delete",
            .Location = New Point(310, currentYPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkRed,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(deleteButton)

        Dim searchButton As New Button With {
            .Text = "Search",
            .Location = New Point(430, currentYPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.Orange,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(searchButton)

        ' Add Dashboard button
        Dim btnDashboard As New Button With {
            .Text = "Back to Dashboard",
            .Location = New Point(900, currentYPosition),
            .Size = New Size(150, 40),
            .BackColor = Color.Gray,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnDashboard)

        ' Add Refresh button
        Dim btnRefresh As New Button With {
    .Text = "Refresh",
     .Location = New Point(700, currentYPosition),
    .Size = New Size(100, 40),
    .BackColor = Color.DarkCyan,
    .ForeColor = Color.White,
    .Font = New Font("Arial", 10, FontStyle.Bold)
}
        Me.Controls.Add(btnRefresh) ' This line was missing


        ' Add a DataGridView to display donor information.
        dataGridViewDonor = New DataGridView With {
            .Location = New Point(20, currentYPosition + 60),
            .Size = New Size(750, 300),
            .ReadOnly = True,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }
        Me.Controls.Add(dataGridViewDonor)

        ' Attach event handlers for button clicks.
        AddHandler saveButton.Click, AddressOf SaveDonorInformation
        AddHandler updateButton.Click, AddressOf UpdateDonorInformation
        AddHandler deleteButton.Click, AddressOf DeleteDonorInformation
        AddHandler searchButton.Click, AddressOf SearchDonorInformation
        AddHandler btnDashboard.Click, AddressOf DashboardButton_Click
        AddHandler btnRefresh.Click, AddressOf RefreshData



        ' Load donor data into the DataGridView when the form loads.
        LoadDonorData()
    End Sub

    Private Sub LoadDonorData()
        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "SELECT * FROM Donor"

        Try
            Using connection As New SqlConnection(connectionString)
                Using dataAdapter As New SqlDataAdapter(query, connection)
                    Dim dataTable As New DataTable()
                    dataAdapter.Fill(dataTable)
                    dataGridViewDonor.DataSource = dataTable
                End Using
            End Using
        Catch exception As Exception
            MessageBox.Show("Error loading donor data: " & exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveDonorInformation(sender As Object, e As EventArgs)
        Dim donorID As String = Me.Controls("textBoxDonorID").Text
        Dim firstName As String = Me.Controls("textBoxFirstName").Text
        Dim lastName As String = Me.Controls("textBoxLastName").Text
        Dim address As String = Me.Controls("textBoxAddress").Text
        Dim email As String = Me.Controls("textBoxEmail").Text
        Dim phone As String = Me.Controls("textBoxPhone").Text
        Dim gender As String = Me.Controls("textBoxGender").Text
        Dim bloodType As String = Me.Controls("textBoxBloodType").Text
        Dim adminID As String = Me.Controls("textBoxAdminID").Text

        If String.IsNullOrWhiteSpace(donorID) OrElse String.IsNullOrWhiteSpace(firstName) Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "INSERT INTO Donor (Donor_ID, First_name, Last_name, Address, Email, Phone, Gender, Blood_Type, Admin_ID) 
                               VALUES (@DonorID, @FirstName, @LastName, @Address, @Email, @Phone, @Gender, @BloodType, @AdminID)"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DonorID", Integer.Parse(donorID))
                    command.Parameters.AddWithValue("@FirstName", firstName)
                    command.Parameters.AddWithValue("@LastName", lastName)
                    command.Parameters.AddWithValue("@Address", address)
                    command.Parameters.AddWithValue("@Email", email)
                    command.Parameters.AddWithValue("@Phone", phone)
                    command.Parameters.AddWithValue("@Gender", gender)
                    command.Parameters.AddWithValue("@BloodType", bloodType)
                    command.Parameters.AddWithValue("@AdminID", Integer.Parse(adminID))

                    connection.Open()
                    If command.ExecuteNonQuery() > 0 Then
                        MessageBox.Show("Donor saved successfully.", "Success")
                        LoadDonorData()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving donor: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateDonorInformation(sender As Object, e As EventArgs)
        Dim donorID As String = Me.Controls("textBoxDonorID").Text
        Dim firstName As String = Me.Controls("textBoxFirstName").Text
        Dim lastName As String = Me.Controls("textBoxLastName").Text
        Dim address As String = Me.Controls("textBoxAddress").Text
        Dim email As String = Me.Controls("textBoxEmail").Text
        Dim phone As String = Me.Controls("textBoxPhone").Text
        Dim gender As String = Me.Controls("textBoxGender").Text
        Dim bloodType As String = Me.Controls("textBoxBloodType").Text
        Dim adminID As String = Me.Controls("textBoxAdminID").Text

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "UPDATE Donor SET First_name = @FirstName, Last_name = @LastName, Address = @Address, Email = @Email, 
                                Phone = @Phone, Gender = @Gender, Blood_Type = @BloodType, Admin_ID = @AdminID WHERE Donor_ID = @DonorID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DonorID", Integer.Parse(donorID))
                    command.Parameters.AddWithValue("@FirstName", firstName)
                    command.Parameters.AddWithValue("@LastName", lastName)
                    command.Parameters.AddWithValue("@Address", address)
                    command.Parameters.AddWithValue("@Email", email)
                    command.Parameters.AddWithValue("@Phone", phone)
                    command.Parameters.AddWithValue("@Gender", gender)
                    command.Parameters.AddWithValue("@BloodType", bloodType)
                    command.Parameters.AddWithValue("@AdminID", Integer.Parse(adminID))

                    connection.Open()
                    If command.ExecuteNonQuery() > 0 Then
                        MessageBox.Show("Donor updated successfully.", "Success")
                        LoadDonorData()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating donor: " & ex.Message)
        End Try
    End Sub

    Private Sub DeleteDonorInformation(sender As Object, e As EventArgs)
        Dim donorID As String = Me.Controls("textBoxDonorID").Text

        ' Validate that the donor ID is provided
        If String.IsNullOrWhiteSpace(donorID) Then
            MessageBox.Show("Please provide a valid Donor ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                ' Step 1: Delete related records from the Donations table
                Dim deleteDonationsQuery As String = "DELETE FROM Donations WHERE Donor_ID = @DonorID"
                Using commandDeleteDonations As New SqlCommand(deleteDonationsQuery, connection)
                    commandDeleteDonations.Parameters.AddWithValue("@DonorID", Integer.Parse(donorID))
                    commandDeleteDonations.ExecuteNonQuery()
                End Using

                ' Step 2: Delete the donor from the Donor table
                Dim deleteDonorQuery As String = "DELETE FROM Donor WHERE Donor_ID = @DonorID"
                Using commandDeleteDonor As New SqlCommand(deleteDonorQuery, connection)
                    commandDeleteDonor.Parameters.AddWithValue("@DonorID", Integer.Parse(donorID))

                    If commandDeleteDonor.ExecuteNonQuery() > 0 Then
                        MessageBox.Show("Donor and associated donations deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No donor found with the specified ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using

            ' Reload the data grid
            LoadDonorData()
        Catch ex As Exception
            MessageBox.Show("Error deleting donor: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SearchDonorInformation(sender As Object, e As EventArgs)
        Dim donorID As String = Me.Controls("textBoxDonorID").Text

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "SELECT * FROM Donor WHERE Donor_ID = @DonorID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DonorID", Integer.Parse(donorID))

                    Using adapter As New SqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)

                        If dataTable.Rows.Count > 0 Then
                            dataGridViewDonor.DataSource = dataTable
                        Else
                            MessageBox.Show("No donor found with the provided ID.", "Info")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error searching donor: " & ex.Message)
        End Try
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'Donor
        '
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Name = "Donor"
        Me.ResumeLayout(False)

    End Sub

    Private Sub Donor_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

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
        LoadDonorData()

    End Sub


End Class