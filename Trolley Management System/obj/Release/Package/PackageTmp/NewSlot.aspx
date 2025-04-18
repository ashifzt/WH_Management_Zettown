<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="NewSlot.aspx.vb" Inherits="Trolley_Management_System.NewSlot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 137px;
        }
        .auto-style3 {
            width: 8px;
        }
        .auto-style4 {
            width: 137px;
            height: 23px;
        }
        .auto-style5 {
            width: 8px;
            height: 23px;
        }
        .auto-style6 {
            height: 23px;
        }
        .auto-style7 {
            height: 23px;
            width: 146px;
        }
        .auto-style8 {
            width: 146px;
        }
        .auto-style9 {
            font-size: large;
        }
        .auto-style10 {
            width: 137px;
            height: 20px;
        }
        .auto-style11 {
            width: 8px;
            height: 20px;
        }
        .auto-style12 {
            height: 20px;
            width: 146px;
        }
        .auto-style13 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style4"></td>
                <td class="auto-style5"></td>
                <td class="auto-style7"></td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Create New Trolley</td>
                <td class="auto-style3">:</td>
                <td class="auto-style8">
                    <asp:Button ID="Button1" runat="server" Text="Create" Width="117px" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style9" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style11">:</td>
                <td class="auto-style12">
                    <asp:Button ID="Button2" runat="server" Text="Attach Trolley" Width="117px" />
                </td>
                <td class="auto-style13">
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</asp:Content>
