import React, { Component } from "react";
import ChangePaymentTypes from "../components/Users/ChangePaymentTypes";
import NotLoggedNavigationBar from "../components/Common/NavigationBar";
import Layout from "../layouts/Layout";

class ChangePaymentTypesPage extends Component {
  render() {
    return (
      //<Layout>
      <div>
      <header style={{ backgroundColor: "whitesmoke" }} className="pt-4">
        
        <NotLoggedNavigationBar />
        <hr />
      </header>
      <ChangePaymentTypes />
      </div>
      //</Layout>
    );
  }
}

export default ChangePaymentTypesPage;
