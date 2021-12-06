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
import PrivateRoute from "./routes/PrivateRoute";
import AdminProtectedRoute from "./routes/AdminProtectedRoute";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

function App() {
  return (
    <Router>
      <div>
        <Routes>
          <Route exact path="/" element={<PrivateRoute />}>
            <Route exact path="/" element={<ItemsPage />} />
          </Route>
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

          <Route path="/items" element={<ItemsPage />} />
          <Route exact path="/owners-items" element={<AdminProtectedRoute />}>
            <Route path="/owners-items" element={<OwnerItemsPage />} />
          </Route>
          <Route
            exact
            path="owners-conferences"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/owners-conferences"
              element={<OwnerConferencesPage />}
            />
          </Route>
          <Route
            exact
            path="/owners-transportations"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/owners-transportations"
              element={<OwnerTransportationsPage />}
            />
          </Route>
          <Route
            exact
            path="/owners-accommodations"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/owners-accommodations"
              element={<OwnerAccommodationsPage />}
            />
          </Route>
          <Route exact path="/owners-courses" element={<AdminProtectedRoute />}>
            <Route path="/owners-courses" element={<OwnerCoursesPage />} />
          </Route>
          <Route exact path="/edit-item" element={<AdminProtectedRoute />}>
            <Route path="/edit-item" element={<EditItem />} />
          </Route>
          <Route
            exact
            path="/create-conference"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/create-conference"
              element={<CreateConferencePage />}
            />
          </Route>
          <Route
            exact
            path="/edit-conference"
            element={<AdminProtectedRoute />}
          >
            <Route path="/edit-conference" element={<EditConferencePage />} />
          </Route>
          <Route
            exact
            path="/create-transportation"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/create-transportation"
              element={<CreateTransportationPage />}
            />
          </Route>
          <Route
            exact
            path="/edit-transportation"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/edit-transportation"
              element={<EditTransportationPage />}
            />
          </Route>
          <Route exact path="/create-item" element={<AdminProtectedRoute />}>
            <Route path="/create-item" element={<CreateItemPage />} />
          </Route>
          <Route
            exact
            path="/edit-accommodation"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/edit-accommodation"
              element={<EditAccommodationPage />}
            />
          </Route>
          <Route exact path="/create-course" element={<AdminProtectedRoute />}>
            <Route path="/create-course" element={<CreateCoursePage />} />
          </Route>
          <Route
            exact
            path="/create-accommodation"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/create-accommodation"
              element={<CreateAccommodationPage />}
            />
          </Route>
          <Route exact path="/edit-course" element={<AdminProtectedRoute />}>
            <Route path="/edit-course" element={<EditCoursePage />} />
          </Route>
          <Route
            exact
            path="/sellers-transactions"
            element={<AdminProtectedRoute />}
          >
            <Route
              path="/sellers-transactions"
              element={<TransactionsForSellerPage />}
            />
          </Route>
          <Route path="/registration" element={<RegistrationPage />} />
          <Route path="/conferences" element={<ConferencesPage />} />

          <Route path="/transportations" element={<TransportationsPage />} />

          <Route path="/courses" element={<CoursesPage />} />

          <Route
            path="/accommodations-for-city"
            element={<AccommodationsForCityPage />}
          />
          <Route exact path="/buyers-transactions" element={<PrivateRoute />}>
            <Route
              path="/buyers-transactions"
              element={<TransactionsForBuyerPage />}
            />
          </Route>

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
