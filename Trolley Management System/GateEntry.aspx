<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="GateEntry.aspx.vb" Inherits="Trolley_Management_System.GateEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 258px;
        }
        .auto-style3 {
            width: 258px;
            font-size: small;
        }
        .auto-style4 {
            width: 465px;
        }
        .auto-style5 {
            width: 258px;
            height: 31px;
        }
        .auto-style6 {
            width: 465px;
            height: 31px;
        }
        .auto-style7 {
            height: 31px;
        }
        .auto-style8 {
            width: 258px;
            font-size: medium;
        }
    </style>

    <script>
        function isNumber(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
&& (charCode < 48 || charCode > 57))
                return false;
 
            return true;
        }
</script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style4">
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">Vendor Invoice Number</td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox1" runat="server" Width="428px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">Vendor Name</td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox2" runat="server" Width="428px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">Vendor Invoice Date</td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox3" runat="server" Width="428px"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/calendar.jpg" style="padding-bottom: 0px;" Width="25px"/>
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" Width="432px" NextPrevFormat="FullMonth" TitleFormat="Month">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Height="10pt" Font-Size="7pt" ForeColor="#333333" />
                        <DayStyle Width="14%" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                        <TodayDayStyle BackColor="#CCCC99" />
                    </asp:Calendar>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">Invoice Value</td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox4" runat="server" onkeypress="return isNumber(event);" Width="428px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">Vehicle Number</td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox5" runat="server" Width="428px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">Remarks</td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox6" runat="server" Width="428px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style6">
                    <asp:Button ID="Button1" runat="server" Text="Submit" Width="142px" />
                </td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</asp:Content>
