
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO.Directory
Imports ClosedXML.Excel
Imports System.Net.Mail

Public Class Transfer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If

        '------------------------

        If Not IsPostBack Then

            Me.DropDownList2.Enabled = False
            Me.TextBox1.Enabled = False
            Me.Button2.Enabled = False
            Me.Label1.Visible = False

        End If

    End Sub


    Sub Get_Warehouse_Loaded_Trolley()

        '-----------------------------------Get Trolley Number----------------------------------------
        Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
        Dim cn As New SqlConnection(Connstr)
        Dim cmd As New SqlCommand("select distinct(TrolleyNo) from currentTI where trolleylocation='Warehouse' and currentitem is not NULL order by TrolleyNo asc", cn)

        Dim adaptor As New SqlDataAdapter(cmd)
        Dim table2 As New DataTable
        adaptor.Fill(table2)

        Me.DropDownList2.Items.Add("----Trolley No----")

        Dim i As Integer
        For i = 0 To table2.Rows.Count - 1
            Me.DropDownList2.Items.Add(table2.Rows(i)(0).ToString)
        Next i

    End Sub


    Sub Get_production_Loaded_Trolley()

        '-----------------------------------Get Trolley Number----------------------------------------
        Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
        Dim cn As New SqlConnection(Connstr)

        'Dim cmd As New SqlCommand("select trolleyno from (select Trolleyno, count(currentitem) Tcount from CurrentTI where TrolleyLocation='Production' group by TrolleyNo) t1 where Tcount<1", cn)

        Dim cmd As New SqlCommand("select distinct(trolleyno) from currentti where TrolleyLocation='Production'", cn)

        Dim adaptor As New SqlDataAdapter(cmd)
        Dim table2 As New DataTable
        adaptor.Fill(table2)

        Me.DropDownList2.Items.Add("----Upload No----")

        Dim i As Integer
        For i = 0 To table2.Rows.Count - 1
            Me.DropDownList2.Items.Add(table2.Rows(i)(0).ToString)
        Next i

    End Sub




    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim user_role As String
        user_role = Me.Session("Roles")

        'MsgBox(user_role)

        If Me.DropDownList1.SelectedItem.Text = "Warehouse" And (user_role.ToUpper.Contains("WHUSER") Or user_role.ToUpper.Contains("WHMANAGER") Or user_role.ToUpper.Contains("ADMIN")) Then

            Me.DropDownList2.Enabled = True
            Me.TextBox1.Enabled = True
            Me.Button2.Enabled = True

            Me.DropDownList1.Enabled = False
            Me.Button1.Enabled = False

            Call Get_Warehouse_Loaded_Trolley()


        ElseIf Me.DropDownList1.SelectedItem.Text = "Warehouse_Locked" And (user_role.ToUpper.Contains("PRDUSER") Or user_role.ToUpper.Contains("PRMANAGER") Or user_role.ToUpper.Contains("ADMIN")) Then

            Me.DropDownList2.Enabled = True
            Me.TextBox1.Enabled = True
            Me.Button2.Enabled = True

            Me.DropDownList1.Enabled = False
            Me.Button1.Enabled = False

            Call Get_production_Loaded_Trolley()

        Else


            Dim msg As String = " Not Accessed to complete GR..." & Me.DropDownList1.SelectedItem.Text
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

        End If



    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Me.DropDownList1.SelectedItem.Text = "Warehouse" Then

            Call Transfer_WH_Trolley()

            'Call Attchments_Email_Warehouse()


        ElseIf Me.DropDownList1.SelectedItem.Text = "Warehouse_Locked" Then

            Call Transfer_PRD_Trolley()

        End If

    End Sub





    '-----------Transfer WH Trolley-------------------------------------
    Sub Transfer_WH_Trolley()

        If Me.DropDownList2.SelectedItem.Text = "----Upload No----" Then

            Dim msg As String = " Please Select WH Area First " & Me.DropDownList1.SelectedItem.Text
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        Else

            Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
            Dim cn As New SqlConnection(Connstr)

            '----------------Getiing upload number from trolley no------------------------------------
            Dim uploadno As String

            Dim cmd1 As New SqlCommand("select top 1 uploadno from CurrentTI where Trolleyno ='" & Me.DropDownList2.SelectedItem.Text & "' and Currentitem is not null ", cn)

            Dim adaptor1 As New SqlDataAdapter(cmd1)
            Dim table1 As New DataTable
            adaptor1.Fill(table1)

            uploadno = table1.Rows(0)(0)

            'MsgBox("uplaod no  " & uploadno)
            '-----------------------------------Transfer Warehouse Trolley ----------------------

            Dim Qry As String = "DECLARE @tempable1 TABLE (Uploadno varchar(50), item varchar(50),Picklist_Qty decimal(10,3),Trolleyno varchar(50), Uploadno1 nvarchar(50),Currentitem varchar(50), Issued_Qty decimal(10,3),Delta_cal int)" _
                                 & "INSERT INTO @tempable1 exec WH_Issuence_Check '" & uploadno & "','" & Me.DropDownList2.SelectedItem.Text & "'" _
                                 & "Select * from @tempable1 where Delta_cal In(-1)"

            Dim cmd2 As New SqlCommand(Qry, cn)

            Dim adaptor2 As New SqlDataAdapter(cmd2)
            Dim table2 As New DataTable
            adaptor2.Fill(table2)

            If table2.Rows.Count = 0 Then


                Dim cmd3 As New SqlCommand("update CurrentTI Set TrolleyLocation='Warehouse_Locked' where Trolleyno='" & Me.DropDownList2.SelectedItem.Text & "'", cn)

                cn.Open()
                cmd3.ExecuteNonQuery()
                cn.Close()

                Me.Label1.Text = "You Selected WH Area is Successfully Updated.."
                Me.Label1.Visible = True

                '------------updating picklist status table as closed when Material is issued with full capacity-------------------

                Dim cmd4 As New SqlCommand("update PicklistStatus set Status1='Closed' where uploadno='" & uploadno & "'", cn)
                cn.Open()
                cmd4.ExecuteNonQuery()
                cn.Close()
                '-----------------------------------------------------------------------------------------------------------------

            Else


                Dim user_role As String
                user_role = Me.Session("Roles")

                If Me.TextBox1.Text = String.Empty Then

                    Dim msg As String = "Insufficient item Please fill remarks and then complete GR "
                    Dim script = String.Format("alert('{0}');", msg)
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                    Exit Sub

                ElseIf Me.TextBox1.Text <> String.Empty And (user_role.ToUpper.Contains("WHMANAGER") Or user_role.ToUpper.Contains("ADMIN")) Then

                    Dim cmd4 As New SqlCommand("update CurrentTI set TrolleyLocation='Warehouse_Locked' where Trolleyno='" & Me.DropDownList2.SelectedItem.Text & "'", cn)

                    cn.Open()
                    cmd4.ExecuteNonQuery()
                    cn.Close()

                    Me.Label1.Text = "You Selected Trolley is Successfully Transferred"
                    Me.Label1.Visible = True


                Else

                    Dim msg As String = "You are not accesse to complete GR with insufficient item"
                    Dim script = String.Format("alert('{0}');", msg)
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                    Exit Sub

                End If

            End If

        End If

        '------------------call to email function-------------------------------------------

        'Call Attchments_Email_Warehouse()


    End Sub



    '----------------Transfer production trolley------------------

    Sub Transfer_PRD_Trolley()


        Dim user_role As String
        user_role = Me.Session("Roles")

        If Me.DropDownList2.SelectedItem.Text = "----Upload No----" Then

            Dim msg As String = " Please Select Trolley First " & Me.DropDownList1.SelectedItem.Text
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub


        Else

            Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
            Dim cn As New SqlConnection(Connstr)

            'Dim cmd As New SqlCommand("select trolleyno from (select Trolleyno, count(currentitem) Tcount from CurrentTI where TrolleyLocation='Production' group by TrolleyNo) t1 where Tcount<1", cn)

            If user_role.ToUpper().Contains("PRDMANAGER") Or user_role.ToUpper().Contains("ADMIN") Then

                If TextBox1.Text = "" Or TextBox1.Text.Length < 10 Then
                    Dim msg As String = " Please put Remarks minimumn with 10 character "
                    Dim script = String.Format("alert('{0}');", msg)
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                Else
                    Dim cmd As New SqlCommand("update CurrentTI Set TrolleyLocation='Warehouse' where Trolleyno='" & Me.DropDownList2.SelectedItem.Text & "'", cn)

                    cn.Open()
                    cmd.ExecuteNonQuery()
                    cn.Close()

                    Me.Label1.Text = "You Selected Trolley is Successfully Transferred"
                    Me.Label1.Visible = True

                End If

            Else

                Dim cmd1 As New SqlCommand("select * from CurrentTI where TrolleyNo='" & Me.DropDownList1.SelectedItem.Text & "' and qty is not null", cn)
                Dim adaptor As New SqlDataAdapter(cmd1)
                Dim table2 As New DataTable
                adaptor.Fill(table2)

                If table2.Rows.Count <= 0 Then

                    Dim cmd2 As New SqlCommand("update CurrentTI Set TrolleyLocation='Warehouse' where Trolleyno='" & Me.DropDownList2.SelectedItem.Text & "'", cn)

                    cn.Open()
                    cmd2.ExecuteNonQuery()
                    cn.Close()

                    Me.Label1.Text = "You Selected Trolley is Successfully Transferred"
                    Me.Label1.Visible = True


                Else
                    Dim msg As String = "You do not have rights to move trolly with available material, please consume material first or coordinate with Production Manager "
                    Dim script = String.Format("alert('{0}');", msg)
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)
                    Exit Sub
                End If



            End If

        End If



    End Sub


    '--------------------********************* Data Downloads and Email  **********************---------------------

    Sub Attchments_Email_Warehouse()

        Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
        Dim cn As New SqlConnection(Connstr)


        '----------------Getiing upload number from trolley no------------------------------------
        Dim uploadno As String

        Dim cmd1 As New SqlCommand("select top 1 uploadno from CurrentTI where Trolleyno ='" & Me.DropDownList2.SelectedItem.Text & "' and Currentitem is not null ", cn)

        Dim adaptor_1 As New SqlDataAdapter(cmd1)
        Dim table1_2 As New DataTable
        adaptor_1.Fill(table1_2)

        uploadno = table1_2.Rows(0)(0)
        '----------------------------------------------------------------------------------------

        Dim Qry As String = "DECLARE @tempable1 TABLE (Uploadno varchar(50), item varchar(50),Picklist_Qty decimal(10,3),Trolleyno varchar(50), Uploadno1 nvarchar(50),Currentitem varchar(50), Issued_Qty decimal(10,3),Delta_cal int)" _
                                 & "INSERT INTO @tempable1 exec WH_Issuence_Check '" & uploadno & "','" & Me.DropDownList2.SelectedItem.Text & "'" _
                                 & "Select Uploadno,item,Picklist_Qty,Trolleyno,Issued_Qty, iif(Issued_Qty-Picklist_Qty>0,'Full Material','Insufficient Material') Delta from @tempable1"
        '& "Select * from @tempable1"
        Dim cmd2 As New SqlCommand(Qry, cn)

        Dim adaptor2 As New SqlDataAdapter(cmd2)
        Dim table2 As New DataTable
        adaptor2.Fill(table2)


        '--------------------------------------------------------------------------------------------------------------------

        Using wb As New XLWorkbook()

            Dim sh2 As IXLWorksheet
            ' wb.Worksheets.Add(table2, "Inspection Report")
            sh2 = wb.Worksheets.Add("Inspection Report")

            '-------------------Adding Header--------------------
            sh2.Range("A1").Value = "Uploadno"
            sh2.Range("B1").Value = "Item"
            sh2.Range("C1").Value = "Picklist QTy"
            sh2.Range("D1").Value = "Trolley"
            sh2.Range("E1").Value = "Issued Qty"
            sh2.Range("F1").Value = "Delta"
            '----------------------------------------------------------------------

            Dim i As Integer
            For i = 1 To table2.Rows.Count

                sh2.Range("A" & i + 1).Value = table2.Rows(i - 1)(0).ToString
                sh2.Range("B" & i + 1).Value = table2.Rows(i - 1)(1).ToString
                sh2.Range("C" & i + 1).Value = table2.Rows(i - 1)(2).ToString
                sh2.Range("D" & i + 1).Value = table2.Rows(i - 1)(3).ToString
                sh2.Range("E" & i + 1).Value = table2.Rows(i - 1)(4).ToString
                sh2.Range("F" & i + 1).Value = table2.Rows(i - 1)(5).ToString

            Next

            Dim filepath As String
            filepath = Server.MapPath("~/Files/Report.xlsx")

            wb.SaveAs(filepath)


            cn.Close()

            '-----------------------------------------------------------------------------------------------------------
            'Response.Clear()
            'Response.Buffer = True
            'Response.Charset = ""
            'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

            'Response.AddHeader("content-disposition", "attachment;filename=" & "Report.xlsx")

            'Dim filePath As String = String.Format("{0}/{1}", Server.MapPath("~/Files/"), "Report.xlsx")

            'If System.IO.File.Exists(filePath) Then System.IO.File.Delete(filePath)

            'wb.SaveAs(filePath)

            '-----------------------------------------------------------------------------------------------------------------

            Dim command_2 As New SqlCommand("select * from EmailDL where ReportName='Trolley Transfer'", cn)
            cn.Open()
            Dim adaptor1 As New SqlDataAdapter(command_2)
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

            'SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
            Smtp_Server.DeliveryMethod = SmtpDeliveryMethod.Network


            e_mail = New MailMessage()
            e_mail.From = New MailAddress("notification@neolync.com")
            e_mail.To.Add(emailto)
            e_mail.CC.Add(emailcc)
            e_mail.Bcc.Add(emailbcc)

            e_mail.Attachments.Add(New Net.Mail.Attachment(filePath))

            e_mail.Subject = "Material Issued agains Upload no " '"upload no daalana hai" & "TrolleyNo"

            e_mail.IsBodyHtml = False

            e_mail.Body = "Hi Production Team, " & vbNewLine & vbNewLine & "Material has been moved to the production on Trlley no :" & Me.DropDownList2.SelectedItem.Text & ", Attached is the issuence report against picklist" & vbNewLine & vbNewLine & "Remarks : " & Me.TextBox1.Text & vbNewLine & vbNewLine & "Regards," & vbNewLine & "Server Neolync on behalaf of " & Me.Session("userName")

            Smtp_Server.Send(e_mail)

        End Using

    End Sub

    'Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

    '    'Call Attchments_Email_Warehouse()
    '    'MsgBox("ho gya")

    'End Sub
End Class