import React, { Component } from "react";
import OwnerAccommodations from "../components/Accommodations/OwnerAccommodations";
import Layout from "../layouts/Layout";

class OwnerAccommodationsPage extends Component {
  render() {
    return (
      <Layout>
        <OwnerAccommodations />
      </Layout>
    );
  }
}

export default OwnerAccommodationsPage;
