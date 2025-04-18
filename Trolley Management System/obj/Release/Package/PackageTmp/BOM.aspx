<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="BOM.aspx.vb" Inherits="Trolley_Management_System.BOM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style2 {
            width: 194px;
        }
        .auto-style3 {
            width: 13px;
        }
        .auto-style4 {
            width: 266px;
        }
        .auto-style1 {
            width: 100%;
        }
        .auto-style39 {
            font-size: small;
        }
        .auto-style44 {
            width: 194px;
            height: 30px;
        }
        .auto-style45 {
            width: 13px;
            height: 30px;
        }
        .auto-style46 {
            width: 266px;
            height: 30px;
            text-align: left;
        }
        .auto-style47 {
            height: 30px;
        }
        .auto-style50 {
        width: 266px;
        height: 37px;
            text-align: right;
        }
        .auto-style51 {
            width: 194px;
            height: 37px;
        }
        .auto-style52 {
            width: 13px;
            height: 37px;
        }
        .auto-style53 {
            height: 37px;
        }
        .auto-style56 {
            width: 266px;
            height: 37px;
        }
        .auto-style57 {
            width: 194px;
            height: 36px;
        }
        .auto-style58 {
            width: 13px;
            height: 36px;
        }
        .auto-style59 {
            width: 266px;
            height: 36px;
            text-align: left;
        }
        .auto-style60 {
            height: 36px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style51">&nbsp;</td>
            <td class="auto-style52"></td>
            <td class="auto-style50">
                <asp:LinkButton ID="LinkButton1" runat="server">BoM Per Item</asp:LinkButton>
            </td>
            <td class="auto-style53"></td>
            <td class="auto-style53"></td>
        </tr>
        <tr>
            <td class="auto-style51">&nbsp;&nbsp;&nbsp; FG/SFG Code</td>
            <td class="auto-style52">:</td>
            <td class="auto-style56">
                <asp:TextBox ID="TextBox1" runat="server" Width="260px" AutoPostBack="true"></asp:TextBox>
            </td>
            <td class="auto-style53"></td>
            <td class="auto-style53"></td>
        </tr>
        <tr>
            <td class="auto-style57">&nbsp;&nbsp;&nbsp; Item Code Description</td>
            <td class="auto-style58">:</td>
            <td class="auto-style59">
                <asp:TextBox ID="TextBox2" runat="server" Width="260px" AutoPostBack="true"></asp:TextBox>
            </td>
            <td class="auto-style60">
                </td>
            <td class="auto-style60"></td>
        </tr>
        <tr>
            <td class="auto-style44"></td>
            <td class="auto-style45"></td>
            <td class="auto-style46">
                <asp:Button ID="Button1" runat="server" Text="Add" Width="129px" CssClass="auto-style39" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;
            </td>
            <td class="auto-style47">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style47"></td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style4">
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        </table>

     
        <%--========================Data Grid view===================--%>
         
        <div style="padding-left:100px; padding-bottom:50px" class="text-center">
        <strong>
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="auto-style39" Width="1000px" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Item Code">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Description">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Updated Date">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Updated_Dt") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Updated_Dt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
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
