import React, { Component } from "react";
import ChoosePaymentTypes from "../components/Users/ChoosePaymentType"
import NotLoggedNavigationBar from "../components/Common/NavigationBar";
import Layout from "../layouts/Layout";

class ChoosePaymentTypesPage extends Component {
  render() {
    return (
      //<Layout>
      <div>
      <header style={{ backgroundColor: "whitesmoke" }} className="pt-4">
        
        <NotLoggedNavigationBar />
        <hr />
      </header>
      <ChoosePaymentTypes />
      </div>
      //</Layout>
    );
  }
}

export default ChoosePaymentTypesPage;
