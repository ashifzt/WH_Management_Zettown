Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic

Public Class GateEntry
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If

        If Not IsPostBack Then

            Me.Calendar1.Visible = False
            Me.TextBox3.Enabled = False

        End If

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click

        If Me.Calendar1.Visible = True Then
            Me.Calendar1.Visible = False

        Else
            Me.Calendar1.Visible = True
        End If

    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        Me.TextBox3.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd")
        Me.Calendar1.Visible = False
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim userid As String
        userid = Me.Session("Userid")
        Dim vendor_invoiceno, vendor_name, vendor_invoicedate, invoice_value, vehicle_number, GateEntryNumber, remarks As String

        Dim Roles As String = Me.Session("Roles").ToString.ToUpper

        If Roles.Contains("ADMIN") Or Roles.Contains("GATEENTRY") Then

        Else

            Dim msg As String = "You do not have right to this activity"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)
            Exit Sub

        End If

        '=================================================

        vendor_invoiceno = TextBox1.Text
        vendor_name = TextBox2.Text
        vendor_invoicedate = TextBox3.Text
        invoice_value = TextBox4.Text
        vehicle_number = TextBox5.Text
        remarks = TextBox6.Text

        cn.Open()
        Dim qry As String = "select case when (select max(substring(GateEntryNo,10,4)) from GateEntry where left(GateEntryNo,8)=FORMAT(getdate(),'ddMMyyyy')) is null then FORMAT(getdate(),'ddMMyyyy')+'-1001' else FORMAT(getdate(),'ddMMyyyy')+'-'+ (select(convert(varchar(50),max(substring(GateEntryno,10,4))+1))  from GateEntry where left(GateEntryNo,8)=FORMAT(getdate(),'ddMMyyyy') ) end"
        Dim cmd As New SqlCommand(qry, cn)
        Dim sdt As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        sdt.Fill(dt)
        cn.Close()

        GateEntryNumber = dt.Rows(0)(0)


        cn.Open()
        Dim qry1 As String = "insert into GateEntry(GateEntryNo,VendorInvoiceNo,VendorName,VendorInvoice_Date,InvoiceValue,VehicleNo,GrnStatus,EntryDate,EntryBy,Remarks) values('" & GateEntryNumber & "','" & vendor_invoiceno & "','" & vendor_name & "','" & vendor_invoicedate & "','" & invoice_value & "','" & vehicle_number & "','GRN Pending',Getdate(),'" & userid & "','" & remarks.Trim & "')"
        Dim cmd1 As New SqlCommand(qry1, cn)
        cmd1.ExecuteNonQuery()
        cn.Close()


        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        TextBox3.Text = String.Empty
        TextBox4.Text = String.Empty
        TextBox5.Text = String.Empty
        TextBox6.Text = String.Empty

        Label1.Visible = True
        Label1.Text = "Last Gate Entry done with Gate entry number : " & GateEntryNumber & " for Vendor invoice no : " & vendor_invoiceno
        Label1.ForeColor = System.Drawing.Color.Green
        Label1.BackColor = System.Drawing.Color.Yellow


    End Sub
End Class