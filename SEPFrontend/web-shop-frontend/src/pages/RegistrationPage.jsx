import React, { Component } from "react";
import Registration from "../components/Users/Registration";
import Layout from "../layouts/Layout";

class RegistrationPage extends Component {
  render() {
    return (
      <Layout>
        <Registration />
      </Layout>
    );
  }
}

export default RegistrationPage;
