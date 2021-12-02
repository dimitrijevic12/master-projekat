import React from "react";
import "./App.css";
import WebShopRegistrationPage from "./pages/WebShopRegistrationPage";
import LoginPage from "./pages/LoginPage";
import ChangePaymentTypesPage from "./pages/ChangePaymentTypesPage";
import TransactionsPage from "./pages/TransactionsPage";




import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

function App() {
  return (
    <Router>
      <div>
        <Routes>      
          <Route path="/registration" element={<WebShopRegistrationPage />} />  
          <Route path="/login" element={<LoginPage />} />       
          <Route path="/paymenttypes" element={<ChangePaymentTypesPage />} />  
          <Route path="/transactions" element={<TransactionsPage />} /> 

        </Routes>
      </div>
    </Router>
  );
}

export default App;
