import React, { Component } from "react";
import SuccessfulTransaction from "../components/Transactions/SuccessfulTransaction";
import Layout from "../layouts/Layout";

class SuccessfulTransactionPage extends Component {
  render() {
    return (
      <Layout>
        <SuccessfulTransaction />
      </Layout>
    );
  }
}

export default SuccessfulTransactionPage;
