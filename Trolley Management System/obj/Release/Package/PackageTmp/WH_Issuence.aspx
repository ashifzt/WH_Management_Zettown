<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="WH_Issuence.aspx.vb" Inherits="Trolley_Management_System.WH_Issuence" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
  
    
    
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style4 {
            width: 196px;
            height: 23px;
        }
        .auto-style5 {
            width: 361px;
            height: 23px;
        }
        .auto-style6 {
            height: 23px;
        }
        .auto-style7 {
            width: 196px;
        }
        .auto-style12 {
            width: 196px;
            height: 30px;
        }
        .auto-style13 {
            width: 361px;
            height: 30px;
        }
        .auto-style14 {
            height: 30px;
        }
        .auto-style15 {
            width: 14px;
        }
        .auto-style16 {
            width: 14px;
            height: 23px;
        }
        .auto-style17 {
            width: 14px;
            height: 30px;
        }
        .auto-style19 {
            width: 196px;
            height: 28px;
        }
        .auto-style20 {
            width: 14px;
            height: 28px;
        }
        .auto-style21 {
            width: 361px;
            height: 28px;
        }
        .auto-style22 {
            height: 28px;
        }
        .auto-style23 {
            width: 196px;
            height: 16px;
        }
        .auto-style24 {
            width: 14px;
            height: 16px;
        }
        .auto-style25 {
            width: 361px;
            height: 16px;
        }
        .auto-style26 {
            height: 16px;
        }
        .auto-style35 {
            width: 196px;
            height: 11px;
        }
        .auto-style36 {
            width: 14px;
            height: 11px;
        }
        .auto-style37 {
            width: 361px;
            height: 11px;
        }
        .auto-style38 {
            height: 11px;
        }
        .auto-style39 {
            font-size: small;
        }
        .auto-style40 {
            width: 231px;
            height: 23px;
        }
        .auto-style41 {
            width: 231px;
        }
        .auto-style42 {
            width: 231px;
            height: 28px;
        }
        .auto-style43 {
            width: 231px;
            height: 11px;
        }
        .auto-style44 {
            width: 231px;
            height: 30px;
        }
        .auto-style47 {
            width: 231px;
            height: 16px;
        }
        .auto-style52 {
            width: 196px;
            height: 25px;
        }
        .auto-style53 {
            width: 14px;
            height: 25px;
        }
        .auto-style54 {
            width: 361px;
            height: 25px;
        }
        .auto-style55 {
            width: 231px;
            height: 25px;
        }
        .auto-style56 {
            height: 25px;
        }
        .auto-style57 {
            width: 361px;
        }
        .auto-style58 {
            width: 196px;
            height: 33px;
        }
        .auto-style59 {
            width: 14px;
            height: 33px;
        }
        .auto-style60 {
            width: 361px;
            height: 33px;
        }
        .auto-style61 {
            width: 231px;
            height: 33px;
        }
        .auto-style62 {
            height: 33px;
        }
        .auto-style63 {
            width: 196px;
            height: 39px;
        }
        .auto-style64 {
            width: 14px;
            height: 39px;
        }
        .auto-style65 {
            width: 361px;
            height: 39px;
        }
        .auto-style66 {
            width: 231px;
            height: 39px;
        }
        .auto-style67 {
            height: 39px;
        }
        .auto-style68 {
            width: 196px;
            height: 36px;
        }
        .auto-style69 {
            width: 14px;
            height: 36px;
        }
        .auto-style70 {
            width: 361px;
            height: 36px;
        }
        .auto-style71 {
            width: 231px;
            height: 36px;
        }
        .auto-style72 {
            height: 36px;
        }
        .auto-style73 {
            width: 196px;
            height: 34px;
        }
        .auto-style74 {
            width: 14px;
            height: 34px;
        }
        .auto-style75 {
            width: 361px;
            height: 34px;
        }
        .auto-style76 {
            width: 231px;
            height: 34px;
        }
        .auto-style77 {
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <table class="auto-style1">
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style16">
                &nbsp;</td>
            <td class="auto-style5">
                &nbsp;</td>
            <td class="auto-style40">&nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style58">&nbsp;&nbsp;&nbsp; Upload No</td>
            <td class="auto-style59">
                :</td>
            <td class="auto-style60">
                <asp:DropDownList ID="DropDownList1" runat="server" Height="26px" Width="260px">
                </asp:DropDownList>
            </td>
            <td class="auto-style61"></td>
            <td class="auto-style62"></td>
            <td class="auto-style62"></td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style15">
                &nbsp;</td>
            <td class="auto-style57">
                <asp:Button ID="Button1" runat="server" Text="Get WH Area" Width="168px" CssClass="auto-style39" />
            </td>
            <td rowspan="2" class="auto-style41">
                <asp:Label ID="Label3" runat="server" Font-Size="XX-Large" Text="Label" CssClass="auto-style39"></asp:Label>
            </td>
            <td rowspan="3">
                <asp:Label ID="Label4" runat="server" Font-Size="XX-Large" Text="Label" CssClass="auto-style39"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="auto-style63">&nbsp;&nbsp;&nbsp; Scan Type</td>
            <td class="auto-style64">
                :</td>
            <td class="auto-style65">
                <asp:DropDownList ID="DropDownList3" runat="server" Height="26px" Width="260px">
                    <asp:ListItem>----Scan Type----</asp:ListItem>
                    <asp:ListItem>QR Code</asp:ListItem>
                    <asp:ListItem>Barcode / Manual</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style66">
                <asp:TextBox ID="TextBox3" runat="server" Width="165px" AutoPostBack="true"></asp:TextBox>
            </td>
            <td class="auto-style67"></td>
        </tr>
        <tr>
            <td class="auto-style35">&nbsp;</td>
            <td class="auto-style36">
                &nbsp;</td>
            <td class="auto-style37">
                <asp:Button ID="Button3" runat="server" Text="Confirm Scanning process" Width="170px" />
            </td>
            <td class="auto-style43">
                <asp:Button ID="Button5" runat="server" Text="Fill" Width="105px" />
            </td>
            <td class="auto-style38"></td>
            <td class="auto-style38"></td>
        </tr>
        <tr>
            <td class="auto-style12"></td>
            <td class="auto-style17">
                </td>
            <td class="auto-style13">
                &nbsp;</td>
            <td class="auto-style44"></td>
            <td class="auto-style14"></td>
            <td class="auto-style14"></td>
        </tr>
        <tr>
            <td class="auto-style68">
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="QR_Material_Code"></asp:Label>
            </td>
            <td class="auto-style69">
                :</td>
            <td class="auto-style70">
                <asp:TextBox ID="TextBox1" runat="server" Width="260px" AutoPostBack="true"></asp:TextBox>
            </td>
            <td class="auto-style71"></td>
            <td class="auto-style72"></td>
            <td class="auto-style72"></td>
        </tr>
        <tr>
            <td class="auto-style73">
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Text="Quantity"></asp:Label>
            </td>
            <td class="auto-style74">
                :</td>
            <td class="auto-style75">
                <asp:TextBox ID="TextBox2" runat="server" Width="260px" AutoPostBack="true"></asp:TextBox>
            </td>
            <td class="auto-style76"></td>
            <td class="auto-style77"></td>
            <td class="auto-style77"></td>
        </tr>
        <tr>
            <td class="auto-style19"></td>
            <td class="auto-style20">
                </td>
            <td class="auto-style21">
                <asp:Button ID="Button4" runat="server" Text="Show Slot" Width="90px" />
            </td>
            <td class="auto-style42"></td>
            <td class="auto-style22"></td>
            <td class="auto-style22"></td>
        </tr>
        <tr>
            <td class="auto-style52"></td>
            <td class="auto-style53"></td>
            <td class="auto-style54"></td>
            <td class="auto-style55"></td>
            <td class="auto-style56"></td>
            <td class="auto-style56"></td>
        </tr>
        <tr>
            <td class="auto-style4"></td>
            <td class="auto-style16"></td>
            <td class="auto-style5">
                &nbsp;</td>
            <td class="auto-style40"></td>
            <td class="auto-style6"></td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style23"></td>
            <td class="auto-style24"></td>
            <td class="auto-style25"></td>
            <td class="auto-style47"></td>
            <td class="auto-style26"></td>
            <td class="auto-style26"></td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
            <td class="auto-style57">&nbsp;</td>
            <td class="auto-style41">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </form>
</asp:Content>
