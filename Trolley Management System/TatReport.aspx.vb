
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic

Public Class TatReport
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If

        If Not IsPostBack Then

            GetGridview()

        End If

    End Sub


    Sub GetGridview()


        Dim Roles As String = Me.Session("Roles").ToString.ToUpper

        If Roles.Contains("ADMIN") Or Roles.Contains("PRDUSER") Then

            cn.Open()
            Dim cmd1 As New SqlCommand("select * from vw_TAT", cn)
            Dim sdt1 As New SqlDataAdapter(cmd1)
            Dim dt1 As New DataTable
            sdt1.Fill(dt1)
            cn.Close()

            GridView1.DataSource = dt1
            GridView1.DataBind()

        Else

            Dim msg As String = "You do not have right to this activity"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

        End If



    End Sub


End Class