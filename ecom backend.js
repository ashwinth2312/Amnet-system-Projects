const express = require('express');
const cors = require('cors');

const app = express();
const port = 3000;

// Dummy product data
const products = [
  { id: 1, name: 'Product 1', price: 10 },
  { id: 2, name: 'Product 2', price: 20 },
  // ... more products
];

// In-memory cart storage (for simplicity)
let cart = [];

app.use(cors());
app.use(express.json());

app.get('/products', (req, res) => {
  res.json(products);
});

app.post('/cart', (req, res) => {
  const { productId, quantity } = req.body;
  const product = products.find(p => p.id === productId);
  if (product) {
    const existingItem = cart.find(item => item.productId === productId);
    if (existingItem) {
      existingItem.quantity += quantity;
    } else {
      cart.push({ productId, quantity });
    }
    res.json(cart);
  } else {
    res.status(404).json({ error: 'Product not found' });
  }
});

// ... other API endpoints for updating, removing, and viewing cart

app.listen(port, () => {
  console.log(`Server listening on port ${port}`);
});
