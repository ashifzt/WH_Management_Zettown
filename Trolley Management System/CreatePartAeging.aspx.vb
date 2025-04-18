Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic

Public Class CreatePartAeging
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.Session("Userid") Is Nothing Then

            Response.Redirect("~/Default.aspx")
        Else
            Call Getdata_onGridview()

        End If

    End Sub


    Sub Getdata_onGridview()


        cn.Open()

        Dim cmd2 As New SqlCommand("select Id, Partnumber, WarningMinutes,AegingMinutes from partTAT", cn)
        Dim adaptor As New SqlDataAdapter(cmd2)
        Dim table1 As New DataTable
        adaptor.Fill(table1)

        cn.Close()

        GridView1.DataSource = table1
        GridView1.DataBind()


    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Roles As String = Me.Session("Roles").ToString.ToUpper

        Dim partnumber As String
        Dim Warning_minute, Aeging_minute As Integer

        partnumber = TextBox1.Text
        Warning_minute = Convert.ToInt32(TextBox2.Text)
        Aeging_minute = Convert.ToInt32(TextBox3.Text)


        If Roles.Contains("ADMIN") Or Roles.Contains("AGING") Then

            cn.Open()
            Dim cmd1 As New SqlCommand("insert into PartTAT(Partnumber,WarningMinutes,AegingMinutes)values('" & partnumber & "'," & Warning_minute & "," & Aeging_minute & ")", cn)
            cmd1.ExecuteNonQuery()
            cn.Close()

            Call Getdata_onGridview()

            TextBox1.Text = String.Empty
            TextBox2.Text = String.Empty
            TextBox3.Text = String.Empty


        Else

            Dim msg As String = "You do not have right to this activity"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

        End If


    End Sub
End Class