Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic


Public Class ScanForAeging
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.Session("Userid") Is Nothing Then

            Response.Redirect("~/Default.aspx")
        Else
            Call Getdata_onGridview()

        End If

    End Sub


    Sub Getdata_onGridview()


        cn.Open()

        Dim cmd2 As New SqlCommand("select Id, partNumber,HandlingUnit, ScannedTime,Status1, ScannedUser,Consumedtime,ConsumedUser,warningMinutes, AegingMinutes,warning_at,aging_at,new_status,Dryin_DT,DryOut_DT,TimeRemaining from new_status", cn)
        Dim adaptor As New SqlDataAdapter(cmd2)
        Dim table1 As New DataTable
        adaptor.Fill(table1)

        cn.Close()

        GridView1.DataSource = table1
        GridView1.DataBind()


    End Sub




    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Roles As String = Me.Session("Roles").ToString.ToUpper

        Dim HandlingUnit As String
        Dim PartNumber As String
        Dim transaction_type As String

        Dim userid As String = Me.Session("Userid")

        'HandlingUnit = Convert.ToInt32(TextBox1.Text)
        HandlingUnit = TextBox1.Text


        If Roles.Contains("ADMIN") Or Roles.Contains("AGING") Then

            cn.Open()
            Dim cmd1 As New SqlCommand("select * from TrolleyLaser where ItemUID='" & HandlingUnit & "'", cn)
            Dim sdt1 As New SqlDataAdapter(cmd1)
            Dim dt1 As New DataTable
            sdt1.Fill(dt1)
            cn.Close()

            If dt1.Rows.Count <= 0 Then

                Dim msg As String = "Scanned handling Unit is not availabe in WMS System"
                Dim script = String.Format("alert('{0}');", msg)
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)
                Exit Sub

            Else

                transaction_type = dt1.Rows(0)("transaction_type")
                PartNumber = dt1.Rows(0)("item")

                If transaction_type.ToUpper = "INV_IN" Then

                    Dim msg1 As String = "Scanned Handling Unit is Still in Warehouse"
                    Dim script1 = String.Format("alert('{0}');", msg1)
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script1, True)
                    Exit Sub

                Else


                    cn.Open()
                    Dim cmd2 As New SqlCommand("insert into PartTAT_Laser(PartNumber,HandlingUnit,ScannedTime,Status1,ScannedUser) values('" & PartNumber & "','" & HandlingUnit & "',GETDATE(),'In-Progress','" & userid & "')", cn)
                    cmd2.ExecuteNonQuery()
                    cn.Close()

                    Call Getdata_onGridview()
                    TextBox1.Text = String.Empty



                End If


            End If



        Else

            Dim msg As String = "You Do Not have right To this activity"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

        End If




    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)

        Me.Session("ButtonNmae") = "Button_One"

    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs)

        Me.Session("ButtonNmae") = "Button_Two"

    End Sub

    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs)

        Me.Session("ButtonNmae") = "Button_Three"

    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating


        Dim btn_name As String = Me.Session("ButtonNmae")

        If btn_name = "Button_One" Then

            Dim id As Label
            id = GridView1.Rows(e.RowIndex).FindControl("Label1")

            cn.Open()
            'Dim cmd As New SqlCommand("update PartTAT_Laser set Status1='Done' where id='" & id.Text & "'", cn)
            Dim cmd As New SqlCommand("update PartTAT_Laser set Status1='Done', ConsumedTime = GETDATE(), ConsumedUser = '" & Me.Session("Userid") & "'  where id='" & id.Text & "'", cn)
            cmd.ExecuteNonQuery()
            cn.Close()

            'Call Getdata_onGridview()


        ElseIf btn_name = "Button_Two" Then

            Dim id As Label
            id = GridView1.Rows(e.RowIndex).FindControl("Label1")

            cn.Open()
            Dim cmd1 As New SqlCommand("update PartTAT_Laser set Dryin_DT=GETDATE() where id='" & id.Text & "'", cn)
            cmd1.ExecuteNonQuery()
            cn.Close()


            'MsgBox("button two executed" & id.Text)

        ElseIf btn_name = "Button_Three" Then

            Dim id As Label
            id = GridView1.Rows(e.RowIndex).FindControl("Label1")

            cn.Open()
            Dim cmd2 As New SqlCommand("update PartTAT_Laser set DryOut_DT=GETDATE() where id='" & id.Text & "'", cn)
            cmd2.ExecuteNonQuery()
            cn.Close()

            'MsgBox("button threen executed" & id.Text)

        End If


        Call Getdata_onGridview()




    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim id As Label
            id = e.Row.Cells(10).FindControl("Label13")


            Dim Dryin As Label = e.Row.Cells(11).FindControl("Label8")
            Dim Dryout As Label = e.Row.Cells(12).FindControl("Label14")
            '===============================================================
            Dim dryinbtn As LinkButton = e.Row.Cells(14).FindControl("LinkButton2")
            Dim dryoutbtn As LinkButton = e.Row.Cells(14).FindControl("LinkButton3")
            Dim stopbtn As LinkButton = e.Row.Cells(14).FindControl("LinkButton1")
            '================================================================

            Dim cellValue As String = id.Text

            If cellValue.ToUpper = "IN-PROGRESS" Then 'Mid(cellValue, 1, 1) = "-" Then
                e.Row.BackColor = System.Drawing.Color.Green
            ElseIf cellValue.ToUpper = "WARNING STAGE" Then 'Mid(cellValue, 1, 1) = "-" Then
                e.Row.BackColor = System.Drawing.Color.Orange
            Else
                e.Row.BackColor = System.Drawing.Color.Red
                e.Row.Cells(10).ForeColor = System.Drawing.Color.White
            End If

            '=============================================================

            If Dryin.Text = String.Empty And Dryout.Text = String.Empty Then

                dryinbtn.Enabled = True
                dryoutbtn.Enabled = False
                stopbtn.Enabled = True

                'Dryout.Text = ""

            ElseIf Dryin.Text <> String.Empty And Dryout.Text = String.Empty Then

                dryinbtn.Enabled = False
                dryoutbtn.Enabled = True
                stopbtn.Enabled = False

                'dryinbtn.Text = ""


            ElseIf Dryin.Text <> String.Empty And Dryout.Text <> String.Empty Then

                dryinbtn.Enabled = False
                dryoutbtn.Enabled = False
                stopbtn.Visible = True

                'dryinbtn.Text = ""
                'dryoutbtn.Text = ""


            End If



        End If

    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Call Getdata_onGridview()
        'MsgBox("success")
    End Sub
End Class