import axios from 'axios';
import React, { useEffect, useState } from 'react';

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

  const addToCart = (productId) => {
    // Send a POST request to the backend API to add the product to the cart
    axios.post('/cart', { productId, quantity: 1 })
      .then(response => {
        setCart(response.data);
      })
      .catch(error => {
        console.error(error);
      });
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
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ProductList;