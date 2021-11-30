import React, { Component } from "react";
import CreateConference from "../components/Conferences/CreateConference";
import Layout from "../layouts/Layout";

class CreateConferencePage extends Component {
  render() {
    return (
      <Layout>
        <CreateConference />
      </Layout>
    );
  }
}

export default CreateConferencePage;
