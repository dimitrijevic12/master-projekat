import React, { Component } from "react";
import OwnerTransportations from "../components/Transportations/OwnerTransportations";
import Layout from "../layouts/Layout";

class OwnerTransportationsPage extends Component {
  render() {
    return (
      <Layout>
        <OwnerTransportations />
      </Layout>
    );
  }
}

export default OwnerTransportationsPage;
