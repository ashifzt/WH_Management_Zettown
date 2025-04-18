Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic

Public Class RmLoader
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


    Sub RawMaterial_Loader()

        Dim FGItemCode, SmtLine As String
        Dim Qty As Integer
        Dim RmPartcode, Grn, LotNumber, CustomerName, VendorPartcode As String
        Dim str_GrnDate, str_ExpiryDate, str_MfgDate As String
        Dim GrnDate, ExpiryDate, MfgDate As Date

        Dim usedrid As String = Session("Userid")

        FGItemCode = DropDownList1.Text
        SmtLine = DropDownList2.Text

        Dim Qrcode As String = TextBox1.Text

        RmPartcode = Qrcode.Split("/")(0).ToString()
        Qty = Convert.ToInt32(Qrcode.Split("/")(1))
        Grn = Qrcode.Split("/")(2).ToString()
        str_GrnDate = Qrcode.Split("/")(3).ToString() 'String Date
        LotNumber = Qrcode.Split("/")(4).ToString()
        CustomerName = Qrcode.Split("/")(5).ToString()
        VendorPartcode = Qrcode.Split("/")(6).ToString()
        str_ExpiryDate = Qrcode.Split("/")(7).ToString() 'String Date
        str_MfgDate = Qrcode.Split("/")(8).ToString() 'String Date

        '---------------------------
        GrnDate = DateSerial(Convert.ToInt32(Mid(str_GrnDate, 1, 4)), Convert.ToInt32(Mid(str_GrnDate, 5, 2)), Convert.ToInt32(Mid(str_GrnDate, 7, 2)))
        ExpiryDate = DateSerial(Convert.ToInt32(Mid(str_ExpiryDate, 1, 4)), Convert.ToInt32(Mid(str_ExpiryDate, 5, 2)), Convert.ToInt32(Mid(str_ExpiryDate, 7, 2)))
        MfgDate = DateSerial(Convert.ToInt32(Mid(str_MfgDate, 1, 4)), Convert.ToInt32(Mid(str_MfgDate, 5, 2)), Convert.ToInt32(Mid(str_MfgDate, 7, 2)))

        'GrnDate = GrnDate.ToString("yyyy-MM-dd")
        'ExpiryDate = ExpiryDate.ToString("yyyy-MM-dd")
        'MfgDate = MfgDate.ToString("yyyy-MM-dd")



        '===========Cross validate RM code in BOM Table against FG Item code===============

        cn.Open()
        Dim cmd1 As New SqlCommand("select * from BOM where ItemCode='" & FGItemCode & "' and RMCode='" & RmPartcode & "'", cn)
        Dim sdt1 As New SqlDataAdapter(cmd1)
        Dim dt1 As New DataTable
        sdt1.Fill(dt1)
        cn.Close()

        If dt1.Rows.Count <= 0 Then

            Dim msg As String = RmPartcode & ": is not available in BOM against FG item : " & FGItemCode
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        End If



        '===========Cross validate LotNumber(Handling Unit) in RM Loader Table===============

        cn.Open()
        Dim cmd2 As New SqlCommand("select * from RmLoader where LotNumber='" & LotNumber & "'", cn)
        Dim sdt2 As New SqlDataAdapter(cmd2)
        Dim dt2 As New DataTable
        sdt2.Fill(dt2)
        cn.Close()

        If dt2.Rows.Count >= 1 Then

            Dim msg As String = "you can not load this lot number : " & LotNumber & " this is alreay loaded is system "
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        End If


        '===============Getting Quantity par unit from BOM Table====================

        Dim Quantity_per_Unit As Integer

        Dim cmd4 As New SqlCommand("select * from BOM where ItemCode='" & FGItemCode & "' and RMCode='" & RmPartcode & "'", cn)
        Dim sdt4 As New SqlDataAdapter(cmd4)
        Dim dt4 As New DataTable
        sdt4.Fill(dt4)

        If dt4.Rows.Count <= 0 Then

            Dim msg As String = RmPartcode & " may not created in BOM"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)
            Exit Sub

        Else

            Quantity_per_Unit = Convert.ToInt32(dt4.Rows(0)(2))

        End If



        '=================================Check Whether Same Rawmaterial against same FG with +ve number quantity is available==============

        cn.Open()

        Dim cmd3 As New SqlCommand("select top 1 * from RMLoader where ItemCode='" & FGItemCode & "' and SmtLine='" & SmtLine & "' and RmPartCode='" & RmPartcode & "' order by Created_Dt desc", cn)
        Dim sdt3 As New SqlDataAdapter(cmd3)
        Dim dt3 As New DataTable
        sdt3.Fill(dt3)

        cn.Close()


        Dim RemainQty As Integer = 0


        If dt3.Rows.Count <= 0 Then


        Else

            RemainQty = Convert.ToInt32(dt3.Rows(0)(18))


            If RemainQty > 0 Then

                Dim msg As String = "Remaining Quantity: " & RemainQty & " already available, you can first scrap remaining quantity and then load new"
                Dim script = String.Format("alert('{0}');", msg)
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                Exit Sub

            End If

        End If




        '====================================Initial Entry (Inserting data into RMLoader table)==============================================

        Dim new_remaining_qty = RemainQty + Qty

        '-------updating data against null value in  RmLoader_laser table for Negative Quantity  ---------------------------------------------

        cn.Open()
        Dim cmd_u As New SqlCommand("update RMLoader_Laser set QTy='" & Qty & "', GRN='" & Grn & "',GRNDate='" & GrnDate.ToString("yyyy-MM-dd") & "',LotNumber='" & LotNumber & "',CustomerName='" & CustomerName & "',VendorPartCode='" & VendorPartcode & "',ExpiryDate='" & ExpiryDate.ToString("yyyy-MM-dd") & "',MfgDate='" & MfgDate.ToString("yyyy-MM-dd") & "',Created_Dt=GETDATE(),Created_By='" & usedrid & "',Remain_Qty_Marker='Negative_Filled'  where ItemCode='" & FGItemCode & "' and SmtLine='" & SmtLine & "' and RmPartCode='" & RmPartcode & "' and Remain_Qty_Marker='Negative'", cn)
        cmd_u.ExecuteNonQuery()
        cn.Close()


        '-----Initial Entry---
        cn.Open()
        Dim cmd As New SqlCommand("INSERT INTO RMLoader(ItemCode,SmtLine,RmPartCode,Qty,GRN,GRNDate,LotNumber,CustomerName,VendorPartCode,ExpiryDate,MfgDate,Created_Dt,Created_By,Updated_Dt,Updated_By,EntryType,PcbaNo,RemainQty)values('" & FGItemCode & "','" & SmtLine & "','" & RmPartcode & "','" & Qty & "','" & Grn & "','" & GrnDate.ToString("yyyy-MM-dd") & "','" & LotNumber & "','" & CustomerName & "','" & VendorPartcode & "','" & ExpiryDate.ToString("yyyy-MM-dd") & "','" & MfgDate.ToString("yyyy-MM-dd") & "',GETDATE(),'" & usedrid & "',GETDATE(),'" & usedrid & "','Initial','Initial Entry','" & new_remaining_qty & "')", cn)
        cmd.ExecuteNonQuery()
        cn.Close()

        '---initial entry in RMLoader_laser-----
        cn.Open()
        Dim cmd_1 As New SqlCommand("insert into RMLoader_Laser select top 1 * from RMLoader where ItemCode='" & FGItemCode & "' and SmtLine='" & SmtLine & "' and RmPartCode='" & RmPartcode & "' order by Created_Dt desc", cn)
        cmd_1.ExecuteNonQuery()
        cn.Close()


        '-------Capturing loading dat in  RMLoader_History as well------
        cn.Open()
        Dim cmd_h As New SqlCommand("INSERT INTO RMLoader_History(ItemCode,SmtLine,RmPartCode,Qty,GRN,GRNDate,LotNumber,CustomerName,VendorPartCode,ExpiryDate,MfgDate,Created_Dt,Created_By,Updated_Dt,Updated_By,EntryType,PcbaNo,RemainQty)values('" & FGItemCode & "','" & SmtLine & "','" & RmPartcode & "','" & Qty & "','" & Grn & "','" & GrnDate.ToString("yyyy-MM-dd") & "','" & LotNumber & "','" & CustomerName & "','" & VendorPartcode & "','" & ExpiryDate.ToString("yyyy-MM-dd") & "','" & MfgDate.ToString("yyyy-MM-dd") & "',GETDATE(),'" & usedrid & "',GETDATE(),'" & usedrid & "','Initial','Initial Entry','" & new_remaining_qty & "')", cn)
        cmd_h.ExecuteNonQuery()
        cn.Close()
        '---------------------------------------------------------------

        Label1.Visible = True
        Label1.Text = "Row Material Partcode : " & RmPartcode & " successfully loaded with :" & Qty
        Label1.BackColor = System.Drawing.Color.Yellow
        TextBox1.Text = String.Empty
        '--------------------




    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        RawMaterial_Loader()
        GetGridview()
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("~/ScrapRM.aspx")
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

    Sub GetGridview()

        Dim Itemcode, Smtline As String

        Itemcode = DropDownList1.Text
        Smtline = DropDownList2.Text

        cn.Open()
        Dim cmd1 As New SqlCommand("exec Get_Latest_RowMaterialdata '" & Itemcode & "','" & Smtline & "'", cn)
        Dim sdt1 As New SqlDataAdapter(cmd1)
        Dim dt1 As New DataTable
        sdt1.Fill(dt1)
        cn.Close()

        GridView1.DataSource = dt1
        GridView1.DataBind()



    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        GetGridview()
    End Sub
End Class