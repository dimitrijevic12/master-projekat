import React, { Component } from "react";
import Items from "../components/Items/Items";
import Layout from "../layouts/Layout";

class ItemsPage extends Component {
  render() {
    return (
      <Layout>
        <Items />
      </Layout>
    );
  }
}

export default ItemsPage;
