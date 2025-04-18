<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ScrapRM.aspx.vb" Inherits="Trolley_Management_System.ScrapRM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">

        .auto-style1 {
            width: 100%;
        }
        

        .auto-style2 {
            width: 145px;
        }
        .auto-style3 {
            width: 13px;
        }
        .auto-style4 {
             width: 355px;
         }
        .auto-style48 {
            width: 145px;
            height: 34px;
        }
        .auto-style49 {
            width: 13px;
            height: 34px;
        }
        .auto-style50 {
             width: 355px;
             height: 34px;
         }
        .auto-style51 {
            height: 34px;
        }
        .auto-style44 {
            width: 145px;
            height: 19px;
        }
        .auto-style45 {
            width: 13px;
            height: 19px;
        }
        .auto-style46 {
            width: 355px;
            height: 19px;
        }
        .auto-style39 {
            font-size: small;
        }
        .auto-style47 {
            height: 19px;
        }
        .auto-style52 {
            width: 145px;
            height: 37px;
        }
        .auto-style53 {
            width: 13px;
            height: 37px;
        }
        .auto-style54 {
             width: 355px;
             height: 37px;
         }
        .auto-style55 {
            height: 37px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <form id="form1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style48">&nbsp;&nbsp;&nbsp; FG Item code</td>
            <td class="auto-style49">:</td>
            <td class="auto-style50">
                <asp:DropDownList ID="DropDownList1"  runat="server" Height="26px" Width="265px" AutoPostBack="true" required="true">
                    <asp:ListItem>----Select Itemcode----</asp:ListItem>
                </asp:DropDownList>

                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

            </td>
            <td class="auto-style51"></td>
            <td class="auto-style51"></td>
        </tr>
        <tr>
            <td class="auto-style52">&nbsp;&nbsp;&nbsp; SMT Line</td>
            <td class="auto-style53"></td>
            <td class="auto-style54">
                <asp:DropDownList ID="DropDownList2"  runat="server" Height="26px" Width="265px" AutoPostBack="true" required="true" >
                    <asp:ListItem>----Select SMT Line----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style55">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style55"></td>
        </tr>
        <tr>
            <td class="auto-style48">&nbsp;&nbsp;&nbsp; RM Part Code</td>
            <td class="auto-style49"></td>
            <td class="auto-style50">
                <asp:TextBox ID="TextBox1"  runat="server" Width="302px" AutoPostBack="true" required="true"></asp:TextBox>
            </td>
            <td class="auto-style51"></td>
            <td class="auto-style51"></td>
        </tr>
        <tr>
            <td class="auto-style52">&nbsp;&nbsp;&nbsp; Quantity</td>
            <td class="auto-style53"></td>
            <td class="auto-style54">
                <asp:TextBox ID="TextBox2" Type="number"  runat="server" Width="302px" AutoPostBack="true" required="true"></asp:TextBox>
            </td>
            <td class="auto-style55"></td>
            <td class="auto-style55"></td>
        </tr>
        <tr>
            <td class="auto-style44"></td>
            <td class="auto-style45"></td>
            <td class="auto-style46">
                <asp:Button ID="Button1" runat="server" Text="Submit" Width="129px" CssClass="auto-style39" />
            </td>
            <td class="auto-style47"></td>
            <td class="auto-style47"></td>
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
