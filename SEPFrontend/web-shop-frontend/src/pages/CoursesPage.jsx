import React, { Component } from "react";
import Courses from "../components/Courses/Courses";
import Layout from "../layouts/Layout";

class CoursesPage extends Component {
  render() {
    return (
      <Layout>
        <Courses />
      </Layout>
    );
  }
}

export default CoursesPage;
