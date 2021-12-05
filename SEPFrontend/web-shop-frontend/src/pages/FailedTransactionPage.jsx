import React, { Component } from "react";
import FailedTransaction from "../components/Transactions/FailedTransaction";
import Layout from "../layouts/Layout";

class FailedTransactionPage extends Component {
  render() {
    return (
      <Layout>
        <FailedTransaction />
      </Layout>
    );
  }
}

export default FailedTransactionPage;
