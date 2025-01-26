Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1
    Inherits Form

    ' UI Controls
    Private lblUsername As Label
    Private lblPassword As Label
    Private txtUsername As TextBox
    Private txtPassword As TextBox
    Private btnLogin As Button
    Private btnExit As Button
    Private lblMessage As Label

    ' Connection string to your database
    Private connectionString As String = "Data Source=DESKTOP-NM01V18;Initial Catalog=Blood Donation;Integrated Security=True"

    ' Constructor
    Public Sub New()
        ' Form properties
        Me.Text = "Login Form"
        Me.Size = New Size(400, 300)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set the background image
        Me.BackgroundImage = Image.FromFile("F:\gallery\download\blood.jpg") ' Path to your background image
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Initialize controls
        lblUsername = New Label() With {.Text = "Username:", .Location = New Point(50, 50), .Size = New Size(100, 20), .BackColor = Color.Transparent}
        lblPassword = New Label() With {.Text = "Password:", .Location = New Point(50, 100), .Size = New Size(100, 20), .BackColor = Color.Transparent}
        txtUsername = New TextBox() With {.Location = New Point(150, 50), .Size = New Size(150, 20)}
        txtPassword = New TextBox() With {.Location = New Point(150, 100), .Size = New Size(150, 20), .PasswordChar = "*"c}
        btnLogin = New Button() With {.Text = "Login", .Location = New Point(150, 150), .Size = New Size(70, 30)}
        btnExit = New Button() With {.Text = "Exit", .Location = New Point(230, 150), .Size = New Size(70, 30)}
        lblMessage = New Label() With {.Location = New Point(50, 200), .Size = New Size(300, 20), .ForeColor = Color.Red, .BackColor = Color.Transparent}

        ' Add controls to form
        Me.Controls.Add(lblUsername)
        Me.Controls.Add(lblPassword)
        Me.Controls.Add(txtUsername)
        Me.Controls.Add(txtPassword)
        Me.Controls.Add(btnLogin)
        Me.Controls.Add(btnExit)
        Me.Controls.Add(lblMessage)

        ' Event handlers
        AddHandler btnLogin.Click, AddressOf BtnLogin_Click
        AddHandler btnExit.Click, AddressOf BtnExit_Click
    End Sub

    ' Login button click event
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs)
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) Then
            lblMessage.Text = "Please enter both username and password."
            Return
        End If

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                ' Query to check credentials
                Dim query As String = "SELECT Username FROM Users WHERE Username = @Username AND Password = @Password"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Username", username)
                    command.Parameters.AddWithValue("@Password", password)

                    Dim result As Object = command.ExecuteScalar()

                    If result IsNot Nothing Then
                        lblMessage.Text = "Login successful!"
                        lblMessage.ForeColor = Color.Green

                        ' Navigate to Dashboard
                        Dim dashboard As New Dashboard()
                        dashboard.Show()
                        Me.Hide()
                    Else
                        lblMessage.Text = "Invalid username or password."
                        lblMessage.ForeColor = Color.Red
                    End If
                End Using
            End Using
        Catch ex As Exception
            lblMessage.Text = "An error occurred: " & ex.Message
            lblMessage.ForeColor = Color.Red
        End Try
    End Sub

    ' Exit button click event
    Private Sub BtnExit_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class
