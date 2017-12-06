<%@ Page Title="" Language="C#" MasterPageFile="~/PortalFacturas.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PortalFacturas.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" />

    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />

    <hr />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="File Name" />
            <asp:TemplateField   ItemStyle-CssClass="tableOptions"  ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <hr />

    <div>
        <asp:Literal ID="ltEmbed" runat="server" />
    </div>

</asp:Content>
