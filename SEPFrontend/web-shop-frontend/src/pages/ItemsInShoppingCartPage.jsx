import React, { Component } from "react";
import ItemsInShoppingCart from "../components/Items/ItemsInShoppingCart";
import Layout from "../layouts/Layout";

class ItemsInShoppingCartPage extends Component {
  render() {
    return (
      <Layout>
        <ItemsInShoppingCart />
      </Layout>
    );
  }
}

export default ItemsInShoppingCartPage;
