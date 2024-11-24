import React, { useState, useEffect } from 'react';
import axios from 'axios';

function ProductList() {
  const [products, setProducts] = useState([]);
  const [cart, setCart] = useState([]);

  useEffect(() => {
    const fetchProducts = async () => {
      const response = await axios.get('/products');
      setProducts(response.data);
    };
    fetchProducts();
  }, []);

  const addToCart = async (productId) => {
    try {
      const response = await axios.post('/cart', { productId, quantity: 1 });
      setCart(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const removeFromCart = async (productId) => {
    // Implement logic to remove an item from the cart
    // You can send a DELETE request to the backend API
  };

  const updateCartItem = async (productId, newQuantity) => {
    // Implement logic to update the quantity of an item in the cart
    // You can send a PUT request to the backend API
  };

  return (
    <div>
      <h2>Products</h2>
      <ul>
        {products.map(product => (
          <li key={product.id}>
            {product.name} - ${product.price}
            <button onClick={() => addToCart(product.id)}>Add to Cart</button>
          </li>
        ))}
      </ul>

      <h2>Cart</h2>
      <ul>
        {cart.map(item => (
          <li key={item.productId}>
            {products.find(p => p.id === item.productId).name} x {item.quantity}
            <button onClick={() => removeFromCart(item.productId)}>Remove</button>
            <input type="number" value={item.quantity} onChange={(e) => updateCartItem(item.productId, e.target.value)} />
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ProductList;
