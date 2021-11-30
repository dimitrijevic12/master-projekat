import React from "react";
import "./App.css";
import WebShopRegistrationPage from "./pages/WebShopRegistrationPage";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

function App() {
  return (
    <Router>
      <div>
        <Routes>      
          <Route path="/registration" element={<WebShopRegistrationPage />} />       
        </Routes>
      </div>
    </Router>
  );
}

export default App;
