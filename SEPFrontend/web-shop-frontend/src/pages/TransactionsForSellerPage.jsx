import React, { Component } from "react";
import TransactionsForSeller from "../components/Transactions/TransactionsForSeller";
import Layout from "../layouts/Layout";

class TransactionsForSellerPage extends Component {
  render() {
    return (
      <Layout>
        <TransactionsForSeller />
      </Layout>
    );
  }
}

export default TransactionsForSellerPage;
