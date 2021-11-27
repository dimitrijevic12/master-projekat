import React from "react";
import "./App.css";
import LoginPage from "./pages/LoginPage";
import ItemsPage from "./pages/ItemsPage";
import ShoppingCart from "./components/Items/ShoppingCart";
import ReviewItemPage from "./pages/ReviewItemPage";
import ItemsInShoppingCartPage from "./pages/ItemsInShoppingCartPage";
import ReviewShoppingItemPage from "./pages/ReviewShoppingItemPage";
import CreateItemPage from "./pages/CreateItemPage";
import OwnerItemsPage from "./pages/OwnerItemsPage";
import EditItem from "./components/Items/EditItem";
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
          <Route path="/create-item" element={<CreateItemPage />} />
          <Route path="/items" element={<ItemsPage />} />
          <Route path="/owners-items" element={<OwnerItemsPage />} />
          <Route path="/edit-item" element={<EditItem />} />
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
