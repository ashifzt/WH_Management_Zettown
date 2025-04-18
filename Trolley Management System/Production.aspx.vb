
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration

Public Class Production
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If

        If Not IsPostBack Then

            Me.Label12.Visible = False
            Me.TextBox1.Focus()


        End If


    End Sub


    Sub Getdata_onGridview()

        'Dim trolleyno As String = Me.TextBox1.Text

        Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
        Dim cn As New SqlConnection(Connstr)


        cn.Open()
        Dim cmd_101 As New SqlCommand("select * from expiry_itemmaterial where expiry_itemmaterial='" & TextBox1.Text.Trim & "'", cn)
        Dim sdt_101 As New SqlDataAdapter(cmd_101)
        Dim dt_101 As New DataTable
        sdt_101.Fill(dt_101)
        cn.Close()

        Dim cmd2_sql As String

        If dt_101.Rows.Count <= 0 Then
            cmd2_sql = "select top ((select count(*) from (select * from (select *, SUM(qty) OVER (ORDER BY Updated_DT) as CumulativeSales from TrolleyLaser where  Updatetype='inv_in' and item='" & TextBox1.Text.Trim & "') t1
where t1.CumulativeSales<" & TextBox12.Text.Trim & " ) t2)+1) *  from (select * from (select *, SUM(qty) OVER (ORDER BY Updated_DT) as CumulativeSales from TrolleyLaser where  Updatetype='inv_in' and item='" & TextBox1.Text.Trim & "') t1 ) t2
"
        Else

            cmd2_sql = "select top ((select count(*) from (select * from (select *, SUM(qty) OVER (ORDER BY expiry_date,Updated_DT) as CumulativeSales from TrolleyLaser where  Updatetype='inv_in' and item='" & TextBox1.Text.Trim & "' and expiry_date>GETDATE()) t1
