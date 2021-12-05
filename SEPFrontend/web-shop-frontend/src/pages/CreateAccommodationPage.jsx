import React, { Component } from "react";
import CreateAccommodation from "../components/Accommodations/CreateAccommodation";
import Layout from "../layouts/Layout";

class CreateAccommodationPage extends Component {
  render() {
    return (
      <Layout>
        <CreateAccommodation />
      </Layout>
    );
  }
}

export default CreateAccommodationPage;
