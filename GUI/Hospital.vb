Imports System.Data.SqlClient

Public Class Hospital
    Private dgvHospital As DataGridView

    Private Sub Hospital_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the Hospital form
        Me.Text = "Hospital Registration"
        Me.Size = New Size(800, 600)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set the background image
        Me.BackgroundImage = Image.FromFile("F:\gallery\download\blod.jpg") ' Update the path to your image
        Me.BackgroundImageLayout = ImageLayout.Stretch ' Adjust as needed (Tile, Center, Zoom, None, or Stretch)



        ' Add input controls for the Hospital table columns
        Dim labels() As String = {"Hospital ID", "Name", "Address", "Email", "Phone", "Admin ID"}
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

        ' Add Save button
        Dim btnSave As New Button With {
            .Text = "Save",
            .Location = New Point(70, yPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkBlue,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnSave)

        ' Add Update button (added this button)
        Dim btnUpdate As New Button With {
            .Text = "Update",
            .Location = New Point(190, yPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkGreen,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnUpdate)

        ' Add Delete button
        Dim btnDelete As New Button With {
            .Text = "Delete",
            .Location = New Point(310, yPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.DarkRed,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnDelete)

        ' Add Search button
        Dim btnSearch As New Button With {
            .Text = "Search",
            .Location = New Point(430, yPosition),
            .Size = New Size(100, 40),
            .BackColor = Color.Orange,
            .ForeColor = Color.White,
            .Font = New Font("Arial", 10, FontStyle.Bold)
        }
        Me.Controls.Add(btnSearch)

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

        ' Add DataGridView to display hospital data
        dgvHospital = New DataGridView With {
            .Location = New Point(20, yPosition + 60),
            .Size = New Size(750, 300),
            .ReadOnly = True,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }
        Me.Controls.Add(dgvHospital)



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



        ' Handle button click events
        AddHandler btnSave.Click, AddressOf SaveData
        AddHandler btnUpdate.Click, AddressOf UpdateData ' Added the Update button handler
        AddHandler btnDelete.Click, AddressOf DeleteData
        AddHandler btnSearch.Click, AddressOf SearchData
        AddHandler btnDashboard.Click, AddressOf DashboardButton_Click
        AddHandler btnRefresh.Click, AddressOf RefreshData


        ' Load data into DataGridView
        LoadHospitalData()
    End Sub

    Private Sub LoadHospitalData()
        ' Database connection string
        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "SELECT * FROM Hospital"

        Try
            Using connection As New SqlConnection(connectionString)
                Using adapter As New SqlDataAdapter(query, connection)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    dgvHospital.DataSource = table
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveData(sender As Object, e As EventArgs)
        Dim hospitalID As String = Me.Controls("txtHospitalID").Text
        Dim name As String = Me.Controls("txtName").Text
        Dim address As String = Me.Controls("txtAddress").Text
        Dim email As String = Me.Controls("txtEmail").Text
        Dim phone As String = Me.Controls("txtPhone").Text
        Dim adminID As String = Me.Controls("txtAdminID").Text

        If String.IsNullOrWhiteSpace(hospitalID) OrElse String.IsNullOrWhiteSpace(name) OrElse
           String.IsNullOrWhiteSpace(address) OrElse String.IsNullOrWhiteSpace(email) OrElse
           String.IsNullOrWhiteSpace(phone) OrElse String.IsNullOrWhiteSpace(adminID) Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "INSERT INTO Hospital (Hospital_ID, Name, Address, Email, Phone, Admin_ID) " &
                              "VALUES (@HospitalID, @Name, @Address, @Email, @Phone, @AdminID)"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@HospitalID", Integer.Parse(hospitalID))
                    command.Parameters.AddWithValue("@Name", name)
                    command.Parameters.AddWithValue("@Address", address)
                    command.Parameters.AddWithValue("@Email", email)
                    command.Parameters.AddWithValue("@Phone", phone)
                    command.Parameters.AddWithValue("@AdminID", Integer.Parse(adminID))

                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Hospital data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadHospitalData()
                    Else
                        MessageBox.Show("Failed to save hospital data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' New UpdateData method added (Update button handler)
    Private Sub UpdateData(sender As Object, e As EventArgs)
        Dim hospitalID As String = Me.Controls("txtHospitalID").Text
        Dim name As String = Me.Controls("txtName").Text
        Dim address As String = Me.Controls("txtAddress").Text
        Dim email As String = Me.Controls("txtEmail").Text
        Dim phone As String = Me.Controls("txtPhone").Text
        Dim adminID As String = Me.Controls("txtAdminID").Text

        If String.IsNullOrWhiteSpace(hospitalID) OrElse String.IsNullOrWhiteSpace(name) OrElse
           String.IsNullOrWhiteSpace(address) OrElse String.IsNullOrWhiteSpace(email) OrElse
           String.IsNullOrWhiteSpace(phone) OrElse String.IsNullOrWhiteSpace(adminID) Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "UPDATE Hospital SET Name = @Name, Address = @Address, Email = @Email, " &
                              "Phone = @Phone, Admin_ID = @AdminID WHERE Hospital_ID = @HospitalID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@HospitalID", Integer.Parse(hospitalID))
                    command.Parameters.AddWithValue("@Name", name)
                    command.Parameters.AddWithValue("@Address", address)
                    command.Parameters.AddWithValue("@Email", email)
                    command.Parameters.AddWithValue("@Phone", phone)
                    command.Parameters.AddWithValue("@AdminID", Integer.Parse(adminID))

                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Hospital data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadHospitalData()
                    Else
                        MessageBox.Show("Failed to update hospital data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteData(sender As Object, e As EventArgs)
        Dim hospitalID As String = Me.Controls("txtHospitalID").Text

        If String.IsNullOrWhiteSpace(hospitalID) Then
            MessageBox.Show("Please enter the Hospital ID to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this hospital?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "DELETE FROM Hospital WHERE Hospital_ID = @HospitalID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@HospitalID", Integer.Parse(hospitalID))

                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Hospital data deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadHospitalData()
                    Else
                        MessageBox.Show("Failed to delete hospital data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SearchData(sender As Object, e As EventArgs)
        Dim hospitalID As String = Me.Controls("txtHospitalID").Text

        If String.IsNullOrWhiteSpace(hospitalID) Then
            MessageBox.Show("Please enter the Hospital ID to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"
        Dim query As String = "SELECT * FROM Hospital WHERE Hospital_ID = @HospitalID"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@HospitalID", Integer.Parse(hospitalID))

                    Dim adapter As New SqlDataAdapter(command)
                    Dim table As New DataTable()

                    connection.Open()
                    adapter.Fill(table)

                    If table.Rows.Count > 0 Then
                        dgvHospital.DataSource = table
                    Else
                        MessageBox.Show("No hospital found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        dgvHospital.DataSource = Nothing
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while searching: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        LoadHospitalData()

    End Sub

End Class