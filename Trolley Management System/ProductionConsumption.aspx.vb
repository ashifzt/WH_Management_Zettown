Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic

Public Class ProductionConsumption
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
            Dim cmd1 As New SqlCommand("select Id, item, Qty,LastConsumed_DT IssuedDate, expiry_date ,ISNULL(DATEDIFF(d,cast(getdate() as date),cast(expiry_date as date)),365) Remaining_days from TrolleyLaser where Updatetype='ATProduction'", cn)
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

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating

        Dim id As Label
        id = GridView1.Rows(e.RowIndex).FindControl("Label2")

        Dim Remainingdays As Label
        Remainingdays = GridView1.Rows(e.RowIndex).FindControl("Label6")


        If Convert.ToInt32(Remainingdays.Text) >= 0 Then

            'update inventory out and transaction type aas inventory out

            cn.Open()
            Dim cmd As New SqlCommand("update TrolleyLaser set Updatetype='Inv_Out',Transaction_Type='Inv_Out' where id='" & id.Text & "'", cn)
            cmd.ExecuteNonQuery()
            cn.Close()

            Call GetGridview()

        Else

            Dim msg As String = "This is expired Item"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)


        End If




    End Sub


End Class