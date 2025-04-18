Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlDataAdapter
Imports System.IO

Public Class _Default
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



    Sub Login_Programs()

        Dim command As New SqlCommand("select * from Logindetails where userid = @username and [Userpassword] = @password  COLLATE SQL_Latin1_General_CP1_CS_AS", cn)

        command.Parameters.Add("@username", SqlDbType.VarChar).Value = Me.txtEmail.Text.Trim
        command.Parameters.Add("@password", SqlDbType.VarChar).Value = Me.txtpass.Text.Trim


        Dim adaptor As New SqlDataAdapter(command)
        Dim table1 As New DataTable
        adaptor.Fill(table1)

        If table1.Rows.Count <= 0 Then
            Dim msg As String = "Userid or Password is incorrect"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

        Else
            Me.Session("Userid") = txtEmail.Text.Trim
            Me.Session("pass") = txtpass.Text.Trim
            Me.Session("Username") = table1.Rows(0)(0).ToString
            Me.Session("Roles") = table1.Rows(0)(4).ToString


            Me.txtEmail.Text = String.Empty
            Me.txtpass.Text = String.Empty

            Dim user_role As String

            user_role = Me.Session("Roles")

            If user_role.ToUpper.Contains("WHUSER") Or user_role.ToUpper.Contains("WHMANAGER") Then

                Response.Redirect("~/WH_Issuence.aspx")

            Else
                'MsgBox("ddd")
                Response.Redirect("~/BOM.aspx")
            End If

        End If


    End Sub

    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click

        Call Login_Programs()

    End Sub
End Class