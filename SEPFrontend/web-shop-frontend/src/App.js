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
import RegistrationPage from "./pages/RegistrationPage";
import AdminRegistrationPage from "./pages/AdminRegistrationPage";
import ConferencesPage from "./pages/ConferencesPage";
import CreateConferencePage from "./pages/CreateConference";
import ReviewConferencePage from "./pages/ReviewConferencePage";
import EditConferencePage from "./pages/EditConference";
import CoursesPage from "./pages/CoursesPage";
import ReviewCoursePage from "./pages/ReviewCoursePage";
import CreateCoursePage from "./pages/CreateCoursePage";
import EditCoursePage from "./pages/EditCoursePage";
import ReviewCourseInShoppingCartPage from "./pages/ReviewCourseInShoppingCartPage";
import ReviewConferenceInShoppingCartPage from "./pages/ReviewConferenceInShoppingCartPage";
import OwnerConferencesPage from "./pages/OwnerConferencesPage";
import OwnerCoursesPage from "./pages/OwnerCoursesPage";
import TransactionsForBuyerPage from "./pages/TransactionsForBuyerPage";
import ReviewTransactionPage from "./pages/ReviewTransactionPage";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

function App() {
  return (
    <Router>
      <div>
        <Routes>
          <Route path="/item/:itemId" element={<ReviewItemPage />} />
          <Route path="/course/:courseId" element={<ReviewCoursePage />} />
          <Route
            path="/conference/:conferenceId"
            element={<ReviewConferencePage />}
          />
          <Route
            path="/shopping-item/:itemId"
            element={<ReviewShoppingItemPage />}
          />
          <Route
            path="/shopping-course/:courseId"
            element={<ReviewCourseInShoppingCartPage />}
          />
          <Route
            path="/shopping-conference/:conferenceId"
            element={<ReviewConferenceInShoppingCartPage />}
          />
          <Route
            path="/transaction/:transactionId"
            element={<ReviewTransactionPage />}
          />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/create-item" element={<CreateItemPage />} />
          <Route path="/items" element={<ItemsPage />} />
          <Route path="/owners-items" element={<OwnerItemsPage />} />
          <Route
            path="/owners-conferences"
            element={<OwnerConferencesPage />}
          />
          <Route path="/owners-courses" element={<OwnerCoursesPage />} />
          <Route path="/edit-item" element={<EditItem />} />
          <Route path="/registration" element={<RegistrationPage />} />
          <Route path="/conferences" element={<ConferencesPage />} />
          <Route path="/create-conference" element={<CreateConferencePage />} />
          <Route path="/edit-conference" element={<EditConferencePage />} />
          <Route path="/courses" element={<CoursesPage />} />
          <Route path="/create-course" element={<CreateCoursePage />} />
          <Route path="/edit-course" element={<EditCoursePage />} />
          <Route
            path="/buyers-transactions"
            element={<TransactionsForBuyerPage />}
          />
          <Route
            path="/admin-registration"
            element={<AdminRegistrationPage />}
          />
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