where t1.CumulativeSales<" & TextBox12.Text.Trim & " ) t2)+1) *  from (select * from (select *, SUM(qty) OVER (ORDER BY expiry_date,Updated_DT) as CumulativeSales from TrolleyLaser where  Updatetype='inv_in' and item='" & TextBox1.Text.Trim & "' and expiry_date>GETDATE()) t1 ) t2
"
        End If



        '        Dim cmd2 As New SqlCommand("select top ((select count(*) from
        '(select * from (select *, SUM(qty) OVER (ORDER BY Updated_DT) as CumulativeSales from TrolleyLaser where  Updatetype='inv_in') t1
        'where t1.CumulativeSales<" & TextBox12.Text.Trim & " and item='" & TextBox1.Text.Trim & "'     ) t2)+1) *  from (select * from (select *, SUM(qty) OVER (ORDER BY Updated_DT) as CumulativeSales from TrolleyLaser where  Updatetype='inv_in' and item='" & TextBox1.Text.Trim & "') t1 
        ') t2", cn)

        Dim cmd2 As New SqlCommand(cmd2_sql, cn)



        Dim adaptor As New SqlDataAdapter(cmd2)

        Dim table1 As New DataTable
        adaptor.Fill(table1)

        If table1.Rows.Count <= 0 Or TextBox12.Text <= 0 Then
            TextBox1.Text = ""
            TextBox12.Text = ""
            GridView1.DataSource = Nothing
            GridView1.DataBind()


        End If

        GridView1.DataSource = table1
        GridView1.DataBind()

        Me.Label12.Visible = False




    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, GridView1.SelectedIndexChanged

        Dim userrole As String = Me.Session("Roles").ToString.ToUpper
        If userrole.Contains("PRDUSER") Or userrole.Contains("ADMIN") Then


            Call Getdata_onGridview()



        Else

            Dim msg As String = "You do not have rights to this page"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

        End If



    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub LinkButton1_Click1(sender As Object, e As EventArgs)

        'Dim lbl As Label = GridView1.Rows(0).FindControl("Label1")
        'MsgBox(lbl.Text & "-" & Me.GridView1.Rows.Count)

    End Sub

    Protected Sub LinkButton1_Command(sender As Object, e As CommandEventArgs)

    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating

        Dim lbl1 As Label = GridView1.Rows(e.RowIndex).FindControl("Label1")
        Dim lbl13 As Label = GridView1.Rows(e.RowIndex).FindControl("Label13")
        Dim txt1 As TextBox = GridView1.Rows(e.RowIndex).FindControl("TextBox13")


        Dim Trolleyno As Label = GridView1.Rows(e.RowIndex).FindControl("Label2")
        Dim Rackno As Label = GridView1.Rows(e.RowIndex).FindControl("Label3")
        Dim Slotno As Label = GridView1.Rows(e.RowIndex).FindControl("Label4")
        Dim Size As Label = GridView1.Rows(e.RowIndex).FindControl("Label5")
        Dim Currentitem As Label = GridView1.Rows(e.RowIndex).FindControl("Label6")
        Dim Qty As Label = GridView1.Rows(e.RowIndex).FindControl("Label7")
        'Dim Lastfilledby As Label = GridView1.Rows(e.RowIndex).FindControl("Label8")
        'Dim Lastfilled_Dt As Label = GridView1.Rows(e.RowIndex).FindControl("Label9")
        Dim HandlingUnit As Label = GridView1.Rows(e.RowIndex).FindControl("Label10")
        Dim Uploadno As Label = GridView1.Rows(e.RowIndex).FindControl("Label11")




        Dim Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
        Dim cn As New SqlConnection(Connstr)




        ' Dim cmd2 As New SqlCommand("select Uniqueid,TrolleyNo,RackNo,slotNo,Size,Currentitem,Qty,LastFilledBy,LastFilled_DT,LastConsumedBy,LastConsumed_DT,TrolleyLocation,Uploadno from currentti where TrolleyLocation='Warehouse' and Currentitem='" & trolleyno & "' and Qty is not NULL", cn)
        Dim cmd2 As New SqlCommand("select * from CurrentTI where Uniqueid='" & lbl1.Text & "'", cn)
        Dim adaptor As New SqlDataAdapter(cmd2)
        Dim table1 As New DataTable
        adaptor.Fill(table1)


        Dim currenttiqty = table1.Rows(0)("Qty")

        '================

        If HandlingUnit.Text.ToUpper = txt1.Text.ToUpper Then
            If TextBox1.Text = "" Or TextBox12.Text = "" Then


            Else
                If currenttiqty = Qty.Text Then

                    Dim cmd22 As New SqlCommand("update CurrentTI set Currentitem=NULL, Qty=NULL, LastConsumedBy='" & Me.Session("Username") & "', LastConsumed_DT=Getdate() where Uniqueid='" & lbl1.Text & "'", cn)

                    cn.Open()
                    cmd22.ExecuteNonQuery()
                    cn.Close()


                    Dim Qry1 As String = "update TrolleyLaser set Updatetype='ATProduction', LastConsumedBy='" & Me.Session("Username") & "',LastConsumed_DT=Getdate(), transaction_type='ATProduction' where id ='" & lbl13.Text & "'"

                    'Qry1 = "Insert into TrolleyLaser values (CAST(GETDATE() as date),GETDATE(), 'Production Consumption', '" & lbl1.Text & "','" & Trolleyno.Text & "','" & Rackno.Text & "','" & Slotno & "','" & Currentitem & "','" & Qty & "',NULL,NULL,'" & Me.Session("Username") & "', getdate(),'Production','" & Uploadno & "')"

                    Dim cmd3 As New SqlCommand(Qry1, cn)
                    cn.Open()
                    cmd3.ExecuteNonQuery()
                    cn.Close()

                    'Me.TextBox1.Text = String.Empty
                    Me.TextBox12.Text = TextBox12.Text.Trim - Qty.Text

                    Call Getdata_onGridview()


                    Me.Label12.Visible = True

                    Me.Label12.BackColor = System.Drawing.Color.Yellow
                    Me.Label12.Text = "Successfully Consumed"


                    Me.TextBox1.Focus()

                Else


                    Dim cmd22 As New SqlCommand("update CurrentTI set  Qty=" & currenttiqty - Qty.Text & ", LastConsumedBy='" & Me.Session("Username") & "', LastConsumed_DT=Getdate() where Uniqueid='" & lbl1.Text & "'", cn)

                    cn.Open()
                    cmd22.ExecuteNonQuery()
                    cn.Close()


                    Dim Qry1 As String = "update TrolleyLaser set Updatetype='ATProduction', LastConsumedBy='" & Me.Session("Username") & "',LastConsumed_DT=Getdate(), transaction_type='ATProduction' where id ='" & lbl13.Text & "'"

                    'Qry1 = "Insert into TrolleyLaser values (CAST(GETDATE() as date),GETDATE(), 'Production Consumption', '" & lbl1.Text & "','" & Trolleyno.Text & "','" & Rackno.Text & "','" & Slotno & "','" & Currentitem & "','" & Qty & "',NULL,NULL,'" & Me.Session("Username") & "', getdate(),'Production','" & Uploadno & "')"

                    Dim cmd3 As New SqlCommand(Qry1, cn)
                    cn.Open()
                    cmd3.ExecuteNonQuery()
                    cn.Close()

                    Me.TextBox12.Text = TextBox12.Text.Trim - Qty.Text

                    Call Getdata_onGridview()


                    Me.Label12.Visible = True

                    Me.Label12.BackColor = System.Drawing.Color.Yellow
                    Me.Label12.Text = "Successfully Consumed"

                    Me.TextBox1.Focus()

                End If


            End If

        Else
            Dim msg As String = "System Handling Unit and Scanned Handling unit is different"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)
            Exit Sub

        End If



        '---------------------------------------------------------------------




    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        If Me.TextBox1.Text <> String.Empty Then

            TextBox12.Focus()

        End If

    End Sub

    Protected Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged

        If Me.TextBox12.Text <> String.Empty Then
            Button1_Click(sender, e)
        End If
    End Sub
End Class