<!DOCTYPE html>
<html>
<head>
    <title>Add to Cart</title>
</head>
<body>
    <h1>Add to Cart</h1>
    <form id="addToCartForm" runat="server">
        <asp:Label ID="productIdLabel" runat="server" Text="Product ID:"></asp:Label>
        <asp:TextBox ID="productIdTextBox" runat="server" type="number"></asp:TextBox><br />
        <asp:Label ID="quantityLabel" runat="server" Text="Quantity:"></asp:Label>
        <asp:TextBox ID="quantityTextBox" runat="server" type="number"></asp:TextBox><br />
        <asp:Button ID="addToCartButton" runat="server" Text="Add to Cart" OnClick="AddToCartButton_Click" />
    </form>