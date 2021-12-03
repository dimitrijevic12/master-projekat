import React, { Component } from "react";
import CreateTransportation from "../components/Transportations/CreateTransportation";
import Layout from "../layouts/Layout";

class CreateTransportationPage extends Component {
  render() {
    return (
      <Layout>
        <CreateTransportation />
      </Layout>
    );
  }
}

export default CreateTransportationPage;
