Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail

Public Class NewSlot
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If
        '---------------------------

        If Not IsPostBack Then
            Me.TextBox1.Enabled = False
            Me.Label1.Visible = False
        End If

    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.TextBox1.Text = String.Empty
        Me.Label1.Visible = False

        Dim userrole As String = Me.Session("Roles").ToString.ToUpper

        If userrole.Contains("ADMIN") Then

            Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
            Dim cn As New SqlConnection(Connstr)
            Dim cmd1 As New SqlCommand("select max(trolleyindex) from CurrentTI", cn)

            Dim adaptor As New SqlDataAdapter(cmd1)

            Dim table1 As New DataTable
            adaptor.Fill(table1)

            Me.TextBox1.Text = "T" & table1.Rows(0)(0) + 1

        Else

            Dim msg As String = "You have not accessed to Create Trolley, Please connect with Admin"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        End If

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim userrole As String = Me.Session("Roles").ToString.ToUpper

        If userrole.Contains("UPLOADER") Or userrole.Contains("ADMIN") Then

            Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
            Dim cn As New SqlConnection(Connstr)
            Dim cmd2 As New SqlCommand("Exec TrolleyCreation", cn)

            cn.Open()
            cmd2.ExecuteNonQuery()
            cn.Close()



            '------------------------------------Email Notification Send-------------------------------------------


            Dim cmd_3 As New SqlCommand("select * from EmailDL where ReportName='Trolley Creation'", cn)
            cn.Open()
            Dim adaptor1 As New SqlDataAdapter(cmd_3)
            Dim table1 As New DataTable
            adaptor1.Fill(table1)

            Dim emailto, emailcc, emailbcc As String
            emailto = table1.Rows(0)(1).ToString
            emailcc = table1.Rows(0)(2).ToString
            emailbcc = table1.Rows(0)(3).ToString

            '----------emailing----------

            Dim Smtp_Server As New SmtpClient

            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("notification@neolync.com", "Zuf97574")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.office365.com"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("notification@neolync.com")
            e_mail.To.Add(emailto)
            e_mail.CC.Add(emailcc)
            e_mail.Bcc.Add(emailbcc)

            e_mail.Subject = "New Trolley Attached : " & Me.TextBox1.Text

            e_mail.IsBodyHtml = False
            e_mail.Body = "Hi Team, " & vbNewLine & vbNewLine & "New Trolley is Attached in Trolley Management System with Id :" & Me.TextBox1.Text & "" & vbNewLine & vbNewLine & "Regards," & vbNewLine & "Server Neolync on behalaf of " & Me.Session("userName")
            Smtp_Server.Send(e_mail)


            '----------emailing-----------------

            Me.TextBox1.Text = String.Empty
            Me.Label1.Text = "Trolley is Successfully attcahed."
            Me.Label1.Visible = True


        Else

            Dim msg As String = "You have not accessed to Attach Trolley"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        End If


    End Sub
End Class