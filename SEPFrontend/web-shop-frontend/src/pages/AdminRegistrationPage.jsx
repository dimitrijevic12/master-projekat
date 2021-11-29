import React, { Component } from "react";
import AdminRegistration from "../components/Users/AdminRegistration";
import Layout from "../layouts/Layout";

class AdminRegistrationPage extends Component {
  render() {
    return (
      <Layout>
        <AdminRegistration />
      </Layout>
    );
  }
}

export default AdminRegistrationPage;
