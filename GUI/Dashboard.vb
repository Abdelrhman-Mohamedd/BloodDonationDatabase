Public Class Dashboard
    Private btnDoctor As Button
    Private btnDonor As Button
    Private btnDonation As Button
    Private btnHospital As Button
    Private btnLogout As Button ' Add the Logout button

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set form title and other properties
        Me.Text = "Welcome to Dashboard"
        Me.Size = New Size(400, 400)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set the background image
        Me.BackgroundImage = Image.FromFile("C:\Users\SAMSUNG\Desktop\add\dash.jpg") ' Update the path to your image
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' Add a "Doctor" button to the form
        btnDoctor = New Button()
        btnDoctor.Size = New Size(100, 40)
        btnDoctor.FlatStyle = FlatStyle.Flat
        btnDoctor.FlatAppearance.BorderSize = 0
        btnDoctor.BackgroundImage = Image.FromFile("C:\Users\SAMSUNG\Desktop\add\doctor.png") ' Path to doctor image
        btnDoctor.BackgroundImageLayout = ImageLayout.Stretch
        AddHandler btnDoctor.Click, AddressOf BtnDoctor_Click
        Me.Controls.Add(btnDoctor)

        ' Add a "Donor" button to the form
        btnDonor = New Button()
        btnDonor.Size = New Size(100, 40)
        btnDonor.FlatStyle = FlatStyle.Flat
        btnDonor.FlatAppearance.BorderSize = 0
        btnDonor.BackgroundImage = Image.FromFile("C:\Users\SAMSUNG\Desktop\add\donor.png") ' Path to donor image
        btnDonor.BackgroundImageLayout = ImageLayout.Stretch
        AddHandler btnDonor.Click, AddressOf BtnDonor_Click
        Me.Controls.Add(btnDonor)

        ' Add a "Donation" button to the form
        btnDonation = New Button()
        btnDonation.Size = New Size(100, 40)
        btnDonation.FlatStyle = FlatStyle.Flat
        btnDonation.FlatAppearance.BorderSize = 0
        btnDonation.BackgroundImage = Image.FromFile("C:\Users\SAMSUNG\Desktop\add\heart.png") ' Path to donation image
        btnDonation.BackgroundImageLayout = ImageLayout.Stretch
        AddHandler btnDonation.Click, AddressOf BtnDonation_Click
        Me.Controls.Add(btnDonation)

        ' Add a "Hospital" button to the form
        btnHospital = New Button()
        btnHospital.Size = New Size(100, 40)
        btnHospital.FlatStyle = FlatStyle.Flat
        btnHospital.FlatAppearance.BorderSize = 0
        btnHospital.BackgroundImage = Image.FromFile("C:\Users\SAMSUNG\Desktop\add\hospital.png") ' Path to hospital image
        btnHospital.BackgroundImageLayout = ImageLayout.Stretch
        AddHandler btnHospital.Click, AddressOf BtnHospital_Click
        Me.Controls.Add(btnHospital)

        ' Add a "Logout" button to the form
        btnLogout = New Button()
        btnLogout.Size = New Size(100, 40)
        btnLogout.Text = "Logout"
        btnLogout.FlatStyle = FlatStyle.Flat
        btnLogout.FlatAppearance.BorderSize = 0
        btnLogout.BackColor = Color.Red
        btnLogout.ForeColor = Color.White
        AddHandler btnLogout.Click, AddressOf BtnLogout_Click
        Me.Controls.Add(btnLogout)

        ' Initial positioning
        CenterControls()
    End Sub

    Private Sub Dashboard_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        CenterControls()
    End Sub

    Private Sub CenterControls()
        If btnDoctor IsNot Nothing Then
            btnDoctor.Location = New Point((Me.ClientSize.Width - btnDoctor.Width) \ 2, (Me.ClientSize.Height - btnDoctor.Height) \ 2 - 60)
        End If
        If btnDonor IsNot Nothing Then
            btnDonor.Location = New Point((Me.ClientSize.Width - btnDonor.Width) \ 2, (Me.ClientSize.Height - btnDonor.Height) \ 2 - 10)
        End If
        If btnDonation IsNot Nothing Then
            btnDonation.Location = New Point((Me.ClientSize.Width - btnDonation.Width) \ 2, (Me.ClientSize.Height - btnDonation.Height) \ 2 + 40)
        End If
        If btnHospital IsNot Nothing Then
            btnHospital.Location = New Point((Me.ClientSize.Width - btnHospital.Width) \ 2, (Me.ClientSize.Height - btnHospital.Height) \ 2 + 90)
        End If
        If btnLogout IsNot Nothing Then
            btnLogout.Location = New Point((Me.ClientSize.Width - btnLogout.Width) \ 2, Me.ClientSize.Height - btnLogout.Height - 20)
        End If
    End Sub

    ' Open the Doctor form when the "Doctor" button is clicked
    Private Sub BtnDoctor_Click(sender As Object, e As EventArgs)
        Dim doctorForm As New Doctor() ' Opens the Doctor form
        doctorForm.Show()
        Me.Hide() ' Optionally hide the current form
    End Sub

    ' Open the Donor form when the "Donor" button is clicked
    Private Sub BtnDonor_Click(sender As Object, e As EventArgs)
        Dim donorForm As New Donor() ' Open the Donor form (ensure the class name is Donor)
        donorForm.Show() ' Opens the Donor form
        Me.Hide() ' Optionally hide the current form
    End Sub

    ' Open the Donation form when the "Donation" button is clicked
    Private Sub BtnDonation_Click(sender As Object, e As EventArgs)
        Dim donationForm As New Donation() ' Open the Donation form (ensure the class name is Donation)
        donationForm.Show() ' Opens the Donation form
        Me.Hide() ' Optionally hide the current form
    End Sub

    ' Open the Hospital form when the "Hospital" button is clicked
    Private Sub BtnHospital_Click(sender As Object, e As EventArgs)
        Dim hospitalForm As New Hospital() ' Open the Hospital form (ensure the class name is Hospital)
        hospitalForm.Show() ' Opens the Hospital form
        Me.Hide() ' Optionally hide the current form
    End Sub

    ' Handle Logout button click
    Private Sub BtnLogout_Click(sender As Object, e As EventArgs)
        ' Show the Login form (Form1)
        Dim loginForm As New Form1()
        loginForm.Show()

        ' Close the current Dashboard form
        Me.Close()
    End Sub
End Class