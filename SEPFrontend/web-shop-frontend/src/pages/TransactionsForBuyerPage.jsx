import React, { Component } from "react";
import TransactionsForBuyer from "../components/Transactions/TransactionsForBuyer";
import Layout from "../layouts/Layout";

class TransactionsForBuyerPage extends Component {
  render() {
    return (
      <Layout>
        <TransactionsForBuyer />
      </Layout>
    );
  }
}

export default TransactionsForBuyerPage;
