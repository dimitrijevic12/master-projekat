import React, { Component } from "react";
import EditConference from "../components/Conferences/EditConference";
import Layout from "../layouts/Layout";

class EditConferencePage extends Component {
  render() {
    return (
      <Layout>
        <EditConference />
      </Layout>
    );
  }
}

export default EditConferencePage;
