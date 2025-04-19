<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="RmLoader.aspx.vb" Inherits="Trolley_Management_System.RmLoader" %>
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
        width: 445px;
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
            width: 445px;
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
            width: 445px;
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
            width: 445px;
            height: 37px;
        }
        .auto-style55 {
            height: 37px;
        }
        .auto-style56 {
            width: 445px;
            text-align: right;
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
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style56">
                <asp:LinkButton ID="LinkButton1" runat="server">Scrap Row Material</asp:LinkButton>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style48">&nbsp;&nbsp;&nbsp; FG Item code</td>
            <td class="auto-style49">:</td>
            <td class="auto-style50">
                <asp:DropDownList ID="DropDownList1"  runat="server" Height="26px" Width="265px" AutoPostBack="true" >
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
                <asp:DropDownList ID="DropDownList2"  runat="server" Height="26px" Width="265px" AutoPostBack="true" >
                    <asp:ListItem>----Select SMT Line----</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style55">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style55"></td>
        </tr>
        <tr>
            <td class="auto-style48">&nbsp;&nbsp;&nbsp; QR Code</td>
            <td class="auto-style49"></td>
            <td class="auto-style50">
                <asp:TextBox ID="TextBox1"  runat="server" Width="433px" AutoPostBack="true"></asp:TextBox>
            </td>
            <td class="auto-style51"></td>
            <td class="auto-style51"></td>
        </tr>
        <tr>
            <td class="auto-style52"></td>
            <td class="auto-style53"></td>
            <td class="auto-style54">
                <asp:Button ID="Button1" runat="server" Text="Submit" Width="129px" CssClass="auto-style39" />
            </td>
            <td class="auto-style55"></td>
            <td class="auto-style55"></td>
        </tr>
        <tr>
            <td class="auto-style44"></td>
            <td class="auto-style45"></td>
            <td class="auto-style46">&nbsp;</td>
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

        <%--========================Data Grid view===================--%>
         
        <div style="padding-left:100px; padding-bottom:50px" class="text-center">
        <strong>
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="auto-style39" Width="998px" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Item Code">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RMCode" HeaderText="RM Partcode" />
            <asp:BoundField DataField="SmtLine" HeaderText="Smt Line" />
            <asp:BoundField DataField="QuantityPerUnit" HeaderText="QPS" />
            <asp:BoundField DataField="Available_Qty" HeaderText="Available Quantity" />
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>
        </strong>
</div>
 


</form>
</asp:Content>
