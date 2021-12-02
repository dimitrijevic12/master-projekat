import React, { Component } from "react";
import Transactions from "../components/Transactions/Transactions";
import Layout from "../layouts/Layout";

class TransactionsPage extends Component {
  render() {
    return (
      <Layout>
        <Transactions />
      </Layout>
    );
  }
}

export default TransactionsPage;