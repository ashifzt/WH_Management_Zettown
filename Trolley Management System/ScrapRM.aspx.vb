Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic


Public Class ScrapRM
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If

        If Not IsPostBack Then

            Label1.Visible = False
            Label2.Visible = False
            GetItemData()
            GetSMTData()

        End If
    End Sub



    Sub GetItemData()

        cn.Open()

        Dim cmd As New SqlCommand("select Itemcode from Itemcode", cn)
        Dim sdt As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        sdt.Fill(dt)

        cn.Close()

        Me.DropDownList1.DataSource = dt
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            Me.DropDownList1.Items.Add(dt.Rows(i)(0).ToString)
        Next i

    End Sub


    Sub GetSMTData()

        cn.Open()

        Dim cmd As New SqlCommand("select SmtLine from SmtLine", cn)
        Dim sdt As New SqlDataAdapter(cmd)
        Dim dt As New DataTable()
        sdt.Fill(dt)

        cn.Close()

        Me.DropDownList2.DataSource = dt
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            Me.DropDownList2.Items.Add(dt.Rows(i)(0).ToString)
        Next i

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If DropDownList1.Text = "----Select Itemcode----" OrElse DropDownList2.Text = "----Select SMT Line----" Then

            Dim msg As String = "Select correct Itemcode and SMT Line"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        Else

            Scrap_Row_Material()

        End If


    End Sub


    Sub Scrap_Row_Material()

        Dim FGItemCode, SmtLine, RmPartcode As String
        Dim Qty, RemmainQty As Integer

        Dim id As String

        FGItemCode = DropDownList1.Text
        SmtLine = DropDownList2.Text
        RmPartcode = TextBox1.Text
        Qty = Convert.ToInt32(TextBox2.Text)


        cn.Open()

        Dim cmd3 As New SqlCommand("select top 1 * from RMLoader where ItemCode='" & FGItemCode & "' and SmtLine='" & SmtLine & "' and RmPartCode='" & RmPartcode & "' order by Created_Dt desc", cn)
        Dim sdt3 As New SqlDataAdapter(cmd3)
        Dim dt3 As New DataTable
        sdt3.Fill(dt3)

        cn.Close()

        If dt3.Rows.Count <= 0 Then

            Dim msg As String = RmPartcode & " : is not available to scrap "
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        Else

            id = dt3.Rows(0)(0).ToString()
            RemmainQty = Convert.ToInt32(dt3.Rows(0)(18))

            If RemmainQty <= 0 Then

                Dim msg As String = RmPartcode & " : can not scrap due to Current remaining Quantity is : " & RemmainQty
                Dim script = String.Format("alert('{0}');", msg)
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                Exit Sub

            ElseIf RemmainQty < Qty Then

                Dim msg As String = "can not scrap due to remaining quantity : " & RemmainQty & " is less than Scrap Quantity : " & Qty
                Dim script = String.Format("alert('{0}');", msg)
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                Exit Sub

            Else


                Dim new_remaining_qty = (RemmainQty - Qty)
                Dim Scrapped_Qty_Remarks = "Scrapped_Qty_" & Qty

                '----------------updating scrap in RMloader Table---------------------
                cn.Open()
                Dim cmd As New SqlCommand("update RMLoader set Updated_Dt=GETDATE(), Updated_By='" & Me.Session("Userid") & "', EntryType='Scrap', PcbaNo='" & Scrapped_Qty_Remarks & "',RemainQty='" & new_remaining_qty & "' where id='" & id & "'", cn)
                cmd.ExecuteNonQuery()
                cn.Close()

                '----------------Inserting scrap data in RMLoader_Laser Table---------------------
                cn.Open()
                Dim cmd1 As New SqlCommand("insert into RMLoader_Laser select * from RMLoader where id='" & id & "'", cn)
                cmd1.ExecuteNonQuery()
                cn.Close()

                Dim msg As String = "Scrapped booked Successfully against : " & RmPartcode & " and Quantity : " & Qty
                Dim script = String.Format("alert('{0}');", msg)
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                TextBox1.Text = String.Empty
                TextBox2.Text = String.Empty


            End If


        End If



    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        Dim itemcode As String = DropDownList1.Text


        cn.Open()
        Dim cmd As New SqlCommand("select * from itemcode where itemcode='" & itemcode & "'", cn)
        Dim sdt As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        sdt.Fill(dt)
        cn.Close()

        If dt.Rows.Count <= 0 Then

        Else
            Label2.Visible = True
            Label2.Text = dt.Rows(0)(1).ToString()
        End If

    End Sub
End Class