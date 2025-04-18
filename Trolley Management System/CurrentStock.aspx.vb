
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Xml
Imports System.Drawing
Imports System.IO.Directory
Imports ClosedXML.Excel
Imports System.Net.Mail
Imports System.IO.DirectoryInfo
Imports Spire.Xls


Public Class CurrentStock

    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Me.Session("Userid") Is Nothing Then

            Response.Redirect("~/Default.aspx")
        Else
            Call Getdata_onGridview()
        End If


    End Sub



    Sub Getdata_onGridview()

        'Dim trolleyno As String = Me.TextBox1.Text

        Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
        Dim cn As New SqlConnection(Connstr)


        cn.Open()
        'Dim cmd2 As New SqlCommand("select id,Uniqueid as Bin_No,size as Bin_type,item as Item_Code,Qty,LastFilled_DT,ItemUID from TrolleyLaser where Updatetype='Inv_In'", cn)

        Dim cmd2 As New SqlCommand("select id,Uniqueid as Bin_No,size as Bin_type,item as Item_Code,Qty,LastFilled_DT,ItemUID,expiry_date,DATEDIFF(d,cast(getdate() as date),cast(expiry_date as date)) Remaining_days from TrolleyLaser where Updatetype in ('Inv_In','ATProduction')", cn)
        Dim adaptor As New SqlDataAdapter(cmd2)

        Dim table1 As New DataTable
        adaptor.Fill(table1)

        cn.Close()

        GridView1.DataSource = table1
        GridView1.DataBind()

        'GridView2.DataSource = table1
        'GridView2.DataBind()


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=GridViewData.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"

        Dim sb As New StringBuilder()

        ' Add the header row
        For k As Integer = 0 To GridView1.HeaderRow.Cells.Count - 1
            sb.Append(CleanCSVString(GridView1.HeaderRow.Cells(k).Text) + ","c)
        Next

        sb.Append(vbCr & vbLf)

        ' Add the data rows
        For i As Integer = 0 To GridView1.Rows.Count - 1
            For k As Integer = 0 To GridView1.HeaderRow.Cells.Count - 1
                sb.Append(CleanCSVString(GridView1.Rows(i).Cells(k).Text) + ","c)
            Next

            sb.Append(vbCr & vbLf)
        Next

        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()


    End Sub


    Private Function CleanCSVString(input As String) As String
        If String.IsNullOrWhiteSpace(input) OrElse input = "&nbsp;" Then
            Return String.Empty
        Else
            Return input.Replace(",", "").Replace(vbCr, "").Replace(vbLf, "").Trim()
        End If
    End Function


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        ' Check if the row is a DataRow (not a Header or Footer row)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Retrieve the value from the specific column, e.g., column index 1 (second column)
            Dim cellValue As String = e.Row.Cells(8).Text

            ' Check the value and set the background color to red if the condition is met
            If Mid(cellValue, 1, 1) = "-" Then
                e.Row.BackColor = System.Drawing.Color.Red
            End If
        End If
    End Sub



    'Sub DOWNLOAD_EXCELFILE_AND_SEND_THROUGH_EMAIL()
    '    '**********************************************


    '    Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    '    Dim cn As New SqlConnection(Connstr)


    '    'delete all old file ---
    '    Dim pth As String = Server.MapPath("~/Files/")

    '    For Each fl As String In Directory.GetFiles(pth)
    '        File.Delete(fl)
    '    Next

    '    '----------

    '    cn.Open()

    '    Using cmd As New SqlCommand("select Purchasing_DocNo,EAN_Code,Serial_Number,Pallet_Number,Master_Shipper_Number,Vendor_Lot_Number,Invoice_Number,Month_of_Manufacturing,Invoice_Date,Warranty_in_Days,Physical_Device_Model,Physical_Device_Type,Physical_Device_Vendor,Device_IMEI_number,OUI,Software_Version,Pre_Password,Device_Status,Logical_Function_Type,Device_Component,Base_MAC_ID,Global_SSID1,Global_SSID2,WPA_PSK_SSID_PWD,GPON_ID,IMEI2,ICCID,Action_for_Granite,CPLC from asn_exl_uploader order by Base_MAC_ID asc")


    '        Using sda As New SqlDataAdapter()
    '            cmd.Connection = cn
    '            sda.SelectCommand = cmd

    '            Using dt As New DataTable()
    '                sda.Fill(dt)

    '                Dim filename As String

    '                filename = dt.Rows(0)(0) & "_" & Day(Date.Now) & "_" & Month(Date.Now) & "_" & Year(Date.Now) & "_" & Hour(Date.Now) & "_" & Minute(Date.Now)

    '                Using wb As New XLWorkbook()

    '                    ' Dim sh1 As IXLWorksheet
    '                    Dim sh2 As IXLWorksheet

    '                    'sh1 = wb.Worksheets.Add(dt, "FirstSheet")
    '                    sh2 = wb.Worksheets.Add("Input File Format")

    '                    Dim i As Long

    '                    For i = 1 To dt.Rows.Count

    '                        sh2.Range("A" & i + 1).Value = dt.Rows(i - 1)(0).ToString
    '                        sh2.Range("B" & i + 1).Value = dt.Rows(i - 1)(1).ToString
    '                        sh2.Range("C" & i + 1).Value = dt.Rows(i - 1)(2).ToString
    '                        sh2.Range("D" & i + 1).Value = dt.Rows(i - 1)(3).ToString
    '                        sh2.Range("E" & i + 1).Value = dt.Rows(i - 1)(4).ToString
    '                        sh2.Range("F" & i + 1).Value = dt.Rows(i - 1)(5).ToString
    '                        sh2.Range("G" & i + 1).Value = dt.Rows(i - 1)(6).ToString
    '                        sh2.Range("H" & i + 1).Value = dt.Rows(i - 1)(7).ToString

    '                        sh2.Range("I" & i + 1).Value = dt.Rows(i - 1)(8).ToString
    '                        sh2.Range("J" & i + 1).Value = dt.Rows(i - 1)(9).ToString
    '                        sh2.Range("K" & i + 1).Value = dt.Rows(i - 1)(10).ToString
    '                        sh2.Range("L" & i + 1).Value = dt.Rows(i - 1)(11).ToString
    '                        sh2.Range("M" & i + 1).Value = dt.Rows(i - 1)(12).ToString
    '                        sh2.Range("N" & i + 1).Value = dt.Rows(i - 1)(13).ToString
    '                        sh2.Range("O" & i + 1).Value = dt.Rows(i - 1)(14).ToString
    '                        sh2.Range("P" & i + 1).Value = dt.Rows(i - 1)(15).ToString

    '                        sh2.Range("Q" & i + 1).Value = dt.Rows(i - 1)(16).ToString
    '                        sh2.Range("R" & i + 1).Value = dt.Rows(i - 1)(17).ToString
    '                        sh2.Range("S" & i + 1).Value = dt.Rows(i - 1)(18).ToString
    '                        sh2.Range("T" & i + 1).Value = dt.Rows(i - 1)(19).ToString
    '                        sh2.Range("U" & i + 1).Value = dt.Rows(i - 1)(20).ToString
    '                        sh2.Range("V" & i + 1).Value = dt.Rows(i - 1)(21).ToString
    '                        sh2.Range("W" & i + 1).Value = dt.Rows(i - 1)(22).ToString
    '                        sh2.Range("X" & i + 1).Value = dt.Rows(i - 1)(23).ToString

    '                        sh2.Range("Y" & i + 1).Value = dt.Rows(i - 1)(24).ToString
    '                        sh2.Range("Z" & i + 1).Value = dt.Rows(i - 1)(25).ToString
    '                        sh2.Range("AA" & i + 1).Value = dt.Rows(i - 1)(26).ToString
    '                        sh2.Range("AB" & i + 1).Value = dt.Rows(i - 1)(27).ToString
    '                        sh2.Range("AC" & i + 1).Value = dt.Rows(i - 1)(28).ToString

    '                    Next i

    '                    '------------------Adding Header as well-----------------------------------------

    '                    sh2.Range("A1").Value = "Purchasing Document Number"
    '                    sh2.Range("B1").Value = "EAN Code of Device"
    '                    sh2.Range("C1").Value = "Serial Number"
    '                    sh2.Range("D1").Value = "Pallet Number"
    '                    sh2.Range("E1").Value = "Master Shipper Number"
    '                    sh2.Range("F1").Value = "Vendor Lot Number"
    '                    sh2.Range("G1").Value = "Invoice Number"
    '                    sh2.Range("H1").Value = "Month of Manufacturing (YYYYMM)"
    '                    sh2.Range("I1").Value = "Invoice Date (DD.MM.YYYY)- Warranty Start Date"
    '                    sh2.Range("J1").Value = "Warranty in Days"
    '                    sh2.Range("K1").Value = "Physical Device Model"
    '                    sh2.Range("L1").Value = "Physical Device Type"
    '                    sh2.Range("M1").Value = "Physical  Device Vendor"
    '                    sh2.Range("N1").Value = "Device IMEI number"
    '                    sh2.Range("O1").Value = "OUI"
    '                    sh2.Range("P1").Value = "Software Version"
    '                    sh2.Range("Q1").Value = "Pre-Password"
    '                    sh2.Range("R1").Value = "Device Status"
    '                    sh2.Range("S1").Value = "Logical Function Type"
    '                    sh2.Range("T1").Value = "Device Component"
    '                    sh2.Range("U1").Value = "Base MAC ID(for each Logical function)"
    '                    sh2.Range("V1").Value = "Global SSID1"
    '                    sh2.Range("W1").Value = "Global SSID2"
    '                    sh2.Range("X1").Value = "WPA PSK SSID-PWD"
    '                    sh2.Range("Y1").Value = "GPON ID"
    '                    sh2.Range("Z1").Value = "IMEI2"
    '                    sh2.Range("AA1").Value = "ICCID"
    '                    sh2.Range("AB1").Value = "Action for Granite"
    '                    sh2.Range("AC1").Value = "CPLC"

    '                    '------------------------------------------------------------------------------

    '                    'wb.SaveAs("D:\Files\" & filename & ".xlsx")
    '                    wb.SaveAs(Server.MapPath("~/Files/" & filename & ".xlsx"))

    '                    cn.Close()


    '                    '===========================update file name in AsnData Table====================================


    '                    Dim qry As String = "update asndata set field4='" & filename & "' where field4 is NULL"
    '                    Dim sqlcmd As New SqlCommand(qry, cn)
    '                    cn.Open()
    '                    sqlcmd.ExecuteNonQuery()
    '                    cn.Close()

    '                    '=======================================================Email Sending Process==========================================

    '                    cn.Open()
    '                    Dim cmd_11 As SqlCommand = New SqlCommand("select * from CurrentProject where ProjectName='ashifkhan'", cn)
    '                    Dim adapter_11 As New SqlDataAdapter(cmd_11)
    '                    Dim table_11 As New DataTable
    '                    adapter_11.Fill(table_11)
    '                    cn.Close()
    '                    Dim emailto, emailcc As String

    '                    emailto = table_11.Rows(0)(4)
    '                    emailcc = table_11.Rows(0)(5)

    '                    Dim Smtp_Server As New SmtpClient


    '                    Dim e_mail As New MailMessage()
    '                    Smtp_Server.UseDefaultCredentials = False
    '                    Smtp_Server.Credentials = New Net.NetworkCredential("notification@neolync.com", "Zuf97574")
    '                    Smtp_Server.Port = 587
    '                    Smtp_Server.EnableSsl = True
    '                    Smtp_Server.Host = "smtp.office365.com"

    '                    'SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
    '                    Smtp_Server.DeliveryMethod = SmtpDeliveryMethod.Network


    '                    e_mail = New MailMessage()
    '                    e_mail.From = New MailAddress("notification@neolync.com")
    '                    e_mail.To.Add(emailto)
    '                    e_mail.CC.Add(emailcc)

    '                    e_mail.Attachments.Add(New Net.Mail.Attachment(Server.MapPath("~/Files/" & filename & ".xlsx")))

    '                    e_mail.Subject = "New ASN File: " & filename '"upload no daalana hai" & "TrolleyNo"

    '                    e_mail.IsBodyHtml = False

    '                    e_mail.Body = "Hi Team," & vbNewLine & "Please find the attached ASN file" & vbNewLine & vbNewLine & "Regards," & vbNewLine & "Server Neolync on behalaf of " & Me.Session("userName")
    '                    'e_mail.Body = "Hi Production Team, " & vbNewLine & vbNewLine & "Material has been moved to the production on Trlley no :" & Me.DropDownList2.SelectedItem.Text & ", Attached is the issuence report against picklist" & vbNewLine & vbNewLine & "Remarks : " & Me.TextBox1.Text & vbNewLine & vbNewLine & "Regards," & vbNewLine & "Server Neolync on behalaf of " & Me.Session("userName")

    '                    Smtp_Server.Send(e_mail)

    '                    e_mail.Dispose()
    '                    Smtp_Server.Dispose()



    '                End Using
    '            End Using
    '        End Using
    '    End Using


    'End Sub










End Class