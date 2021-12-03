import React, { Component } from "react";
import EditTransportation from "../components/Transportations/EditTransportation";
import Layout from "../layouts/Layout";

class EditTransportationPage extends Component {
  render() {
    return (
      <Layout>
        <EditTransportation />
      </Layout>
    );
  }
}

export default EditTransportationPage;
