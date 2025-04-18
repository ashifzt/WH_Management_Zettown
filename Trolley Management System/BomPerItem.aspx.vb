
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic


Public Class BomPerItem
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If

        If Not IsPostBack Then

            Label1.Visible = False
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox6.Enabled = False

            FileUpload1.Visible = False
            Button2.Visible = False

            LinkButton1.Visible = False

            GetIteData()

        End If

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        If DropDownList1.Text = "----Select Itemcode----" Then

        Else
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox6.Enabled = True
            TextBox1.Focus()

            DropDownList1.Enabled = False

            GetGridview()
            LinkButton1.Visible = True


        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Call Item_Creation()
        GetGridview()
    End Sub

    Sub GetGridview()

        Dim itemcode As String = DropDownList1.Text


        cn.Open()
        Dim cmd1 As New SqlCommand("select Itemcode,RMCode,QuantityPerUnit, Updated_Dt,UnitOfMesurement from BOM where ItemCode='" & itemcode & "'", cn)
        Dim sdt1 As New SqlDataAdapter(cmd1)
        Dim dt1 As New DataTable
        sdt1.Fill(dt1)
        cn.Close()

        GridView1.DataSource = dt1
        GridView1.DataBind()



        'Dim msg As String = "You do not have right to this activity"
        'Dim script = String.Format("alert('{0}');", msg)
        'ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)




    End Sub




    Sub GetIteData()

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



    Sub Item_Creation()

        Dim ItemCode, RmCode, UnitOfMeasurement, username As String
        Dim Quantity As Integer

        If TextBox1.Text = String.Empty OrElse TextBox2.Text = String.Empty OrElse TextBox6.Text = String.Empty Then

            Dim msg As String = "Please fill required details first"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        End If


        ItemCode = DropDownList1.Text
        RmCode = TextBox1.Text
        Quantity = TextBox2.Text
        UnitOfMeasurement = TextBox6.Text

        username = Session("Userid")

        If DropDownList1.Text = "----Select Itemcode----" Then

            Dim msg As String = "select item code from drop down"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        End If



        Try
            cn.Open()
            Dim cmd As New SqlCommand("insert into BOM (ItemCode,RMCode,QuantityPerUnit,UnitOfMesurement,Updated_Dt,Updated_By) values ('" & ItemCode & "','" & RmCode & "','" & Quantity & "','" & UnitOfMeasurement & "',GETDATE(),'" & username & "')", cn)
            cmd.ExecuteNonQuery()
            cn.Close()

            Label1.Visible = True
            Label1.Text = RmCode & " successfully created with Quantity :" & Quantity
            Label1.BackColor = System.Drawing.Color.Yellow

            TextBox1.Text = String.Empty
            TextBox2.Text = String.Empty
            TextBox6.Text = String.Empty


        Catch ex As Exception

            Label1.Visible = True
            Label1.Text = ex.Message
            Label1.BackColor = System.Drawing.Color.Red

        End Try


    End Sub


    Protected Sub DropDownList1_TextChanged(sender As Object, e As EventArgs) Handles DropDownList1.TextChanged

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        If LinkButton1.Text.ToUpper = "MASS UPLOAD" Then

            TextBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox6.Enabled = False
            Button1.Enabled = False

            FileUpload1.Visible = True
            Button2.Visible = True

            LinkButton1.Text = "single upload"

        ElseIf LinkButton1.Text.ToUpper = "SINGLE UPLOAD" Then

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox6.Enabled = True
            Button1.Enabled = True

            FileUpload1.Visible = False
            Button2.Visible = False

            LinkButton1.Text = "mass upload"


        End If



    End Sub


    Sub Mass_BOM_Uploading()



        Dim userrole As String = Me.Session("Roles").ToString.ToUpper
        If userrole.Contains("UPLOADER") Or userrole.Contains("ADMIN") Then


            If FileUpload1.HasFile Then

                If FileUpload1.FileName.Contains(".csv") Then


                    Dim csvpath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
                    FileUpload1.SaveAs(csvpath)

                    Dim dt As New DataTable()

                    dt.Columns.AddRange(New DataColumn(2) _
                                        {New DataColumn("RmCode", GetType(String)),
                                        New DataColumn("QuntityPerUnit", GetType(Integer)),
                                        New DataColumn("MesurementUnit", GetType(String))
                                        })


                    Dim csvdata As String = File.ReadAllText(csvpath)

                    For Each row As String In csvdata.Split(ControlChars.Lf)

                        If row.Contains("RmCode") Or row.Contains("QuantityPerUnit") Or row.Contains("MesurementUnit") Then

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

                    dt.Columns.AddRange(New DataColumn(2) _
                                      {New DataColumn("ItemCode", GetType(String)),
                                      New DataColumn("Uploaded_DT", GetType(DateTime)),
                                      New DataColumn("Updated_By", GetType(String))
                                      })

                    Dim ItemCode, UpdatedBy As String

                    ItemCode = DropDownList1.Text
                    Dim Uploaded_DT As DateTime = DateTime.Now
                    UpdatedBy = Session("Userid")


                    For j As Integer = 0 To dt.Rows.Count - 1


                        dt.Rows(j)(3) = ItemCode
                        dt.Rows(j)(4) = Uploaded_DT
                        dt.Rows(j)(5) = UpdatedBy

                    Next



                    cn.Open()

                    Using copy As New SqlBulkCopy(cn)
                        copy.BulkCopyTimeout = 3000

                        'copy.ColumnMappings.Add(0, 3) '0 index in SQL - itemcode
                        'copy.ColumnMappings.Add(1, 0) '1 index in SQL - RM Code

                        'copy.ColumnMappings.Add(2, 1) '2 index in SQL - Quantity
                        'copy.ColumnMappings.Add(3, 4) '3 index in SQL - Updated_Dt
                        'copy.ColumnMappings.Add(4, 5) '4 index in SQL - Updated_By
                        'copy.ColumnMappings.Add(5, 2) '5 index in SQL - UnitOfMesurement

                        copy.ColumnMappings.Add(3, 0) '0 index in SQL - itemcode
                        copy.ColumnMappings.Add(0, 1) '1 index in SQL - RM Code

                        copy.ColumnMappings.Add(1, 2) '2 index in SQL - Quantity
                        copy.ColumnMappings.Add(4, 3) '3 index in SQL - Updated_Dt
                        copy.ColumnMappings.Add(5, 4) '4 index in SQL - Updated_By
                        copy.ColumnMappings.Add(2, 5) '5 index in SQL - UnitOfMesurement

                        copy.DestinationTableName = "BOM"
                        copy.WriteToServer(dt)


                    End Using

                    cn.Close()

                    '--------------Data inserting in Pickliststatus table----------------------


                    'Dim command_1 As New SqlCommand("insert into PicklistStatus values('" & Uploadno & "','" & Workorder & "','" & SequenceNo & "','" & UploadedBy & "',getdate(),'Open')", cn)
                    'cn.Open()
                    'command_1.ExecuteNonQuery()
                    'cn.Close()


                    'cn.Open()
                    'Dim cmd_2 As New SqlCommand("update GateEntry set GrnStatus='GRN Done' where VendorInvoiceNo='" & TextBox2.Text.Trim & "'", cn)
                    'cmd_2.ExecuteNonQuery()
                    'cn.Close()



                    '--------------------****---------------------------------------------------
                    Label1.Visible = True
                    Me.Label1.ForeColor = System.Drawing.Color.Green
                    Me.Label1.BackColor = System.Drawing.Color.Yellow
                    Me.Label1.Text = "Data Uploaded Successfully Uploaded"



                    'Me.TextBox1.Text = String.Empty
                    'Me.TextBox2.Text = String.Empty



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

            Dim msg As String = "You do not have rights to upload"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)


        End If


    End Sub





    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '===============Mass uploading=====================

        Mass_BOM_Uploading()
        GetGridview()

    End Sub
End Class