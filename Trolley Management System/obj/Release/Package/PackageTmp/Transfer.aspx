<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="Transfer.aspx.vb" Inherits="Trolley_Management_System.Transfer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            margin-bottom: 19px;
        }
        .auto-style2 {
            width: 112px;
        }
        .auto-style3 {
            width: 29px;
        }
        .auto-style4 {
            width: 112px;
            height: 26px;
        }
        .auto-style5 {
            width: 29px;
            height: 26px;
        }
        .auto-style6 {
            height: 26px;
        }
        .auto-style7 {
            width: 112px;
            height: 22px;
        }
        .auto-style8 {
            width: 29px;
            height: 22px;
        }
        .auto-style9 {
            height: 22px;
        }
        .auto-style10 {
            width: 219px;
        }
        .auto-style11 {
            height: 22px;
            width: 219px;
        }
        .auto-style12 {
            height: 26px;
            width: 219px;
        }
        .auto-style13 {
            width: 112px;
            height: 25px;
        }
        .auto-style14 {
            width: 29px;
            height: 25px;
        }
        .auto-style15 {
            width: 219px;
            height: 25px;
        }
        .auto-style16 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Transfer From</td>
                <td class="auto-style3">
                    :</td>
                <td class="auto-style10">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="18px" Width="170px">
                        <asp:ListItem>Warehouse</asp:ListItem>
                        <asp:ListItem>Warehouse_Locked</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7"> </td>
                <td class="auto-style8">
                </td>
                <td class="auto-style11">
                    <asp:Button ID="Button1" runat="server" Text="Get WH Area" Width="168px" />
                </td>
                <td class="auto-style9"></td>
            </tr>
            <tr>
                <td class="auto-style4">Select WH Area</td>
                <td class="auto-style5">
                    :</td>
                <td class="auto-style12">
                    <asp:DropDownList ID="DropDownList2" runat="server" Height="18px" Width="170px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style6">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style13">Remarks </td>
                <td class="auto-style14">:</td>
                <td class="auto-style15">
                    <asp:TextBox ID="TextBox1" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td class="auto-style16"></td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style10">
                    <asp:Button ID="Button2" runat="server" Text="Transfer" Width="168px" Height="26px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </form>
</asp:Content>
