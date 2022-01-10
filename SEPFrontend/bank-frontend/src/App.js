import React from "react";
import "./App.css";
import CardPaymentPage from "./pages/CardPaymentPage";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import QRCodePaymentPage from "./pages/QRCodePaymentPage";
import PaymentTypePage from "./pages/PaymentTypePage";
import PendingPage from "./pages/PendingPage";

function App() {
  return (
    <Router>
      <div>
        <Routes>
          <Route path="/payment/:paymentId" element={<PaymentTypePage />} />
          <Route path="/cardPayment/:paymentId" element={<CardPaymentPage />} />
          <Route path="/qrPayment/:paymentId" element={<QRCodePaymentPage />} />
          <Route path="/pending/:transactionId" element={<PendingPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
