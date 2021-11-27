import React, { Component } from "react";
import CreateItem from "../components/Items/CreateItem";
import Layout from "../layouts/Layout";

class CreateItemPage extends Component {
  render() {
    return (
      <Layout>
        <CreateItem />
      </Layout>
    );
  }
}

export default CreateItemPage;
