Imports System.Data.SqlClient

Public Class Doctor
    Private dgvDoctor As DataGridView

    Private Sub Doctor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the Doctor form
        Me.Text = "Doctor Registration"
        Me.Size = New Size(800, 700) ' Increased size to accommodate wider grid and buttons
        Me.StartPosition = FormStartPosition.CenterScreen


        ' Set the background image
        Me.BackgroundImage = Image.FromFile("F:\gallery\download\doc.jpg") ' Update the path to your image
        Me.BackgroundImageLayout = ImageLayout.Stretch ' Adjust as needed (Tile, Center, Zoom, None, or Stretch)



        ' Add input controls for the Doctor table columns
        Dim labels() As String = {"Doctor ID", "First Name", "Last Name", "Address", "Email", "Phone", "Gender", "Admin ID", "Hospital ID"}
        Dim yPosition As Integer = 30

        ' Create labels and textboxes dynamically
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

        ' Add Buttons
        Dim btnSave As New Button With {.Text = "Save", .Location = New Point(200, yPosition), .Size = New Size(100, 40), .BackColor = Color.DarkBlue, .ForeColor = Color.White}
        Dim btnUpdate As New Button With {.Text = "Update", .Location = New Point(310, yPosition), .Size = New Size(100, 40), .BackColor = Color.DarkGreen, .ForeColor = Color.White}
        Dim btnDelete As New Button With {.Text = "Delete", .Location = New Point(420, yPosition), .Size = New Size(100, 40), .BackColor = Color.DarkRed, .ForeColor = Color.White}
        Dim btnSearch As New Button With {.Text = "Search", .Location = New Point(530, yPosition), .Size = New Size(100, 40), .BackColor = Color.DarkOrange, .ForeColor = Color.White}
        Dim btnRefresh As New Button With {.Text = "Refresh", .Location = New Point(640, yPosition), .Size = New Size(100, 40), .BackColor = Color.DarkCyan, .ForeColor = Color.White}

        Me.Controls.AddRange({btnSave, btnUpdate, btnDelete, btnSearch, btnRefresh})

        ' Add DataGridView
        dgvDoctor = New DataGridView With {.Location = New Point(20, yPosition + 60), .Size = New Size(750, 250), .ReadOnly = True, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
        Me.Controls.Add(dgvDoctor)

        ' Add Dashboard button
        Dim btnDashboard As New Button With {
            .Text = "Back to Dashboard",
            .Location = New Point(900, yPosition + 90),
            .Size = New Size(150, 40),
            .BackColor = Color.Gray,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnDashboard)


        ' Event Handlers
        AddHandler btnSave.Click, AddressOf SaveData
        AddHandler btnUpdate.Click, AddressOf UpdateData
        AddHandler btnDelete.Click, AddressOf DeleteData
        AddHandler btnSearch.Click, AddressOf SearchData
        AddHandler btnRefresh.Click, AddressOf RefreshData
        AddHandler btnDashboard.Click, AddressOf DashboardButton_Click


        LoadDoctorData()
    End Sub

    Private Sub LoadDoctorData()
        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "SELECT * FROM Doctor"

        Try
            Using connection As New SqlConnection(connectionString)
                Using adapter As New SqlDataAdapter(query, connection)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    dgvDoctor.DataSource = table
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub SaveData(sender As Object, e As EventArgs)
        Dim doctorID As String = Me.Controls("txtDoctorID").Text
        Dim firstName As String = Me.Controls("txtFirstName").Text
        Dim lastName As String = Me.Controls("txtLastName").Text
        Dim address As String = Me.Controls("txtAddress").Text
        Dim email As String = Me.Controls("txtEmail").Text
        Dim phone As String = Me.Controls("txtPhone").Text
        Dim gender As String = Me.Controls("txtGender").Text
        Dim adminID As String = Me.Controls("txtAdminID").Text
        Dim hospitalID As String = Me.Controls("txtHospitalID").Text

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "INSERT INTO Doctor VALUES (@DoctorID, @FirstName, @LastName, @Address, @Email, @Phone, @Gender, @AdminID, @HospitalID)"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DoctorID", doctorID)
                    command.Parameters.AddWithValue("@FirstName", firstName)
                    command.Parameters.AddWithValue("@LastName", lastName)
                    command.Parameters.AddWithValue("@Address", address)
                    command.Parameters.AddWithValue("@Email", email)
                    command.Parameters.AddWithValue("@Phone", phone)
                    command.Parameters.AddWithValue("@Gender", gender)
                    command.Parameters.AddWithValue("@AdminID", adminID)
                    command.Parameters.AddWithValue("@HospitalID", hospitalID)

                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Doctor saved successfully!")
                    LoadDoctorData()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub UpdateData(sender As Object, e As EventArgs)
        Dim doctorID As String = Me.Controls("txtDoctorID").Text
        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "UPDATE Doctor SET First_name = @FirstName WHERE Doctor_ID = @DoctorID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DoctorID", doctorID)
                    command.Parameters.AddWithValue("@FirstName", Me.Controls("txtFirstName").Text)

                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Doctor updated successfully!")
                    LoadDoctorData()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub DeleteData(sender As Object, e As EventArgs)
        Dim doctorID As String = Me.Controls("txtDoctorID").Text
        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"

        ' First, delete related records in the Sees table to avoid foreign key conflicts
        Dim deleteSeesQuery As String = "DELETE FROM Sees WHERE Doctor_ID = @DoctorID"

        Try
            ' Delete related Sees records first
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(deleteSeesQuery, connection)
                    command.Parameters.AddWithValue("@DoctorID", doctorID)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using

            ' Now, delete the doctor record
            Dim deleteDoctorQuery As String = "DELETE FROM Doctor WHERE Doctor_ID = @DoctorID"
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(deleteDoctorQuery, connection)
                    command.Parameters.AddWithValue("@DoctorID", doctorID)
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Doctor deleted successfully!")
                    LoadDoctorData()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub SearchData(sender As Object, e As EventArgs)
        Dim doctorID As String = Me.Controls("txtDoctorID").Text
        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "SELECT * FROM Doctor WHERE Doctor_ID = @DoctorID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@DoctorID", doctorID)
                    Using adapter As New SqlDataAdapter(command)
                        Dim table As New DataTable()
                        adapter.Fill(table)
                        dgvDoctor.DataSource = table
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshData(sender As Object, e As EventArgs)
        ' Clear all textboxes
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Clear()
            End If
        Next

        ' Reload data into DataGridView
        LoadDoctorData()

    End Sub
    Private Sub DashboardButton_Click(sender As Object, e As EventArgs)
        Dim dashboardForm As New Dashboard()
        dashboardForm.Show()
        Me.Hide()
    End Sub
End Class