import React, { Component } from "react";
import AccommodationsForCity from "../components/Accommodations/AccommodationsForCity";
import Layout from "../layouts/Layout";

class AccommodationsForCityPage extends Component {
  render() {
    return (
      <Layout>
        <AccommodationsForCity />
      </Layout>
    );
  }
}

export default AccommodationsForCityPage;
