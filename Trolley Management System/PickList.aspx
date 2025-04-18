<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="PickList.aspx.vb" Inherits="Trolley_Management_System.PickList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 152px;
        }
        .auto-style3 {
            width: 13px;
        }
        .auto-style4 {
            width: 270px;
        }
        .auto-style5 {
            width: 152px;
            height: 26px;
        }
        .auto-style6 {
            width: 13px;
            height: 26px;
        }
        .auto-style7 {
            width: 270px;
            height: 26px;
        }
        .auto-style8 {
            height: 26px;
        }
    .auto-style9 {
        width: 152px;
        height: 20px;
    }
    .auto-style10 {
        width: 13px;
        height: 20px;
    }
    .auto-style11 {
        width: 270px;
        height: 20px;
    }
    .auto-style12 {
        height: 20px;
    }
        .auto-style13 {
            width: 152px;
            height: 33px;
        }
        .auto-style14 {
            width: 13px;
            height: 33px;
        }
        .auto-style15 {
            width: 270px;
            height: 33px;
        }
        .auto-style16 {
            height: 33px;
        }
        .auto-style17 {
            width: 152px;
            height: 35px;
        }
        .auto-style18 {
            width: 13px;
            height: 35px;
        }
        .auto-style19 {
            width: 270px;
            height: 35px;
        }
        .auto-style20 {
            height: 35px;
        }
        .auto-style21 {
            width: 152px;
            height: 37px;
        }
        .auto-style22 {
            width: 13px;
            height: 37px;
        }
        .auto-style23 {
            width: 270px;
            height: 37px;
        }
        .auto-style24 {
            height: 37px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style13">&nbsp;&nbsp;&nbsp; GRN No</td>
                <td class="auto-style14">:</td>
                <td class="auto-style15">
                    <asp:TextBox ID="TextBox1" runat="server" Width="260px"></asp:TextBox>
                </td>
                <td class="auto-style16"></td>
                <td class="auto-style16"></td>
            </tr>
            <tr>
                <td class="auto-style17">&nbsp;&nbsp;&nbsp; Vendor Invoice No</td>
                <td class="auto-style18">:</td>
                <td class="auto-style19">
                    <asp:TextBox ID="TextBox2" runat="server" Width="260px"></asp:TextBox>
                </td>
                <td class="auto-style20"></td>
                <td class="auto-style20"></td>
            </tr>
            <tr>
                <td class="auto-style21">&nbsp;&nbsp;&nbsp; Select File</td>
                <td class="auto-style22">:</td>
                <td class="auto-style23">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="260px" />
                </td>
                <td class="auto-style24"></td>
                <td class="auto-style24"></td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style4">
                    <asp:Button ID="Button1" runat="server" Text="Upload" Width="97px" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style10"></td>
                <td class="auto-style11"></td>
                <td class="auto-style12">
                    </td>
                <td class="auto-style12"></td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</asp:Content>
