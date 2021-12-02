import React, { Component } from "react";
import TransactionReview from "../components/Transactions/TransactionReview";
import Layout from "../layouts/Layout";

class ReviewTransactionPage extends Component {
  render() {
    return (
      <Layout>
        <TransactionReview />
      </Layout>
    );
  }
}

export default ReviewTransactionPage;
