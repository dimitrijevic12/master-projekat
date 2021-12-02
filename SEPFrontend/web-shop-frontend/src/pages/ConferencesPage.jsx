import React, { Component } from "react";
import Conferences from "../components/Conferences/Conferences";
import Layout from "../layouts/Layout";

class ConferencesPage extends Component {
  render() {
    return (
      <Layout>
        <Conferences />
      </Layout>
    );
  }
}

export default ConferencesPage;
