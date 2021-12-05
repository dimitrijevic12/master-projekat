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
import CreateAccommodationPage from "./pages/CreateAccommodationPage";
import AccommodationsForCityPage from "./pages/AccommodationsForCityPage";
import ReviewAccommodationPage from "./pages/ReviewAccommodationPage";
import ReviewAccommodationInShoppingCartPage from "./pages/ReviewAccommodationInShoppingCart";
import OwnerAccommodationsPage from "./pages/OwnerAccommodationsPage";
import TransactionsForSellerPage from "./pages/TransactionsForSellerPage";
import EditAccommodationPage from "./pages/EditAccommodationPage";
import TransportationsPage from "./pages/TransportationsPage";
import CreateTransportationPage from "./pages/CreateTransportationPage";
import ReviewTransportationPage from "./pages/ReviewTransportationPage";
import ReviewTransportationInShoppingCartPage from "./pages/ReviewTransportationInShoppingCartPage";
import OwnerTransportationsPage from "./pages/OwnerTransportationsPage";
import EditTransportationPage from "./pages/EditTransportationPage";
import SuccessfulTransactionPage from "./pages/SuccessfulTransactionPage";
import FailedTransactionPage from "./pages/FailedTransactionPage";
import ErrorTransactionPage from "./pages/ErrorTransactionPage";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

function App() {
  return (
    <Router>
      <div>
        <Routes>
          <Route path="/item/:itemId" element={<ReviewItemPage />} />
          <Route
            path="/successful-transaction/:transactionId"
            element={<SuccessfulTransactionPage />}
          />
          <Route
            path="/failed-transaction/:transactionId"
            element={<FailedTransactionPage />}
          />
          <Route
            path="/error-transaction/:transactionId"
            element={<ErrorTransactionPage />}
          />
          <Route path="/course/:courseId" element={<ReviewCoursePage />} />
          <Route
            path="/transportation/:transportationId"
            element={<ReviewTransportationPage />}
          />
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
            path="/shopping-transportation/:transportationId"
            element={<ReviewTransportationInShoppingCartPage />}
          />
          <Route
            path="/shopping-conference/:conferenceId"
            element={<ReviewConferenceInShoppingCartPage />}
          />
          <Route
            path="/transaction/:transactionId"
            element={<ReviewTransactionPage />}
          />
          <Route
            path="/accommodation/:accommodationId"
            element={<ReviewAccommodationPage />}
          />
          <Route
            path="/shopping-accommodation/:accommodationId"
            element={<ReviewAccommodationInShoppingCartPage />}
          />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/create-item" element={<CreateItemPage />} />
          <Route path="/items" element={<ItemsPage />} />
          <Route path="/owners-items" element={<OwnerItemsPage />} />
          <Route
            path="/owners-conferences"
            element={<OwnerConferencesPage />}
          />
          <Route
            path="/owners-transportations"
            element={<OwnerTransportationsPage />}
          />
          <Route
            path="/owners-accommodations"
            element={<OwnerAccommodationsPage />}
          />
          <Route path="/owners-courses" element={<OwnerCoursesPage />} />
          <Route path="/edit-item" element={<EditItem />} />
          <Route path="/registration" element={<RegistrationPage />} />
          <Route path="/conferences" element={<ConferencesPage />} />
          <Route path="/create-conference" element={<CreateConferencePage />} />
          <Route path="/edit-conference" element={<EditConferencePage />} />
          <Route path="/transportations" element={<TransportationsPage />} />
          <Route
            path="/create-transportation"
            element={<CreateTransportationPage />}
          />
          <Route
            path="/edit-transportation"
            element={<EditTransportationPage />}
          />
          <Route
            path="/edit-accommodation"
            element={<EditAccommodationPage />}
          />
          <Route path="/courses" element={<CoursesPage />} />
          <Route path="/create-course" element={<CreateCoursePage />} />
          <Route
            path="/create-accommodation"
            element={<CreateAccommodationPage />}
          />
          <Route
            path="/accommodations-for-city"
            element={<AccommodationsForCityPage />}
          />
          <Route path="/edit-course" element={<EditCoursePage />} />
          <Route
            path="/buyers-transactions"
            element={<TransactionsForBuyerPage />}
          />
          <Route
            path="/sellers-transactions"
            element={<TransactionsForSellerPage />}
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
