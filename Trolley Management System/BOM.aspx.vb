Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic


Public Class BOM
    Inherits System.Web.UI.Page

    Public Connstr As String = ConfigurationManager.ConnectionStrings("DBCS").ToString
    Dim cn As New SqlConnection(Connstr)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.Session("Userid") Is Nothing Then
            Response.Redirect("~/Default.aspx")
        End If

        If Not IsPostBack Then
            Label1.Visible = False
            GetGridview()

        End If



    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = String.Empty Then

            Dim msg As String = "please enter item code first"
            Dim script = String.Format("alert('{0}');", msg)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", script, True)

            Exit Sub

        Else
            Call Item_Creation()
            GetGridview()
        End If



    End Sub


    Sub GetGridview()


        cn.Open()
        Dim cmd1 As New SqlCommand("select Itemcode, ItemDesc, Updated_Dt from Itemcode", cn)
        Dim sdt1 As New SqlDataAdapter(cmd1)
        Dim dt1 As New DataTable
        sdt1.Fill(dt1)
        cn.Close()

        GridView1.DataSource = dt1
        GridView1.DataBind()



    End Sub


    Sub Item_Creation()



        Dim ItemCode, ItemDesc, username As String

        ItemCode = TextBox1.Text
        ItemDesc = TextBox2.Text

        username = Session("Userid")

        Try
            cn.Open()
            Dim cmd As New SqlCommand("insert into Itemcode (ItemCode,ItemDesc,Updated_Dt,Updated_By) values ('" & ItemCode & "','" & ItemDesc & "',GETDATE(),'" & username & "')", cn)
            cmd.ExecuteNonQuery()
            cn.Close()

            Label1.Visible = True
            Label1.Text = TextBox1.Text & " : Item successfully created"
            Label1.BackColor = System.Drawing.Color.Yellow

            TextBox1.Text = String.Empty


        Catch ex As Exception

            Label1.Visible = True
            Label1.Text = ex.Message
            Label1.BackColor = System.Drawing.Color.Red

        End Try


    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("~/BomPerItem.aspx")
    End Sub
End Class