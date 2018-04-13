<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="WingtipToysInAspDotNet45WebForm.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
     <div>
         <hgroup>
                <h2><%: Page.Title %></h2>
         </hgroup>
         <asp:ListView ID="productList" runat="server" DataKeyNames="ProductID" GroupItemCount="4"
         ItemType="WingtipToysInAspDotNet45WebForm.Models.Product" SelectMethod="GetProducts">
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>No Data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <td></td>
        </EmptyItemTemplate>
        <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
             </tr>
        </GroupTemplate>
        <ItemTemplate>
            <td runat="server">
                <table>
                    <tr>
                        <td>
                            <a href="ProductDetails.aspx?productID=<%#: Item.ProductID %>">
                                <image src="/Catalog/Images/Thumbs/<%#: Item.ImagePath %>"
                                    width="100" height="75" style="border: solid" />
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="ProductDetails.aspx?productID=<%#: Item.ProductID %>">
                                <span>
                                    <%#: Item.ProductName %>
                                </span>
                            </a>
                            <br />
                            <span>
                                <b>Price: </b><%#: String.Format("{0:c}", Item.UnitPrice) %>
                            </span>                                
                            <br />
                            <a href="AddToCart.aspx?productID=<%#: Item.ProductID %>">
                                <span class="ProductListItem">
                                    <b>Add To Cart</b>
                                </span>
                            </a>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </ItemTemplate>
        <LayoutTemplate>
            <table style="width:100%;">
               <tbody>
                 <tr>
                    <td>
                       <table id="groupPlaceholderContainer" runat="server" style="width:100%">
                           <tr id="groupPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr></tr>
              </tbody>
           </table>
        </LayoutTemplate>
     </asp:ListView>
    </div>
   </section>
</asp:Content>
