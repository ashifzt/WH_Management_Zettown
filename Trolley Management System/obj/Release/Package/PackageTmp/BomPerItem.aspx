<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="BomPerItem.aspx.vb" Inherits="Trolley_Management_System.BomPerItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


        .auto-style2 {
            width: 177px;
        }
        .auto-style3 {
            width: 13px;
        }
        .auto-style4 {
            width: 273px;
        }
        .auto-style1 {
            width: 100%;
        }
        .auto-style39 {
            font-size: small;
        }
        .auto-style68 {
            width: 177px;
            height: 32px;
        }
        .auto-style69 {
            width: 13px;
            height: 32px;
        }
        .auto-style70 {
            width: 273px;
            height: 32px;
        }
        .auto-style71 {
            height: 32px;
        }
        .auto-style72 {
            width: 177px;
            height: 34px;
        }
        .auto-style73 {
            width: 13px;
            height: 34px;
        }
        .auto-style74 {
            width: 273px;
            height: 34px;
        }
        .auto-style75 {
            height: 34px;
        }
        .auto-style78 {
            width: 273px;
            height: 40px;
        }
        .auto-style80 {
            width: 177px;
            height: 42px;
        }
        .auto-style81 {
            width: 13px;
            height: 42px;
        }
        .auto-style82 {
            height: 42px;
        }
        .auto-style83 {
            width: 177px;
            height: 43px;
        }
        .auto-style84 {
            width: 13px;
            height: 43px;
        }
        .auto-style85 {
            height: 43px;
        }
        .auto-style86 {
            width: 177px;
            height: 40px;
        }
        .auto-style87 {
            width: 13px;
            height: 40px;
        }
        .auto-style88 {
            height: 40px;
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
                <td class="auto-style68">&nbsp;&nbsp;&nbsp; Item code</td>
                <td class="auto-style69">:</td>
                <td class="auto-style70">
                <asp:DropDownList ID="DropDownList1" runat="server" Height="26px" Width="265px" AutoPostBack="true" >
                    <asp:ListItem>----Select Itemcode----</asp:ListItem>
                </asp:DropDownList>
                </td>
                <td class="auto-style71"></td>
                <td class="auto-style71"></td>
            </tr>
            <tr>
                <td class="auto-style72">&nbsp;&nbsp;&nbsp; RM Code</td>
                <td class="auto-style73"></td>
                <td class="auto-style74">
                <asp:TextBox ID="TextBox1" runat="server" Width="261px" AutoPostBack="true"></asp:TextBox>
                </td>
                <td class="auto-style75"></td>
                <td class="auto-style75"></td>
            </tr>
            <tr>
                <td class="auto-style68">&nbsp;&nbsp;&nbsp; Quantity</td>
                <td class="auto-style69"></td>
                <td class="auto-style70">
                <asp:TextBox ID="TextBox2" runat="server" Width="261px" AutoPostBack="true" TextMode="Number" ></asp:TextBox>
                </td>
                <td class="auto-style71">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="auto-style71"></td>
            </tr>
            <tr>
                <td class="auto-style86">&nbsp;&nbsp; Unit of Measurement&nbsp;</td>
                <td class="auto-style87">&nbsp;</td>
                <td class="auto-style78">
                <asp:TextBox ID="TextBox6" runat="server" Width="261px" AutoPostBack="true"></asp:TextBox>
                </td>
                <td class="auto-style88">&nbsp;</td>
                <td class="auto-style88">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style86"></td>
                <td class="auto-style87"></td>
                <td class="auto-style78">
                <asp:Button ID="Button1" runat="server" Text="Add BOM" Width="129px" CssClass="auto-style39" />
                <asp:LinkButton ID="LinkButton1" runat="server">mass upload</asp:LinkButton>
                </td>
                <td class="auto-style88"></td>
                <td class="auto-style88"></td>
            </tr>
            <tr>
                <td class="auto-style80"></td>
                <td class="auto-style81"></td>
                <td colspan="2" class="auto-style82">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="268px" />
                </td>
                <td class="auto-style82"></td>
            </tr>
            <tr>
                <td class="auto-style83"></td>
                <td class="auto-style84"></td>
                <td colspan="2" class="auto-style85">
                <asp:Button ID="Button2" runat="server" Text="Upload" Width="129px" CssClass="auto-style39" /></td>
                <td class="auto-style85"></td>
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
            <asp:TemplateField HeaderText="RM Code">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("RMCode") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("RMCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity per Unit">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("QuantityPerUnit") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("QuantityPerUnit") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Of Mesurement">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("UnitOfMesurement") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("UnitOfMesurement") %>'></asp:Label>
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
