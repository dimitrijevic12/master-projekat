import React, { Component } from "react";
import WebShopRegistration from "../components/Users/WebShopRegistration";
import "bootstrap/dist/css/bootstrap.min.css";
import Layout from "../layouts/Layout";

class WebShopRegistrationPage extends Component {
  render() {
    return (
      <Layout>
        <WebShopRegistration />
      </Layout>
    );
  }
}

export default WebShopRegistrationPage;
