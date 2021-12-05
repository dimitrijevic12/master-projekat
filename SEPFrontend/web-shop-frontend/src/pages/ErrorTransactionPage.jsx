import React, { Component } from "react";
import ErrorTransaction from "../components/Transactions/ErrorTransaction";
import Layout from "../layouts/Layout";

class ErrorTransactionPage extends Component {
  render() {
    return (
      <Layout>
        <ErrorTransaction />
      </Layout>
    );
  }
}

export default ErrorTransactionPage;
