import React from "react";
import "./App.css";
import WebShopRegistrationPage from "./pages/WebShopRegistrationPage";
import LoginPage from "./pages/LoginPage";
import ChangePaymentTypesPage from "./pages/ChangePaymentTypesPage";
import TransactionsPage from "./pages/TransactionsPage";
import ChoosePaymentTypesPage from "./pages/ChoosePaymentTypesPage";
import PayPalPayment from "./components/PayPal/PayPalPayment";
import PayPalButtonV2Page from "./pages/PayPalButtonV2Page";
import PayPalButtonSubscriptionPage from "./pages/PayPalButtonSubscriptionPage";

import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import SuccessRedirect from "./components/Transactions/SuccessRedirect";
import FailedRedirect from "./components/Transactions/FailedRedirect";

function App() {
  return (
    <Router>
      <div>
        <Routes>
          <Route path="/registration" element={<WebShopRegistrationPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/paymenttypes" element={<ChangePaymentTypesPage />} />
          <Route path="/transactions" element={<TransactionsPage />} />
          <Route path="/psp/:orderId" element={<ChoosePaymentTypesPage />} />
          <Route path="/paypal/:orderId" element={<PayPalPayment />} />
          <Route path="/paid-transaction/:orderId" element={<SuccessRedirect />} />
          <Route path="/invalid-transaction/:orderId" element={<FailedRedirect />} />
          <Route
            path="/paypal-page/:orderId"
            element={<PayPalButtonV2Page />}
          />
          <Route
            path="/paypal-page/subscription-paypal/:orderId"
            element={<PayPalButtonSubscriptionPage />}
          />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
