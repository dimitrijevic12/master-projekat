import React, { Component } from "react";
import OwnerConferences from "../components/Conferences/OwnerConferences";
import Layout from "../layouts/Layout";

class OwnerConferencesPage extends Component {
  render() {
    return (
      <Layout>
        <OwnerConferences />
      </Layout>
    );
  }
}

export default OwnerConferencesPage;
