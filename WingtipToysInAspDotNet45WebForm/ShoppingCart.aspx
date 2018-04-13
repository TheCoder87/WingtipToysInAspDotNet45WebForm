<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="WingtipToysInAspDotNet45WebForm.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <div id="ShoppingCartTitle" runat="server" class="ContentHead"><h1>ShoppingCart</h1></div>
    <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="false" ShowFooter="true" GridLines="Vertical"
         ItemType="WingtipToysInAspDotNet45WebForm.Models.CartItem" SelectMethod="GetShoppingCartItems" CssClass="table table-striped table-border" >
         <Columns>
             <asp:BoundField DataField="ProductID" HeaderText="ID" SortExpression="ProductID" />
             <asp:BoundField DataField="Product.ProductName" HeaderText="Name" />
             <asp:BoundField DataField="Product.UnitPrice" HeaderText="Price (each)" DataFormatString="{0:c}" />
             <asp:TemplateField HeaderText="Quantity">
                 <ItemTemplate>
                     <asp:TextBox ID="PurchaseQuantity" runat="server" Width="40" Text="<%#: Item.Quantity %>"></asp:TextBox>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Item Total">
                 <ItemTemplate>
                     <%#: String.Format("{0:c}", (Convert.ToDouble(Item.Quantity) * Convert.ToDouble(Item.Product.UnitPrice)) )%>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Remove Item">
                 <ItemTemplate>
                     <asp:CheckBox Id="Remove" runat="server" />
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
    </asp:GridView>
    <div>
   <p></p>
    <strong>
        <asp:Label ID="lblToTotal" runat="server" Text="Order Total: "></asp:Label>
        <asp:Label ID="lblTotal" runat="server" EnableViewState="false"></asp:Label>
    </strong>
    </div>
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            </td>
            <td></td>
        </tr>
    </table>

</asp:Content>
