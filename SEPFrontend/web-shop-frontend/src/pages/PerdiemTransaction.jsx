import React, { Component } from "react";
import PerdiemTransaction from "../components/Transactions/PerdiemTransaction";
import Layout from "../layouts/Layout";

class PerdiemTransactionPage extends Component {
  render() {
    return (
      <Layout>
        <PerdiemTransaction />
      </Layout>
    );
  }
}

export default PerdiemTransactionPage;
