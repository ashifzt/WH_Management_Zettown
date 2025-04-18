Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.Session("Userid") Is Nothing Then

            Response.Redirect("~/Default.aspx")

        End If

        Session.Abandon()
        Response.AppendHeader("Refresh", "0.1:url=Default.aspx")
        Response.Redirect("~/Default.aspx")

    End Sub

End Class