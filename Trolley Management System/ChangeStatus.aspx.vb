
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic


Public Class ChangeStatus
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If


        If Not IsPostBack Then

            Select_Status()

        End If

    End Sub



    Sub Select_Status()

        cn.Open()
        Dim cmd As New SqlCommand("select distinct(DropdownData) from GRN_Status", cn)
        Dim sdt As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        sdt.Fill(dt)
        cn.Close()

        If dt.Rows.Count >= 1 Then


            DropDownList1.DataTextField = "DropdownData"
            DropDownList1.DataValueField = "DropdownData"

            DropDownList1.DataSource = dt
            DropDownList1.DataBind()


        End If


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim dropdowndata, gridviewselection, userrole As String

        Dim Roles As String = Me.Session("Roles").ToString.ToUpper

        dropdowndata = Me.DropDownList1.SelectedItem.Text

        cn.Open()
        Dim cmd As New SqlCommand("select * from GRN_Status where DropdownData='" & dropdowndata & "'", cn)
        Dim sdt As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        sdt.Fill(dt)
        cn.Close()

        If dt.Rows.Count >= 1 Then

            gridviewselection = dt.Rows(0)("GridviewSelection")
            userrole = dt.Rows(0)("UserRoles")

        End If

        If Roles.Contains("ADMIN") Or Roles.Contains(userrole.ToUpper) Then

            cn.Open()
            Dim cmd1 As New SqlCommand("select id, UploadNo, Workorder GrnNumber, SequenceNo VendorInvoiceNo,item,Qty from picklist where Status1='" & gridviewselection & "'", cn)
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
        id = GridView1.Rows(e.RowIndex).FindControl("Label1")

        '----------------------

        Dim dropdowndata, gridviewselection, userrole As String

        Dim Roles As String = Me.Session("Roles").ToString.ToUpper

        dropdowndata = Me.DropDownList1.SelectedItem.Text

        cn.Open()
        Dim cmd As New SqlCommand("select * from GRN_Status where DropdownData='" & dropdowndata & "'", cn)
        Dim sdt As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        sdt.Fill(dt)
        cn.Close()

        If dt.Rows.Count >= 1 Then

            gridviewselection = dt.Rows(0)("GridviewSelection")
            userrole = dt.Rows(0)("UserRoles")

        End If

        If Roles.Contains("ADMIN") Or Roles.Contains(userrole.ToUpper) Then

            cn.Open()
            Dim cmd10 As New SqlCommand("update Picklist set Status1='" & dropdowndata & "'," & dropdowndata & "= getdate() where id='" & id.Text & "'", cn)
            cmd10.ExecuteNonQuery()
            cn.Close()


            cn.Open()
            Dim cmd1 As New SqlCommand("select id, UploadNo, Workorder GrnNumber, SequenceNo VendorInvoiceNo,item,Qty from picklist where Status1='" & gridviewselection & "'", cn)
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


        'MsgBox(id.Text)


    End Sub
End Class