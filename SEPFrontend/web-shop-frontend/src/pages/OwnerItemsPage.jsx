import React, { Component } from "react";
import OwnerItemsList from "../components/Items/OwnerItemsList";
import Layout from "../layouts/Layout";

class OwnerItemsPage extends Component {
  render() {
    return (
      <Layout>
        <OwnerItemsList />
      </Layout>
    );
  }
}

export default OwnerItemsPage;
