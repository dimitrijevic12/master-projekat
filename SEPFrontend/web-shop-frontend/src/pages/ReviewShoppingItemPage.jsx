import React, { Component } from "react";
import ReviewItemInShoppingCart from "../components/Items/ReviewItemInShoppingCart";
import Layout from "../layouts/Layout";

class ReviewShoppingItemPage extends Component {
  render() {
    return (
      <Layout>
        <ReviewItemInShoppingCart />
      </Layout>
    );
  }
}

export default ReviewShoppingItemPage;
