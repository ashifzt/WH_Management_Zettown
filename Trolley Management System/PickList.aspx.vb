Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.IO

Public Class PickList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.Session("Userid") Is Nothing Then

            Response.Redirect("~/Default.aspx")

        End If

        Me.Label1.Text = ""

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ''-------------Data is uploading through CSV file front end--------------------------------------------------------------------------


        Dim userrole As String = Me.Session("Roles").ToString.ToUpper
        If userrole.Contains("UPLOADER") Or userrole.Contains("ADMIN") Then


            If FileUpload1.HasFile Then

                If FileUpload1.FileName.Contains(".csv") Then

                    Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
                    Dim cn As New SqlConnection(Connstr)

                    cn.Open()
                    Dim cmd_1 As New SqlCommand("select * from GateEntry where VendorInvoiceNo='" & TextBox2.Text.Trim & "'", cn)
                    Dim sdt_1 As New SqlDataAdapter(cmd_1)
                    Dim dt_1 As New DataTable
                    sdt_1.Fill(dt_1)
                    cn.Close()


                    If dt_1.Rows.Count <= 0 Then

                        Dim msg As String = "Vendor Invoice number is not matching with any of the Gate Entry Data"
                        Dim script = String.Format("alert('{0}');", msg)
                        ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)
                        Exit Sub

                    Else

                    End If


                    '--------------Check whether picklist uploaded earlier also or not----------------------------------------------------



                    Dim cmd_11 As New SqlCommand("select * from PicklistStatus where Workorder='" & Me.TextBox1.Text & "' and SequenceNo='" & Me.TextBox2.Text & "'", cn)

                    Dim adptr_11 As New SqlDataAdapter(cmd_11)
                    Dim tabl_11 As New DataTable
                    adptr_11.Fill(tabl_11)


                    If tabl_11.Rows.Count > 0 Then

                        Dim msg As String = "Current GRN & Vendor Invoice is uploaded earlier also, this is to Notify only.."
                        Dim script = String.Format("alert('{0}');", msg)
                        ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                    End If


                    '------------------------------above----------------------------------------------------------------------------------


                    Dim csvpath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
                    FileUpload1.SaveAs(csvpath)

                    Dim dt As New DataTable()

                    dt.Columns.AddRange(New DataColumn(1) _
                                        {New DataColumn("Item", GetType(String)),
                                        New DataColumn("Qty", GetType(String))
                                        })


                    Dim csvdata As String = File.ReadAllText(csvpath)

                    For Each row As String In csvdata.Split(ControlChars.Lf)

                        If row.Contains("Item") Or row.Contains("Qty") Then

                            row.Remove(1)

                        Else
                            If Not String.IsNullOrEmpty(row) Then
                                dt.Rows.Add()
                                Dim i As Integer = 0
                                For Each cell As String In row.Split(","c)

                                    dt.Rows(dt.Rows.Count - 1)(i) = cell
                                    i = i + 1
                                Next

                            End If

                        End If


                    Next

                    '--------- -------------------------------------------------------------------------------------


                    dt.Columns.AddRange(New DataColumn(5) _
                                      {New DataColumn("UploadNo", GetType(String)),
                                      New DataColumn("Workorder", GetType(String)),
                                      New DataColumn("SequenceNo", GetType(String)),
                                      New DataColumn("UploadedBy", GetType(String)),
                                      New DataColumn("Uploaded_DT", GetType(DateTime)),
                                      New DataColumn("Status1", GetType(String))
                                      })

                    Dim Uploadno, Workorder, SequenceNo, UploadedBy, Status1 As String

                    Workorder = Me.TextBox1.Text
                    SequenceNo = Me.TextBox2.Text
                    UploadedBy = Me.Session("Userid")
                    Status1 = "GRNUploaded"


                    Dim Uploaded_DT As DateTime = DateTime.Now


                    'Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
                    'Dim cn As New SqlConnection(Connstr)

                    'Dim command As New SqlCommand("Select max(substring(uploadno, 3,8)+1) from picklist", cn)
                    Dim command As New SqlCommand("Select Case When (Select max(substring(uploadno,3,8)) from Picklist) Is null Then 'NL10000000' else 'NL'+ (select(convert(varchar(50),max(substring(uploadno,3,8))+1))  from Picklist) end", cn)


                    cn.Open()
                    command.ExecuteNonQuery()
                    cn.Close()
                    Dim adapter As New SqlDataAdapter(command)
                    Dim table As New DataTable

                    adapter.Fill(table)

                    Uploadno = table.Rows(0)(0).ToString


                    For j As Integer = 0 To dt.Rows.Count - 1

                        dt.Rows(j)(2) = Uploadno
                        dt.Rows(j)(3) = Workorder
                        dt.Rows(j)(4) = SequenceNo
                        dt.Rows(j)(5) = UploadedBy
                        dt.Rows(j)(6) = Uploaded_DT
                        dt.Rows(j)(7) = Status1

                    Next

                    cn.Open()

                    Using copy As New SqlBulkCopy(cn)
                        copy.BulkCopyTimeout = 3000

                        copy.ColumnMappings.Add(0, 3)
                        copy.ColumnMappings.Add(1, 4)

                        copy.ColumnMappings.Add(2, 0)
                        copy.ColumnMappings.Add(3, 1)
                        copy.ColumnMappings.Add(4, 2)
                        copy.ColumnMappings.Add(5, 5)
                        copy.ColumnMappings.Add(6, 6)
                        copy.ColumnMappings.Add(7, 8)

                        copy.DestinationTableName = "picklist"
                        copy.WriteToServer(dt)


                    End Using

                    cn.Close()

                    '--------------Data inserting in Pickliststatus table----------------------

                    'Dim command_1 As New SqlCommand("insert into PicklistStatus values('NL12111','WO111',12,'Ashifkhan',getdate(),'Active')", cn)

                    Dim command_1 As New SqlCommand("insert into PicklistStatus values('" & Uploadno & "','" & Workorder & "','" & SequenceNo & "','" & UploadedBy & "',getdate(),'Open')", cn)
                    cn.Open()
                    command_1.ExecuteNonQuery()
                    cn.Close()


                    cn.Open()
                    Dim cmd_2 As New SqlCommand("update GateEntry set GrnStatus='GRN Done' where VendorInvoiceNo='" & TextBox2.Text.Trim & "'", cn)
                    cmd_2.ExecuteNonQuery()
                    cn.Close()



                    '--------------------****---------------------------------------------------

                    Me.Label1.ForeColor = System.Drawing.Color.Green
                    Me.Label1.BackColor = System.Drawing.Color.Yellow
                    Me.Label1.Text = "Data Uploaded Successfully with upload No : " & Uploadno

                    '-------------------------Eamil Sending Method--------------------------------------------------

                    Dim command_2 As New SqlCommand("select * from EmailDL where ReportName='PickList upload'", cn)
                    cn.Open()
                    Dim adaptor1 As New SqlDataAdapter(command_2)
                    Dim table1 As New DataTable
                    adaptor1.Fill(table1)

                    Dim emailto, emailcc, emailbcc As String
                    emailto = table1.Rows(0)(1).ToString
                    emailcc = table1.Rows(0)(2).ToString
                    emailbcc = table1.Rows(0)(3).ToString

                    '----------emailing----------

                    'Dim Smtp_Server As New SmtpClient

                    'Dim e_mail As New MailMessage()
                    'Smtp_Server.UseDefaultCredentials = False
                    'Smtp_Server.Credentials = New Net.NetworkCredential("notification@neolync.com", "Zuf97574")
                    'Smtp_Server.Port = 587
                    'Smtp_Server.EnableSsl = True
                    'Smtp_Server.Host = "smtp.office365.com"

                    'e_mail = New MailMessage()
                    'e_mail.From = New MailAddress("notification@neolync.com")
                    'e_mail.To.Add(emailto)
                    'e_mail.CC.Add(emailcc)
                    'e_mail.Bcc.Add(emailbcc)

                    'e_mail.Subject = "New Picklist Uploaded : " & Uploadno

                    'e_mail.IsBodyHtml = False
                    ''e_mail.Body = "Hi Warehouse Team, " & vbNewLine & vbNewLine & "New Picklist is uploaded on Trolley Management System with Id :" & Uploadno & ", please issue the material." & vbNewLine & vbNewLine & "Regards," & vbNewLine & "Server Neolync on behalaf of " & Me.Session("userName")
                    'e_mail.Body = "Hi Warehouse Team, " & vbNewLine & vbNewLine & "New Picklist is uploaded on Trolley Management System, hence Please issue is material. " & vbNewLine & vbNewLine & "Upload Id : " & Uploadno & vbNewLine & "Work Order : " & Workorder & vbNewLine & "Sequence No : " & SequenceNo & vbNewLine & vbNewLine & "Regards," & vbNewLine & "Server Neolync on behalaf of " & Me.Session("userName")

                    'Smtp_Server.Send(e_mail)

                    ' MsgBox("Mail Sent")

                    '----------emailing-----------------

                    Me.TextBox1.Text = String.Empty
                    Me.TextBox2.Text = String.Empty



                Else

                    Dim msg As String = "The file format Is Not correct"
                    Dim script = String.Format("alert('{0}');", msg)
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

                End If


            Else

                Dim msg As String = "Please Attach file to Upload"
                Dim script = String.Format("alert('{0}');", msg)
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)


            End If

        Else

            Dim msg As String = "You do not have rights to upload picklist"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)


        End If

    End Sub
End Class