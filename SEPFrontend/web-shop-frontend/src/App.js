import React from "react";
import "./App.css";
import LoginPage from "./pages/LoginPage";
import ItemsPage from "./pages/ItemsPage";
import ShoppingCart from "./components/Items/ShoppingCart";
import ReviewItemPage from "./pages/ReviewItemPage";
import ItemsInShoppingCartPage from "./pages/ItemsInShoppingCartPage";
import ReviewShoppingItemPage from "./pages/ReviewShoppingItemPage";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

function App() {
  return (
    <Router>
      <div>
        <Routes>
          <Route path="/item/:itemId" element={<ReviewItemPage />} />
          <Route
            path="/shopping-item/:itemId"
            element={<ReviewShoppingItemPage />}
          />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/items" element={<ItemsPage />} />
          <Route
            path="/items-in-shopping-cart"
            element={<ItemsInShoppingCartPage />}
          />
          <Route path="/cart" element={<ShoppingCart />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
